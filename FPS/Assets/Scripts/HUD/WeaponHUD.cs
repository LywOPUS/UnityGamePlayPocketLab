using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHUD : MonoBehaviour {

    private int ammoCap;//携带弹夹上限
    private int gunClipCap;//手枪弹夹上限
    public Text ammoCapText;
    public Text gunClipText;
    private PlayerState L_player;
    private GunState L_gun;

    private void Awake()
    {

        L_player = GameplayStatics.LocalPlayer;
        L_player.ammo.AddChangedListener(ChangeAmmo);

        L_gun = GameplayStatics.LocalRoscoeGun;
        L_gun.gunClip.AddChangedListener(GunClipCange);

        ammoCap = L_player.ammo.Get();
        gunClipCap = L_gun.gunClip.Get();
        ammoCapText.text = "Ammo: " + L_player.ammo.Get().ToString() + "/" + ammoCap;
        gunClipText.text = "Ballet: " + L_gun.gunClip.Get().ToString() + "/" + gunClipCap;

    }

    void ChangeAmmo()
    {
        if (L_player.ammo.Get()!= L_player.ammo.GetPreviousValue())
        {
            ammoCapText.text = "Ammo: " + L_player.ammo.Get().ToString()+"/"+ammoCap;
        }
    }

    void GunClipCange()
    {
        if (L_gun.gunClip.Get()!= L_gun.gunClip.GetPreviousValue())
        {
            gunClipText.text = "Ballet: " + L_gun.gunClip.Get().ToString() + "/" + gunClipCap;
        }
    }

   
}
