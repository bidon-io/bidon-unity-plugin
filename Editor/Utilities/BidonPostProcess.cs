#if UNITY_IOS
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

// ReSharper Disable CheckNamespace
namespace Bidon.Mediation.Editor.Utilities
{
    internal class BidonPostProcess : MonoBehaviour
    {
        private const string Suffix = ".framework";

        private static readonly string[] FrameworkList =
        {
            "AdSupport",
            "AudioToolbox",
            "AVFoundation",
            "CFNetwork",
            "CoreFoundation",
            "CoreGraphics",
            "CoreImage",
            "CoreLocation",
            "CoreMedia",
            "CoreMotion",
            "CoreTelephony",
            "CoreText",
            "EventKitUI",
            "EventKit",
            "GLKit",
            "ImageIO",
            "JavaScriptCore",
            "MediaPlayer",
            "MessageUI",
            "MobileCoreServices",
            "QuartzCore",
            "SafariServices",
            "Security",
            "Social",
            "StoreKit",
            "SystemConfiguration",
            "Twitter",
            "UIKit",
            "VideoToolbox",
            "WatchConnectivity",
            "WebKit"
        };

        private static readonly string[] WeakFrameworkList =
        {
            "AppTrackingTransparency"
        };

        [PostProcessBuild(100)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target.ToString() != "iOS") return;

            PrepareProject(path);
        }

        private static void PrepareProject(string buildPath)
        {
            Debug.Log("Preparing your Xcode project for Bidon");

            string projectPath = PBXProject.GetPBXProjectPath(buildPath);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);

            string mainTarget = project.GetUnityMainTargetGuid();
            string unityFrameworkTarget = project.GetUnityFrameworkTargetGuid();

            AddProjectFrameworks(FrameworkList, project, mainTarget, false);
            AddProjectFrameworks(WeakFrameworkList, project, mainTarget, true);

            project.SetBuildProperty(mainTarget, "SWIFT_VERSION", "5.0");
            project.SetBuildProperty(mainTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

            project.SetBuildProperty(project.ProjectGuid(), "ENABLE_BITCODE", "NO");

            project.SetBuildProperty(unityFrameworkTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");

            project.AddBuildProperty(mainTarget, "OTHER_LDFLAGS", "-ObjC");
            project.AddBuildProperty(mainTarget, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries");
            project.AddBuildProperty(mainTarget, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
            project.AddBuildProperty(mainTarget, "LIBRARY_SEARCH_PATHS", "$(TOOLCHAIN_DIR)/usr/lib/swift/$(PLATFORM_NAME)");

            project.WriteToFile(projectPath);
        }

        private static void AddProjectFrameworks(IEnumerable<string> frameworks, PBXProject project, string target, bool weak)
        {
            foreach (var framework in frameworks)
            {
                if (!project.ContainsFramework(target, framework))
                {
                    project.AddFrameworkToProject(target, framework + Suffix, weak);
                }
            }
        }
    }
}
#endif
