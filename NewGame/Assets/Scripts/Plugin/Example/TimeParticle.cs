using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeParticle : TimeBehaviour
{
    private ParticleSystem particle;
    private float time;

    protected override void Awake()
    {
        base.Awake();
        particle = GetComponent<ParticleSystem>();
    }

    public override void UpdateTime(float deltaTime)
    {
        time += deltaTime;

        particle.Simulate(time, true, true);
    }
}
