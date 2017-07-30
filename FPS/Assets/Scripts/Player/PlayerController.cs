using System;
using UnityEngine;

public class PlayerController : PlayerBehaviour
{
    //lyw
    GunState gunstate;
    //
    private InputManager inputManager;

    private void Awake()
    {
        //
        gunstate = FindObjectOfType<GunState>(); 
        //
        if (!GameplayStatics.InputManager)
        {
            enabled = false;
            return;
        }
        inputManager = GameplayStatics.InputManager;
    }

    private void Update()
    {
        if (Player.viewLocked.Is(false))
        {
            // Movement.
            Player.movementInput.Set(new Vector2(inputManager.GetAxis("Horizontal"), inputManager.GetAxis("Vertical")));

            // Look.
            Player.lookInput.Set(new Vector2(inputManager.GetAxisRaw("Mouse X"), inputManager.GetAxisRaw("Mouse Y")));

            // Jump
            if(inputManager.GetButtonDown("Jump"))
            {
                Player.jump.Start();
            }

            // Attack.
            if (inputManager.GetButton("Attack"))
                Player.attackContinuously.Do();

            if (inputManager.GetButtonDown("Attack"))
                Player.attackOnce.Do();


            // Magaine
            if (Input.GetKeyDown("r"))
            {
                if (Player.ammo.Get()!=0&&(gunstate.gunClip.Get()!=GunState.maxGunClip))
                {
                    int dClip = GunState.maxGunClip - gunstate.gunClip.Get();
                    Player.ammo.Set(Player.ammo.Get() - dClip);

                    gunstate.gunClip.Set(gunstate.gunClip.Get()+dClip);
                }
                
            }
            
        }
        else
        {
            // Movement.
            Player.movementInput.Set(Vector2.zero);

            // Look.
            Player.lookInput.Set(Vector2.zero);
        }
    }
}