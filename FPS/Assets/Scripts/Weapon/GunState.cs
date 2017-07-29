using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : MonoBehaviour
{
    public static   int maxGunClip = 6;
    public Value<int> gunClip = new Value<int>(maxGunClip);
    public Attempt isBlank = new Attempt();
    public Value<int> dClip;


}
