using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject afterImage;//残影
    public static ObjectPool Instance;
    private Queue<GameObject> queue = new Queue<GameObject>();//队列
    [Header("初始生成残影数量")] public int startAfterCount;
    private void Start()
    {
        Instance = this;
    }
    //实例化
    private void Init()
    {
        for (int i = 0; i < startAfterCount; i++)
        {
            var item = Instantiate(afterImage, transform);
            AddPool(item);
        }
    }
    //添加队列
    public void AddPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        queue.Enqueue(gameObject);
    }
    //出队列
    public void GetPool()
    {
        if (queue.Count == 0)
        {
            Init();
        }
        var item = queue.Dequeue();
        item.SetActive(true);
    }
}
