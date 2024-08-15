
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public GameObject[] ventList;
    public GameObject arrow;
    public GameObject ventPanel;
    public List<GameObject> arrowList = new List<GameObject>();
    public float arrowDistanceFromCentre;
    private void Start()
    {
        ventList = GameObject.FindGameObjectsWithTag("Vent");
        ShowOtherVents();
        ventPanel.SetActive(false);
    }

    private void ShowOtherVents()
    {
        foreach (GameObject vent in ventList)
        {
            if (vent != this.gameObject)
            {
                GameObject ventDirection = Instantiate(arrow, ventPanel.transform);
                arrowList.Add(ventDirection);
                ventDirection.GetComponent<VentArrowButton>().target = vent;

                Vector2 direction = (vent.transform.position - this.transform.position).normalized;
                ventDirection.transform.localPosition = direction * arrowDistanceFromCentre;
                
                float directionAngle = Mathf.Atan2(direction.y, direction.x)*180/Mathf.PI;
                ventDirection.transform.rotation = Quaternion.Euler(0, 0, directionAngle-90);
            }
        }
    }
}
