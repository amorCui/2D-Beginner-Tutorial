using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour {
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


    /// <summary>
    /// 
    /// </summary>
    float horizontal;
    /// <summary>
    /// 
    /// </summary>
    float vertical;

    /// <summary>
    /// 绑定对象的刚体
    /// </summary>
    Rigidbody2D rigidbody2d;

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

        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate() {
        Vector2 position = transform.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;
        transform.position = position;

        UpdateInvincibleTimer();
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
