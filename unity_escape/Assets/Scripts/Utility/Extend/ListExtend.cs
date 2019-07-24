using System;
using System.Collections.Generic;
using System.Linq;

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
    /// 引数にラムダ式で条件を指定した関数を渡す
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

    /// <summary>
    /// 1つでも指定した条件を満たすものを含んでいるかどうか
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static bool IsInclude<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var n in source)
        {
            if (predicate(n))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 自身の持っている最小値を全て返す
    /// 引数にラムダ式で条件を渡す
    /// ラムダで条件式trueなどを返すと、それを削除したものが返る
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> MinElementsAll<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        var value = source.Min(selector);
        return source.Where(c => selector(c).Equals(value));
    }

    /// <summary>
    /// 自身が持っている最大値を全て返す
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> MaxElementsAll<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
    {
        var value = source.Max(selector);
        return source.Where(c => selector(c).Equals(value));
    }

    /// <summary>
    /// 指定されたインデックスに要素が存在するかどうか
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool IsDefinedAt<T>(this IList<T> self, int index)
    {
        return index < self.Count;
    }

    /// <summary>
    /// リスト内の要素に対して引数で指定されたラムダ関数の処理を行う
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="func"></param>
    public static void Apply<T>(this IList<T> self, Func<T, T> func)
    {
        for (int i = 0; i < self.Count; i++)
        {
            self[i] = func(self[i]);
        }

    }

    /// <summary>
    /// リスト内の要素に対して引数で指定されたラムダ関数の処理を行う
    /// カウントも同時に渡すパターン
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="func"></param>
    public static void Apply<T>(this IList<T> self, Func<T, int, T> func)
    {
        for (int i = 0; i < self.Count; i++)
        {
            self[i] = func(self[i], i);
        }
    }

    /// <summary>
    /// 自身に対して引数で渡されたリストの要素を結合したものを返す
    /// 自身の後ろに順次追加
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="sources"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> JoinBack<TSource>(this IEnumerable<TSource> self, params IEnumerable<TSource>[] sources)
    {
        foreach (var n in self)
        {
            yield return n;
        }

        foreach (var source in sources)
        {
            foreach (var n in source)
            {
                yield return n;
            }
        }
    }

    /// <summary>
    /// 自身に対して引数で渡されたリストの要素を結合したものを返す
    /// 自身の前に順次追加
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="self"></param>
    /// <param name="sources"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> JoinFront<TSource>(this IEnumerable<TSource> self, params IEnumerable<TSource>[] sources)
    {
        foreach (var source in sources)
        {
            foreach (var n in source)
            {
                yield return n;
            }
        }

        foreach (var n in self)
        {
            yield return n;
        }
    }

    /// <summary>
    /// 自身の要素の後ろに値を追加
    /// 引数の値はListではなく単体で指定
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="self"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> BackWith<TSource>(this IEnumerable<TSource> self, params TSource[] value)
    {
        foreach (var n in self)
        {
            yield return n;
        }

        foreach (var n in value)
        {
            yield return n;
        }
    }

    /// <summary>
    /// 自身の要素の先頭に値を追加
    /// 引数の値はListではなく単体で指定
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="self"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> FrontWith<TSource>(this IEnumerable<TSource> self, params TSource[] value)
    {
        foreach (var n in value)
        {
            yield return n;
        }

        foreach (var n in self)
        {
            yield return n;
        }
    }

    /// <summary>
    /// 自身の中から条件を満たさないものを全て返す
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> WhereNot<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        return source.Where(c => !predicate(c));
    }

    /// <summary>
    /// 自身の中から条件を満たすもの全てを返す
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> WhereCorrect<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        return source.Where(c => predicate(c));
    }

    /// <summary>
    /// 自身のシーケンスが空かどうか
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
    {
        return !source.Any();
    }

    /// <summary>
    /// 自身の先頭から指定された数の要素を削除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="count"></param>
    public static void RemoveFromFront<T>(this List<T> self, int count)
    {
        self.RemoveRange(0, count);
    }

    /// <summary>
    /// 自身の後ろから指定された数の要素を削除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="count"></param>
    public static void RemoveFromBack<T>(this List<T> self, int count)
    {
        self.RemoveRange(self.Count - count, count);
    }

    /// <summary>
    /// 指定された値と同じ要素を全て削除する
    /// 引数はラムダ式で条件を記述
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="self"></param>
    /// <param name="match"></param>
    public static void RemoveAtValue<T>(this List<T> self, Predicate<T> match)
    {
        var index = self.FindIndex(match);
        if (index == -1)
        {
            return;
        }

        self.RemoveAt(index);
    }





}
