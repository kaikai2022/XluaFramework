--[[
-- added by luac @ 16/8/2022 11:49:00 AM
-- UIGuessTheIdiomGaming视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UIGuessTheIdiomGamingView = BaseClass("UIGuessTheIdiomGamingView", UIBaseView)
local base = UIBaseView

local img_idiom_path = "IdiomBg/idiom"

local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    self.img_idiom = self:AddComponent(UIImage_Atlas, img_idiom_path, 'MinniGames/GuessTheIdiom/AllIdionms.spriteatlas', "1")
    self:initGameOverPanel()
    self:initGameInputPanel()


end
-- 打开
local function OnEnable(self)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这
    --self.img_idiom.:SetSpriteName(string.format('MinniGames/GuessTheIdiom/AllIdionms/'))
    self.img_idiom:SetSpriteNameAndShow(self.model.nowLeve)
    self.inputPanel:InitData(self.model.nowLeve)
end
-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

function UIGuessTheIdiomGamingView:initGameOverPanel()
    self.overPanel = self:AddComponent(require("UI.UIGuessTheIdiom.UIGamingOverPanel"), 'game_over_panel', Bind(self.ctrl,self.ctrl.OnClickNextGame))
end

function UIGuessTheIdiomGamingView:initGameInputPanel()
    self.waitInput = self:AddComponent(require("UI.UIGuessTheIdiom.UIGamingWaitPanel"), 'input_panel')
    self.inputPanel = self:AddComponent(require("UI.UIGuessTheIdiom.UIGamingInputPanel"), 'input_btns_panel', function(text)
        Logger.Log("点击了按钮" .. text)
        self.waitInput:AddText(text)
        if #self.waitInput.textStr >= 4 then
            
        end
    end, function()
        Logger.Log("点击了删除按钮")
        self.waitInput:RemoveText()
    end)
end

function UIGuessTheIdiomGamingView:OnAddListener()
    base.OnAddListener(self)
    self:AddUIListener(UIMessageNames.UILOGIN_ON_SELECTED_SVR_CHG, OnRefreshServerInfo)

end

function UIGuessTheIdiomGamingView:OnRemoveListener()
    --self.class_type.super:OnRemoveListener()
    base.OnRemoveListener(self)

end

UIGuessTheIdiomGamingView.OnCreate = OnCreate
UIGuessTheIdiomGamingView.OnEnable = OnEnable
UIGuessTheIdiomGamingView.OnDestroy = OnDestroy

return UIGuessTheIdiomGamingView


