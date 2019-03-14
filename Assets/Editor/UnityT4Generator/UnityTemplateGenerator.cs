/*
 * 
 * 
 */

using System;
using System.Text;
using System.IO;
using UnityEngine;
using Mono.TextTemplating;

public abstract class UnityTemplateGenerator
{
    /// <summary>
    /// Export할 msg 본문
    /// </summary>
    protected string msg = "";

    protected string fileName;

    /// <summary>
    /// T4 template generator
    /// </summary>
    protected TemplateGenerator generator = new TemplateGenerator();

    protected string sampleT4FilePath;
    protected string exportTtFilePath;
    protected string csFilePath;

    protected string nameSpace;
    protected string className;
       
    protected abstract bool PreGenerateSetup();

    public UnityTemplateGenerator()
    {
        if(generator != null)
        {
            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if(assembly.ReflectionOnly)
                {
                    continue;
                }

                try
                {
                    var location = assembly.Location;
                    if(string.IsNullOrEmpty(location))
                    {
                        continue;
                    }
                    generator.Refs.Add(location);
                }
                catch
                {

                }
            }
            generator.ReferencePaths.Add(Path.GetDirectoryName(typeof(Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost).Assembly.Location));
            generator.ReferencePaths.Add(Path.GetDirectoryName(typeof(UnityEngine.Debug).Assembly.Location));
            generator.ReferencePaths.Add(Path.GetDirectoryName(typeof(UnityEditor.EditorApplication).Assembly.Location));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected bool GenerateFile()
    {
        string language = "";
        string[] refrences;

        //Preprocessig 처리
        if (generator.PreprocessTemplate(exportTtFilePath, className, nameSpace, csFilePath, Encoding.UTF8, out language, out refrences) == false)
        {
            Debug.LogWarningFormat("Fail to pre-process template : {0}", csFilePath);
            foreach (var error in generator.Errors)
            {
                Debug.LogError(error);
            }
            return false;
        }

        //Preprocessing Success시 Template Process 실행
        if (generator.ProcessTemplate(exportTtFilePath, csFilePath) == false)
        {
            Debug.LogWarningFormat("Failed to process template : {0}", csFilePath);
            foreach (var error in generator.Errors)
            {
                Debug.LogError(error);
            }
            return true;
        }

        // Processing success

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected string ExportT4Text()
    {
        string outputPath = Application.dataPath + "/" + fileName + ".tt";
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(outputPath, false))
        {
            writer.WriteLine(msg);
        }
        return outputPath;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ttFilePath"></param>
    /// <returns></returns>
    protected string ReadT4Sample(string ttFilePath)
    {
        using (System.IO.StreamReader reader = new System.IO.StreamReader(ttFilePath))
        {
            msg = reader.ReadToEnd();
        }
        return msg;
    }


    /// <summary>
    /// Default run method
    /// </summary>
    /// <returns></returns>
    public virtual bool Run()
    {
        bool retVal;
        retVal = PreGenerateSetup();
        if(retVal == false)
        {
            Debug.LogWarning("T4Template Generate Error) Setup error");
            return retVal;
        }
        exportTtFilePath = ExportT4Text();
        retVal = GenerateFile();
        return retVal;
    }
}
