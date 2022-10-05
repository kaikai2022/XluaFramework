using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AppConfig", menuName = "AppConfig")]
public class AppConfig : ScriptableObject
{
    [BoxGroup("App启动界面配置")] [LabelText("启动界面加载等待使用的预制体路径")]
    public string launchPrefabPath = "UI/Prefabs/UILoading/UILoading.prefab";

    [BoxGroup("App启动界面配置")] [LabelText("默认通知预制体路径")]
    public string noticeTipPrefabPath = "UI/Prefabs/Common/UINoticeTip.prefab";

    [BoxGroup("App启动界面配置")] [LabelText("默认预制体显示的夫物体路径")]
    public string launchLayerPath = "UIRoot/TopLayer";
}