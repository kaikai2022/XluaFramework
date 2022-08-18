--[[
-- added by luac @16/8/2022 11:49:00 AM
-- +UIGuessTheIdiomGaming控制层
--]]
local UIGuessTheIdiomGamingCtrl = BaseClass("UIGuessTheIdiomGamingCtrl", UIBaseCtrl)
--local Idiom_DataMessageNames = require("UI.UIGuessTheIdiom.Idiom_DataMessageNames")

local IdiomConfig = require('UI.Config.IdiomConfig')

---@public OnClickNextGame 点击了下一关
function UIGuessTheIdiomGamingCtrl:OnClickNextGame()
    --self.model.NowLeve = self.model.NowLeve + 1
    Logger.Log("点击了下一关")
    IdiomConfig.setNowLeve(self.model.nowLeve + 1)
end

---@public OnClickInputText 玩家输入了文字
function UIGuessTheIdiomGamingCtrl:OnClickInputText(text)
    self.model:OnAddInputText(text)
end

function UIGuessTheIdiomGamingCtrl:OnClickRemoveText()
    self.model:OnRemoveInputText()
end

function UIGuessTheIdiomGamingCtrl:GameStart()
    IdiomConfig.setNowLeve(self.model.nowLeve)
end

return UIGuessTheIdiomGamingCtrl