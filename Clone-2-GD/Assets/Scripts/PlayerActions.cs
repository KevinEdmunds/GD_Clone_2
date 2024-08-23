using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerActions : NetworkBehaviour
{
    public bool canVent = false;
    public bool inVent = false;
    public bool canKill = false;
    public bool canMove = false;
    public GameObject currentVent = null;
    public GameObject killTarget;
    public KeyCode interactKey;
    public PlayerType playerType;
    public Transform thisPlayerHitbox;
    public PlayerHUD playerHUD;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerType.isImposter)
        {
            if(isLocalPlayer)
            {
                if (collision.tag == "Vent")
                {
                    canVent = true;
                    currentVent = collision.gameObject;
                    playerHUD.ventButton.interactable = true;
                }
            }
            if (collision.tag == "PlayerHitbox"&&collision.transform.parent.gameObject.GetComponent<PlayerType>().isAlive)
            {
                canKill = true;
                playerHUD.killButton.interactable = true;
                killTarget = collision.transform.parent.gameObject;
            }
        }
        if(collision.tag=="PlayerHitbox")
        {
            GameObject otherPlayer = collision.transform.parent.gameObject;
            if(!otherPlayer.GetComponent<PlayerType>().isAlive)
            {
                playerHUD.reportButton.interactable = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerType.isImposter)
        {
            if (isLocalPlayer)
            {
                if (collision.tag == "Vent")
                {
                    canVent = false;
                    currentVent = null;
                    playerHUD.ventButton.interactable = false;
                }
            }
            if (collision.tag == "PlayerHitbox")
            {
                canKill = false;
                playerHUD.killButton.interactable = false;
                killTarget = null;
            }
        }
        if (collision.tag == "PlayerHitbox")
        {
            GameObject otherPlayer = collision.transform.parent.gameObject;
            if (!otherPlayer.GetComponent<PlayerType>().isAlive)
            {
                playerHUD.reportButton.interactable = false;
            }
        }
    }
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

    }
    private void Update()
    {
        if (canVent && Input.GetKeyDown(interactKey) && !inVent)
        {
            UpdateVentState(true);
        }
        else if (inVent && Input.GetKeyDown(interactKey))
        {
            UpdateVentState(false);
        }
    }

    [Command(requiresAuthority =false)]
    public void KillTarget()
    {
        killTarget.GetComponent<PlayerType>().isAlive = false;
        MoveImposter(killTarget.transform.position);
    }
    [ClientRpc]
    public void MoveImposter(Vector3 newPos)
    {
        this.transform.position = newPos;
    }

    IEnumerator KillTimer(int duration)
    {
        int i = duration;
        while(i>0)
        {
            yield return new WaitForSeconds(1);
        }
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
