using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandle : MonoBehaviour
{
    public static CanvasHandle Instance { get; private set; }
    public Text health { get; private set; }
    public GameObject deadTimer { get; private set; }

    private void Awake()
    {
        Instance = this;
        health = transform.Find("Health").GetComponent<Text>();
        deadTimer = transform.Find("DeadTimer").gameObject;
    }

    private void Start()
    {
        health.text = $"当前生命值为 :";
        SetDeadTimer(false);
        CheckEnvironment();
    }

    public void SetCanvasBtnState(bool value)
    {
        transform.Find("button").gameObject.SetActive(value);
        transform.Find("Switch").gameObject.SetActive(value);
        transform.Find("move").gameObject.SetActive(value);
    }

    public void SetDeadTimer(bool value) => deadTimer.gameObject.SetActive(value);

    public void CheckEnvironment()
    {
        if (Player.Instance.sense.Android())
        {
            SetCanvasBtnState(true);
        }
        else
        {
            SetCanvasBtnState(false);
        }
    }
}