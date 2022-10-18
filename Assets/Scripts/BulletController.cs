using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class BulletController : MonoBehaviour {

    //[Header("子弹")]
    //[Tooltip("子弹Prefab")]
    //public GameObject bullet;

    Rigidbody2D _rigidbody2D;

    [Header("子弹射速")]
    [Tooltip("")]
    public float force = 500.0f;

    [Header("子弹移动最大长度")]
    [Tooltip("子弹移动超过最大长度销毁子弹实例")]
    public float maxDestoryLength = 20.0f;

    // 子弹初始位置
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start() {
        startPosition = this.transform.position;
    }

    /// <summary>
    /// 加载脚本实例时调用
    /// </summary>
    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {

        float dist = Vector3.Distance(startPosition, transform.position);
        Debug.Log($"transform.position:{transform.position} ===> {dist} =====> {dist > maxDestoryLength}" );
        //子弹移动超过最大长度，销毁子弹实例
        if ( dist > maxDestoryLength ) {
            Debug.Log($"Destory bullet");
            Destroy(gameObject);
        }
    }

    public void Launcher(Vector2 vector2) {
        _rigidbody2D.AddForce(vector2 * force);
    }

    /// <summary>
    /// 2d 碰撞事件
    /// </summary>
    /// <param name="collision">碰撞的对象</param>
    private void OnCollisionEnter2D(Collision2D collision) {

        collision.collider.GetComponent<EnemyController>()?.Fix();

        Debug.Log($"子弹碰撞到了{collision.gameObject}");
        // 碰撞之后子弹消失
        Destroy(gameObject);

    }
}
