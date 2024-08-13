using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{

    public GameObject[] ventList;
    public GameObject arrow;
    public List<GameObject> arrowList = new List<GameObject>();
    public float arrowDistanceFromCentre;
    private void Start()
    {
        ventList = GameObject.FindGameObjectsWithTag("Vent");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerActions currentPlayer = collision.GetComponent<PlayerActions>();
        if (currentPlayer.isImposter)
        {
            ShowOtherVents();
        }
    }
    private void ShowOtherVents()
    {
        foreach (GameObject vent in ventList)
        {
            if (vent != this.gameObject)
            {
                Vector2 direction = vent.transform.position - this.transform.position;
                GameObject ventDirection = Instantiate(arrow, this.transform);
                ventDirection.transform.localPosition = direction * arrowDistanceFromCentre;
                float directionAngle = Mathf.Atan2(direction.y, direction.x);
                ventDirection.transform.rotation = Quaternion.Euler(0, 0, directionAngle);
                arrowList.Add(ventDirection);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerActions currentPlayer = collision.GetComponent<PlayerActions>();
        if (currentPlayer.isImposter)
        {

        }
    }
    private void HideAllVents()
    {
        foreach (GameObject arrow in arrowList)
        {
            Destroy(arrow);
        }
        arrowList.Clear();
    }
}
