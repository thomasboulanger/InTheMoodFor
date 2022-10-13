using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumUtils
{
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}
