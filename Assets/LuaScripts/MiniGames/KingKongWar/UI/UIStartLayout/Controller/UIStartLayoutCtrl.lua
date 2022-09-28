--[[
-- added by author @1/9/2022 7:11:49 PM
-- +UIStartLayout控制层
--]]
local UIStartLayoutCtrl = BaseClass("UIStartLayoutCtrl", UIBaseCtrl)

function UIStartLayoutCtrl:OnClickSingleBtn()
    Logger.Log("点击了单人游戏")
    UIManager:GetInstance():OpenWindow(UIWindowNames.UIGamingLayout, 1)
    UIManager:GetInstance():CloseWindow(UIWindowNames.UIStartLayout)
end

function UIStartLayoutCtrl:OnClickDoubleBtn()
    Logger.Log("点击了双人游戏")
    UIManager:GetInstance():OpenWindow(UIWindowNames.UIGamingLayout, 2)
    UIManager:GetInstance():CloseWindow(UIWindowNames.UIStartLayout)
end

return UIStartLayoutCtrl