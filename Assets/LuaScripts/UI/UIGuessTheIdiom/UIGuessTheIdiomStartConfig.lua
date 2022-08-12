--[[
-- added by yoyo @ 10/8/2022 5:31:59 PM
-- UIGuessTheIdiomStart模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local UIGuessTheIdiomStart = {
    Name = UIWindowNames.UIGuessTheIdiomStart,
    Layer = UILayers.SceneLayer,
    Model = require "UI.UIGuessTheIdiom.Model.UIGuessTheIdiomStartModel",
    Ctrl = require "UI.UIGuessTheIdiom.Controller.UIGuessTheIdiomStartCtrl",
    View = require "UI.UIGuessTheIdiom.View.UIGuessTheIdiomStartView",
    PrefabPath = "MinniGames/GuessTheIdiom/UIStartPanel.prefab",
}

-- 等级界面
local UIGuessTheIdiomLevel = {
    Name = UIWindowNames.UIGuessTheIdiomLevel,
    Layer = UILayers.SceneLayer,
    Model = require "UI.UIGuessTheIdiom.Model.UIGuessTheIdiomLevelModel",
    Ctrl = require "UI.UIGuessTheIdiom.Controller.UIGuessTheIdiomLevelCtrl",
    View = require "UI.UIGuessTheIdiom.View.UIGuessTheIdiomLevelView",
    PrefabPath = "MinniGames/GuessTheIdiom/UILevelSelection2.prefab",
}

return {
    UIGuessTheIdiomStart = UIGuessTheIdiomStart,
    UIGuessTheIdiomLevel = UIGuessTheIdiomLevel,
}
