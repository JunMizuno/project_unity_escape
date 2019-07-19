using UnityEngine;

public static class StringExtend
{
    /// <summary>
    /// カラータグ付きの文字列を返す
    /// </summary>
    /// <param name="self"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string WithColorTag(this string self, Color color)
    {
        string colorTag = ExchangeToByte(color);
        return string.Format("<color=#{0}>{1}</color>", colorTag, self);
    }

    /// <summary>
    /// カラータグ付きの文字列を返す
    /// </summary>
    /// <param name="self"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string WithColorTag(this string self, Color32 color)
    {
        string colorTag = ExchangeToString(color.r, color.g, color.b);
        return string.Format("<color=#{0}>{1}</color>", colorTag, self);
    }

    /// <summary>
    /// RGB値に変換
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private static string ExchangeToByte(Color color)
    {
        int r = (int)(color.r * 255.0f);
        int g = (int)(color.g * 255.0f);
        int b = (int)(color.b * 255.0f);

        return ExchangeToString(r, g, b);
    }

    /// <summary>
    /// カラーコードに変換
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static string ExchangeToString(int r, int g, int b)
    {
        // @memo. 16進数に変換
        string r16 = string.Format("{0}", r.ToString("x4"));
        string g16 = string.Format("{0}", g.ToString("x4"));
        string b16 = string.Format("{0}", b.ToString("x4"));

        // @memo. 先頭の余分な桁数を削除
        r16 = r16.Remove(0, 2);
        g16 = g16.Remove(0, 2);
        b16 = b16.Remove(0, 2);

        return r16 + g16 + b16;
    }

    /// <summary>
    /// 指定された文字列が後部にあった場合、それを削除した文字列を返す
    /// </summary>
    /// <param name="self"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string RemoveAtLast(this string self, string key)
    {
        return self.Remove(self.LastIndexOf(key, System.StringComparison.CurrentCulture), key.Length);
    }
}
