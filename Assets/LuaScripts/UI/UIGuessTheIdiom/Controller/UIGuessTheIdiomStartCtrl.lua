--[[
-- added by yoyo @10/8/2022 5:31:59 PM
-- +UIGuessTheIdiomStart控制层
--]]
local UIGuessTheIdiomStartCtrl = BaseClass("UIGuessTheIdiomStartCtrl", UIBaseCtrl)

function UIGuessTheIdiomStartCtrl:OnEnterLeave()
    Logger.Log("进入Leave")
    UIManager:GetInstance():CloseWindow(UIWindowNames.UIGuessTheIdiomStart)
    UIManager:GetInstance():OpenWindow(UIWindowNames.UIGuessTheIdiomLevel)
end

return UIGuessTheIdiomStartCtrl