--[[
-- added by author @ 2/9/2022 10:53:11 AM
-- UIGamingLayoutPanel视图层
-- 注意：
-- 1、成员变量最好预先在__init函数声明，提高代码可读性
-- 2、OnEnable函数每次在窗口打开时调用，直接刷新
-- 3、组件命名参考代码规范
--]]
local UIGamingLayoutPanelView = BaseClass("UIGamingLayoutPanelView", UIBaseView)
local base = UIBaseView

local blueBloodPath = "BoolBarPanel/Image_blue_bar"
local redBloodPath = "BoolBarPanel/Image_red_bar"

local playerBlueControllerLeftPath = "BlueController/Left_Btn"
local playerBlueControllerRightPath = "BlueController/Right_Btn"
local playerRedControllerLeftPath = "RedController/Left_Btn"
local playerRedControllerRightPath = "RedController/Right_Btn"

local bluePlayerPath = "Player_Blue"
local redPlayerPath = "Player_Rad"
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    self.blueBlood = self:AddComponent(UIBloodView, blueBloodPath)
    self.redBlood = self:AddComponent(UIBloodView, redBloodPath)

    self.bluePlayerRigidbody2D = self.transform:Find(bluePlayerPath):GetComponent("Rigidbody2D")
    self.redPlayerRigidbody2D = self.transform:Find(redPlayerPath):GetComponent("Rigidbody2D")
    self:InitPlayersController()

end

-- 打开
local function OnEnable(self)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这
    Logger.Log(string.format("玩家1的id:%s", self.model.player[1].playerId))
    self:SetMap()
end

-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

UIGamingLayoutPanelView.OnCreate = OnCreate
UIGamingLayoutPanelView.OnEnable = OnEnable
UIGamingLayoutPanelView.OnDestroy = OnDestroy

function UIGamingLayoutPanelView:OnAddListener()
    base.OnAddListener(self)
    self:AddUIListener(UIMessageNames.UI_UPDATE_PLAYER_HURT, self.UpdateHurt)
end

function UIGamingLayoutPanelView:OnRemoveListener()
    base.OnRemoveListener(self)
    self:RemoveUIListener(UIMessageNames.UI_UPDATE_PLAYER_HURT, self.UpdateHurt)
end

---@private UpdateHurt 伤害更新通知
function UIGamingLayoutPanelView:UpdateHurt()
    self.blueBlood:SetBloodValue(self.model.player[1].nowBloodVolume / self.model.player[1].maxBloodVolume)
    self.redBlood:SetBloodValue(self.model.player[2].nowBloodVolume / self.model.player[2].maxBloodVolume)
end

function UIGamingLayoutPanelView:SetMap()

    --主屏幕中的地图边界
    local size = self.transform.parent:GetComponent("RectTransform").sizeDelta;
    local collider2d = self.rectTransform:GetComponents(typeof(CS.UnityEngine.BoxCollider2D))
    collider2d[0].offset = Vector2.New(1, 1)
    collider2d[0].size = Vector2.New(1, size.y)

    collider2d[1].offset = Vector2.New(0, -(size.y / 2) + 40)
    collider2d[1].size = Vector2.New(size.x, 1)

    collider2d[2].offset = Vector2.New(-size.x / 2, 0)
    collider2d[2].size = Vector2.New(1, size.y)

    collider2d[3].offset = Vector2.New(size.x / 2, 0)
    collider2d[3].size = Vector2.New(1, size.y)

    --添加地图
    
end

function UIGamingLayoutPanelView:InitPlayersController()
    self.playerBlueControllerLeftEtcBtn = self.transform:Find(playerBlueControllerLeftPath):GetComponent("ETCButton")
    self.playerBlueControllerRightEtcBtn = self.transform:Find(playerBlueControllerRightPath):GetComponent("ETCButton")
    self.playerRedControllerLeftEtcBtn = self.transform:Find(playerRedControllerLeftPath):GetComponent("ETCButton")
    self.playerRedControllerRightEtcBtn = self.transform:Find(playerRedControllerRightPath):GetComponent("ETCButton")
    local sceneSize = self.transform.parent:GetComponent("RectTransform").sizeDelta;
    local playerSize = self.bluePlayerRigidbody2D:GetComponent("RectTransform").sizeDelta

    local upVector2 = Vector2.New(0, 1)
    local zeroVector2 = Vector2.New(0, 0)

    --local playerBlueControllerUpEtcBtnListener = CS.ETCButton.OnUPHandler()
    --playerBlueControllerUpEtcBtnListener:AddListener(function()
    --    self.bluePlayerRigidbody2D.velocity = zeroVector2
    --    self.redPlayerRigidbody2D.velocity = zeroVector2
    --end)
    --self.playerBlueControllerLeftEtcBtn.onUp = playerBlueControllerUpEtcBtnListener
    --self.playerBlueControllerRightEtcBtn.onUp = playerBlueControllerUpEtcBtnListener
    --self.playerRedControllerLeftEtcBtn.onUp = playerBlueControllerUpEtcBtnListener
    --self.playerRedControllerRightEtcBtn.onUp = playerBlueControllerUpEtcBtnListener
    --blue left按钮
    local oldBluePlayerPosX;

    local playerBlueControllerLeftEtcBtnListener = CS.ETCButton.OnPressedHandler()
    self.playerBlueControllerLeftEtcBtn.onPressed = playerBlueControllerLeftEtcBtnListener

    playerBlueControllerLeftEtcBtnListener:AddListener(function()
        local posX = self.bluePlayerRigidbody2D.transform:rectTransform().anchoredPosition.x
        posX = math.floor(posX)
        --self.bluePlayerRigidbody2D.velocity = leftVector2
        if oldBluePlayerPosX and posX == oldBluePlayerPosX and posX > -((sceneSize.x * 0.5) - playerSize.x * 0.5) then
            self.bluePlayerRigidbody2D.velocity = upVector2
        else
            oldBluePlayerPosX = posX
        end
    end)

    --blue right按钮
    local playerBlueControllerRightEtcBtnListener = CS.ETCButton.OnPressedHandler()
    self.playerBlueControllerRightEtcBtn.onPressed = playerBlueControllerRightEtcBtnListener
    playerBlueControllerRightEtcBtnListener:AddListener(function()
        local posX = self.bluePlayerRigidbody2D.transform:rectTransform().anchoredPosition.x
        posX = math.floor(posX)
        --self.bluePlayerRigidbody2D.velocity = rightVector2
        if oldBluePlayerPosX and posX == oldBluePlayerPosX and posX < -playerSize.x * 0.5 then
            self.bluePlayerRigidbody2D.velocity = upVector2
        else
            oldBluePlayerPosX = posX
        end
    end)

    --red left按钮
    local oldRedPlayerPosX;

    local playerRedControllerLeftEtcBtnListener = CS.ETCButton.OnPressedHandler()
    self.playerRedControllerLeftEtcBtn.onPressed = playerRedControllerLeftEtcBtnListener

    playerRedControllerLeftEtcBtnListener:AddListener(function()
        local posX = self.redPlayerRigidbody2D.transform:rectTransform().anchoredPosition.x
        posX = math.floor(posX)
        --self.redPlayerRigidbody2D.velocity = leftVector2
        if oldRedPlayerPosX and posX == oldRedPlayerPosX and posX > playerSize.x * 0.5 then
            self.redPlayerRigidbody2D.velocity = upVector2
        else
            oldRedPlayerPosX = posX
        end
    end)

    --blue right按钮
    local playerRedControllerRightEtcBtnListener = CS.ETCButton.OnPressedHandler()
    self.playerRedControllerRightEtcBtn.onPressed = playerRedControllerRightEtcBtnListener
    playerRedControllerRightEtcBtnListener:AddListener(function()
        local posX = self.redPlayerRigidbody2D.transform:rectTransform().anchoredPosition.x
        posX = math.floor(posX)

        --self.redPlayerRigidbody2D.velocity = rightVector2
        if oldRedPlayerPosX and posX == oldRedPlayerPosX and posX < ((sceneSize.x * 0.5) - playerSize.x * 0.5) then
            self.redPlayerRigidbody2D.velocity = upVector2
        else
            oldRedPlayerPosX = posX
        end
    end)


end

return UIGamingLayoutPanelView


