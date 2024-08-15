using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public bool isImposter = false;
    public bool canVent = false;
    public bool inVent = false;
    public GameObject currentVent = null;
    public KeyCode interactKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isImposter && collision.tag == "Vent")
        {
            //currentVent.GetComponent<Vent>().ventPanel.SetActive(true);
            canVent = true;
            currentVent = collision.gameObject;
        }
    }
    private void Update()
    {
        if (canVent && Input.GetKeyDown(interactKey)&&!inVent)
        {
            UpdateVentState(true);
        }else if (inVent && Input.GetKeyDown(interactKey))
        {
            UpdateVentState(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (isImposter && collision.tag == "Vent")
        {
            canVent = false;
            currentVent.GetComponent<Vent>().ventPanel.SetActive(false);
            currentVent = null;
        }*/
    }
    public void UpdateVentState(bool state)
    {
        inVent = state;
        this.GetComponent<SpriteRenderer>().enabled = !state;
        currentVent.GetComponent<Vent>().ventPanel.SetActive(state);
    }
    public void TransportPlayerVent(ref GameObject destination)
    {
        this.transform.position = destination.transform.position;
        currentVent.GetComponent<Vent>().ventPanel.SetActive(false);
        currentVent = destination;
        currentVent.GetComponent<Vent>().ventPanel.SetActive(true);
        Debug.Log(currentVent.GetComponent<Vent>().ventPanel.activeInHierarchy);
    }
}
