using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    float _horizontal;
    /// <summary>
    /// 
    /// </summary>
    float _vertical;

    /// <summary>
    /// 绑定对象的刚体
    /// </summary>
    Rigidbody2D _rigidbody2d;

    /// <summary>
    /// 动画
    /// </summary>
    Animator _animator;

    /// <summary>
    /// 静止时朝向
    /// </summary>
    Vector2 _lookDirection = new Vector2(0, 1);

    Vector2 _move;


    /// <summary>
    /// 移动速度
    /// </summary>
    [Header("移动")]
    [Tooltip("移动速度")]
    [Range(0, 5.0f)]
    public float speed = 0.1f;

    /// <summary>
    /// 最大生命值
    /// </summary>
    [Header("最大生命值")]
    [Tooltip("最大生命值")]
    public int maxHealth = 5;

    public int CurrentHealth {
        get => _currentHealth;
        //set => _currentHealth = value;
    }
    /// <summary>
    /// 当前生命值
    /// </summary>
    private int _currentHealth = 1;


    public bool IsInvincible {
        get => _isInvincible;
        set => _isInvincible = value;
    }
    /// <summary>
    /// 无敌帧flag
    /// </summary>
    private bool _isInvincible = false;


    //public float TimeInvincible {
    //    get => _timeInvincible;
    //    set => _timeInvincible = value;
    //}

    /// <summary>
    /// 无敌帧总时长
    /// </summary>
    [Header("无敌帧")]
    [Tooltip("无敌帧时间")]
    [Range(0, 10.0f)]
    public float timeInvincible = 2f;

    public float InvincibleTimer {
        get => _invincibleTimer;
        set => _invincibleTimer = value;
    }
    /// <summary>
    /// 无敌帧计时器
    /// </summary>
    float _invincibleTimer;


    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start() {
        //锁帧
        //Application.targetFrameRate = 60;

        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate() {
        Vector2 position = transform.position;
        position.x += speed * _horizontal * Time.deltaTime;
        position.y += speed * _vertical * Time.deltaTime;
        transform.position = position;

        //更新角色动画
        UpdateAnimation();

        //更新无敌时间
        UpdateInvincibleTimer();
    }

    /// <summary>
    /// 更新角色动画
    /// </summary>
    private void UpdateAnimation() {

        _move = new Vector2(_horizontal, _vertical);
        
        if (!(Mathf.Approximately(_move.x, 0.0f) && Mathf.Approximately(_move.y, 0.0f))) {
            //转向的时候，重新设定面向
            _lookDirection.Set(_move.x, _move.y);
            //设定为单位向量
            _lookDirection.Normalize();
        }

        //Debug.Log($"x:{_lookDirection.x}");
        //Debug.Log($"y:{_lookDirection.y}");
        //Debug.Log($"speed:{_lookDirection.magnitude}");

        // 在无敌期间第一次收到伤害，才会触发受击动画
        bool isHurt = _isInvincible && Mathf.Approximately(_invincibleTimer, timeInvincible);
        UpdateAnimator(_move.magnitude, _lookDirection.x, _lookDirection.y, isHurt, false);
    }


    /// <summary>
    /// 更新角色动画
    /// </summary>
    /// <param name="speed">速度</param>
    /// <param name="lookX">x轴面向</param>
    /// <param name="lookY">y轴面向</param>
    /// <param name="isHit">是否被击中</param>
    /// <param name="isLaunch">是否发射飞弹</param>
    private void UpdateAnimator(float speed, float lookX, float lookY, bool isHit, bool isLaunch) {
        _animator.SetFloat("Speed", speed);
        _animator.SetFloat("Look X", lookX);
        _animator.SetFloat("Look Y", lookY);
        if (isHit) {
            _animator.SetTrigger("Hit");
        }
        if (isLaunch) {
            _animator.SetTrigger("Launch");
        }
        
    }


    /// <summary>
    /// 更新无敌时间
    /// </summary>
    private void UpdateInvincibleTimer() {

        if (_isInvincible && _invincibleTimer >= 0) {
            //Debug.Log($"UpdateInvincibleTimer:_isInvincible=>{_isInvincible},_invincibleTimer=>{_invincibleTimer}");
            _invincibleTimer = _invincibleTimer - Time.deltaTime;
        } else if (_isInvincible && _invincibleTimer < 0) {
            //Debug.Log($"UpdateInvincibleTimer:_isInvincible=>{_isInvincible},_invincibleTimer=>{_invincibleTimer}");
            _isInvincible = false;
            _invincibleTimer = timeInvincible;
        }
    }

    /// <summary>
    /// 变更当前生命值的方法
    /// </summary>
    /// <param name="amount">当前生命值的变化量</param>
    public void ChangeHealth(int amount) {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log($"当前生命值为:{CurrentHealth}/{maxHealth}");
    }
}
