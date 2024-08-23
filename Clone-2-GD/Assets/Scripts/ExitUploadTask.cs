using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUploadTask : MonoBehaviour
{
    public GameObject uploadTaskPanel;
    public UploadTask uploadTask;

 
    //
    
    public void ExitUploadTask1()
    {
        uploadTask.ResetUpload();
        uploadTaskPanel.SetActive(false);
    }
}
