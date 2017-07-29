using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ActorState
{
    public Value<Vector2> movementInput = new Value<Vector2>(Vector2.zero);

    public Value<Vector2> lookInput = new Value<Vector2>(Vector2.zero);

    public Value<Vector3> lookDirection = new Value<Vector3>(Vector3.zero);

    public Value<bool> viewLocked = new Value<bool>(false);

    public Activity jump = new Activity();

    public Activity aim = new Activity();

    public Attempt attackOnce = new Attempt();

    public Attempt attackContinuously = new Attempt();

    #region Lyw
    public Value<int> ammo = new Value<int>(30);

    public Attempt reload = new Attempt();

    
    #endregion
}