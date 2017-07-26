using System;

public class RewindAction
{
    public float time;
    public Action action;

    public RewindAction(float time, Action action)
    {
        this.time = time;
        this.action = action;
    }
}
