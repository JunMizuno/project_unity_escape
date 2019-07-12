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
}
