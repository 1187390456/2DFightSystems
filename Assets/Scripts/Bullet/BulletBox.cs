using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    public static BulletBox Instance { get; private set; }
    private GameObject archerBulletRes;     // 弓箭手子弹资源

    private void Awake()
    {
        Instance = this;
        archerBulletRes = Resources.Load<GameObject>("Perfabs/Bullet/ArcherBullet");
    }

    // 生成弓箭手子弹
    public ArcherBullet GetArcherBullet(Transform targetPos)
    {
        var item = Instantiate(archerBulletRes, targetPos.position, targetPos.rotation, transform);
        return item.GetComponent<ArcherBullet>();
    }
}