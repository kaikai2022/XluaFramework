--[[
-- added by luac @16/8/2022 11:49:00 AM
-- +UIGuessTheIdiomGaming控制层
--]]
local UIGuessTheIdiomGamingCtrl = BaseClass("UIGuessTheIdiomGamingCtrl", UIBaseCtrl)
---@public OnClickNextGame 点击了下一关
function UIGuessTheIdiomGamingCtrl:OnClickNextGame()
    --self.model.NowLeve = self.model.NowLeve + 1
    Logger.Log("点击了下一关")
end
return UIGuessTheIdiomGamingCtrl