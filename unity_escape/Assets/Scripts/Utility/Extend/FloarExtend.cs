using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// float型の拡張メソッド
/// </summary>
public static class FloarExtend
{
    public static bool SafeEquals(this float self, float value, float threshold = 0.001f)
    {
        return Mathf.Abs(self - value) <= threshold;
    }
}
