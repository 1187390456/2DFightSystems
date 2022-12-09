using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("对象池生成实例的预制件")] public GameObject perfabs;
    [Header("需要初始实例的个数")] public int count;
    public static ObjectPool Instance; // 单例
    private Queue<GameObject> poolQueue = new Queue<GameObject>(); // 队列池

    private void Awake()
    {
        Instance = this;
        //  InitPool();
    }

    // 初始化对象池
    private void InitPool()
    {
        for (int i = 0; i < count; i++)
        {
            var poolItem = Instantiate(perfabs, transform);
            AddPool(poolItem);
        }
    }

    // 在对象池添加一个对象
    public void AddPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    // 获取对象池第一个游戏对象
    public GameObject GetObjFormPool()
    {
        if (poolQueue.Count == 0)
        {
            InitPool();
        }
        var item = poolQueue.Dequeue();
        item.SetActive(true);
        return item;
    }
}