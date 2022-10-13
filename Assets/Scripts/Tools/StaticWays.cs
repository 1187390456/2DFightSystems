using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public static class StaticWays
{
    /// <summary>
    /// 设置一个或多个游戏对象状态
    /// </summary>
    /// <param name="state">必填</param>
    /// <param name="obj1">必填</param>
    /// <param name="obj2"></param>
    /// <param name="obj3"></param>
    /// <param name="obj4"></param>
    public static void SetGameObjectActive(bool state, GameObject obj0, GameObject obj1 = null, GameObject obj2 = null, GameObject obj3 = null)
    {
        JudgeGobjWay(obj0, state);
        JudgeGobjWay(obj1, state);
        JudgeGobjWay(obj2, state);
        JudgeGobjWay(obj3, state);
    }

    /// <summary>
    /// 判断游戏对象是否为空 不为空则设置状态
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="state"></param>
    public static void JudgeGobjWay(GameObject obj, bool state)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(state);
        }
    }

    /// <summary>
    /// 将obj2的某个值赋值给obj1
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <param name="type"></param>
    public static void AssignmentEach(GameObject obj1, GameObject obj2, string type)
    {
        switch (type)
        {
            case "Position":
                obj1.transform.position = obj2.transform.position;
                break;

            case "Rotation":
                obj1.transform.rotation = obj2.transform.rotation;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 传入一个角度判断是在那个象限
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static int JudgeQuadrant(float angle)
    {
        if (angle >= 0.0f && angle <= 90.0f) return 1;
        else if (angle >= 90.0f && angle <= 180.0f) return 2;
        else if (angle >= -90.0f && angle <= 0.0f) return 4;
        else if (angle >= 180.0f && angle <= 270.0f) return 3;
        else if (angle >= -180.0f && angle <= -90.0f) return 3;
        else if (angle >= 270.0f && angle <= 360.0f) return 4;
        return -1;
    }

    /// <summary>
    /// 传入一个角度判断是在哪个方向
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Vector2 JudgeDirection(float angle)
    {
        if (angle == 0.0f || angle == 360.0f) return new Vector2(1.0f, 0.0f);
        else if (angle == 45.0f) return new Vector2(1.0f, 1.0f);
        else if (angle == 90.0f) return new Vector2(0.0f, 1.0f);
        else if (angle == 135.0f) return new Vector2(-1.0f, 1.0f);
        else if (angle == 180.0f) return new Vector2(-1.0f, 0.0f);
        else if (angle == 225.0f) return new Vector2(-1.0f, -1.0f);
        else if (angle == 270.0f) return new Vector2(0.0f, -1.0f);
        else if (angle == 315.0f) return new Vector2(1.0f, -1.0f);
        return Vector2.zero;
    }
}