using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitTaskButton : MonoBehaviour
{
    public GameObject taskPanel;


    void Start()
    {

       GetComponent<Button>().onClick.AddListener(ExitCardTask);
    }

    public void ExitCardTask()
    {
        taskPanel.gameObject.SetActive(false);
    }


}
