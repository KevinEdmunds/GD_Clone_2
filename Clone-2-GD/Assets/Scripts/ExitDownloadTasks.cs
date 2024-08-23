using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDownloadTasks : MonoBehaviour
{
    public GameObject downloadTaskPanel;
    public DownloadTask downloadTask;

    void Start()
    {

    }

    public void ExitDownloadTask()
    {
        downloadTask.ResetDownload();
        downloadTaskPanel.SetActive(false);
    }
}
