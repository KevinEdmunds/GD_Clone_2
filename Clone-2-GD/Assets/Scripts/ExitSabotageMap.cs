using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitSabotageMap : MonoBehaviour
{
    public GameObject SabotagePanel;


    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ExitSabotageMapButton);
    }

    void ExitSabotageMapButton()
    {
        SabotagePanel.gameObject.SetActive(false);
    }


}
