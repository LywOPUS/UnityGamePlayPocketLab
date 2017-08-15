using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartClick : MonoBehaviour
{
    public Button startButton;
    public Image lodingSlider;
    public Text lodingText;
    void Start()
    {
        startButton.onClick.AddListener(LoadScene);
    }
    public void LoadScene()
    {
        startButton.gameObject.SetActive(false);
        StartCoroutine(StartLoading());
    }
    private IEnumerator StartLoading()
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(2);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress;

            while (displayProgress < toProgress)
            {
                ++displayProgress;
                lodingSlider.fillAmount = displayProgress * Time.deltaTime;
                lodingText.text = displayProgress.ToString() + "%";
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            lodingSlider.fillAmount = displayProgress * Time.deltaTime;
            lodingText.text = displayProgress.ToString() + "%";
            yield return new WaitForEndOfFrame();
        }

        op.allowSceneActivation = true;
    }
}