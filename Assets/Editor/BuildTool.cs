using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class BuildTool : MonoBehaviour
{
    [MenuItem("Build/Build and Run WebGL")]
    public static void MyWebGLBuild()
    {
        // ===== This sample is taken from the Unity scripting API here:
        // https://docs.unity3d.com/ScriptReference/BuildPipeline.BuildPlayer.html
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] 
        {
            "Assets/Scenes/MainMenuScene.unity",
            "Assets/Scenes/DynaDrawScene.unity",
            "Assets/Scenes/ShootEmScene.unity",
            "Assets/Scenes/GalleryScene.unity",
            "Assets/Scenes/CreditsScene.unity"             
        };
        buildPlayerOptions.locationPathName = "WebGLBuild";
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.AutoRunPlayer;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    [MenuItem("Build/Build and Run UWP")]
    public static void MyUwpBuild()
    {
        // ===== This sample is taken from the Unity scripting API here:
        // https://docs.unity3d.com/ScriptReference/BuildPipeline.BuildPlayer.html
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[]
        {
            "Assets/Scenes/MainMenuScene.unity",
            "Assets/Scenes/DynaDrawScene.unity",
            "Assets/Scenes/ShootEmScene.unity",
            "Assets/Scenes/GalleryScene.unity",
            "Assets/Scenes/CreditsScene.unity"
        };
        buildPlayerOptions.locationPathName = "UwpBuild";
        buildPlayerOptions.target = BuildTarget.WSAPlayer;
        buildPlayerOptions.options = BuildOptions.AutoRunPlayer;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}