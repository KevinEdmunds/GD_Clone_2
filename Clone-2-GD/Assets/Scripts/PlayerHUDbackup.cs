using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.UI;

public class PlayerHUDbackup : NetworkBehaviour
{
    public GameObject imposterHUD, playerHUD;
    public PlayerType playerType;
    public PlayerActions playerActions;
    public GameObject lobbyHUD;
    public GameObject viewLimiter;
    public GameObject playerRolePanel;
    public Text playerRoleText;
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
        ShowPlayerRole();
    }

    void ShowPlayerRole()
    {
        StartCoroutine(ShowRolePanel());
    }
    IEnumerator ShowRolePanel()
    {
        playerActions.canMove = false;
        playerRolePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        playerRoleText.text = "You are " + (playerType.isImposter ?  " the Imposter": "a Crewmate");
        playerRoleText.color = playerType.isImposter ? Color.blue : Color.red;
        yield return new WaitForSeconds(3);
        playerRolePanel.SetActive(false);
        playerActions.canMove = true;
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
