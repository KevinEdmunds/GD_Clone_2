using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerHUD : NetworkBehaviour
{
    public GameObject imposterHUD, playerHUD;
    public PlayerType playerType;
    public PlayerActions playerActions;
    public Button ventButton, useButton, reportButton, killButton, sabotageButton;

    // Initialize HUD elements
    void Start()
    {
        if(isLocalPlayer)
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent = this.transform;
            playerHUD.SetActive(true);
            ventButton.interactable = false;
            killButton.interactable = false;
            reportButton.interactable = false;
            useButton.interactable = false;
        }
    }

    public void KillButton()
    {
        playerActions.KillTarget();
    }

    public void UpdateHUD(bool isImposter)
    {
        imposterHUD.SetActive(isImposter);
    }

    public void ReportDeath()
    {
        //Link to command to call logic to start vote
    }
}
