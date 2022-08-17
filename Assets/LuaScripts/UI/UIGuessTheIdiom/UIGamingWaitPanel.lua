---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 16/8/2022 5:24 下午
---
local UIGamingWaitPanel = BaseClass("UIGamingWaitPanel", UIBaseComponent)
local base = UIBaseComponent

function UIGamingWaitPanel:OnCreate()
    base.OnCreate(self)
    local imageTs = self.transform:Find("Image")
    local wait_input_item_box_prefab = ResourcesManager:GetInstance():CoLoadAsync("MinniGames/GuessTheIdiom/GamingPrefab/wait_input_item_box.prefab", typeof(GameObject), nil)
    self.texts = {}
    for index = 1, 4 do
        local wait_input_item_box = GameObject.Instantiate(wait_input_item_box_prefab, imageTs)
        local text = wait_input_item_box.transform:Find("Text"):GetComponent("Text")
        table.insert(self.texts, text)
    end
    self.textStr = {}
    self:UpdateText()
end
---@public UpdateText 刷新文字显示
function UIGamingWaitPanel:UpdateText()
    for index, text in ipairs(self.texts) do
        text.text = self.textStr[index] or ""
    end
end

---@public AddText 添加一个文字
function UIGamingWaitPanel:AddText(text)
    assert(text, '添加的文字是nil')
    if #self.textStr >= #self.texts then
        return
    end
    table.insert(self.textStr, text)
    self:UpdateText()
end

---@public RemoveText 删除一个文字
function UIGamingWaitPanel:RemoveText()
    self.textStr[#self.textStr] = nil
    self:UpdateText()
end

---public GetString 获取当前的文字
function UIGamingWaitPanel:GetString()
    local str = ""
    for index, textStr in ipairs(self.textStr) do
        str = str .. textStr
    end
    return str
end

return UIGamingWaitPanel