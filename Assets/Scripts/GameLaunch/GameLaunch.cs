using UnityEngine;
using System.Collections;
using AssetBundles;
using GameChannel;
using System;
using UnityEngine.Android;
using UnityEngine.UI;
using XLua;

[Hotfix]
[LuaCallCSharp]
public class GameLaunch : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }

    IEnumerator Start()
    {
        //注释掉IOS的推送服务
//#if UNITY_IPHONE
//        UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
//        UnityEngine.iOS.Device.SetNoBackupFlag(Application.persistentDataPath);
//#endif

        // 启动资源管理模块
        var start = DateTime.Now;
        yield return AssetBundleManager.Instance.Initialize();
        Logger.Log(string.Format("AssetBundleManager Initialize use {0}ms", (DateTime.Now - start).Milliseconds));

        yield return null;

        XLuaManager.Instance.Startup();
        yield return StartGame();
        Logger.Log(string.Format("XLuaManager StartUp use {0}ms", (DateTime.Now - start).Milliseconds));
    }

    IEnumerator StartGame()
    {
        string luaAssetbundleName = XLuaManager.Instance.AssetbundleName;
        AssetBundleManager.Instance.SetAssetBundleResident(luaAssetbundleName, true);
        var abloader = AssetBundleManager.Instance.LoadAssetBundleAsync(luaAssetbundleName, typeof(TextAsset));
        yield return abloader;
        abloader.Dispose();

        XLuaManager.Instance.OnInit();
        XLuaManager.Instance.StartHotfix();
        XLuaManager.Instance.StartToLuaSprite();
        CustomDataStruct.Helper.Startup();
    }
}