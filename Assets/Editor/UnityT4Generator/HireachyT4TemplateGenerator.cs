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
public class HireachyT4TemplateGenerator : UnityTemplateGenerator
{
    /// <summary>
    /// property로 넘길 object리스트
    /// </summary>
    List<GameObject> savedList = new List<GameObject>();

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
        savedList.Add(go);
        nameSpace = defNamespace;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            savedList.Add(go.transform.GetChild(i).gameObject);
        }

        sampleT4FilePath = defaultTemplatePath;
        if (string.IsNullOrEmpty(sampleT4FilePath) == true)
        {
            sampleT4FilePath = Application.dataPath + "/T4Sample/TextTemplate1.tt";
        }
    }

    protected override bool PreGenerateSetup()
    {
        using (System.IO.StreamReader reader = new System.IO.StreamReader(sampleT4FilePath))
        {
            var str = reader.ReadToEnd();
            string[] seperator = { "\"PORPERTY\"", "CLASSNAME", };

            //0 index: header
            //length -1 : footer

            var templates = str.Split(seperator, System.StringSplitOptions.None);
            StringBuilder builder = new StringBuilder();
            builder.Append(templates[0]);
            foreach (var iter in savedList)
            {
                builder.Append("\"" + iter.name + "\",");
            }
            builder.Append(templates[1]);
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
