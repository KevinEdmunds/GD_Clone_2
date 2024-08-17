using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class GameManagerVS : NetworkBehaviour
{
    [SerializeField]
    [SyncVar(hook = nameof(PlayerCountChanged))]
    public int PlayerCount = 0;

    [SerializeField]
    [SyncVar(hook = nameof(GameStateChanged))]
    public GameState CurrentGameState;


    [SerializeField]
    public PlayerManager PLManager;

    [SerializeField]
    private TMP_Text PlayerNumbers, PlayerID;

    public enum GameState
    {
        Normal,
        Voting
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameState.Normal;
    }

    // Update is called once per frame
    void Update()
    {

        PlayerNumbers.text = PlayerCount.ToString();
        PlayerID.text = PLManager.PlayerId.ToString();


    }

    [Command(requiresAuthority = false)]
   public void AddPlayerCount()
    {
        PlayerCount += 1;

        
    }

    [Command(requiresAuthority = false)]
    public void RemovePlayer()
    {
        PlayerCount -= 1;
    }

    void PlayerCountChanged(int OldValue, int NewValue)
    {
        Debug.Log("AmountOfPlayers changed from: " + OldValue + " to:  " + NewValue);

        if (OldValue > NewValue &&
            PLManager.PlayerId >= OldValue)
        {
            PLManager.PlayerId -= 1;
        }
    }

    void GameStateChanged(GameState OldState, GameState NewState)
    {
        Debug.Log("GameState changed from: " + OldState + " to: " + NewState);
    }
}
