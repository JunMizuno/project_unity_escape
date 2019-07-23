using System;
using System.Collections.Generic;

public static class ListExtend
{
    /// <summary>
    /// 自身が空かどうか
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsEmpty<T>(this List<T> self)
    {
        return self.Count == 0;
    }

    /// <summary>
    /// 全要素が指定した条件を満たすかどうか
    /// 全ての要素が条件を満たさない場合はtrueを返す
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool IsNone<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var n in source)
        {
            if (predicate(n))
            {
                return false;
            }
        }

        return true;
    }
}