--[[
-- added by luac @ 16/8/2022 11:49:00 AM
-- UIGuessTheIdiomGamingUIMain模型层
-- 注意：
-- 1、成员变量预先在OnCreate、OnEnable函数声明，提高代码可读性
-- 2、OnCreate内放窗口生命周期内保持的成员变量，窗口销毁时才会清理
-- 3、OnEnable内放窗口打开时才需要的成员变量，窗口关闭后及时清理
-- 4、OnEnable函数每次在窗口打开时调用，可传递参数用来初始化Model
--]]
local UIGuessTheIdiomGamingModel = BaseClass("UIGuessTheIdiomGamingModel", UIBaseModel)
local IdiomConfig = require('UI.Config.IdiomConfig')
local Idiom_UIMessageNames = require("UI.UIGuessTheIdiom.Idiom_UIMessageNames")

local base = UIBaseModel-- 创建
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
end
-- 打开
local function OnEnable(self)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这
    self.nowLeve = leve or IdiomConfig.getNowLeve()
end

-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

---@public OnAddInputText 玩家输入了字
function UIGuessTheIdiomGamingModel:OnAddInputText(text)
    self:UIBroadcast(Idiom_UIMessageNames.ON_INPUT_TEXT, text)
end

---@public  OnRemoveInputText 删除文字
function UIGuessTheIdiomGamingModel:OnRemoveInputText()
    self:UIBroadcast(Idiom_UIMessageNames.ON_REMOVE_TEXT)
end


-- 监听数据
function UIGuessTheIdiomGamingModel:OnAddListener()
    base.OnAddListener(self)
    --self:AddDataListener(DataMessageNames.ON_LOGIN_SERVER_ID_CHG, OnSelectedSvrChg)
end

function UIGuessTheIdiomGamingModel:OnRemoveListener()
    base.OnRemoveListener(self)
    --self:RemoveDataListener(DataMessageNames.ON_LOGIN_SERVER_ID_CHG, OnSelectedSvrChg)
end

UIGuessTheIdiomGamingModel.OnCreate = OnCreate
UIGuessTheIdiomGamingModel.OnEnable = OnEnable
UIGuessTheIdiomGamingModel.OnDestroy = OnDestroy

return UIGuessTheIdiomGamingModel
