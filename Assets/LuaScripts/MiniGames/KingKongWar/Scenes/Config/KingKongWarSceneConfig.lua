---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 1/9/2022 7:41 下午
---

--[[
-- added by wsh @ 2017-11-15
-- 场景配置
-- 注意：
-- 1、level、name决定加载哪个物理场景
-- 2、Type决定加载哪个逻辑场景，多个逻辑场景可以使用同一个物理场景
--]]

local SceneConfig = {
    KingKongWarStartScene = {
        ---热更新 的场景
        SceneABPath = "MiniGames/KingKongWar/StartLayer/StartScene.unity",
        Name = "StartScene",
        Type = require "MiniGames.KingKongWar.Scenes.KingKongWarStartScene",
    }
}

return ConstClass("KingKongWarSceneConfig", SceneConfig)