using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSceneDataTransfer : MonoBehaviour
{
    [SerializeField]
    private Color playerColor;
    [SerializeField]
    private string playerName;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayerColor(Color col)
    {
        playerColor = col;
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public Color GetPlayerColor()
    {
        return playerColor;
    }
    public string GetPlayerName()
    {
        return playerName;
    }
}
