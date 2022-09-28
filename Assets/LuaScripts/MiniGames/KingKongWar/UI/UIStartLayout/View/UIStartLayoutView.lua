--[[
-- added by author @ 1/9/2022 7:11:49 PM
-- UIStartLayout视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UIStartLayoutView = BaseClass("UIStartLayoutView", UIBaseView)
local base = UIBaseView

local singleBtnPath = "SingleBtn"
local doubleBtnPath = "DoubleBtn"
local function OnCreate(self)
	base.OnCreate(self)
	-- 窗口生命周期内保持的成员变量放这
	self.singleBtn = self:AddComponent(UIButton,singleBtnPath)
	self.doubleBtn = self:AddComponent(UIButton,doubleBtnPath)
	self.singleBtn:SetOnClick(self.ctrl,self.ctrl.OnClickSingleBtn)
	self.doubleBtn:SetOnClick(self.ctrl,self.ctrl.OnClickDoubleBtn)
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


UIStartLayoutView.OnCreate = OnCreate
UIStartLayoutView.OnEnable = OnEnable
UIStartLayoutView.OnDestroy = OnDestroy

return UIStartLayoutView


