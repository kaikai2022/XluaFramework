---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 1/9/2022 5:32 下午
---

local GuessTheIdiomStartScene = BaseClass("GuessTheIdiomStartScene", BaseScene)
local base = BaseScene

function GuessTheIdiomStartScene:OnCreate()
    base.OnCreate(self)
    self:AddPreloadResource(UIConfig[UIWindowNames.UIStartLayout].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource(UIConfig[UIWindowNames.UIGamingLayout].PrefabPath, typeof(CS.UnityEngine.GameObject), 1)
    --self:AddPreloadResource("MiniGames/KingKongWar/Gaming/Prefab/UIGamingLayoutPanel.prefab", typeof(CS.UnityEngine.GameObject), 1)
    self:AddPreloadResource("MiniGames/KingKongWar/Gaming/Prefab/Maps/Map1.prefab", typeof(CS.UnityEngine.GameObject), 10)
    self:AddPreloadResource("MiniGames/KingKongWar/Gaming/Prefab/Maps/Map2.prefab", typeof(CS.UnityEngine.GameObject), 10)
    self:AddPreloadResource("MiniGames/KingKongWar/Gaming/Prefab/Maps/Map3.prefab", typeof(CS.UnityEngine.GameObject), 10)
    self:AddPreloadResource("MiniGames/KingKongWar/Gaming/Prefab/Maps/Map4.prefab", typeof(CS.UnityEngine.GameObject), 10)
    self:AddPreloadResource("MiniGames/KingKongWar/Gaming/Prefab/Maps/Map5.prefab", typeof(CS.UnityEngine.GameObject), 10)

end

function GuessTheIdiomStartScene:OnComplete()
    base.OnComplete()
    UIManager:GetInstance():OpenWindow(UIWindowNames.UIStartLayout)

end

return GuessTheIdiomStartScene