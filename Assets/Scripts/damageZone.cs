using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageZone : MonoBehaviour {

    /// <summary>
    /// 伤害数值
    /// </summary>
    [Header("伤害")]
    [Tooltip("伤害数值")]
    public int damage = 1;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision) {
        try {
            RubyController rubyController = collision.GetComponent<RubyController>();

            if (!rubyController.IsInvincible) {
                // 处于非无敌帧状态时
                // 设置无敌状态
                rubyController.IsInvincible = true;
                makeDamage(rubyController, damage);
            }
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
    }

    /// <summary>
    /// 制造伤害
    /// </summary>
    /// <param name="rubyController">受到伤害的实体</param>
    /// <param name="damage">伤害数值</param>
    private void makeDamage(RubyController rubyController, int damage) {
        Debug.Log($"伤害来源{gameObject}对{rubyController}造成了{damage}点伤害！");
        rubyController.ChangeHealth(damage * -1);
    }
}
