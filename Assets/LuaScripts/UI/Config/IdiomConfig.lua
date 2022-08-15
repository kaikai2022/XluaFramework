---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lucus.
--- DateTime: 14/7/2022 4:06 PM
---

local IdiomConfig = {
    "鱼贯而入",
    "四通八达",
    "网开一面",
    "头重脚轻",
    "命悬一线",
    "鱼目混珠",
    "水滴石穿",
    "穷困潦倒",
    "胆大包天",
    "破口大骂",
    "天外有天",
    "表里如一",
    "前赴后继",
    "横冲直撞",
    "杀鸡取卵",
    "狗急跳墙",
    "貌合神离",
    "东张西望",
    "对牛弹琴",
    "普天同庆",
    "半夜三更",
    "妙手回春",
    "百川归海",
    "掩人耳目",
    "指鹿为马",
    "弹尽粮绝",
    "沧海一粟",
    "若隐若现",
    "远走高飞",
    "只手遮天",
    "一目十行",
    "胆小如鼠",
    "心直口快",
    "坐吃山空",
    "月下老人",
    "哭笑不得",
    "苦中作乐",
    "风花雪月",
    "人面兽心",
    "偷天换日",
    "嫦娥奔月",
    "狗急跳墙",
    "三人成虎",
    "青黄不接",
    "德高望重",
    "七手八脚",
    "见钱眼开",
    "四大皆空",
    "如坐针毡",
    "百里挑一",
    "绝处逢生",
    "开门见山",
    "八面来风",
    "话中有话",
    "大跌眼镜",
    "鸡犬升天",
    "日上三竿",
    "大显身手",
    "逆水行舟",
    "火烧眉毛",
    "一心一意",
    "唉声叹气",
    "满腹经纶",
    "妙笔生花",
    "善男信女",
    "三心二意",
    "班门弄斧",
    "名震一时",
    "思前想后",
    "画蛇添足",
    "萍水相分",
    "不可一世",
    "平步青云",
    "怒火中烧",
    "落叶知秋",
    "颠三倒四",
    "目中无人",
    "锦上添花",
    "走马观花",
    "七零八落",
    "甘拜下风",
    "拨乱反正",
    "画地为牢",
    "一举两得",
    "弃笔从戎",
    "缺衣少食",
    "面红耳赤",
    "七上八下",
    "门可罗雀",
    "参差不齐",
    "渐入佳境",
    "马到成功",
    "八仙过海",
    "行尸走肉",
    "披星戴月",
    "无与伦比",
    "两情相悦",
    "拈花惹草",
    "乘风破浪",
    "立锥之地",
}
---@public Count 当前的成语总数
IdiomConfig.Count = #IdiomConfig

local PlayerPrefsLeveKey = "PlayerPrefsIdiomLeveKey"

---@public getNowLeve 获取当前本地化的等级
IdiomConfig.getNowLeve = function()
    return CS.UnityEngine.PlayerPrefs.GetInt(PlayerPrefsLeveKey, 1)
end

---@public setNowLeve 设置本地化存储等级
IdiomConfig.setNowLeve = function(value)
    assert(value > 1 and value <= IdiomConfig.Count, "设置等级时出错")
    CS.UnityEngine.PlayerPrefs.SetInt(PlayerPrefsLeveKey, value)
end

---@class IdiomConfig 猜谜语的一般配置
return ConstClass("IdiomConfig", IdiomConfig)