/*
 * 
 * 
 */

using System.Text;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 선택된 게임 오브젝트의 구조에 따라 자식들을 읽어서 접근가능한 View 클래스를 생성하는 Generator
/// 기본으로 TextTemplate1.tt 파일을 샘플로 사용하며, 다른 템플릿을 인자로 입력하여 대체가 가능하다.
/// 다만 다른 템플릿으로 변경시, PreGenerateSetup 메서드의 내용을 변경할 필요가 있다.
/// 필수로 Run을 하기전 Setup의 호출이 필요하다.
/// </summary>
public class HierarchyT4TemplateGenerator : UnityTemplateGenerator
{
    const string seperator = "&";
    class ObjectTree
    {
        public string parent;
        public string name;
        const string seperator = "/";
        public List<ObjectTree> children = new List<ObjectTree>();
        public List<string> childrenName = new List<string>();

        public ObjectTree()
        {
            parent = "";
            name = "";
        }

        public ObjectTree(string name)
        {
            parent = "";
            this.name = name;
        }

        public ObjectTree(string parentName, string mineName)
        {
            parent = parentName;
            name = mineName;
        }

        public ObjectTree Add(string parentName, string name)
        {
            ObjectTree child = new ObjectTree(parentName, 
                parentName 
                + (string.IsNullOrEmpty(parentName) ? "" : seperator)
                + name);
            children.Add(child);
            childrenName.Add(child.name);
            return child;
        }

        /// <summary>
        /// Hierachy 구조를 string으로 export
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return base.ToString();
            string retVal = "";
            foreach(var iter in childrenName)
            {
                retVal += "\"" + iter + "\",";
            }
            return retVal;
        }
    }

    /// <summary>
    /// property로 넘길 object리스트
    /// </summary>
    private List<GameObject> savedList = new List<GameObject>();

    public void Perspective(GameObject go)
    {
        string[] strs = new string[6];
        //System.Array.IndexOf()

        ObjectTree tree = new ObjectTree(go.name);

        for (int i = 0; i < go.transform.childCount; i++)
        {
            //tree.Add(go.name, go.transform.GetChild(i).name);
            RecursiveSearchTree(ref tree, "", go.transform.GetChild(i).gameObject);
        }
        Debug.Log(tree.ToString());
    }

    /// <summary>
    /// 선택된 go와 설정된 네임스페이스, 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="defNamespace"></param>
    /// <param name="defaultTemplatePath"></param>
    public void Setup(GameObject go, string defNamespace, string defaultTemplatePath)
    {
        //clear
        savedList.Clear();
        className = "";
        nameSpace = "";
        csFilePath = "";
        fileName = go.name;

        //init
        //savedList.Add(go);
        nameSpace = defNamespace;
        RecursiveSearch(go);
        savedList.Remove(go);

        sampleT4FilePath = defaultTemplatePath;
        if (string.IsNullOrEmpty(sampleT4FilePath) == true)
        {
            sampleT4FilePath = Application.dataPath + "/T4Sample/TextTemplate1.tt";
        }

        tree = new ObjectTree(go.name);
        for (int i = 0; i < go.transform.childCount; i++)
        {
            RecursiveSearchTree(ref tree, "", go.transform.GetChild(i).gameObject);
        }
    }

    void RecursiveSearchTree(ref ObjectTree tree, string parent, GameObject go)
    {
        string retVal = go.name;
        if(go.transform.childCount == 0)
        {
            tree.Add(parent, go.name);
            return;
        }
        else
        {
            var node = tree.Add(parent, go.name);
            for(int i = 0;i < go.transform.childCount; i++)
            {
                RecursiveSearchTree(ref tree, node.name, go.transform.GetChild(i).gameObject);
            }
        }
        return ;
    }

    void RecursiveSearch(GameObject go)
    {
        if (go.transform.childCount == 0)
        {
            savedList.Add(go);
            return;
        }
        else
        {
            savedList.Add(go);
            for (int i = 0; i < go.transform.childCount; i++)
            {
                RecursiveSearch(go.transform.GetChild(i).gameObject);
            }
        }
        return;
    }

    ObjectTree tree;
    protected override bool PreGenerateSetup()
    {
        using (System.IO.StreamReader reader = new System.IO.StreamReader(sampleT4FilePath))
        {
            var str = reader.ReadToEnd();
            string[] seperator = { "\"PORPERTY\"", "\"PATHKEY\"", "CLASSNAME", };

            //0 index: header
            //1 index: property name
            //2 index: add body
            //3 index: property path key
            //length -1 : footer

            var templates = str.Split(seperator, System.StringSplitOptions.None);
            StringBuilder builder = new StringBuilder();
            builder.Append(templates[0]);
            foreach (var iter in savedList)
            {
                builder.Append("\"" + iter.name + "\",");
            }
            builder.Append(templates[1]);
            builder.Append(tree.ToString());
            builder.Append(templates[2]);
            className = fileName + "View";
            csFilePath = Application.dataPath + "/" + className + ".cs";
            builder.Append(className);
            builder.Append(templates[templates.Length - 1]);
            msg = builder.ToString();
        }

        exportTtFilePath = ExportT4Text();
        if(string.IsNullOrEmpty(exportTtFilePath))
        {
            return false;
        }
        return true;
    }
}
