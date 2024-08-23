using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerLobbyManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetPlayerName))]
    public string playerName;

    public GameObject playerLobbyHUD;
    public Text playerNameText;
    public bool playerNameReady = false;
    public Button startGameButton;
    public GameObject hostButton;
    public Button submitName;
    public InputField nameInput;

    private void Start()
    {
        if (isLocalPlayer)
        {
            playerLobbyHUD.SetActive(true);
        }
        else
        {
            playerLobbyHUD.SetActive(false);
        }

        submitName.onClick.AddListener(SendName);
    }

    public void SendName()
    {
        if (!string.IsNullOrWhiteSpace(nameInput.text))
        {
            CmdChangeName(nameInput.text);
            nameInput.text = "";
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        playerName = newName;
    }

    public void SetPlayerName(string oldName, string newName)
    {
        playerNameText.text = newName;
        playerNameReady = true;
    }
}
