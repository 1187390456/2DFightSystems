using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform respawnPoint;//重生点

    [SerializeField]
    GameObject player;

    [SerializeField]
    float respawnTime;//重生时间

    float respawnTimeStart;//重生开始时间

    bool respawn;

    CinemachineVirtualCamera CVC;

    private void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        CheckRespawn();
    }
    //重生
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }
    //检查重生
    void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }
}
