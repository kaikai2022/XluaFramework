using System;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using DG.DemiEditor;

namespace AssetBundles
{
    internal class LaunchAssetBundleServer : ScriptableSingleton<LaunchAssetBundleServer>
    {
        [SerializeField] int mServerPID = 0;

        public static void CheckAndDoRunning()
        {
            WriteAssetBundleServerURL();

            bool needRunning = AssetBundleConfig.IsSimulateMode;
            bool isRunning = IsRunning();
            if (needRunning != isRunning)
            {
                KillRunningAssetBundleServer();
                if (needRunning)
                {
                    Run();
                }
            }
        }

        static bool IsRunning()
        {
            if (instance.mServerPID == 0)
            {
                return false;
            }

            try
            {
                var process = Process.GetProcessById(instance.mServerPID);
                if (process == null)
                {
                    return false;
                }

                return !process.HasExited;
            }
            catch
            {
                return false;
            }
        }

        static void KillRunningAssetBundleServer()
        {
            try
            {
                if (instance.mServerPID == 0)
                    return;

                var lastProcess = Process.GetProcessById(instance.mServerPID);
                lastProcess.Kill();
                instance.mServerPID = 0;
                UnityEngine.Debug.Log("Local assetbundle server stop!");
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError(string.Format(
                    "Local assetbundle server stop! \n error:{0} \n instance.mServerPID:{1}", exception,
                    instance.mServerPID));
            }
        }


        static void Run()
        {
#if UNITY_EDITOR_OSX
//             Process.Start("/System/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal", "mono " + AssetBundleConfig.LocalSvrAppPath +
//                                                                          " " +
//                                                                          AssetBundleConfig.LocalSvrAppWorkPath);
             return;
#endif
            string args = string.Format("\"{0}\" {1}", AssetBundleConfig.LocalSvrAppWorkPath,
                Process.GetCurrentProcess().Id);
            ProcessStartInfo startInfo =
                ExecuteInternalMono.GetProfileStartInfoForMono(
                    MonoInstallationFinder.GetMonoInstallation("MonoBleedingEdge"), GetMonoProfileVersion(),
                    AssetBundleConfig.LocalSvrAppPath, args, true);
            startInfo.WorkingDirectory = AssetBundleConfig.LocalSvrAppWorkPath;

            Process launchProcess = Process.Start(startInfo);
            launchProcess.Exited += (sender, eventArgs) =>
            {
                UnityEngine.Debug.LogError(string.Format("AssetBundleServer over {0}", eventArgs.ToString()));
            };
            if (launchProcess == null || launchProcess.HasExited == true || launchProcess.Id == 0)
            {
                UnityEngine.Debug.LogError("Unable Start AssetBundleServer process!");
                UnityEngine.Debug.LogError(launchProcess.StandardError.ReadToEnd());
            }
            else
            {
                instance.mServerPID = launchProcess.Id;
                UnityEngine.Debug.Log(string.Format("Local assetbundle server run! pid:{0}", launchProcess.Id));
            }

            var launchProcessDebugThread = new Thread(async () =>
            {
                while (true)
                {
                    string str = await launchProcess.StandardOutput.ReadLineAsync();
                    if (!str.IsNullOrEmpty())
                        UnityEngine.Debug.Log(str);
                }
            });
            var launchProcessErrorThread = new Thread(async () =>
            {
                while (true)
                {
                    string errStr = await launchProcess.StandardError.ReadLineAsync();
                    if (!errStr.IsNullOrEmpty())
                    {
                        UnityEngine.Debug.LogError(errStr);
                    }
                }
            });
            launchProcessDebugThread.Start();
            launchProcessErrorThread.Start();

            // var thread = new Thread(() =>
            // {
            //     AssetBundleServer.MainClass.Run(new[]
            //     {
            //         AssetBundleConfig.LocalSvrAppWorkPath,
            //         // Process.GetCurrentProcess().Id.ToString()
            //     });
            // });
            // // instance.mServerPID = thread.GetHashCode();
            // thread.Start();
        }

        static string GetMonoProfileVersion()
        {
            string path =
                Path.Combine(Path.Combine(MonoInstallationFinder.GetMonoInstallation("MonoBleedingEdge"), "lib"),
                    "mono");

            string[] folders = Directory.GetDirectories(path);
            string[] foldersWithApi = folders.Where(f => f.Contains("-api")).ToArray();
            string profileVersion = "0";

            for (int i = 0; i < foldersWithApi.Length; i++)
            {
                foldersWithApi[i] = foldersWithApi[i].Split(Path.DirectorySeparatorChar).Last();
                foldersWithApi[i] = foldersWithApi[i].Split('-').First();

                if (string.Compare(foldersWithApi[i], profileVersion) > 0)
                {
                    profileVersion = foldersWithApi[i];
                }
            }

            return profileVersion; // + "-api";
            // return "4.5";
        }

        public static string GetStreamingAssetBundleServerUrl()
        {
            string assetBundleServerUrl =
                Path.Combine(Application.streamingAssetsPath, AssetBundleConfig.AssetBundlesFolderName);
            assetBundleServerUrl = Path.Combine(assetBundleServerUrl, AssetBundleConfig.AssetBundleServerUrlFileName);
            return assetBundleServerUrl;
        }

        public static void WriteAssetBundleServerURL()
        {
            var path = GetStreamingAssetBundleServerUrl();
            GameUtility.SafeWriteAllText(path, GetAssetBundleServerURL());
            AssetDatabase.Refresh();
        }

        public static void ClearAssetBundleServerURL()
        {
            var path = GetStreamingAssetBundleServerUrl();
            GameUtility.SafeDeleteFile(path);
            AssetDatabase.Refresh();
        }

        public static string GetAssetBundleServerURL()
        {
            string ip = PackageUtils.GetLocalServerIP();
            string downloadURL = "http://" + ip + ":7888/";
            downloadURL = downloadURL + PackageUtils.GetCurPlatformChannelRelativePath() + "/";
            return downloadURL;
        }
    }
}