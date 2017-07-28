using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHud : MonoBehaviour
{

    public Slider HealthSolider;
    public Text HealthTest;
    Value<float> L_PlayerHealth;

    private void Awake()
    {
        L_PlayerHealth = GameplayStatics.LocalPlayer.health;
        L_PlayerHealth.AddChangedListener(ChangeHealth);
        HealthTest.text = "Health: " + L_PlayerHealth.Get().ToString();
       
    }

    void ChangeHealth()
    {
        if (L_PlayerHealth.Get() == L_PlayerHealth.GetPreviousValue())
        {
            return;
        }
        HealthTest.text = "Health: " + GameplayStatics.LocalPlayer.health.Get().ToString();
        HealthSolider.value = L_PlayerHealth.Get()*0.01f;
    }




}
