using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class VersionTool : MonoBehaviour
{
    [MenuItem("Build/Set Build Version")]
    public static void MyVersion()
    {
        // This gets the Build Version from Git via the `git describe` command
        PlayerSettings.bundleVersion = Git.BuildVersion;
    }
}