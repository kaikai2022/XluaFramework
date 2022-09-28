using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class AnimationTest : MonoBehaviour
{
    private AnimatorEventGOLua _animatorEventGOLua;
    private Animator _animator;

    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        if (_animatorEventGOLua == null)
        {
            _animatorEventGOLua = gameObject.AddComponent<AnimatorEventGOLua>();
        }

        // _animatorEventGOLua.AddEventEnd("left_runing", MoveRuning,);
        // _animatorEventGOLua.AddEvent("left_runing", MoveRuning, 0.1f);
        _animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_animator.GetFloat("move_speed") != 0)
        {
            var pos = transform.rectTransform().anchoredPosition;
            pos.x += _animator.GetFloat("move_speed");
            transform.rectTransform().anchoredPosition = pos;
        }
    }

    private void OnGUI()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float speed = Input.GetKey(KeyCode.A) ? -1f : 1f;
            _animator.SetFloat("move_speed", speed);
        }
        else
        {
            _animator.SetFloat("move_speed", 0f);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            _animator.SetLayerWeight(1, 1f);
            float direction = Input.GetKey(KeyCode.W) ? 1f : -1f;
            _animator.SetFloat("direction", direction);
        }
        else // if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            _animator.SetFloat("direction", 0);
            _animator.SetLayerWeight(1, 0);

        }
    }

    void MoveRuning()
    {
    }


    //Enter函式是兩個物件碰撞瞬間，執行一次函式
    void OnCollisionEnter2D(Collision2D coll) //傳入碰撞對象 取名coll
    {
        // if (coll.gameObject.tag == "Enemy") //這個對象的tag如果是敵人
        // {
        //     // coll.gameObject.SendMessage("ApplyDamage", 10);
        //     //傳參數10給這個物件，gameObject.SendMessage()之後會花一天了解他~
        // }
        Logger.Log(coll.gameObject.tag);
        // Image image = gameObject.GetComponent<Image>();
        // // transform.rectTransform().sizeDelta.x;
        // image.type = Image.Type.Sliced;
        // image.fillCenter = true;
    }
}