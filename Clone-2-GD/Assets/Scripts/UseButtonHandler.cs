using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseButtonHandler : MonoBehaviour
{
    public GameObject cardSwipeTaskPanel;

    void Start()
    {
        
        cardSwipeTaskPanel.SetActive(false);

        
        GetComponent<Button>().onClick.AddListener(UseTask);
    }

    void UseTask()
    {
       
        cardSwipeTaskPanel.SetActive(true);
    }

}