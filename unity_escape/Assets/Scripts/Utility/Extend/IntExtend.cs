using System;
using UnityEngine;

public static class IntExtend
{
    /// <summary>
    /// ラップアラウンド
    /// 自身の値をベースにして、指定した最小値と最大値との間の数値を返す
    /// </summary>
    /// <returns></returns>
    public static int WrapAround(this int self, int min, int max)
    {
        int retValue = (self - min) % (max - min);
        return (retValue >= 0) ? (retValue + min) : (retValue + max);
    }

    /// <summary>
    /// 自身を3桁区切りのstring型に変換
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static string WithComma(this int self)
    {
        return string.Format("{0:#,##0}", self);
    }

    /// <summary>
    /// 自身を引数で指定された桁数のstring型に変換
    /// </summary>
    /// <param name="self"></param>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static string ZeroFill(this int self, int digits)
    {
        return self.ToString("D" + digits);
    }

    // @todo. 以下、このままではコールバック内でbreakとかが使えない…
    /// <summary>
    /// 自身の数値分、指定された処理をループして実行する
    /// </summary>
    /// <param name="self"></param>
    /// <param name="action"></param>
    public static void LoopFuncByTimes(this int self, Action action)
    {
        for (int i = 0; i < self; i++)
        {
            action.NullSafeCall();
        }
    }

    public static void LoopFuncByTimes(this int self, Action<int> action)
    {
        for (int i = 0; i < self; i++)
        {
            action.NullSafeCall(i);
        }
    }
}
