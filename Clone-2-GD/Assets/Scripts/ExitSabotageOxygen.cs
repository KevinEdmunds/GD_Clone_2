using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSabotageOxygen : MonoBehaviour
{
    public GameObject SabotageO2Panel1;
    public GameObject SabotageO2Panel2;

    public void ExitO2Panel1()
    {
        SabotageO2Panel1.SetActive(false);
    }

    public void ExitO2Panel2()
    {
        SabotageO2Panel2.SetActive(false);
    }
}
