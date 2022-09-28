--[[
-- added by author @ 2/9/2022 10:53:11 AM
-- UIGamingLayoutPanelUIMain模型层
-- 注意：
-- 1、成员变量预先在OnCreate、OnEnable函数声明，提高代码可读性
-- 2、OnCreate内放窗口生命周期内保持的成员变量，窗口销毁时才会清理
-- 3、OnEnable内放窗口打开时才需要的成员变量，窗口关闭后及时清理
-- 4、OnEnable函数每次在窗口打开时调用，可传递参数用来初始化Model
--]]
local UIGamingLayoutPanelModel = BaseClass("UIGamingLayoutPanelModel", UIBaseModel)

local base = UIBaseModel-- 创建
local function OnCreate(self)
    base.OnCreate(self)
    -- 窗口生命周期内保持的成员变量放这
    self.player = {
        PlayerDataClass.New(1),
        PlayerDataClass.New(2),
    }
end

-- 打开
local function OnEnable(self, playerCount)
    base.OnEnable(self)
    -- 窗口关闭时可以清理的成员变量放这
    for _, playerData in pairs(self.player) do
        playerData:GameStartInitData()
    end
end
-- 关闭
local function OnDestroy(self)
    base.OnDestroy(self)
    -- 清理成员变量
end

UIGamingLayoutPanelModel.OnCreate = OnCreate
UIGamingLayoutPanelModel.OnEnable = OnEnable
UIGamingLayoutPanelModel.OnDestroy = OnDestroy

-- 监听数据
function UIGamingLayoutPanelModel:OnAddListener()
    base.OnAddListener(self)
    self:AddDataListener(DataMessageNames.ON_GAMING_HURT, self.OnGamingHurt)
end

function UIGamingLayoutPanelModel:OnRemoveListener()
    base.OnRemoveListener(self)
    self:RemoveDataListener(Idiom_DataMessageNames.ON_GAMING_HURT, self.OnGamingHurt)
end

---@private OnGamingHurt 受到伤害
function UIGamingLayoutPanelModel:OnGamingHurt(playerData)

end

return UIGamingLayoutPanelModel
