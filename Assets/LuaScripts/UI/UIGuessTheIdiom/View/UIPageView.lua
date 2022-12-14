---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 12/8/2022 10:12 上午
---

---@class UIPageView UGUI翻页容器
local UIPageView = BaseClass("UIPageView", UIBaseContainer)
local base = UIBaseContainer

function UIPageView:OnCreate(count, onGetItemByIndex, onSnapNearestChanged)
    base.OnCreate(self)
    assert(count, "count is nil")
    assert(onGetItemByIndex, "onGetItemByIndex is nil")
    self.mPageCount = count
    self._updateAllDots = onSnapNearestChanged
    self.mLoopListView = self.transform:GetComponent("LoopListView2")
    assert(not IsNull(self.mLoopListView))
    if not self.mLoopListView.ItemSnapEnable then
        self.mLoopListView.ItemSnapEnable = true
    end
    if not self.mLoopListView.SupportScrollBar then
        self.mLoopListView.SupportScrollBar = true
    end
    self.mLoopListView.mOnEndDragAction = Bind(self, self._OnEndDrag)
    self.mLoopListView.mOnSnapNearestChanged = Bind(self, self._OnSnapNearestChanged)

    local param = CS.SuperScrollView.LoopListViewInitParam.CopyDefaultInitParam()
    self.mLoopListView:InitListView(self.mPageCount, onGetItemByIndex, param)
end

function UIPageView:_OnEndDrag()
    local vec = self.mLoopListView.ScrollRect.velocity.x
    local curNearestItemIndex = self.mLoopListView.CurSnapNearestItemIndex
    local item = self.mLoopListView:GetShownItemByItemIndex(curNearestItemIndex)
    if (IsNull(item)) then
        self.mLoopListView:ClearSnapData()
        return ;
    end

    if (math.abs(vec) < 50) then

        self.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex)
        return
    end

    local pos = self.mLoopListView:GetItemCornerPosInViewPort(item, CS.SuperScrollView.ItemCornerEnum.LeftTop)

    if (pos.x > 0) then
        if (vec > 0) then

            self.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex - 1);
        else

            self.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex);
        end
    elseif (pos.x < 0) then
        if (vec > 0) then
            self.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex);
        else
            self.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex + 1);
        end
    else
        if (vec > 0) then
            self.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex - 1);
        else
            self.mLoopListView:SetSnapTargetItemIndex(curNearestItemIndex + 1);
        end
    end
end

function UIPageView:_OnSnapNearestChanged(listView, item)
    if self._updateAllDots then
        self._updateAllDots(listView, item)
    end
end

return UIPageView