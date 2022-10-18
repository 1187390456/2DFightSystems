using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Generics<T>
{
    /// <summary>
    /// 捕获空引用错误
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T CatchErrorIfNotType(T value, string name)
    {
        if (value != null)
        {
            return value;
        }
        Debug.LogError(typeof(T) + "未存在引用类型" + name);

        return default;
    }
}