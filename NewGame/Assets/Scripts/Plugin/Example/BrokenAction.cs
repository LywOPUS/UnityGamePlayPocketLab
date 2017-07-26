using UnityEngine;
using System.Collections.Generic;

public class BrokenAction : ActionBehaviour
{
    private Renderer render;
    [SerializeField] private GameObject brokenPrefab;
    [SerializeField] private int brokenNum = 1;

    private List<GameObject> brokenObjects;

    protected override void Initialize()
    {
        render = GetComponent<Renderer>();
        brokenObjects = new List<GameObject>();
    }

    public override void Action()
    {
        render.enabled = false;
        TimeMovement originalMove = GetComponent<TimeMovement>();
        for (int count = 0; count < brokenNum; count++)
        {
            var tempObject = Instantiate<GameObject>(brokenPrefab);
            tempObject.transform.SetParent(transform.parent);
            var spawnPos = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            tempObject.transform.position = spawnPos;

            var tempMovement = tempObject.GetComponent<TimeMovement>();

            if (null != tempMovement && null != originalMove)
            {
                tempMovement.Speed = originalMove.Speed;
                tempMovement.Direction = originalMove.Direction;
            }

            brokenObjects.Add(tempObject);
        }

        TimeManager.Instance.AddRewindAction(RewindAction);
    }

    public override void RewindAction()
    {
        render.enabled = true;
        foreach (GameObject go in brokenObjects)
        {
            Destroy(go);
        }
    }
}
