--[[
-- added by yoyo @10/8/2022 7:08:46 PM
-- +UIGuessTheIdiomLevel控制层
--]]
local UIGuessTheIdiomLevelCtrl = BaseClass("UIGuessTheIdiomLevelCtrl", UIBaseCtrl)

function UIGuessTheIdiomLevelCtrl:OnCreate()
    self.super.OnCreate(self)

end

function UIGuessTheIdiomLevelCtrl:EnterGaming(leve)
    coroutine.start(function()
        UIManager:GetInstance():OpenWindow(UIWindowNames.UIGuessTheIdiomGaming, leve)
    end)
end

return UIGuessTheIdiomLevelCtrl