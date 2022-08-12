public static class GenConfig
{
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [XLua.LuaCallCSharp] public static System.Collections.Generic.List<System.Type> LuaCallCSharp =
        new System.Collections.Generic.List<System.Type>()
        {
            // unity
            typeof(System.Object),
            typeof(UnityEngine.Object),
            typeof(UnityEngine.Ray2D),
            typeof(UnityEngine.GameObject),
            typeof(UnityEngine.Component),
            typeof(UnityEngine.Behaviour),
            typeof(UnityEngine.Transform),
            typeof(UnityEngine.Resources),
            typeof(UnityEngine.TextAsset),
            typeof(UnityEngine.Keyframe),
            typeof(UnityEngine.AnimationCurve),
            typeof(UnityEngine.AnimationClip),
            typeof(UnityEngine.MonoBehaviour),
            typeof(UnityEngine.ParticleSystem),
            typeof(UnityEngine.SkinnedMeshRenderer),
            typeof(UnityEngine.Renderer),
            typeof(UnityEngine.WWW),
            typeof(UnityEngine.Networking.UnityWebRequest),
            typeof(System.Collections.Generic.List<int>),
            typeof(System.Action<string>),
            typeof(UnityEngine.Debug),
            typeof(System.Delegate),
            typeof(System.Collections.Generic.Dictionary<string, UnityEngine.GameObject>),
            typeof(UnityEngine.Events.UnityEvent),

#if PLATFORM_ANDROID || UNITY_ANDROID
            typeof(UnityEngine.Android.Permission),
#endif

            // unity结合lua，这部分导出很多功能在lua侧重新实现，没有实现的功能才会跑到cs侧
            typeof(UnityEngine.Bounds),
            typeof(UnityEngine.Color),
            typeof(UnityEngine.LayerMask),
            typeof(UnityEngine.Mathf),
            typeof(UnityEngine.Plane),
            typeof(UnityEngine.Quaternion),
            typeof(UnityEngine.Ray),
            typeof(UnityEngine.RaycastHit),
            typeof(UnityEngine.Time),
            typeof(UnityEngine.Touch),
            typeof(UnityEngine.TouchPhase),
            typeof(UnityEngine.Vector2),
            typeof(UnityEngine.Vector3),
            typeof(UnityEngine.Vector4),

            // 渲染
            typeof(UnityEngine.RenderMode),

            // UGUI  
            typeof(UnityEngine.Canvas),
            typeof(UnityEngine.Rect),
            typeof(UnityEngine.RectTransform),
            typeof(UnityEngine.RectOffset),
            typeof(UnityEngine.Sprite),
            typeof(UnityEngine.UI.CanvasScaler),
            typeof(UnityEngine.UI.CanvasScaler.ScaleMode),
            typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode),
            typeof(UnityEngine.UI.GraphicRaycaster),
            typeof(UnityEngine.UI.Text),
            typeof(UnityEngine.UI.InputField),
            typeof(UnityEngine.UI.Button),
            typeof(UnityEngine.UI.Image),
            typeof(UnityEngine.UI.ScrollRect),
            typeof(UnityEngine.UI.Scrollbar),
            typeof(UnityEngine.UI.Toggle),
            typeof(UnityEngine.UI.ToggleGroup),
            typeof(UnityEngine.UI.Button.ButtonClickedEvent),
            typeof(UnityEngine.UI.ScrollRect.ScrollRectEvent),
            typeof(UnityEngine.UI.GridLayoutGroup),
            typeof(UnityEngine.UI.ContentSizeFitter),
            typeof(UnityEngine.UI.Slider),

            typeof(UIPointerClick),
            typeof(UIPointerDoubleClick),
            typeof(UIPointerDownUp),
            typeof(UIPointerLongPress),

            typeof(UnityEngine.U2D.SpriteAtlas),

            //TMPro TextMeshPro
            typeof(TMPro.TextMeshProUGUI),
            typeof(TMPro.TextMeshPro),
            
            typeof(ComponentExtensions),

            // easy touch
            // TODO：后续需要什么脚本再添加进来
            typeof(ETCArea),
            typeof(ETCAxis),
            typeof(ETCButton),
            typeof(ETCInput),
            typeof(ETCJoystick),

            // 场景、资源加载
            typeof(UnityEngine.Resources),
            typeof(UnityEngine.ResourceRequest),
            typeof(UnityEngine.SceneManagement.SceneManager),
            typeof(UnityEngine.AsyncOperation),

            // 其它
            typeof(UnityEngine.PlayerPrefs),
            typeof(System.GC),

            //Do Tween
            typeof(DG.Tweening.AutoPlay),
            typeof(DG.Tweening.AxisConstraint),
            typeof(DG.Tweening.Ease),
            typeof(DG.Tweening.LogBehaviour),
            typeof(DG.Tweening.LoopType),
            typeof(DG.Tweening.PathMode),
            typeof(DG.Tweening.PathType),
            typeof(DG.Tweening.RotateMode),
            typeof(DG.Tweening.ScrambleMode),
            typeof(DG.Tweening.TweenType),
            typeof(DG.Tweening.UpdateType),

            typeof(DG.Tweening.DOTween),
            typeof(DG.Tweening.DOVirtual),
            typeof(DG.Tweening.EaseFactory),
            typeof(DG.Tweening.Tweener),
            typeof(DG.Tweening.Tween),
            typeof(DG.Tweening.Sequence),
            typeof(DG.Tweening.TweenParams),
            typeof(DG.Tweening.Core.ABSSequentiable),

            typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3,
                DG.Tweening.Plugins.Options.VectorOptions>),

            typeof(DG.Tweening.TweenCallback),
            typeof(DG.Tweening.TweenExtensions),
            typeof(DG.Tweening.TweenSettingsExtensions),
            typeof(DG.Tweening.ShortcutExtensions),
            // typeof(DG.Tweening.ShortcutExtensions43),
            // typeof(DG.Tweening.ShortcutExtensions46),
            // typeof(DG.Tweening.ShortcutExtensions50),

            //dotween pro 的功能
            typeof(DG.Tweening.DOTweenPath),
            typeof(DG.Tweening.DOTweenVisualManager),

            // SuperScrollView 
            typeof(SuperScrollView.ClickEventListener),
            typeof(SuperScrollView.SnapStatus),
            typeof(SuperScrollView.ItemCornerEnum),
            typeof(SuperScrollView.ListItemArrangeType),
            typeof(SuperScrollView.GridItemArrangeType),
            typeof(SuperScrollView.GridFixedType),
            typeof(SuperScrollView.RowColumnPair),
            typeof(SuperScrollView.ItemSizeGroup),
            typeof(SuperScrollView.ItemPosMgr),
            typeof(SuperScrollView.GridItemGroup),
            typeof(SuperScrollView.GridItemPool),
            typeof(SuperScrollView.GridViewItemPrefabConfData),
            typeof(SuperScrollView.LoopGridViewInitParam),
            typeof(SuperScrollView.LoopGridViewSettingParam),
            typeof(SuperScrollView.LoopGridView),
            typeof(SuperScrollView.LoopGridViewItem),
            typeof(SuperScrollView.ItemPool),
            typeof(SuperScrollView.ItemPrefabConfData),
            typeof(SuperScrollView.LoopListViewInitParam),
            typeof(SuperScrollView.LoopListView2),
            typeof(SuperScrollView.LoopListViewItem2),
            typeof(SuperScrollView.StaggeredGridItemPrefabConfData),
            typeof(SuperScrollView.StaggeredGridViewInitParam),
            typeof(SuperScrollView.ItemIndexData),
            typeof(SuperScrollView.GridViewLayoutParam),
            typeof(SuperScrollView.LoopStaggeredGridView),
            typeof(SuperScrollView.LoopStaggeredGridViewItem),
            typeof(SuperScrollView.StaggeredGridItemGroup),
            typeof(SuperScrollView.StaggeredGridItemPool),
        };

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [XLua.CSharpCallLua] public static System.Collections.Generic.List<System.Type> CSharpCallLua =
        new System.Collections.Generic.List<System.Type>()
        {
            // unity
            typeof(System.Action),
            typeof(System.Action<int>),
            typeof(System.Action<UnityEngine.WWW>),
            typeof(Callback),
            typeof(UnityEngine.Event),
            typeof(UnityEngine.Events.UnityAction),
            typeof(System.Collections.IEnumerator),
            typeof(UnityEngine.Events.UnityAction<UnityEngine.Vector2>),

            typeof(UI.GridPageView.UpdateGridItem),
            
            // SuperScrollView 
            typeof(System.Func<SuperScrollView.LoopListView2, int, SuperScrollView.LoopListViewItem2>),
            typeof(System.Action<SuperScrollView.LoopListView2, SuperScrollView.LoopListViewItem2>),
            typeof(System.Action<SuperScrollView.LoopListView2, SuperScrollView.LoopListViewItem2>),
            typeof(System.Action<SuperScrollView.LoopListViewItem2, object>),
            typeof(System.Action<UnityEngine.GameObject>),
            typeof(System.Action<UnityEngine.EventSystems.PointerEventData>),
            typeof(System.Action<SuperScrollView.LoopGridView, SuperScrollView.LoopGridViewItem>),
            typeof(System.Action<SuperScrollView.LoopGridView>),
            typeof(System.Func<SuperScrollView.LoopGridView, int, int, int, SuperScrollView.LoopGridViewItem>),
            typeof(System.Func<SuperScrollView.LoopStaggeredGridView, int, SuperScrollView.LoopStaggeredGridViewItem>),
            typeof(System.Func<int, int, SuperScrollView.LoopStaggeredGridViewItem>),
        };

    //黑名单
    [XLua.BlackList] public static System.Collections.Generic.List<System.Collections.Generic.List<string>> BlackList =
        new System.Collections.Generic.List<System.Collections.Generic.List<string>>()
        {
            new System.Collections.Generic.List<string>() {"System.Xml.XmlNodeList", "ItemOf"},
            new System.Collections.Generic.List<string>() {"UnityEngine.WWW", "movie"},
            new System.Collections.Generic.List<string>() {"UnityEngine.UI.Text", "OnRebuildRequested"},
#if UNITY_WEBGL
        new System.Collections.Generic.List<string>() {"UnityEngine.WWW", "threadPriority"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "SetLightDirty"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "shadowRadius"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "shadowAngle"},
        new System.Collections.Generic.List<string>() {"System.Activator", "CreateInstance", "System.String", "System.String"},
        new System.Collections.Generic.List<string>() {"System.Activator", "CreateInstance", "System.ActivationContext"},
        new System.Collections.Generic.List<string>() {"System.Activator", "CreateInstance", "System.ActivationContext", "System.String[]"},
        new System.Collections.Generic.List<string>() {"System.Activator", "CreateInstance", "System.String", "System.String", "System.Object[]"},
        new System.Collections.Generic.List<string>() {"System.Activator", "CreateInstance", "System.AppDomain", "System.String", "System.String"},
        new System.Collections.Generic.List<string>()
        {
            "System.Activator", "CreateInstance", "System.AppDomain", "System.String", "System.String",
            "System.Boolean",
            "System.Reflection.BindingFlags", "System.Reflection.Binder", "System.Object[]",
            "System.Globalization.CultureInfo", "System.Object[]"
        },
        new System.Collections.Generic.List<string>()
        {
            "System.Activator", "CreateInstance", "System.String", "System.String", "System.Boolean",
            "System.Reflection.BindingFlags", "System.Reflection.Binder", "System.Object[]",
            "System.Globalization.CultureInfo", "System.Object[]"
        },
        new System.Collections.Generic.List<string>() {"System.Activator", "CreateInstanceFrom", "System.String", "System.String"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstanceFrom", "System.String", "System.String", "System.Object[]"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstanceFrom", "System.AppDomain", "System.String", "System.String"},
        new System.Collections.Generic.List<string>()
        {
            "System.Activator", "CreateInstanceFrom", "System.AppDomain", "System.String", "System.String",
            "System.Boolean",
            "System.Reflection.BindingFlags", "System.Reflection.Binder", "System.Object[]",
            "System.Globalization.CultureInfo", "System.Object[]"
        },
        new System.Collections.Generic.List<string>()
        {
            "System.Activator", "CreateInstanceFrom", "System.String", "System.String", "System.Boolean",
            "System.Reflection.BindingFlags", "System.Reflection.Binder", "System.Object[]",
            "System.Globalization.CultureInfo", "System.Object[]"
        },
        new System.Collections.Generic.List<string>() {"System.Activator", "CreateComInstanceFrom", "System.String", "System.String"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateComInstanceFrom", "System.String", "System.String", "System.Byte[]","System.Configuration.Assemblies.AssemblyHashAlgorithm"},
        new System.Collections.Generic.List<string>() {"System.Activator", "GetObject", "System.Type", "System.String"},
        new System.Collections.Generic.List<string>() {"System.Activator", "GetObject", "System.Type", "System.String", "System.Object"},


#endif
            new System.Collections.Generic.List<string>() {"UnityEngine.Texture2D", "alphaIsTransparency"},
            new System.Collections.Generic.List<string>() {"UnityEngine.Security", "GetChainOfTrustValue"},
            new System.Collections.Generic.List<string>() {"UnityEngine.CanvasRenderer", "onRequestRebuild"},
            new System.Collections.Generic.List<string>() {"UnityEngine.Light", "areaSize"},
            new System.Collections.Generic.List<string>() {"UnityEngine.Light", "lightmapBakeType"},
            new System.Collections.Generic.List<string>() {"UnityEngine.WWW", "MovieTexture"},
            new System.Collections.Generic.List<string>() {"UnityEngine.WWW", "GetMovieTexture"},
            new System.Collections.Generic.List<string>()
                {"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
#if !UNITY_WEBPLAYER
            new System.Collections.Generic.List<string>() {"UnityEngine.Application", "ExternalEval"},
#endif
            new System.Collections.Generic.List<string>() {"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
            new System.Collections.Generic.List<string>() {"UnityEngine.Component", "networkView"}, //4.6.2 not support
            new System.Collections.Generic.List<string>()
                {"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
            new System.Collections.Generic.List<string>()
                {"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
            new System.Collections.Generic.List<string>()
                {"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
            new System.Collections.Generic.List<string>()
                {"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
            new System.Collections.Generic.List<string>()
            {
                "System.IO.DirectoryInfo", "CreateSubdirectory", "System.String",
                "System.Security.AccessControl.DirectorySecurity"
            },
            new System.Collections.Generic.List<string>()
                {"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
            new System.Collections.Generic.List<string>() {"UnityEngine.MonoBehaviour", "runInEditMode"},
            new System.Collections.Generic.List<string>()
                {"System.Type", "MakeGenericSignatureType", "System.Type", "System.Type[]"},
            new System.Collections.Generic.List<string>() {"System.Type", "IsCollectible"},
        };

// #if UNITY_2018_1_OR_NEWER
//     [BlackList]
//     public static Func<MemberInfo, bool> MethodFilter = (memberInfo) =>
//     {
//         if (memberInfo.DeclaringType.IsGenericType && memberInfo.DeclaringType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
//         {
//             if (memberInfo.MemberType == MemberTypes.Constructor)
//             {
//                 ConstructorInfo constructorInfo = memberInfo as ConstructorInfo;
//                 var parameterInfos = constructorInfo.GetParameters();
//                 if (parameterInfos.Length > 0)
//                 {
//                     if (typeof(System.Collections.IEnumerable).IsAssignableFrom(parameterInfos[0].ParameterType))
//                     {
//                         return true;
//                     }
//                 }
//             }
//             else if (memberInfo.MemberType == MemberTypes.Method)
//             {
//                 var methodInfo = memberInfo as MethodInfo;
//                 if (methodInfo.Name == "TryAdd" || methodInfo.Name == "Remove" && methodInfo.GetParameters().Length == 2)
//                 {
//                     return true;
//                 }
//             }
//         }
//         return false;
//     };
//     
// #endif
}