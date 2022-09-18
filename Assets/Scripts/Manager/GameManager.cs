using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Header("重生时间")] private float rebirthTime;
    public static GameManager Instance { get; private set; } // 单例
    private float startRebirthTime; //  开始重生时间
    private bool canRebirth; // 是否可以重生
    private GameObject playerRes; // 玩家资源
    private Transform rebirthPos; // 重生点
    private CinemachineVirtualCamera playerCM; // 玩家虚拟摄像机

    private void Awake()
    {
        Instance = this;
        playerRes = Resources.Load<GameObject>("Perfabs/Player/Player");
        rebirthPos = transform.Find("RebirthPos");
        playerCM = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckRebirthState();
    }

    // 检查重生状态
    private void CheckRebirthState()
    {
        if (canRebirth && Time.time >= startRebirthTime + rebirthTime)
        {
            canRebirth = false;
            var player = Instantiate(playerRes, rebirthPos.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
            player.transform.SetSiblingIndex(4);
            playerCM.Follow = player.transform;
        }
    }

    // 重生
    public void Rebirth()
    {
        canRebirth = true;
        startRebirthTime = Time.time;
    }
}