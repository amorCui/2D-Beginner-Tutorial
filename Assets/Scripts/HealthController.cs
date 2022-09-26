using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    /// <summary>
    /// 每次添加血量
    /// </summary>
    [Header("血量变更")]
    [Tooltip("每次添加血量")]
    public static int amount = 1;
    /// <summary>
    /// 碰撞次数
    /// </summary>
    private int _collideCount = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="healthObj"></param>
    private void OnTriggerEnter2D(Collider2D healthObj) {
        _collideCount++;
        Debug.Log($"和当前物体碰撞的对象是{healthObj},碰撞次数是{_collideCount}");

        try {
            RubyController rubyCtrl = healthObj.GetComponent<RubyController>();
            if (rubyCtrl.CurrentHealth < rubyCtrl.maxHealth) {

                rubyCtrl.ChangeHealth(amount);
                Destroy(gameObject);
            }

        } catch (Exception ex) {
            Debug.LogException(ex);
        }

    }
}
