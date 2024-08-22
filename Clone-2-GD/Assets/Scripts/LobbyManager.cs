using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class LobbyManager : NetworkBehaviour
{
    public int playerCount=0;

    public int minPlayers=4, maxPlayers=8;

    public Button hostButton;
    public PlayerLobbyManager host;

    public bool enoughPlayers = false;
    public Text playerCountText;
    public bool gameReady=false;
    void OnPlayerCountChanged()
    {
        enoughPlayers = (playerCount >= minPlayers && playerCount <= maxPlayers);
    }

    private void FixedUpdate()
    {
        if (NetworkServer.active)
        {
            playerCount = NetworkServer.connections.Count;
            OnPlayerCountChanged();
        }
        if(enoughPlayers)
        {
            CheckIfGameIsReady();
        }
    }


    private void CheckIfGameIsReady()
    {
        PlayerLobbyManager[] playersInLobby= FindObjectsOfType<PlayerLobbyManager>();

        foreach(PlayerLobbyManager player in playersInLobby)
        {
            if (!player.playerReady)
            {
                return;
            }
        }
        Debug.Log("The game can Start");
        host.UpdateHostButton();
    }
}
