using System;

public static class ActionExtend
{
    public static void NullSafeCall(this Action self)
    {
        if (self != null)
        {
            self();
        }
    }

    public static void NullSafeCall<T>(this Action<T> self, T arg)
    {
        if (self != null)
        {
            self(arg);
        }
    }

    public static void NullSafeCall<T1, T2>(this Action<T1, T2> self, T1 arg1, T2 arg2)
    {
        if (self != null)
        {
            self(arg1, arg2);
        }
    }
}
