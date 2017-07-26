using UnityEngine;

public class ActionControl : MonoBehaviour
{
    public enum ActiveType
    {
        Enter,
        Stay,
        Exit,
        None
    }

    [SerializeField] private ActiveType activeType = ActiveType.None;

    protected ActionBehaviour[] actions;

    protected void Actions()
    {
        for (int count = 0; count < actions.Length; count++)
        {
            actions[count].Action();
        }
    }

    private void Awake()
    {
        actions = GetComponents<ActionBehaviour>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (activeType != ActiveType.Enter)
            return;

        Actions();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (activeType != ActiveType.Stay)
            return;

        Actions();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (activeType != ActiveType.Exit)
            return;

        Actions();
    }
}
