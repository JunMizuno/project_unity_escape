using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilitySystems
{
    public static int ConvertDateTimeToTimeStamp(DateTime dateTime)
    {
        var timeStamp = TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, kind: DateTimeKind.Utc);
        return (int)timeStamp.TotalSeconds;
    }
}
