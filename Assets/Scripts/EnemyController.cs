using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    /// <summary>
    /// 刚体坐标
    /// </summary>
    Vector2 _rigidbodyVector2;

    /// <summary>
    /// 绑定的动画
    /// </summary>
    Animator _animator;

    /// <summary>
    /// 绑定对象的刚体
    /// </summary>
    Rigidbody2D _rigidbody2d;

    /// <summary>
    /// 移动时间计时器
    /// </summary>
    private float _moveTimer;

    /// <summary>
    /// 速度
    /// </summary>
    [Header("移动")]
    [Tooltip("移动速度")]
    [Range(0, 1.0f)]
    public float speed = 0.1f;

    /// <summary>
    /// 移动时间计时器
    /// </summary>
    [Header("移动时间")]
    [Tooltip("用来标记移动时间的最大值")]
    [Range(0, 10.0f)]
    public float moveTimer = 5f;


    /// <summary>
    /// 伤害数值
    /// </summary>
    [Header("伤害")]
    [Tooltip("伤害数值")]
    public int damage = 1;

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

    [Header("机器人是否坏掉")]
    [Tooltip("默认为true表示机器人是坏掉的，false表示机器人已修复")]
    public bool broken = true;






    private void Start() {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _moveTimer = moveTimer;

    }

    /// <summary>
    /// 
    /// </summary>
    private void Update() {
        if ( !broken ) {
            return;
        }

        _moveTimer -= Time.deltaTime;
        if ( _moveTimer < 0 ) {
            _moveTimer = moveTimer;
            director = -director;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate() {

        if ( !broken ) {
            return;
        }

        // 移动
        Movement();

    }


    private void Movement() {
        _rigidbodyVector2 = _rigidbody2d.position;
        if ( !isVertical ) {
            //_rigidbodyVector2.x += speed * director * Time.deltaTime;
            _rigidbodyVector2.x += speed * director * Time.fixedDeltaTime;
            //Debug.Log($"Move X-director:{director}");
            _animator.SetFloat("Move X", director);
        } else {
            //_rigidbodyVector2.y += speed * director * Time.deltaTime;
            _rigidbodyVector2.y += speed * director * Time.fixedDeltaTime;
            //Debug.Log($"Move Y-director:{director}");
            _animator.SetFloat("Move Y", director);
        }

        _rigidbody2d.MovePosition(_rigidbodyVector2);
    }





    /// <summary>
    /// 刚体于某个对象碰撞的时候触发的事件
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log($"{gameObject}碰撞到了{other.gameObject}.");

        RubyController rubyController = other.gameObject.GetComponent<RubyController>();

        if ( rubyController != null ) {
            rubyController.IsInvincible = true;
            rubyController.ChangeHealth(-damage);
        }
    }

    /// <summary>
    /// 修复机器人的方法
    /// </summary>
    public void Fix() {
        broken = false;
        _rigidbody2d.simulated = false;
        _animator.SetTrigger("Fixed");
    }

}
