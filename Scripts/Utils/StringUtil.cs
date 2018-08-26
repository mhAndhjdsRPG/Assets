using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Linq;

public static class StringUtil {

    //注意此方法在转型失败的时候不报异常
	public static int ToInt(this string str)
    {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }
    //注意此方法在转型失败的时候不报异常
    public static long ToLong(this string str)
    {
        long temp = 0;
        long.TryParse(str, out temp);
        return temp;
    }

    public static float ParseToFloat(this string str)
    {
        return float.Parse(str);
    }

    public static int ParseToInt(this string str)
    {
        return int.Parse(str);
    }

    public static T ParseToEnum<T>(this string str)
    {
        return (T)Enum.Parse(typeof(T), str);
    }

    public static T GetChildValue<T>(this XElement element, string childElemtName)
    {
        XElement child = element.Element(childElemtName);

        T resulat;

        try
        {
            resulat = (T)Convert.ChangeType(child.Value, typeof(T));
        }
        catch
        {
            resulat = child.Value.ParseToEnum<T>();
        }
        return resulat;

    }


    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

}
