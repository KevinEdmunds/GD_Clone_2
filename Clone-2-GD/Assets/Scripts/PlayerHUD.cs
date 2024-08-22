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
    public GameObject viewLimiter;
    public Button ventButton, useButton, reportButton, killButton, sabotageButton;

    // Initialize HUD elements
    void Start()        //sets the player object up when the game begins
    {
        if(isLocalPlayer)
        {
            viewLimiter.SetActive(true);
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent = this.transform;
          //  playerHUD.SetActive(true);
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

    public void UseButton()
    {

    }

    public void Sabotage()
    {
        //link to command to spawn "task" for player
    }
}
