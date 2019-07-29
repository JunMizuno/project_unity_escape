using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtend
{
    /// <summary>
    /// 指定された条件でソートをかける
    /// 先にselector1でソートした後にselector2で再度ソートをかける
    /// @todo. 使い方に難があるかもしれないので要チェック
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="array"></param>
    /// <param name="selector1"></param>
    /// <param name="selector2"></param>
    public static void Sort<TSource, TResult>(this TSource[] array, Func<TSource, TResult> selector1, Func<TSource, TResult> selector2) where TResult : IComparable
    {
        Array.Sort(array, (x, y) =>
        {
            var result = selector1(x).CompareTo(selector1(y));
            return result != 0 ? result : selector2(x).CompareTo(selector2(y));
        });
    }

    /// <summary>
    /// ランダムに並び替えた新しい配列を返す
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static T[] Shuffle<T>(this T[] array)
    {
        var length = array.Length;
        var result = new T[length];
        Array.Copy(array, result, length);

        var random = new System.Random();
        int n = length;
        while (1 < n)
        {
            n--;
            int k = random.Next(n + 1);
            var tmp = result[k];
            result[k] = result[n];
            result[n] = tmp;
        }

        return result;
    }
}
