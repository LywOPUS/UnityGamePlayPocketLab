using UnityEngine;

public class ColorAction : ActionBehaviour
{
    [SerializeField] private Color color;
    private Color preColor;
    private Material material;

    protected override void Initialize()
    {
        material = GetComponent<Renderer>().material;
    }

    public override void Action()
    {
        if (material.color == color)
            return;

        preColor = material.color;
        material.color = color;

        TimeManager.Instance.AddRewindAction(RewindAction);
    }

    public override void RewindAction()
    {
        material.color = preColor;
    }
}
