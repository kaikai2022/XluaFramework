using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.DemiEditor;
using XLua;

public static class GenConfig
{
    /***************如果你全lua编程，可以参考这份自动化配置***************/
    //--------------begin 纯lua编程配置参考----------------------------
    static List<string> exclude = new List<string>
    {
        "HideInInspector", "ExecuteInEditMode",
        "AddComponentMenu", "ContextMenu",
        "RequireComponent", "DisallowMultipleComponent",
        "SerializeField", "AssemblyIsEditorAssembly",
        "Attribute", "Types",
        "UnitySurrogateSelector", "TrackedReference",
        "TypeInferenceRules", "FFTWindow",
        "RPC", "Network", "MasterServer",
        "BitStream", "HostData",
        "ConnectionTesterStatus", "GUI", "EventType",
        "EventModifiers", "FontStyle", "TextAlignment",
        "TextEditor", "TextEditorDblClickSnapping",
        "TextGenerator", "TextClipping", "Gizmos",
        "ADBannerView", "ADInterstitialAd",
        "Android", "Tizen", "jvalue",
        "iPhone", "iOS", "Windows", "CalendarIdentifier",
        "CalendarUnit", "CalendarUnit",
        "ClusterInput", "FullScreenMovieControlMode",
        "FullScreenMovieScalingMode", "Handheld",
        "LocalNotification", "NotificationServices",
        "RemoteNotificationType", "RemoteNotification",
        "SamsungTV", "TextureCompressionQuality",
        "TouchScreenKeyboardType", "TouchScreenKeyboard",
        "MovieTexture", "UnityEngineInternal",
        "Terrain", "Tree", "SplatPrototype",
        "DetailPrototype", "DetailRenderMode",
        "MeshSubsetCombineUtility", "AOT", "Social", "Enumerator",
        "SendMouseEvents", "Cursor", "Flash", "ActionScript",
        "OnRequestRebuild", "Ping",
        "ShaderVariantCollection", "SimpleJson.Reflection",
        "CoroutineTween", "GraphicRebuildTracker",
        "Advertisements", "UnityEditor", "WSA",
        "EventProvider", "Apple",
        "ClusterInput", "Motion",
        "UnityEngine.UI.ReflectionMethodsCache", "NativeLeakDetection",
        "NativeLeakDetectionMode", "WWWAudioExtensions", "UnityEngine.Experimental", "UnityEngine.ClusterSerialization"
    };

    static bool isExcluded(Type type)
    {
        var fullName = type.FullName;
        for (int i = 0; i < exclude.Count; i++)
        {
            if (fullName.Contains(exclude[i]))
            {
                return true;
            }
        }

        return false;
    }

    [LuaCallCSharp]
    public static IEnumerable<Type> LuaCallCSharp
    {
        get
        {
            List<string> namespaces = new List<string>() // 在这里添加名字空间
            {
                "UnityEngine",
                "UnityEngine.UI"
            };
            var unityTypes = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                where !(assembly.ManifestModule is System.Reflection.Emit.ModuleBuilder)
                from type in assembly.GetExportedTypes()
                where type.Namespace != null && namespaces.Contains(type.Namespace) && !isExcluded(type)
                      && type.BaseType != typeof(MulticastDelegate) && !type.IsInterface && !type.IsEnum
                select type);

            string[] customAssemblys = new string[]
            {
                "Assembly-CSharp",
            };
            var customTypes = (from assembly in customAssemblys.Select(s => Assembly.Load(s))
                from type in assembly.GetExportedTypes()
                where type.Namespace == null || !type.Namespace.StartsWith("XLua")
                    && type.BaseType != typeof(MulticastDelegate) && !type.IsInterface && !type.IsEnum
                select type);
            return unityTypes.Concat(customTypes);
        }
    }

    //自动把LuaCallCSharp涉及到的delegate加到CSharpCallLua列表，后续可以直接用lua函数做callback
    [CSharpCallLua]
    public static List<Type> CSharpCallLua
    {
        get
        {
            var lua_call_csharp = LuaCallCSharp;
            var delegate_types = new List<Type>();
            var flag = BindingFlags.Public | BindingFlags.Instance
                                           | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly;
            foreach (var field in (from type in lua_call_csharp select type).SelectMany(type => type.GetFields(flag)))
            {
                if (typeof(Delegate).IsAssignableFrom(field.FieldType))
                {
                    delegate_types.Add(field.FieldType);
                }
            }

            foreach (var method in (from type in lua_call_csharp select type).SelectMany(type => type.GetMethods(flag)))
            {
                if (typeof(Delegate).IsAssignableFrom(method.ReturnType))
                {
                    delegate_types.Add(method.ReturnType);
                }

                foreach (var param in method.GetParameters())
                {
                    var paramType = param.ParameterType.IsByRef
                        ? param.ParameterType.GetElementType()
                        : param.ParameterType;
                    if (typeof(Delegate).IsAssignableFrom(paramType))
                    {
                        delegate_types.Add(paramType);
                    }
                }
            }

            return delegate_types.Where(t =>
                    t.BaseType == typeof(MulticastDelegate) && !hasGenericParameter(t) && !delegateHasEditorRef(t))
                .Distinct().ToList();
        }
    }
    //--------------end 纯lua编程配置参考----------------------------

    /***************热补丁可以参考这份自动化配置***************/
    [Hotfix]
    static IEnumerable<Type> HotfixInject
    {
        get
        {
            return (from type in Assembly.Load("Assembly-CSharp").GetTypes()
                where type.Namespace == null || !type.Namespace.StartsWith("XLua")
                select type);
        }
    }

    //--------------begin 热补丁自动化配置-------------------------
    static bool hasGenericParameter(Type type)
    {
        if (type.IsGenericTypeDefinition) return true;
        if (type.IsGenericParameter) return true;
        if (type.IsByRef || type.IsArray)
        {
            return hasGenericParameter(type.GetElementType());
        }

        if (type.IsGenericType)
        {
            foreach (var typeArg in type.GetGenericArguments())
            {
                if (hasGenericParameter(typeArg))
                {
                    return true;
                }
            }
        }

        return false;
    }

    static bool typeHasEditorRef(Type type)
    {
        if (type.Namespace != null && (type.Namespace == "UnityEditor" || type.Namespace.StartsWith("UnityEditor.")))
        {
            return true;
        }

        if (type.IsNested)
        {
            return typeHasEditorRef(type.DeclaringType);
        }

        if (type.IsByRef || type.IsArray)
        {
            return typeHasEditorRef(type.GetElementType());
        }

        if (type.IsGenericType)
        {
            foreach (var typeArg in type.GetGenericArguments())
            {
                if (typeArg.IsGenericParameter)
                {
                    //skip unsigned type parameter
                    continue;
                }

                if (typeHasEditorRef(typeArg))
                {
                    return true;
                }
            }
        }

        return false;
    }

    static bool delegateHasEditorRef(Type delegateType)
    {
        if (typeHasEditorRef(delegateType)) return true;
        var method = delegateType.GetMethod("Invoke");
        if (method == null)
        {
            return false;
        }

        if (typeHasEditorRef(method.ReturnType)) return true;
        return method.GetParameters().Any(pinfo => typeHasEditorRef(pinfo.ParameterType));
    }

    // 配置某Assembly下所有涉及到的delegate到CSharpCallLua下，Hotfix下拿不准那些delegate需要适配到lua function可以这么配置
    [CSharpCallLua]
    static IEnumerable<Type> AllDelegate
    {
        get
        {
            BindingFlags flag = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static |
                                BindingFlags.Public;
            List<Type> allTypes = new List<Type>();
            var allAssemblys = new Assembly[]
            {
                Assembly.Load("Assembly-CSharp")
            };
            foreach (var t in (from assembly in allAssemblys from type in assembly.GetTypes() select type))
            {
                var p = t;

                while (p != null)
                {
                    allTypes.Add(p);
                    p = p.BaseType;
                }
            }

            allTypes = allTypes.Distinct().ToList();
            var allMethods = from type in allTypes
                from method in type.GetMethods(flag)
                select method;
            var returnTypes = from method in allMethods
                where method.ReturnType.FullName.IsNullOrEmpty() || method.ReturnType.FullName.IndexOf("<>") == -1
                select method.ReturnType;
            var paramTypes = allMethods.SelectMany(m => m.GetParameters()).Where(pinfo =>
            {
                var type = pinfo.ParameterType.IsByRef ? pinfo.ParameterType.GetElementType() : pinfo.ParameterType;
                return type.FullName.IsNullOrEmpty() || type.FullName.IndexOf("<>") == -1;
            }).Select(pinfo =>
                {
                    Type type;
                    type = pinfo.ParameterType.IsByRef ? pinfo.ParameterType.GetElementType() : pinfo.ParameterType;
                    return type;
                }
            );
            var fieldTypes = from type in allTypes
                from field in type.GetFields(flag)
                where field.FieldType.FullName.IsNullOrEmpty() || field.FieldType.FullName.IndexOf("<>") == -1
                select field.FieldType;
            return (returnTypes.Concat(paramTypes).Concat(fieldTypes)).Where(t =>
                    t.BaseType == typeof(MulticastDelegate) && !hasGenericParameter(t) && !delegateHasEditorRef(t))
                .Distinct();
        }
    }
    //--------------end 热补丁自动化配置-------------------------

    //黑名单
    [BlackList] public static List<List<string>> BlackList = new List<List<string>>()
    {
        new System.Collections.Generic.List<string>() {"UnityEngine.WWW", "movie"},
        new System.Collections.Generic.List<string>() {"UnityEngine.UI.Text", "OnRebuildRequested"},
#if UNITY_IOS || PLATFORM_IOS
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "SetLightDirty"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "shadowRadius"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "shadowAngle"},
#endif
#if UNITY_WEBGL
        new System.Collections.Generic.List<string>() {"UnityEngine.WWW", "threadPriority"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "SetLightDirty"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "shadowRadius"},
        new System.Collections.Generic.List<string>() {"UnityEngine.Light", "shadowAngle"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstance", "System.String", "System.String"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstance", "System.ActivationContext"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstance", "System.ActivationContext", "System.String[]"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstance", "System.String", "System.String", "System.Object[]"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstance", "System.AppDomain", "System.String", "System.String"},
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
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateInstanceFrom", "System.String", "System.String"},
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
        new System.Collections.Generic.List<string>()
            {"System.Activator", "CreateComInstanceFrom", "System.String", "System.String"},
        new System.Collections.Generic.List<string>()
        {
            "System.Activator", "CreateComInstanceFrom", "System.String", "System.String", "System.Byte[]",
            "System.Configuration.Assemblies.AssemblyHashAlgorithm"
        },
        new System.Collections.Generic.List<string>()
            {"System.Activator", "GetObject", "System.Type", "System.String"},
        new System.Collections.Generic.List<string>()
            {"System.Activator", "GetObject", "System.Type", "System.String", "System.Object"},
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


        //Master_Audio_2022_AAA_Sound 
        new System.Collections.Generic.List<string>() {"DarkTonic.MasterAudio.MasterAudio", "MixerWidth"},
        new System.Collections.Generic.List<string>() {"DarkTonic.MasterAudio.MasterAudio", "BusesShownInNarrow"},
        new System.Collections.Generic.List<string>()
            {"DarkTonic.MasterAudio.MasterAudio", "RemoveUnplayedVariationDueToProbability"},
        new System.Collections.Generic.List<string>() {"DarkTonic.MasterAudio.MasterAudio", "UseDbScaleForVolume"},
        new System.Collections.Generic.List<string>() {"DarkTonic.MasterAudio.MasterAudio", "UseCentsForPitch"},
        new System.Collections.Generic.List<string>() {"DarkTonic.MasterAudio.MasterAudio", "HideLogoNav"},
        new System.Collections.Generic.List<string>()
            {"DarkTonic.MasterAudio.MasterAudio", "MasterAudioFolderPath"},
        new System.Collections.Generic.List<string>() {"DarkTonic.MasterAudio.MasterAudio", "GroupTemplateFolder"},
        new System.Collections.Generic.List<string>()
            {"DarkTonic.MasterAudio.MasterAudio", "AudioSourceTemplateFolder"},
        new System.Collections.Generic.List<string>()
            {"DarkTonic.MasterAudio.MasterAudio", "InRangeOcclusionSources"},
        new System.Collections.Generic.List<string>()
            {"DarkTonic.MasterAudio.MasterAudio", "OutOfRangeOcclusionSources"},
        new System.Collections.Generic.List<string>()
            {"DarkTonic.MasterAudio.MasterAudio", "BlockedOcclusionSources"},
    };

#if UNITY_2018_1_OR_NEWER
    [BlackList] public static Func<MemberInfo, bool> MethodFilter = (memberInfo) =>
    {
        if (memberInfo.DeclaringType.IsGenericType &&
            memberInfo.DeclaringType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
        {
            if (memberInfo.MemberType == MemberTypes.Constructor)
            {
                ConstructorInfo constructorInfo = memberInfo as ConstructorInfo;
                var parameterInfos = constructorInfo.GetParameters();
                if (parameterInfos.Length > 0)
                {
                    if (typeof(System.Collections.IEnumerable).IsAssignableFrom(parameterInfos[0].ParameterType))
                    {
                        return true;
                    }
                }
            }
            else if (memberInfo.MemberType == MemberTypes.Method)
            {
                var methodInfo = memberInfo as MethodInfo;
                if (methodInfo.Name == "TryAdd" ||
                    methodInfo.Name == "Remove" && methodInfo.GetParameters().Length == 2)
                {
                    return true;
                }
            }
        }

        return false;
    };
#endif
}