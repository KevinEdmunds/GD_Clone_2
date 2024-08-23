using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UploadTask : MonoBehaviour
{
    public Button uploadButton;
    public Slider uploadSlider;
    public GameObject uploadPanel;
    public TextMeshProUGUI percentageText;
    public TaskManager taskManager;
    private bool isuploading = false;
    private float uploadSpeed = 0.1f;

    void Start()
    {
        uploadButton.onClick.AddListener(Startupload);
        uploadSlider.value = 0;
        percentageText.text = "0%";
       // uploadPanel.SetActive(true);
    }

    void Startupload()
    {
        if (!isuploading)
        {
            StartCoroutine(UploadCoroutine());
        }
    }

    IEnumerator UploadCoroutine()
    {
        isuploading = true;
        uploadButton.interactable = false;

        while (uploadSlider.value < uploadSlider.maxValue)
        {
            uploadSlider.value += uploadSpeed * Time.deltaTime;
            percentageText.text = Mathf.FloorToInt(uploadSlider.value * 100) + "%";
            yield return null;
        }

        percentageText.text = "100%";
        yield return new WaitForSeconds(1);

        uploadPanel.SetActive(false);
        isuploading = false;
        uploadButton.interactable = true;
        CompleteTask();
    }

    void CompleteTask()
    {
        Debug.Log("Upload Task Complete");
        taskManager.numOfTasksCompleted++;
    }

    public void ResetUpload()
    {
        StopAllCoroutines();
        isuploading = false;
        uploadSlider.value = 0;
        percentageText.text = "0%";
        uploadButton.interactable = true;
    }
}
