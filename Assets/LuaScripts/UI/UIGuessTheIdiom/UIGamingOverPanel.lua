---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 16/8/2022 3:49 下午
---

local UIGamingOverPanel = BaseClass("UIGamingOverPanel", UIBaseComponent)
local base = UIBaseComponent

local IdiomUIBtnOnClick = require("UI.UIGuessTheIdiom.IdiomUIBtnOnClick")

---@param model UIGuessTheIdiomGamingModel model
function UIGamingOverPanel:OnCreate(model, nextCallback)
    base.OnCreate(self)
    assert(model, 'model是空')
    ---@field model UIGuessTheIdiomGamingModel 游戏的model
    self.model = model
    self.btn_next = self.transform:Find("btn_next_idiom"):GetComponent("Button")
    self.idiomUIBtnOnClick = IdiomUIBtnOnClick.New(self, self.btn_next.gameObject)
    self.idiomUIBtnOnClick:OnCreate()
    if nextCallback then
        self.nextCallback = nextCallback
        self.btn_next.onClick:AddListener(self.nextCallback)
    end
    self.wrong_panel_go = self.transform:Find("wrong_panel").gameObject
    self.correct_panel_go = self.transform:Find("correct_panel").gameObject

    self.stateImagPosTs = self.transform.parent:Find("input_panel/btn_pos")

    self.wrong_panel_go.transform:Find("state_img").position = self.stateImagPosTs.position
    self.correct_panel_go.transform:Find("state_img").position = self.stateImagPosTs.position

    self.wrong_text = self.transform:Find("wrong_panel/bg/text"):GetComponent("Text")

    self:SetActive(false)
end

function UIGamingOverPanel:SetNextCallback(nextCallback)
    if nextCallback then
        if self.nextCallback then
            self.btn_next.onClick:RemoveListener(self.nextCallback)
        end
        self.nextCallback = nextCallback
        self.btn_next.onClick:AddListener(self.nextCallback)
    end
end

---@public ShowOver 显示结束画面
function UIGamingOverPanel:ShowOverPanel(isWin)
    if isWin then
        self:ShowCorrectPanel()
    else
        self:ShowWrongPanel()
    end
end

---@public ShowWrongPanel 显示打错了
---@param leave number 当前等级
function UIGamingOverPanel:ShowWrongPanel()
    self.wrong_text.text = self.model.idiom or ""
    self.wrong_panel_go:SetActive(true)
    self.correct_panel_go:SetActive(false)
    self:SetActive(true)
end

---@public ShowCorrectPanel 赢了
function UIGamingOverPanel:ShowCorrectPanel()
    self.wrong_panel_go:SetActive(false)
    self.correct_panel_go:SetActive(true)
    self:SetActive(true)
end

function UIGamingOverPanel:InitState()
    self:SetActive(false)
end

function UIGamingOverPanel:OnDestroy()
    base.OnDestroy(self)
    self.btn_next.onClick:RemoveListener(self.nextCallback)

end

return UIGamingOverPanel