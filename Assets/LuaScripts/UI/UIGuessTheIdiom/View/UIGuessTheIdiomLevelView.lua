--[[
-- added by yoyo @ 10/8/2022 7:08:46 PM
-- UIGuessTheIdiomLevel视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UIGuessTheIdiomLevelView = BaseClass("UIGuessTheIdiomLevelView", UIBaseView)
local base = UIBaseView

local ItemBtnLeave = require("UI.UIGuessTheIdiom.View.ItemBtnLeave")
local UIPageView = require("UI.UIGuessTheIdiom.View.UIPageView")
local btn_left = "text_page/btn_left"
local btn_right = "text_page/btn_right"
local TMPro_page = "text_page"

local page_content = "scroll_panel"

local panelCounts = (3 * 8)
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    self.btn_left = self:AddComponent(UIButton, btn_left)
    self.btn_left:SetOnClick(Bind(self, self._PreviousPage))
    self:AddComponent(require("UI.UIGuessTheIdiom.IdiomUIBtnOnClick"), btn_left)

    self.btn_right = self:AddComponent(UIButton, btn_right)
    self.btn_right:SetOnClick(Bind(self, self._NextPage))
    self:AddComponent(require("UI.UIGuessTheIdiom.IdiomUIBtnOnClick"), btn_right)

    self.text_page = self:AddComponent(UIText, TMPro_page)
    --self.pageView = UIUtil.FindComponent(self.transform, typeof(CS.UI.GridPageView), page_content)

    --self.page_content = self.transform:Find(page_content)

    --ResourcesManager:GetInstance():LoadAsync(
    --        "MinniGames/GuessTheIdiom/LevelSelectionPrefab/btn_leva.prefab",
    --        typeof(GameObject),
    --        function(asset)
    --            self.btn_leva_gameObject = asset
    --        end)

    GameObjectPool:GetInstance():GetGameObjectAsync("MinniGames/GuessTheIdiom/LevelSelectionPrefab/btn_leva.prefab", function(asset)
        self.btn_leva_gameObject = asset
        self.btn_leva_gameObject:SetActive(false)
        
    end)
    --self.pageView = self:AddComponent(UIPageView, page_content,
    --        math.ceil(100 / (3 * 8)),
    --        Bind(self, self.OnGetItemByIndex),
    --        Bind(self, self.OnSnapNearestChanged)
    --)

    self.pageView = UIPageView.New(self, page_content)
    self.pageView:OnCreate(
            math.ceil(self.model.idiomConfig.Count / panelCounts),
            Bind(self, self.OnGetItemByIndex),
            Bind(self, self.OnSnapNearestChanged)
    )
    self:AddComponent(require("UI.UIGuessTheIdiom.Idiom_UI_SoundToggle"), "toggle_sound")

end


-- 打开
local function OnEnable(self)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这

    --self.pageView:init(3, 8, 100, true, 5, 5, function(gameObject, index, pageIndex, isReload)
    --    --print(index)
    --    gameObject.transform:Find("text"):GetComponent(typeof(CS.TMPro.TextMeshProUGUI)).text = index + 1
    --end)
    --self.page_content.transform:rectTransform().pivot = Vector2.New(0.5, 0.5)
    --self.page_content.transform:rectTransform().anchoredPosition3D = Vector3.New(0, 0, 0)
end
-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

UIGuessTheIdiomLevelView.OnCreate = OnCreate
UIGuessTheIdiomLevelView.OnEnable = OnEnable
UIGuessTheIdiomLevelView.OnDestroy = OnDestroy

---@private _NextPage 下一页
function UIGuessTheIdiomLevelView:_NextPage()
    Logger.Log("下一页")
    local curNearestItemIndex = self.pageView.mLoopListView.CurSnapNearestItemIndex + 1
    self.pageView.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex)
end

---@private _NextPage 上一页
function UIGuessTheIdiomLevelView:_PreviousPage()
    Logger.Log("上一页")
    local curNearestItemIndex = self.pageView.mLoopListView.CurSnapNearestItemIndex - 1
    self.pageView.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex)
end

function UIGuessTheIdiomLevelView:OnGetItemByIndex(listView, pageIndex)
    if not self.btn_leva_gameObject then
        Logger.Log("self.btn_leva_gameObject 没有初始化完成")
        return
    end
    if (pageIndex < 0 or pageIndex >= self.pageView.mPageCount) then
        return
    end
    Logger.Log(pageIndex)
    local item = listView:NewListViewItem("ItemPrefab");
    Logger.Log(item.IsInitHandlerCalled)
    local panelCounts = panelCounts
    if (not item.IsInitHandlerCalled) then
        item.IsInitHandlerCalled = true;
        local userObjectData = {}
        for index = 1, panelCounts do
            local tag = index + panelCounts * pageIndex
            local button = GameObject.Instantiate(self.btn_leva_gameObject, item.transform)
            button.name = index
            button.transform.localScale = Vector3.New(1, 1, 1)
            local itemBtnLeave = ItemBtnLeave.New(self, button)
            itemBtnLeave:OnCreate(Bind(self, self.OnClickedItem))
            itemBtnLeave:SetLeave(tag)
            itemBtnLeave:IsLock()
            userObjectData[index] = itemBtnLeave
            
        end
        item.UserObjectData = userObjectData
    else
        for index, itemBtnLeave in ipairs(item.UserObjectData) do
            local tag = index + panelCounts * pageIndex
            --itemBtnLeave:OnCreate()
            itemBtnLeave:SetLeave(tag)
            itemBtnLeave:IsLock()
        end
    end
    return item
end

function UIGuessTheIdiomLevelView:OnClickedItem(item)
    Logger.Log(item.leave)
    self.ctrl:EnterGaming(item.leave)
end

function UIGuessTheIdiomLevelView:UpdateAllDots()
    local curNearestItemIndex = self.pageView.mLoopListView.CurSnapNearestItemIndex
    self.text_page:SetText(string.format("D%sY", curNearestItemIndex + 1))
    if curNearestItemIndex == 0 then
        self.btn_left.unity_uibutton.interactable = false
    else
        self.btn_left.unity_uibutton.interactable = true
    end

    if curNearestItemIndex == self.pageView.mPageCount - 1 then
        self.btn_right.unity_uibutton.interactable = false
    else
        self.btn_right.unity_uibutton.interactable = true
    end
end

function UIGuessTheIdiomLevelView:OnSnapNearestChanged(listView, item)
    self:UpdateAllDots(listView, item)
end

return UIGuessTheIdiomLevelView


