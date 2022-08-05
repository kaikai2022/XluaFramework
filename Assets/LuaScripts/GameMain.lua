-- 全局模块
require "Global.Global"

-- 定义为全局模块，整个lua程序的入口类
GameMain = {};

local AssetBundleManager = CS.AssetBundles.AssetBundleManager.Instance
-- 进入游戏
local function EnterGame()

    -- luaide 调试
    -- local breakInfoFun,xpcallFun = require("LuaDebug")("localhost", 7003)
    -- luaide 调试

    -- TODO：服务器信息应该从服务器上拉取，这里读取测试数据
    local ServerData = require "DataCenter.ServerData.ServerData"
    local TestServerData = require "GameTest.DataTest.TestServerData"
    local ClientData = require "DataCenter.ClientData.ClientData"
    ServerData:GetInstance():ParseServerList(TestServerData)
    local selected = ClientData:GetInstance().login_server_id
    if selected == nil or ServerData:GetInstance().servers[selected] == nil then
        ClientData:GetInstance():SetLoginServerID(10001)
    end

    SceneManager:GetInstance():SwitchScene(SceneConfig.LoginScene)
end

--主入口函数。从这里开始lua逻辑
local function Start()
    Logger.Log("GameMain start...")

    local gameLaunch = CS.UnityEngine.GameObject.Find("GameLuach"):GetComponent(typeof(CS.GameLaunch))
    --if gameLaunch:IsUpdateFinal() then
    --    -- 模块启动
    UpdateManager:GetInstance():Startup()
    TimerManager:GetInstance():Startup()
    LogicUpdater:GetInstance():Startup()
    UIManager:GetInstance():Startup()
    --
    --    -- if Config.Debug then
    --    --    -- 单元测试
    --    --    local UnitTest = require "UnitTest.UnitTestMain"
    --    --    UnitTest.Run()
    --    -- end
    --
    --    coroutine.start(function()
    --        EnterGame()
    --    end)
    --
    --else
    --    gameLaunch:StartCoroutine(gameLaunch:InitLaunchPrefab())
    --end
    --coroutine.start(
    --        function()
    --            cs_coroutine.yield_return(gameLaunch:InitLaunchPrefab())
    --            cs_coroutine.yield_return(gameLaunch:InitLaunchPrefab())
    --        end
    --)
    coroutine.start(GameMain.UpdateRes)
    --GameMain.UpdateRes()

end

-- 场景切换通知
local function OnLevelWasLoaded(level)
    collectgarbage("collect")
    Time.timeSinceLevelLoad = 0
end

local function OnApplicationQuit()
    -- 模块注销
    UpdateManager:GetInstance():Dispose()
    TimerManager:GetInstance():Dispose()
    LogicUpdater:GetInstance():Dispose()
    -- HallConnector:GetInstance():Dispose()
end

---@private  初始化UI界面
function GameMain.InitLaunchPrefab()
    ---初始化加载的prefab
    local launchPrefabPath = "UI/Prefabs/UILoading/UILoading.prefab"
    --ResourcesManager:GetInstance():LoadAsync(launchPrefabPath, typeof(CS.UnityEngine.GameObject), function(loadingGameObject)
    --    print(loadingGameObject)
    --end)

    local loadingGameObject = ResourcesManager:GetInstance():CoLoadAsync(launchPrefabPath, typeof(CS.UnityEngine.GameObject))
    coroutine.waitforfixedupdate()
    local go = CS.UnityEngine.GameObject.Instantiate(loadingGameObject)
    CS.UILauncher.Instance.UIGameObject = go
    UIManager:GetInstance():OpenWindow(UIWindowNames.UILoading)
    local loadingWin = UIManager:GetInstance():GetWindow(UIWindowNames.UILoading)
    GameMain.update = loadingWin.View.gameObject:AddComponent(typeof(CS.AssetbundleUpdater))
end

---private GameMain.InitAppVersion 初始化App版本
function GameMain.InitAppVersion()
    local streamingAppVersion = "0.0.0";
    local streamingResVersion = "0.0.0";
    local streamingChannel = "Test";

    --编辑器
    if CS.UnityEngine.Application.platform == CS.UnityEngine.RuntimePlatform.OSXEditor or
            CS.UnityEngine.Application.platform == CS.UnityEngine.RuntimePlatform.WindowsEditor or
            CS.UnityEngine.Application.platform == CS.UnityEngine.RuntimePlatform.LinuxEditor then
        --是否是Edit模式直接运行 不是AssetBundle
        if CS.AssetBundles.AssetBundleConfig.IsEditorMode then
            CS.GameChannel.ChannelManager.Instance.appVersion = streamingAppVersion
            CS.GameChannel.ChannelManager.Instance.resVersion = streamingResVersion
            GameMain.channelName = streamingChannel
            return
        end
    end

    local loader = AssetBundleManager:RequestAssetFileAsync(CS.BuildUtils.AppVersionFileName)
    coroutine.waitforasyncop(loader)
    local streamingTxt = loader.text
    loader:Dispose()

    if not string.isNilOrEmpty(streamingTxt) then
        local strings = string.split(streamingTxt, '|')
        streamingAppVersion = strings[1]
        streamingResVersion = strings[2]
        streamingChannel = strings[3]
    end

    local persistentAppVersion = streamingAppVersion;
    local persistentResVersion = streamingResVersion;
    local persistentChannel = streamingChannel;

    local appVersionPath = CS.AssetBundles.AssetBundleUtility.GetPersistentDataPath(CS.BuildUtils.AppVersionFileName);
    local persistentTxt = CS.GameUtility.SafeReadAllText(appVersionPath);

    if string.isNilOrEmpty(persistentTxt) then
        CS.GameUtility.SafeWriteAllText(appVersionPath, streamingAppVersion .. "|" .. streamingResVersion .. "|" .. streamingChannel)
    else
        local strings = string.split(streamingTxt, '|')
        persistentAppVersion = strings[1]
        persistentResVersion = strings[2]
        persistentChannel = strings[3]
    end

    CS.GameChannel.ChannelManager.Instance.appVersion = persistentAppVersion
    CS.GameChannel.ChannelManager.Instance.resVersion = persistentResVersion
    GameMain.channelName = persistentChannel
    Logger.Log(string.format("persistentResVersion = %s, persistentResVersion = %s, persistentChannel = %s",
            persistentAppVersion, persistentResVersion, persistentChannel))
    Logger.Log(string.format("streamingAppVersion = %s, streamingAppVersion = %s, streamingChannel = %s",
            streamingAppVersion, streamingResVersion, streamingChannel))

    -- 如果persistent目录版本比streamingAssets目录app版本低，说明是大版本覆盖安装，清理过时的缓存
    if (CS.BuildUtils.CheckIsNewVersion(persistentAppVersion, streamingAppVersion)) then

        Logger.Log("大版本覆盖安装，清理过时的缓存")

        CS.GameChannel.ChannelManager.Instance.appVersion = streamingAppVersion
        CS.GameChannel.ChannelManager.Instance.resVersion = streamingResVersion

        local path = CS.AssetBundles.AssetBundleUtility.GetPersistentDataPath()
        CS.GameUtility.SafeDeleteDir(path)
        CS.GameUtility.SafeWriteAllText(appVersionPath, streamingAppVersion .. "|" .. streamingResVersion .. "|" .. streamingChannel)
        -- 重启资源管理器
        coroutine.yieldreturn(AssetBundleManager.Instance.Cleanup())
        coroutine.yieldreturn(AssetBundleManager.Instance.Initialize())
    end
    Logger.Log("app版本初始化完成")
end

---@private GameMain.InitChannel 初始化渠道
function GameMain.InitChannel()
    CS.GameChannel.ChannelManager.Instance:Init(GameMain.channelName)
    Logger.Log(string.format("channelName = %s", GameMain.channelName))
    coroutine.waitforframes(1)
end

---@private GameMain.InitSDK 初始化SDK
function GameMain.InitSDK()
    CS.GameChannel.ChannelManager.Instance:InitSDK(function()
        Logger.Log("sdk 初始化完成")
    end)
    coroutine.waitforframes(1)
end

---@private GameMain.InitNoticeTipPrefab 初始化弹窗提示框
function GameMain.InitNoticeTipPrefab()
    local noticeTipPrefabPath = "UI/Prefabs/Common/UINoticeTip.prefab"
    local loader = AssetBundleManager:LoadAssetAsync(noticeTipPrefabPath, typeof(GameObject))
    coroutine.waitforasyncop(loader)
    local noticeTipPrefab = loader.asset
    loader:Dispose()
    if (noticeTipPrefab == null) then
        Logger.LogError("LoadAssetAsync noticeTipPrefab err : " + noticeTipPrefabPath);
        coroutine.yieldbreak()
    end
    local go = GameObject.Instantiate(noticeTipPrefab)
    CS.UINoticeTip.Instance.UIGameObject = go
    coroutine.waitforframes(1)
end

---@private GameMain.GetPermission 获取权限
function GameMain.GetPermission()
    if CS.UnityEngine.Application.platform == CS.UnityEngine.RuntimePlatform.Android and not CS.UnityEngine.Android.Permission.HasUserAuthorizedPermission(Permission.Microphone) then
        Permission.RequestUserPermission(Permission.Microphone)
    end
end

---@private  GameMain.UpdateAssetRes 更新资源
function GameMain.StartUpdateAssetRes()
    local start = GameMain.update:StartCheckUpdate()
    require('XLua.Common.cs_coroutine').yield_return(start)
    Logger.Log("更新完成 ！！！！！")
end

---@private GameMain.UpdateRes 更新资源
function GameMain.UpdateRes()
    GameMain.InitLaunchPrefab()
    GameMain.InitAppVersion()
    GameMain.InitChannel()
    GameMain.InitSDK()
    GameMain.InitNoticeTipPrefab()
    GameMain.StartUpdateAssetRes()
end

-- GameMain公共接口，其它的一律为私有接口，只能在本模块访问
GameMain.Start = Start
GameMain.OnLevelWasLoaded = OnLevelWasLoaded
GameMain.OnApplicationQuit = OnApplicationQuit

return GameMain