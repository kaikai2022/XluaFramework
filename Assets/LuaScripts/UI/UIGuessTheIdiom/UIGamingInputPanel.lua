---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 16/8/2022 4:23 下午
---

local UIGamingInputPanel = BaseClass("UIGamingInputPanel", UIBaseComponent)
local base = UIBaseComponent
local IdiomConfig = require('UI.Config.IdiomConfig')

function UIGamingInputPanel:OnCreate(inputCallback, removeCallback)
    base.OnCreate(self)
    self.stateImagPosTs = self.transform.parent:Find("input_panel/btn_pos")

    self.btn_remove = self.transform:Find("btn_remove"):GetComponent("Button")
    self.btn_remove.transform.position = self.stateImagPosTs.position
    self.btn_remove.onClick:AddListener(function()
        if removeCallback then
            removeCallback()
        end
    end)
    self.content = self.transform:Find("content")
    local btn_text_prefab = ResourcesManager:GetInstance():CoLoadAsync("MinniGames/GuessTheIdiom/GamingPrefab/btn_text.prefab", typeof(GameObject), nil)
    self.textBtnList = {}
    for index = 1, 16 do
        local btn_text = GameObject.Instantiate(btn_text_prefab, self.content)
        local text = btn_text.transform:Find("text"):GetComponent("Text")

        btn_text:GetComponent("Button").onClick:AddListener(function()
            if inputCallback then
                inputCallback(text.text)
            end
        end)
        table.insert(self.textBtnList, text)
    end

end

---@public InitData 每局游戏初始化游戏数据
function UIGamingInputPanel:InitData(leave)
    assert(leave, "初始化错误 没有等级")
    local idiom = IdiomConfig[leave]
    local texts = {}
    local index_count = 1
    for index = 1, string.len(idiom) / 3 do
        local str = string.sub(idiom, index_count, index_count + 2)
        index_count = index_count + 3
        Logger.Log(str)
        table.insert(texts, str)
    end

    local function getStr()
        return IdiomConfig.texts[math.random(1, #IdiomConfig.texts)]
    end
    for index = #texts, #self.textBtnList - 1 do
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
    --dump(texts)

    math.randomseed(tostring(os.time()):reverse():sub(1, 6))
    local random = math.random(#texts, 50)
    for _ = 1, random do
        local index = math.random(#texts)
        local temp = texts[1]
        texts[1] = texts[index]
        texts[index] = temp
    end
    --dump(texts)
    Logger.Log(table.dump(texts))
    for index, btn_text in ipairs(self.textBtnList) do
        btn_text.text = texts[index]
    end
end

return UIGamingInputPanel