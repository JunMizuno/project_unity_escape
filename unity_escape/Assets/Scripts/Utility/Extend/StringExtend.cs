using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtend
{
    public static string WithColorTag(this string self, Color color)
    {
        string colorTag = ExchangeToByte(color);
        return string.Format("<color=#{0}>{1}</color>", colorTag, self);
    }

    public static string WithColorTag(this string self, Color32 color)
    {
        string colorTag = ExchangeToString(color.r, color.g, color.b);
        return string.Format("<color=#{0}>{1}</color>", colorTag, self);
    }

    private static string ExchangeToByte(Color color)
    {
        int r = (int)(color.r * 255.0f);
        int g = (int)(color.g * 255.0f);
        int b = (int)(color.b * 255.0f);

        return ExchangeToString(r, g, b);
    }

    private static string ExchangeToString(int r, int g, int b)
    {
        string r16 = string.Format("{0}", r.ToString("x4"));
        string g16 = string.Format("{0}", g.ToString("x4"));
        string b16 = string.Format("{0}", b.ToString("x4"));

        // @memo. 先頭の余分な桁数を削除
        r16 = r16.Remove(0, 2);
        g16 = g16.Remove(0, 2);
        b16 = b16.Remove(0, 2);

        return r16 + g16 + b16;
    }
}
