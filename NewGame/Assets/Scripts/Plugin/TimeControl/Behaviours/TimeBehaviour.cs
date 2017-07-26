using UnityEngine;

public abstract class TimeBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        TimeManager.Instance.RegistertimeMachine(this);
    }


    protected virtual void OnDestroy()
    {
        TimeManager.Instance.UnregisterTimeMachine(this);
    }

    public abstract void UpdateTime(float deltaTime);
}
