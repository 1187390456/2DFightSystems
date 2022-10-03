using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    public static BulletBox Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // 生成弓箭手子弹
    public ArcherBullet GetArcherBullet(GameObject bullet, Transform targetPos)
    {
        var item = Instantiate(bullet, targetPos.position, targetPos.rotation, transform);
        return item.GetComponent<ArcherBullet>();
    }
}