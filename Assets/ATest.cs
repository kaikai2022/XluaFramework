using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ATest : MonoBehaviour
{
    private Collider2D[] _collider2Ds;

    private Vector2 sceneSize;

    // Start is called before the first frame update
    void Start()
    {
        sceneSize = FindObjectOfType<Canvas>().transform.rectTransform().sizeDelta;
        _collider2Ds = GetComponents<Collider2D>();
        _collider2Ds[0].offset = Vector2.one;
        ((BoxCollider2D) _collider2Ds[0]).size = new Vector2(1, sceneSize.y);
        _collider2Ds[1].offset = new Vector2(0, -(sceneSize.y / 2) + 40);
        ((BoxCollider2D) _collider2Ds[1]).size = new Vector2(sceneSize.x, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }
}