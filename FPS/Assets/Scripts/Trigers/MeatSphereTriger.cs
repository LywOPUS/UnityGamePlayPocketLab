using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatSphereTriger : MonoBehaviour
{

    public float damageValue;
    Value<float> L_Phealth;
   

    private void Awake()
    {
        L_Phealth = GameplayStatics.LocalPlayer.health;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("执行了伤害方法 ");
            GameplayStatics.LocalPlayer.health.Set(GetDamedHealth(damageValue));
        }
    }





    #region unsortedCode


    /// <summary>
    /// 返回受到伤害后的生命值
    /// </summary>
    /// <param name="d_value">输入伤害的数值</param>
    /// <returns></returns>
    float GetDamedHealth(float d_value)
    {
        Debug.Log("受到了伤害");
        float c_health = L_Phealth.Get() - d_value;
        return c_health;
    }
    #endregion

}
