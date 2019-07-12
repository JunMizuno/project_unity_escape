using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityExtend
{
    public static int ToTimeStamp(this DateTime self)
    {
        var timeStamp = TimeZoneInfo.ConvertTimeToUtc(self) - new DateTime(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, kind: DateTimeKind.Utc);
        return (int)timeStamp.TotalSeconds;
    }
}
