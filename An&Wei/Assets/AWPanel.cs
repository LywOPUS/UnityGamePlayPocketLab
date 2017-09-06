using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AWPanel : MonoBehaviour
{
    public GameObject Panel;
    private Image[] images;
    public static AWPanel instance;

    // Use this for initialization
    private void Awake()
    {
    }

    private void Start()
    {
        Panel.SetActive(false);
        instance = this;

        images = Panel.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame

    public IEnumerator FadeOut()
    {
        float fade = images[0].color.a;

        while (true)
        {
            yield return null;
            fade -= Time.deltaTime * 2f;
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, fade);
            }
            if (fade < 0)
            {
                break;
            }
        }
        Panel.SetActive(false);

        //gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (-122262, 0, 0);
    }

    public IEnumerator FadeIn()
    {
        float fade = images[0].color.a;
        //gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (-82, 0, 0);
        Panel.SetActive(true);
        while (true)
        {
            yield return null;
            fade += Time.deltaTime * 2f;
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, fade);
            }
            if (fade > 1)
            {
                break;
            }
        }
    }
}