using System;
using System.Collections.Generic;

public class TimeManager
{
    public static TimeManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new TimeManager();
            }

            return instance;
        }
    }
    private static TimeManager instance;

    public float Time
    {
        get { return time; }
        set
        {
            if (value != time)
            {
                //计算增量时间
                float deltaTime = value - time;

                if (deltaTime < 0)
                {
                    //如果时间倒退了，那么检查是否有倒回事件
                    while (rewindActions.Count > 0 && rewindActions.Peek().time >= time + deltaTime)
                    {
                        rewindActions.Pop().action();
                    }
                }

                UpdateTime(deltaTime);

                time = value;
            }
        }
    }
    private float time;

    private List<TimeBehaviour> timeBehaviours;
    private Stack<RewindAction> rewindActions;


    public TimeManager()
    {
        timeBehaviours = new List<TimeBehaviour>();
        rewindActions = new Stack<RewindAction>();
    }


    public void RegistertimeMachine(TimeBehaviour timeMachine)
    {
        timeBehaviours.Add(timeMachine);
    }


    public void UnregisterTimeMachine(TimeBehaviour timeMachine)
    {
        timeBehaviours.Remove(timeMachine);
    }


    public void AddRewindAction(Action action)
    {
        rewindActions.Push(new RewindAction(Time, action));
    }


    private void UpdateTime(float deltaTime)
    {
        foreach (TimeBehaviour timeMachine in timeBehaviours)
        {
            timeMachine.UpdateTime(deltaTime);
        }
    }
}
