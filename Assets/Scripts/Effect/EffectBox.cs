using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBox : MonoBehaviour
{
    public static EffectBox Instance { get; private set; } // 单例
    private GameObject chunkRes; // 块特效资源
    private GameObject bloodRes; // 血特效资源
    private GameObject normalHitEffect; // 野猪被击打资源
    private GameObject fakePersonHitRes; // 野猪被击打资源

    private void Awake()
    {
        Instance = this;
        fakePersonHitRes = Resources.Load<GameObject>("Perfabs/Effect/Old/FakePersonHitEffect");
        normalHitEffect = Resources.Load<GameObject>("Perfabs/Effect/Old/NormalHitEffect");
        chunkRes = Resources.Load<GameObject>("Perfabs/Effect/Old/DiedChunkEffect");
        bloodRes = Resources.Load<GameObject>("Perfabs/Effect/Old/DiedBloodEffect");
    }

    // 血块特效
    public void Chunk(Vector2 pos)
    {
        Instantiate(chunkRes, pos, chunkRes.transform.rotation, transform);
    }

    // 血液特效
    public void Blood(Vector2 pos)
    {
        Instantiate(bloodRes, pos, bloodRes.transform.rotation, transform);
    }

    // 野猪击打特效
    public void WildBoarHit(Vector2 pos, Quaternion rot)
    {
        Instantiate(normalHitEffect, pos, rot, transform);
    }

    // 假人击打特效
    public void FakePersonHit(Vector2 pos, Quaternion rot)
    {
        Instantiate(fakePersonHitRes, pos, rot, transform);
    }

    // 生成指定特效
    public void CreateEffect(GameObject res, Vector2 pos, Quaternion rot)
    {
        Instantiate(res, pos, rot, transform);
    }
}