using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class DeployTool : MonoBehaviour
{
    [MenuItem("Build/Deploy to AWS S3 Photoloom.com")]
    public static void MyDeploy()
    {
        using (var process = new System.Diagnostics.Process())
        {
            var arguments = "../Deploy-ToPhotoloom.ps1";
            var exitCode = process.Run(@"powershell", arguments, Application.dataPath,
                out var output, out var errors);
            if (exitCode == 0)
            {                
                Debug.Log("Deploy succeeded");
                Debug.Log($"Output:{output}");
            }
            else
            {                
                Debug.Log("Deploy failed");
                Debug.Log($"Errors: {errors}");
            }
        }        
    }
}