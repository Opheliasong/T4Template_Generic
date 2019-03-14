/*
 * 
 * 
 */

using UnityEngine;

/// <summary>
/// ResourceAsyncLoad.tt 파일을 기반으로 TT 파일을 읽고 output하는 클래스
/// 설정된 Template을 이용하기에, 다른 setup과 같은 행위가 필요하지 않으며, run만 실행하게 되면 
/// "Asset/Resources/AsyncLoad" 폴더내 파일들을 로드하는 TT 템플릿을 사용하여 코드를 생성한다.
/// </summary>
public class ResourceLoadT4TemplateGenerator : UnityTemplateGenerator
{
    /// <summary>
    /// Sample 파일의 경로, Export할 tt파일의 경로, Export할 cs파일의 경로를 설정한다.
    /// </summary>
    /// <returns></returns>
    protected override bool PreGenerateSetup()
    {
        className = "ResourceLoader";
        //csFilePath = "";
        nameSpace = "";
        fileName = "ResourceLoader";

        sampleT4FilePath = Application.dataPath + "/T4Sample/ResourceAsyncLoad.tt";
        exportTtFilePath = Application.dataPath + "/ResourceAsyncLoader.tt";
        csFilePath = Application.dataPath + "/ResourceAsyncLoader.cs";

        msg = ReadT4Sample(sampleT4FilePath);
        return true;
    }
}
