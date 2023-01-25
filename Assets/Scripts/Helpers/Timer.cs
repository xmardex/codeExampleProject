using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Timer
{
    static List<TimerInstance> timerIntInstances = new List<TimerInstance>();
    static List<TimerSimpleFloatInstance> timerSimpleFloatInstances = new List<TimerSimpleFloatInstance>();
    public static void StartNewTimer(string id, int secondsDuration, Action<string> onTimerStart = null, Action<string, int> onTimerTick = null, Action<string,int> onTimerFinish = null)
    {
        GameObject timer_Obj = new GameObject(id);

        TimerInstance newTimer = (TimerInstance)timer_Obj.AddComponent(typeof(TimerInstance));
        
        newTimer.Initialize(id,secondsDuration,onTimerStart,onTimerTick,onTimerFinish);
        timerIntInstances.Add(newTimer);
        newTimer.onTimerFinish += StopTimerIntInstance;
        
    }
    public static void StartSimpleFloatTimer(string id, float duration, Action<string> onTimerFinish, bool realtime = false)
    {
        GameObject timer_Obj = new GameObject(id);

        TimerSimpleFloatInstance newTimer = (TimerSimpleFloatInstance)timer_Obj.AddComponent(typeof(TimerSimpleFloatInstance));
        newTimer.Initialize(id,duration,onTimerFinish,realtime);
        timerSimpleFloatInstances.Add(newTimer);
        newTimer.onTimerFinish += StopSimpleTimerFloatInstance;
    }
    public static void StopSimpleTimerFloatInstance(string id)
    {
        TimerSimpleFloatInstance timer = timerSimpleFloatInstances.Find(t=>t.id == id);
        
        if(timer != null)
        {
            timer.onTimerFinish -= StopSimpleTimerFloatInstance;
            timer.StopAllCoroutines();
            timerSimpleFloatInstances.Remove(timer);
            timer.RemoveTimer();
        }
    }
    public static void StopTimerIntInstance(string id, int seconds)
    {
        TimerInstance timer = timerIntInstances.Find(t=>t.id == id);
        
        if(timer != null)
        {
            timer.onTimerFinish -= StopTimerIntInstance;
            timer.StopAllCoroutines();
            timerIntInstances.Remove(timer);
            timer.RemoveTimer();
        }
    }
    public static void ClearInstance(TimerInstance timerInstance)
    {
        timerInstance.onTimerFinish -= StopTimerIntInstance;
        timerInstance.StopAllCoroutines();
        timerIntInstances.Remove(timerInstance);
    }
}

public class TimerSimpleFloatInstance : MonoBehaviour
{
    public float secondsDuration;
    public Action<string> onTimerFinish;
    public bool isRealTime;
    public string id;

    public void Initialize(string id ,float secondsDuration, Action<string> onTimerFinish, bool isRealTime)
    {
        this.id = id;
        this.secondsDuration = secondsDuration;
        this.onTimerFinish = onTimerFinish;
        this.isRealTime = isRealTime;

        StartCoroutine(TimerTickIE());
    }
    IEnumerator TimerTickIE()
    {
        if(!isRealTime)
            yield return new WaitForSeconds(secondsDuration);
        else
            yield return new WaitForSecondsRealtime(secondsDuration);
        
        onTimerFinish?.Invoke(id);
    }
    public void RemoveTimer()
    {
        Destroy(this.gameObject);
    }
}

public class TimerInstance : MonoBehaviour
{
    public int secondsDuration;
    public Action<string> onTimerStart;
    public Action<string,int> onTimerTick;
    public Action<string,int> onTimerFinish;
    public string id;

    public void Initialize(string id, int secondsDuration, Action<string> onTimerStart = null, Action<string, int> onTimerTick = null, Action<string,int> onTimerFinish = null)
    {
        this.secondsDuration = secondsDuration;
        this.onTimerStart = onTimerStart;
        this.onTimerTick = onTimerTick;
        this.onTimerFinish = onTimerFinish;
        this.id = id;
        StartCoroutine(TimerTickIE());
    }
    IEnumerator TimerTickIE()
    {
        onTimerStart?.Invoke(id);
        int timer = secondsDuration;

        while(timer > 0)
        {
            onTimerTick?.Invoke(id,timer);
            timer--;
            yield return new WaitForSeconds(1);
        }
        timer = 0;
        onTimerFinish?.Invoke(id,timer);
    }
    public void RemoveTimer()
    {
        Destroy(this.gameObject);
    }
    public void OnDestroy()
    {
        Timer.ClearInstance(this);
    }
}