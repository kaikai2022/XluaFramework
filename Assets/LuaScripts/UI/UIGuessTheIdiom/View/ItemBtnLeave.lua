---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 11/8/2022 8:05 下午
---

local ItemBtnLeave = BaseClass("ItemBtnLeave", UIBaseContainer)
local base = UIBaseContainer
local IdiomConfig = require("UI.Config.IdiomConfig")

function ItemBtnLeave:OnCreate(callback)
    base.OnCreate(self)
    self.leave = -1
    self.text = self:AddComponent(UITextMeshProUGUI, "text")
    self.lock = self:AddComponent(UIImage, "lock")
    self.button = self.transform:GetComponent("Button")
    self:IsLock()
    if callback then
        self.button.onClick:AddListener(function(button)
            return callback(self)
        end)
    end
end

---@public SetLeave 设置等级
---@param value number 当前的按钮的等级
function ItemBtnLeave:SetLeave(value)
    self.leave = value
    self.text:SetText(self.leave)
    self.gameObject:SetActive(IdiomConfig.Count >= self.leave)
    self:IsLock()
end

---@public IsLock 当前状态
function ItemBtnLeave:IsLock()
    if self.leave == -1 or self.leave > IdiomConfig.getPlayerPrefsLeve() then
        self.lock:SetActive(true)
        self.text:SetActive(false)
        self.button.interactable = false
    else
        self.button.interactable = true
        self.lock:SetActive(false)
        self.text:SetActive(true)
    end
end

return ItemBtnLeave