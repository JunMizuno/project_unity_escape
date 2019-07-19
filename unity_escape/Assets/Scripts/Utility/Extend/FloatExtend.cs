using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// float型の拡張メソッド
/// Infiniyty:無限大、NaN:ノットアナンバー
/// </summary>
public static class FloatExtend
{
    /// <summary>
    /// 引数で指定された値が正常化どうか
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsValidated(this float self)
    {
        return !float.IsInfinity(self) && !float.IsNaN(self);
    }

    /// <summary>
    /// 引数で指定されたfloat型の値を返す
    /// </summary>
    /// <param name="self"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static float GetValueOrDefault(this float self, float defaultValue = 0)
    {
        if (float.IsInfinity(self) ||
            float.IsNaN(self))
        {
            return defaultValue;
        }

        return self;
    }

    /// <summary>
    /// 引数で受け取った値との比較
    /// </summary>
    /// <param name="self"></param>
    /// <param name="value"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static bool SafeEquals(this float self, float value, float threshold = 0.001f)
    {
        return Mathf.Abs(self - value) <= threshold;
    }

    /// <summary>
    /// 自身の絶対値を返す
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static float ByAbs(this float self)
    {
        return Mathf.Abs(self);
    }
}
