--[[
-- added by yoyo @ 10/8/2022 5:31:59 PM
-- UIGuessTheIdiomStart视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UIGuessTheIdiomStartView = BaseClass("UIGuessTheIdiomStartView", UIBaseView)
local base = UIBaseView

local btn_start_path = "btn_start"
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    self.btn_start = self:AddComponent(UIButton, btn_start_path)
    self.btn_start:SetOnClick(Bind(self.ctrl, self.ctrl.OnEnterLeave))

    self:AddComponent(require("UI.UIGuessTheIdiom.Idiom_UI_SoundToggle"), "toggle_sound")
    self:AddComponent(require("UI.UIGuessTheIdiom.IdiomUIBtnOnClick"), btn_start_path)
end
-- 打开
local function OnEnable(self)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这
end
-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

UIGuessTheIdiomStartView.OnCreate = OnCreate
UIGuessTheIdiomStartView.OnEnable = OnEnable
UIGuessTheIdiomStartView.OnDestroy = OnDestroy

--function UIGuessTheIdiomStartView:()
--    
--end

return UIGuessTheIdiomStartView


