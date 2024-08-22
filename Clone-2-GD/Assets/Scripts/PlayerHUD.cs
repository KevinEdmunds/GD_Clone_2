using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.UI;

public class PlayerHUD : NetworkBehaviour
{
    public GameObject imposterHUD, playerHUD;
    public PlayerType playerType;
    public PlayerActions playerActions;
    public GameObject lobbyHUD;
    public GameObject viewLimiter;
    public Button ventButton, useButton, reportButton, killButton, sabotageButton;

    
    private void Start()
    {

        if (isLocalPlayer)
        {
            Debug.Log("hello");
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "Lobby")
            {
                Debug.Log("in the lobby");
                SetUpLobbyHUD();
            }
            else
            {
                Debug.Log("in the game");
                SetUpGameHUD();
            }
            viewLimiter.SetActive(true);
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent = this.transform;
        }
    }
    void SetUpLobbyHUD()
    {
        lobbyHUD.SetActive(true);
    }
    void SetUpGameHUD()
    {
        playerHUD.SetActive(true);
        ventButton.interactable = false;
        killButton.interactable = false;
        reportButton.interactable = false;
        useButton.interactable = false;
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
