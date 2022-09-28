--[[
-- added by author @ 1/9/2022 7:11:49 PM
-- UIStartLayout模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local UIStartLayout = {
    Name = UIWindowNames.UIStartLayout,
    Layer = UILayers.SceneLayer,
    Model = require "MiniGames.KingKongWar.UI.UIStartLayout.Model.UIStartLayoutModel",
    Ctrl = require "MiniGames.KingKongWar.UI.UIStartLayout.Controller.UIStartLayoutCtrl",
    View = require "MiniGames.KingKongWar.UI.UIStartLayout.View.UIStartLayoutView",
    PrefabPath = "MiniGames/KingKongWar/StartLayer/Prefab/UIStartLayoutPanel.prefab",
}

return {
    UIStartLayout = UIStartLayout,
}
