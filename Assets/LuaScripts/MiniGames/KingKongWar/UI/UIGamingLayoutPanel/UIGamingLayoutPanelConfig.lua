--[[
-- added by author @ 2/9/2022 10:53:11 AM
-- UIGamingLayoutPanel模块窗口配置，要使用还需要导出到UI.Config.UIConfig.lua
--]]
-- 窗口配置
local UIGamingLayoutPanel = {
    Name = UIWindowNames.UIGamingLayout,
    Layer = UILayers.SceneLayer,
    Model = require "MiniGames.KingKongWar.UI.UIGamingLayoutPanel.Model.UIGamingLayoutPanelModel",
    Ctrl = require "MiniGames.KingKongWar.UI.UIGamingLayoutPanel.Controller.UIGamingLayoutPanelCtrl",
    View = require "MiniGames.KingKongWar.UI.UIGamingLayoutPanel.View.UIGamingLayoutPanelView",
    PrefabPath = "MiniGames/KingKongWar/Gaming/Prefab/UIGamingLayoutPanel.prefab",
}

return {
    UIGamingLayoutPanel = UIGamingLayoutPanel,
}
