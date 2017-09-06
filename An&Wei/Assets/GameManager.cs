using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Image selImage;
    public Texture2D selTexture;
    private RaycastHit hit;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (selTexture != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider.tag == "Pannle")
                    {
                        Debug.Log("it'hit");
                        hit.collider.GetComponent<PaperText>().mrB.material.mainTexture = selTexture;
                        //hit.collider.GetComponent<PaperText> ().mrA.material.mainTexture = selTexture;
                    }
                }
            }
        }
    }
}