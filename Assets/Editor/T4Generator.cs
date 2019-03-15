/*
 * 
 * 
 */
using UnityEngine;
using UnityEditor;

public class T4Generator : EditorWindow {
    public Rect windowRect = new Rect(0, 0, 300, 200);
    Object ttFile;
    bool foldGenerateDefaultMenu;
    bool foldGenerateAsyncResMenu;

    public void OnGUI()
    {
        EditorGUILayout.LabelField("TT file Generate");
        GUILayout.BeginArea(new Rect(0, 30, 350, 200));
        DrawGenerateSelectTTArea();
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(0, 100, 350, 200));
        DrawMakeResourceTTArea();
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(0, 170, 350, 200));
        if (GUILayout.Button("Asset Refresh") == true)
        {
            AssetDatabase.Refresh();
        }
        GUILayout.EndArea();
    }

    public void DrawGenerateSelectTTArea()
    {
        //default is empty
        string defNameSpace = "";
        string defaultTTfilePath = "";

        foldGenerateDefaultMenu = EditorGUILayout.Foldout(foldGenerateDefaultMenu, "Generate by default TT file");
        if (foldGenerateDefaultMenu)
        {
            ttFile = EditorGUILayout.ObjectField(ttFile, typeof(object), true);
            if (ttFile != null)
            {
                defaultTTfilePath = AssetDatabase.GetAssetPath(ttFile);
            }

            if (GUILayout.Button("Generate by selected object hierachy") == true)
            {
                HierarchyT4TemplateGenerator savedTemplates = new HierarchyT4TemplateGenerator();
                var go = Selection.gameObjects;

                if(go == null || go.Length == 0)
                {
                    Debug.LogWarning("not selected object");
                    return;
                }

                foreach(var iter in go)
                {
                    savedTemplates.Setup(iter, "", defaultTTfilePath);
                    savedTemplates.Run();
                    //savedTemplates.Perspective(iter);
                }
            }
        }
    }

    public void DrawMakeResourceTTArea()
    {
        foldGenerateAsyncResMenu = EditorGUILayout.Foldout(foldGenerateAsyncResMenu, "Generate Resource Async Load TT file");
        if(foldGenerateAsyncResMenu)
        {
            EditorGUILayout.LabelField("use AsyncLoad.TT file");
            if (GUILayout.Button("Resource Asnyload TT file"))
            {
                ResourceLoadT4TemplateGenerator resourceTemplate = new ResourceLoadT4TemplateGenerator();
                resourceTemplate.Run();
            }
        }
    }

    [MenuItem("Test/Test1")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(T4Generator));
    }
}
