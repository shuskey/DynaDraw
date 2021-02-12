using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class BuildTool : MonoBehaviour
{
    [MenuItem("Build/Build Web GL with Version")]
    public static void MyBuild()
    {
        // This gets the Build Version from Git via the `git describe` command
        PlayerSettings.bundleVersion = Git.BuildVersion;

        // ===== This sample is taken from the Unity scripting API here:
        // https://docs.unity3d.com/ScriptReference/BuildPipeline.BuildPlayer.html
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] 
        { 
            "Assets/Scenes/CreditsScene.unity", 
            "Assets/Scenes/DynaDrawScene.unity",
            "Assets/Scenes/GalleryScene.unity",
            "Assets/Scenes/MainMenuScene.unity",
            "Assets/Scenes/ShootEmScene.unity"
        };
        buildPlayerOptions.locationPathName = "WebGLBuild";
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.None;

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