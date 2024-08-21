using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UseButtonHandler : MonoBehaviour
{
    public GameObject cardSwipeTaskPanel;
    public CardSwipeTask cardSwipeTask;

    void Start()
    {
        
        cardSwipeTaskPanel.SetActive(false);

        
        GetComponent<Button>().onClick.AddListener(UseTask);
    }

    void UseTask()
    {
        if (cardSwipeTask.canDoTask)
        {
            cardSwipeTaskPanel.SetActive(true);
        }
     
    }

}