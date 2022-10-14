using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadTimer : MonoBehaviour
{
    public int dotCount = 1;
    public Text text;

    private void Awake()
    {
        text = transform.Find("Timer").GetComponent<Text>();
    }

    private void OnEnable()
    {
        StartDeadTimer();
    }

    private void OnDisable()
    {
        dotCount = 1;
        CancelInvoke("StartDeadTimer");
    }

    public void StartDeadTimer()
    {
        if (dotCount > 3) dotCount = 1;
        switch (dotCount)
        {
            case 1:
                text.text = "复活中.";
                dotCount++;
                break;

            case 2:
                text.text = "复活中..";
                dotCount++;
                break;

            case 3:
                text.text = "复活中...";
                dotCount++;
                break;

            default:
                break;
        }
        Invoke("StartDeadTimer", .3f);
    }
}