using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerLobbyManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetPlayerName))]
    public string playerName="";

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor=Color.white;

    [SyncVar (hook = nameof(OnPlayerReady))]
    public bool playerReady = false;

    public GameObject playerLobbyHUD;
    public Text playerNameText;
    public bool playerNameReady = false;
    public bool playerColorReady = false;
    public Button startGameButton;
    public GameObject hostButton;
    public Button submitName;
    public InputField nameInput;

    public Text playerReadyText;

    public GameObject[] players;
    public List<Color> playerColors = new List<Color>();

    // List of available colors on the server
    private List<Color> availableColors = new List<Color>
    {
        Color.red, Color.blue, Color.green, Color.yellow,
       Color.magenta, Color.cyan, new Color(1.0f, 0.5f, 0.0f), Color.gray,
        new Color(0.5f, 0.25f, 0.75f), new Color(1.0f, 0.75f, 0.8f)
    };


    public List<Button> buttonList = new List<Button>();
    public GameObject colorButton;
    public GameObject colorPalette;
    PlayerSceneDataTransfer playerData;

    private void Start()
    {
        if (isLocalPlayer)
        {
            playerData = GameObject.FindObjectOfType<PlayerSceneDataTransfer>();
        }
        else
        {
            playerLobbyHUD.SetActive(false);
        }

        if(!isServer)
        {
            hostButton.SetActive(false);
        }

        submitName.onClick.AddListener(SendName);
        CreateColorList();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(isLocalPlayer)
        {
            UpdateAllAvailableColors(); // Update UI based on available colors
        }
        if (isServer&&isLocalPlayer)       // if the player is the host
        {
            hostButton.GetComponent<Button>().interactable = false;
           
            LobbyManager lobby = GameObject.FindObjectOfType<LobbyManager>();
            lobby.host = this;
        }
    }
    public void UpdateHostButton(bool state)
    {
        //Debug.Log("button updated : "+this.gameObject.name);
        hostButton.GetComponent<Button>().interactable = state;
    }


    void CreateColorList()
    {
        foreach (Color col in availableColors)
        {
            GameObject colButton = Instantiate(colorButton, colorPalette.transform);
            colButton.GetComponent<Image>().color = col;
            colButton.GetComponent<Button>().onClick.AddListener(() => CmdChangeColor(col));
            buttonList.Add(colButton.GetComponent<Button>());
        }
    }

    public void SendName()
    {
        if (!string.IsNullOrWhiteSpace(nameInput.text))
        {
            playerData.SetPlayerName(nameInput.text);
            CmdChangeName(nameInput.text);
            nameInput.text = "";
        }
    }

    [Command(requiresAuthority =false)]
    public void CheckPlayerReady()
    {
        //Debug.Log("Checking that the player has a name: " + !string.IsNullOrWhiteSpace(playerName) +" and checking that the player has a color" +  (playerColor != Color.white));
  
        if(playerNameReady)
        {
            if (playerColorReady)
            {
                playerReady = true;
            }
            else
            {
                playerReady = false;
            }
        }
        else
        {
            playerReady = false;
        }
    }

    void OnPlayerReady(bool oldState, bool newState)
    {
        playerReadyText.text = "This player is ready";
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        playerName = newName;
        CheckPlayerReady();
    }

    public void SetPlayerName(string oldName, string newName)
    {
        playerNameText.text = newName;
        playerNameReady = true;
    }

    [Command]
    public void CmdChangeColor(Color newColor)
    {
        playerColor = newColor;
        CheckPlayerReady();
        StorePlayerColor(playerColor);
    }
    [ClientRpc]
    public void StorePlayerColor(Color col)
    {
        if(isLocalPlayer)
        {
            playerData.SetPlayerColor(col);
        }

    }

    void UpdateAllAvailableColors()
    {
        if(isLocalPlayer)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            playerColors.Clear();
            foreach (GameObject player in players)
            {
                playerColors.Add(player.GetComponent<SpriteRenderer>().color);
            }

            foreach (Button button in buttonList)
            {
                Color buttonColor = button.GetComponent<Image>().color;
                button.interactable = !playerColors.Contains(buttonColor);
            }
        }
    }

    void OnColorChanged(Color oldColor, Color newColor)
    {
        playerColorReady = true;
        UpdateColor(newColor);

        PlayerLobbyManager[] pLM = FindObjectsOfType<PlayerLobbyManager>();

        foreach(PlayerLobbyManager instance in pLM)
        {
            instance.UpdateAllAvailableColors();
        }
    }

    void UpdateColor(Color col)
    {
        // Change the player's sprite color
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = col;
        }
    }

    [Command(requiresAuthority =false)]
    public void StartTheGame()
    {
        if(NetworkServer.active)
        {
            NetworkManager.singleton.ServerChangeScene("SampleScene");
        }
    }
}
