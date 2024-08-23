using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class PlayerType : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnRoleChange))]
    public bool isImposter = false;

    [SyncVar(hook = nameof(SetAliveState))]
    public bool isAlive = true;

    public PlayerActions playerActions;
    public PlayerHUD playerHUD;

    PlayerSceneDataTransfer playerData;

    public PlayerLobbyManager playerLobby;

    Text playerNameText;

    [Command(requiresAuthority =false)]
    public void SetPlayerRole(bool state)
    {
        //Debug.Log("Changing Roles  ");
        isImposter = state;
    }
    public bool GetPlayerType()
    {
        return isImposter;
    }
    // Hook method for when isImposter changes
    void OnRoleChange(bool oldState, bool newState)
    {
        if (isLocalPlayer)
        {
            playerHUD.UpdateHUD(newState); // Pass the newState (isImposter) to the HUD
        }
    }

    // Hook method for when isAlive changes
    void SetAliveState(bool oldState, bool newState)
    {
        GetComponent<SpriteRenderer>().color = newState ? Color.white : Color.black;
    }

    private void Start()
    {
        if(isLocalPlayer)
        {
            //debug.Log("Loading into the scene");
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName != "Lobby")
            {
                //debug.Log("The player is not in the lobby");
                playerData = FindObjectOfType<PlayerSceneDataTransfer>();
                //debug.Log("The player has their data");
                UploadPlayerData();
                //debug.Log("The player's data has been uploaded to the new scene");
            }
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        playerNameText= this.GetComponent<PlayerLobbyManager>().playerNameText;
    }


    //[Command(requiresAuthority =false)]
    void UploadPlayerData()
    {
        //debug.Log(this.name + " is telling the server;");
        UpdatePlayerData();
    }

   // [ClientRpc]
    void UpdatePlayerData()
    {
        playerLobby.CmdChangeColor(playerData.GetPlayerColor());
        playerLobby.CmdChangeName(playerData.GetPlayerName());
    }
}
