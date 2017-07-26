using UnityEngine;

public abstract class ActionBehaviour : MonoBehaviour
{
    private void Awake()
    {
        Initialize();
    }

    protected abstract void Initialize();
    public abstract void Action();
    public abstract void RewindAction();
}
