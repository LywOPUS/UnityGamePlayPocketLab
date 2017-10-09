using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoResourceTest : MonoBehaviour
{
    public int sizeCount = 0;
    public Rigidbody player;
    public List<GameObject> Terrians;
    public List<GameObject> Streets;
    private Transform TerriansParent;

    // Use this for initialization
    private void Start()
    {
        InstantTerrians();
    }

    private void Update()
    {
    }

    private void InstantTerrians()
    {
        int count = 0;
        foreach (var item in Terrians)
        {
            Instantiate(item, new Vector3(0, 0, count), Quaternion.identity);
            count += 100;
        }
    }

    private void UpdateTerrians()
    {
    }
}