using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestMeterial : ActionBehaviour
{
    [SerializeField] private Material material;
    private Material prematerial;
    private new Renderer renderer;

    
    

    protected override void Initialize()
    {
        renderer = GetComponent<Renderer>();
        
      
    }


    public override void Action()
    {
        if (renderer.material == material) return;
        prematerial = renderer.material;
        prematerial.Lerp(prematerial, material,Mathf.PingPong(2,2));
        TimeManager.Instance.AddRewindAction(RewindAction);
        
    }

    public override void RewindAction()
    {
        renderer.material = material;
    }
}
