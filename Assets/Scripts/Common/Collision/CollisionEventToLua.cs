using UnityEngine;
using UnityEngine.Events;
using XLua;

[Hotfix]
[LuaCallCSharp]
public class CollisionEventToLua : MonoBehaviour
{
    public static CollisionEventToLua AddEventToGameObject(Transform transform)
    {
        if (transform.GetComponent<Collider>() == null && transform.GetComponent<Collider2D>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Collider 或者 Collider2D 组件", transform.gameObject.name);
            return null;
        }

        return transform.gameObject.AddComponent<CollisionEventToLua>();
    }

    public static CollisionEventToLua AddEventToGameObject(GameObject gameObject)
    {
        if (gameObject.GetComponent<Collider>() == null && gameObject.GetComponent<Collider2D>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Collider 或者 Collider2D 组件", gameObject.name);
            return null;
        }

        return gameObject.AddComponent<CollisionEventToLua>();
    }

    public static CollisionEventToLua AddEventToGameObject(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour.GetComponent<Collider>() == null && monoBehaviour.GetComponent<Collider2D>() == null)
        {
            Logger.LogError("{0} 没有找到对应的 Collider 或者 Collider2D 组件", monoBehaviour.gameObject.name);
            return null;
        }

        return monoBehaviour.gameObject.AddComponent<CollisionEventToLua>();
    }

    public UnityEvent<Collision> OnCollisionEnterUnityEvent { get; private set; }
    public UnityEvent<Collision> OnCollisionStayUnityEvent { get; private set; }
    public UnityEvent<Collision> OnCollisionExitUnityEvent { get; private set; }
    public UnityEvent<Collider> OnTriggerEnterUnityEvent { get; private set; }
    public UnityEvent<Collider> OnTriggerStayUnityEvent { get; private set; }
    public UnityEvent<Collider> OnTriggerExitUnityEvent { get; private set; }

    public UnityEvent<Collision2D> OnCollisionEnterUnityEvent2D { get; private set; }
    public UnityEvent<Collision2D> OnCollisionStayUnityEvent2D { get; private set; }
    public UnityEvent<Collision2D> OnCollisionExitUnityEvent2D { get; private set; }
    public UnityEvent<Collider2D> OnTriggerEnterUnityEvent2D { get; private set; }
    public UnityEvent<Collider2D> OnTriggerStayUnityEvent2D { get; private set; }
    public UnityEvent<Collider2D> OnTriggerExitUnityEvent2D { get; private set; }

    private void Awake()
    {
        OnCollisionEnterUnityEvent = new UnityEvent<Collision>();
        OnCollisionStayUnityEvent = new UnityEvent<Collision>();
        OnCollisionExitUnityEvent = new UnityEvent<Collision>();
        OnTriggerEnterUnityEvent = new UnityEvent<Collider>();
        OnTriggerStayUnityEvent = new UnityEvent<Collider>();
        OnTriggerExitUnityEvent = new UnityEvent<Collider>();

        OnCollisionEnterUnityEvent2D = new UnityEvent<Collision2D>();
        OnCollisionStayUnityEvent2D = new UnityEvent<Collision2D>();
        OnCollisionExitUnityEvent2D = new UnityEvent<Collision2D>();
        OnTriggerEnterUnityEvent2D = new UnityEvent<Collider2D>();
        OnTriggerStayUnityEvent2D = new UnityEvent<Collider2D>();
        OnTriggerExitUnityEvent2D = new UnityEvent<Collider2D>();
    }


    void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterUnityEvent.Invoke(collision);
    } //当碰撞体进入当前物体时触发的回调函数

    void OnCollisionStay(Collision collision)
    {
        OnCollisionStayUnityEvent.Invoke(collision);
    } //当碰撞体停留当前物体内时触发的回调函数

    void OnCollisionExit(Collision collision)
    {
        OnCollisionExitUnityEvent.Invoke(collision);
    } //当碰撞体离开当前物体时触发的回调函数物

    void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterUnityEvent.Invoke(other);
    } //当触发器进入当前物体时触发的回调函数

    void OnTriggerStay(Collider other)
    {
        OnTriggerStayUnityEvent.Invoke(other);
    } //当触发器停留在当前物体时触发的回调函数

    void OnTriggerExit(Collider other)
    {
        OnTriggerExitUnityEvent.Invoke(other);
    } //当触发器离开当前物体时触发的回调函数


    void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnterUnityEvent2D.Invoke(collision);
    } //当2D碰撞体进入当前物体时触发的回调函数

    void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStayUnityEvent2D.Invoke(collision);
    } //当2D碰撞体停留当前物体内时触发的回调函数

    void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExitUnityEvent2D.Invoke(collision);
    } //当2D碰撞体离开当前物体时触发的回调函数物

    void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnterUnityEvent2D.Invoke(other);
    } //当2D触发器进入当前物体时触发的回调函数

    void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStayUnityEvent2D.Invoke(other);
    } //当2D触发器停留在当前物体时触发的回调函数

    void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExitUnityEvent2D.Invoke(other);
    } //当2D触发器离开当前物体时触发的回调函数
}