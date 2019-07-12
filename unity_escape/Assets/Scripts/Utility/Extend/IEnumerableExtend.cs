using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class IEnumerableExtend
{
    /// <summary>
    /// float値のみ指定可能
    /// </summary>
    /// <param name="self"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static float NearestValue(this List<float> self, float target)
    {
        var min = self.Min(c => Mathf.Abs(c - target));
        return self.First(c => Math.Abs(c - target) == min);
    }
}
