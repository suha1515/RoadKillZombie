using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};
public static class Converter
{
    public static float khToms(float kh)
    {
        return (kh * 1000) / 3600;
    }

    // 매핑함수 value <-매핑을 원하는값
    //          from1,to1 <-값의 범위
    //          from2,to2 <-매핑값의 범위
    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
