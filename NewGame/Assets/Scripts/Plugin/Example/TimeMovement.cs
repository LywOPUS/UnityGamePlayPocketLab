using UnityEngine;

public class TimeMovement : TimeBehaviour
{
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;

    public override void UpdateTime(float deltaTime)
    {
        transform.position += direction.normalized * speed * deltaTime;
    }
}
