---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 10/8/2022 2:22 下午
---

local GuessTheIdiomStartScene = BaseClass("GuessTheIdiomStartScene", BaseScene)
local base = BaseScene

function GuessTheIdiomStartScene:OnCreate()
    base.OnCreate(self)
    Logger.Log("OnCreate")

    self:AddPreloadResource(UIConfig[UIWindowNames.UIGuessTheIdiomStart].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource(UIConfig[UIWindowNames.UIGuessTheIdiomLevel].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource(UIConfig[UIWindowNames.UIGuessTheIdiomGaming].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource("MiniGames/GuessTheIdiom/MasterAudio.prefab", typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource("MiniGames/GuessTheIdiom/LevelSelectionPrefab/btn_leva.prefab", typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource("MiniGames/GuessTheIdiom/GamingPrefab/wait_input_item_box.prefab", typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource("MiniGames/GuessTheIdiom/GamingPrefab/btn_text.prefab", typeof(CS.UnityEngine.GameObject), 1)
end

-- 准备工作
function GuessTheIdiomStartScene:OnComplete(self)
    base.OnComplete(self)
    Logger.Log("OnComplete")
    UIManager:GetInstance():OpenWindow(UIWindowNames.UIGuessTheIdiomStart)
    --local masterAudioObj = ResourcesManager:GetInstance():CoLoadAsync("MiniGames/GuessTheIdiom/MasterAudio.prefab", typeof(CS.UnityEngine.GameObject))
    --GameObject.Instantiate(masterAudioObj)
    --local masterAudio = masterAudioObj.GetCom
    GameObjectPool:GetInstance():CoGetGameObjectAsync("MiniGames/GuessTheIdiom/MasterAudio.prefab")
    CS.DarkTonic.MasterAudio.MasterAudio.PlaySound("bg")
end

function GuessTheIdiomStartScene:OnEnter()
    base.OnEnter(self)
    Logger.Log("OnEnter")

    --GameObjectPool:GetInstance():
end

function GuessTheIdiomStartScene:OnLeave()
    base.OnLeave()
    GameObjectPool:GetInstance():Cleanup(true)
end

function GuessTheIdiomStartScene:OnDestroy()
    base.OnDestroy(self)
    
end

return GuessTheIdiomStartScene