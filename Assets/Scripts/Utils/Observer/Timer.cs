using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Timer
{
    public static async Task SetTimeout(Action action, float time)
    {
        await Task.Delay(TimeSpan.FromSeconds(time));
        action();
    }
}
