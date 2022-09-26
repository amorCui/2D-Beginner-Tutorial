using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    /// <summary>
    /// 速度
    /// </summary>
    [Header("移动")]
    [Tooltip("移动速度")]
    [Range(0,1.0f)]
    public float speed = 0.1f;

    /// <summary>
    /// 移动时间计时器
    /// </summary>
    [Header("移动时间")]
    [Tooltip("移动时间计时器")]
    [Range(0, 10.0f)]
    public float moveTimer = 5f;

    private float _moveTimer;

    /// <summary>
    /// 伤害数值
    /// </summary>
    [Header("伤害")]
    [Tooltip("伤害数值")]
    public int damage = -1;

    /// <summary>
    /// 垂直移动吗？
    /// </summary>
    [Header("垂直移动？")]
    [Tooltip("true为垂直移动，false为水平移动")]
    public bool isVertical = false;

    /// <summary>
    /// 移动方向
    /// </summary>
    [Header("移动方向")]
    [Tooltip("director >0 正向移动， director< 0 负向移动")]
    public int director = 1;

    Vector2 vector2;

    Animator animator;


    /// <summary>
    /// 绑定对象的刚体
    /// </summary>
    Rigidbody2D rigidbody2d;

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _moveTimer = moveTimer;
        
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update() {

        _moveTimer -= Time.deltaTime;
        if (_moveTimer < 0) {
            _moveTimer = moveTimer;
            director = -director;
        } 
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate() {
        

        vector2 = rigidbody2d.position;
        if (isVertical) {
            vector2.x += speed * director  * Time.deltaTime;
            Debug.Log($"Move X-director:{director}");
            animator.SetFloat("Move X", director);
        } else {
            vector2.y += speed * director  * Time.deltaTime;
            Debug.Log($"Move Y-director:{director}");
            animator.SetFloat("Move Y", director);
        }

        rigidbody2d.MovePosition(vector2);

    }

    /// <summary>
    /// 刚体于某个对象碰撞的时候触发的事件
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log($"{gameObject}碰撞到了{other.gameObject}.");

        RubyController rubyController = other.gameObject.GetComponent<RubyController>();

        if (rubyController != null) {
            rubyController.ChangeHealth(damage);
        }
        
    }

}
