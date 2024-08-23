using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSabotageReactor : MonoBehaviour
{
    public GameObject SabotageReactorPanel1;
    public GameObject SabotageReactorPanel2;

    public void ExitReactorPanel1()
    {
        SabotageReactorPanel1.SetActive(false);
    }

    public void ExitReactorPanel2()
    {
        SabotageReactorPanel2.SetActive(false);
    }
}
