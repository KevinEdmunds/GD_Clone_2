using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCalibrateDisButton : MonoBehaviour
{
    public GameObject calibrateDisPanel;
    public CalibrateDistributorTask calibrateDistributorTask;

    public void ExitCalibrateTask()
    {
       
        calibrateDisPanel.SetActive(false);
    }
}
