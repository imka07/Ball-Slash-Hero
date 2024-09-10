using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LoadingManager : MonoBehaviour
{
    public GameObject loadingPanel;  
    public GameObject mainMenuPanel, gamePanel;  
    public Slider progressBar;                

    void Start()
    {
        loadingPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(false);
        StartCoroutine(SimulateLoading());
    }

    public void StartLoading()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(SimulateLoading());
    }

    IEnumerator SimulateLoading()
    {
        float loadingTime = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadingTime);

            progressBar.value = progress;

            yield return null;
        }

        OnLoadingComplete();
    }

    void OnLoadingComplete()
    {
        loadingPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
