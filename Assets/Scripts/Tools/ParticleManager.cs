using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    public void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 一般生成
    /// </summary>
    /// <param name="perfab"></param>
    /// <returns></returns>
    public GameObject CreateParticle(GameObject perfab) => Instantiate(perfab, transform);

    /// <summary>
    /// 指定生成位置
    /// </summary>
    /// <param name="perfab"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public GameObject CreateParticle(GameObject perfab, Transform target) => Instantiate(perfab, target.position, Quaternion.identity, transform);

    /// <summary>
    /// 指定 生成位置 旋转度数
    /// </summary>
    /// <param name="perfab"></param>
    /// <param name="target"></param>
    /// <param name="rot"></param>
    /// <returns></returns>

    public GameObject CreateParticle(GameObject perfab, Transform target, Quaternion rot) => Instantiate(perfab, target.position, rot, transform);

    /// <summary>
    /// 指定 生成位置 旋转度数 生成父级
    /// </summary>
    /// <param name="perfab"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject CreateParticle(GameObject perfab, Transform target, Quaternion rot, Transform parent) => Instantiate(perfab, target.position, rot, parent);

    /// <summary>
    /// 指定生成位置 生成父级  随机度数
    /// </summary>
    /// <param name="perfab"></param>
    /// <param name="pos"></param>
    /// <param name="parent"></param>
    /// <returns></returns>

    public GameObject CreateParticleRandomRot(GameObject perfab, Transform pos, Transform parent)
    {
        var rot = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        return Instantiate(perfab, pos.position, rot, parent);
    }
}