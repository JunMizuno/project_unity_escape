using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoolExtend
{
    /// <summary>
    /// 2択をランダムで返す
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool RandomBool(this bool self)
    {
        return Random.Range(0, 2) == 0;
    }
}
