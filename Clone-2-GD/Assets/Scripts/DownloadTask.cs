using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DownloadTask : MonoBehaviour
{
    public Button downloadButton;
    public Slider downloadSlider;
    public GameObject downloadPanel;
    public TextMeshProUGUI percentageText;
    public TaskManager taskManager;
    private bool isDownloading = false;
    private float downloadSpeed = 0.1f;

    void Start()
    {
        downloadButton.onClick.AddListener(StartDownload);
        downloadSlider.value = 0;
        percentageText.text = "0%";
        //downloadPanel.SetActive(false);
    }

    void StartDownload()
    {
        if (!isDownloading)
        {
            StartCoroutine(DownloadCoroutine());
        }
    }

    IEnumerator DownloadCoroutine()
    {
        isDownloading = true;
        downloadButton.interactable = false;

        while (downloadSlider.value < downloadSlider.maxValue)
        {
            downloadSlider.value += downloadSpeed * Time.deltaTime;
            percentageText.text = Mathf.FloorToInt(downloadSlider.value * 100) + "%";
            yield return null;
        }

        percentageText.text = "100%";
        yield return new WaitForSeconds(1);

        downloadPanel.SetActive(false);
        isDownloading = false;
        downloadButton.interactable = true;
        CompleteTask();
    }

    void CompleteTask()
    {

        Debug.Log("Download Task Complete");
        taskManager.numOfTasksCompleted++;
    }

    public void ResetDownload()
    {
        StopAllCoroutines();
        isDownloading = false;
        downloadSlider.value = 0;
        percentageText.text = "0%";
        downloadButton.interactable = true;
    }
}
