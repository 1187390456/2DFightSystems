using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    // 攻击特效完成回调
    public void HitEffectDone()
    {
        Destroy(gameObject);
    }
}