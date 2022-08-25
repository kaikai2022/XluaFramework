--[[
-- added by luac @ 16/8/2022 11:49:00 AM
-- UIGuessTheIdiomGamingUIMain模型层
-- 注意：
-- 1、成员变量预先在OnCreate、OnEnable函数声明，提高代码可读性
-- 2、OnCreate内放窗口生命周期内保持的成员变量，窗口销毁时才会清理
-- 3、OnEnable内放窗口打开时才需要的成员变量，窗口关闭后及时清理
-- 4、OnEnable函数每次在窗口打开时调用，可传递参数用来初始化Model
--]]

---@class UIGuessTheIdiomGamingModel 猜谜语游戏的model
local UIGuessTheIdiomGamingModel = BaseClass("UIGuessTheIdiomGamingModel", UIBaseModel)

local IdiomConfig = require('UI.Config.IdiomConfig')
local Idiom_UIMessageNames = require("UI.UIGuessTheIdiom.Idiom_UIMessageNames")
local Idiom_DataMessageNames = require("UI.UIGuessTheIdiom.Idiom_DataMessageNames")

local base = UIBaseModel-- 创建
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    self.nowInputStrs = {}
end
-- 打开
local function OnEnable(self, leve)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这
    self:InitModelData(leve)
end

-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

UIGuessTheIdiomGamingModel.OnCreate = OnCreate
UIGuessTheIdiomGamingModel.OnEnable = OnEnable
UIGuessTheIdiomGamingModel.OnDestroy = OnDestroy

-- 监听数据
function UIGuessTheIdiomGamingModel:OnAddListener()
    base.OnAddListener(self)
    self:AddDataListener(Idiom_DataMessageNames.ON_GAME_START, self.GameStart)
end

function UIGuessTheIdiomGamingModel:OnRemoveListener()
    base.OnRemoveListener(self)
    self:RemoveDataListener(Idiom_DataMessageNames.ON_GAME_START, self.GameStart)
end

---@public OnAddInputText 玩家输入了字
function UIGuessTheIdiomGamingModel:OnAddInputText(text)
    table.insert(self.nowInputStrs, text)
    self:UIBroadcast(Idiom_UIMessageNames.ON_INPUT_TEXT)
    if #self.nowInputStrs >= (string.len(self.idiom) / 3 --[[idiom 文字是中文 中文的单个字占3个位子]]) then
        local str = ""
        for _, value in ipairs(self.nowInputStrs) do
            str = str .. value
        end
        self:UIBroadcast(Idiom_UIMessageNames.ON_GAME_OVER, str == self.idiom)
    end
end

---@public  OnRemoveInputText 删除文字
function UIGuessTheIdiomGamingModel:OnRemoveInputText()
    table.remove(self.nowInputStrs, #self.nowInputStrs)
    self:UIBroadcast(Idiom_UIMessageNames.ON_REMOVE_TEXT)
end

---@public 初始化所有的数据
function UIGuessTheIdiomGamingModel:InitModelData(leve)
    ---@field nowLeve number 当前的等级
    self.nowLeve = leve or IdiomConfig.getPlayerPrefsLeve()
    ---@field idiom string 当前的成语
    self.idiom = IdiomConfig[self.nowLeve]
    ---@field nowInputStrs table 当前玩家已经输入的文字集合
    self.nowInputStrs = {}
    ---@field texts table 当前所有按钮的文字集合
    self.texts = self:RandomAllText(16)
end

---@public ResetRandomAllText 重置所有单个文字
---@param count number 获取文字的个数
function UIGuessTheIdiomGamingModel:RandomAllText(count)
    assert(self.nowLeve, "初始化错误 没有等级")
    local idiom = self.idiom
    local texts = {}
    local index_count = 1
    ---获取当前关卡的成语单个文字存如表中
    for index = 1, string.len(idiom) / 3 do
        local str = string.sub(idiom, index_count, index_count + 2)
        index_count = index_count + 3
        Logger.Log(str)
        table.insert(texts, str)
    end

    ---随机获取一个字
    local function getStr()
        return IdiomConfig.texts[math.random(1, #IdiomConfig.texts)]
    end
    for index = #texts, count - 1 do
        local isOk = true
        while isOk do
            local str = getStr()
            local remove_index = table.removebyvalue(texts, str)
            if remove_index == 0 then
                isOk = false
            end
            table.insert(texts, str)
        end
    end
    ---随机顺序
    math.randomseed(tostring(os.time()):reverse():sub(1, 6))
    local random = math.random(#texts, 50)
    for _ = 1, random do
        local index = math.random(#texts)
        local temp = texts[1]
        texts[1] = texts[index]
        texts[index] = temp
    end
    Logger.Log(table.dump(texts))
    return texts
end

---@public GameStart 开始游戏
function UIGuessTheIdiomGamingModel:GameStart(leve)
    self:InitModelData(leve)
    self:UIBroadcast(Idiom_UIMessageNames.ON_GAME_START)
end

return UIGuessTheIdiomGamingModel
