using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerLobbyManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetPlayerName))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor;

    public GameObject playerLobbyHUD;
    public Text playerNameText;
    public bool playerNameReady = false;
    public Button startGameButton;
    public GameObject hostButton;
    public Button submitName;
    public InputField nameInput;

    public GameObject[] players;
    public List<Color> playerColors = new List<Color>();

    // List of available colors on the server
    private List<Color> availableColors = new List<Color>
    {
        Color.red, Color.blue, Color.green, Color.yellow,
        Color.magenta, Color.cyan, new Color(1.0f, 0.5f, 0.0f), Color.gray
    };

    public List<Button> buttonList = new List<Button>();
    public GameObject colorButton;
    public GameObject colorPalette;

    private void Start()
    {
        if (isLocalPlayer)
        {
            playerLobbyHUD.SetActive(true);
            UpdateAvailableColors(); // Update UI based on available colors
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

    void CreateColorList()
    {
        foreach (Color col in availableColors)
        {
            GameObject colButton = Instantiate(colorButton, colorPalette.transform);
            colButton.GetComponent<Image>().color = col;

            // Add an event listener to the button for changing color
            colButton.GetComponent<Button>().onClick.AddListener(() => CmdChangeColor(col));
            buttonList.Add(colButton.GetComponent<Button>());
        }
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

    [Command]
    public void CmdChangeColor(Color newColor)
    {
        playerColor = newColor;
    }

    void UpdateAllAvailableColors()
    {
        Debug.Log(this.name);
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

    void UpdateAvailableColors()
    {
        // Request the server to update the available colors
        if (isLocalPlayer)
        {
            CmdChangeColor(playerColor);
        }
    }
}
