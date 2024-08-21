using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;

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
    private MyNetworkBehaviour myNetwork;

    [SerializeField]
    private TMP_Text PlayerNumbers, PlayerIDText, TimerText;



    [SerializeField]
    private GameObject VotingScreen, PLPanelPrefab;
    [SerializeField]
    private Transform ContentParent;

    public bool PlayerSpawned = false;

    [SerializeField]
    [SyncVar(hook = nameof(TimerChanged))]
    private float TimeLeft;
    [SerializeField]
    private float SetTime, minutes, seconds;
    [SyncVar]
    private bool TimerOn = false;
    public enum GameState
    {
        Normal,
        Voting
    }

    // Start is called before the first frame update
    void Start()
    {
        // NetworkServer.Spawn(gameObject);
        CurrentGameState = GameState.Normal;
        Debug.Log("Server is: " + isServer);

        //  VotingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        PlayerNumbers.text = PlayerCount.ToString();

        if (PlayerSpawned == true)
        {
            PlayerIDText.text = PLManager.PlayerId.ToString();
        }

        if (TimerOn == true)
        {
            Countdown();
        }

    }

   
    void Countdown()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            UpdateTime(TimeLeft);
            
        }

        else
        {
            VoteDone();
        }
    }

    void UpdateTime(float currenttime)
    {
        currenttime += 1;

        minutes = Mathf.FloorToInt(currenttime / 60);
        seconds = Mathf.FloorToInt(currenttime % 60);

        
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


        if (NewState == GameState.Normal)
        {
           
        }

        else
        {
            
            VotingScreen.SetActive(true);

            //SetGameToVoting();
        }
    }

    [Command(requiresAuthority = false)]
    private void SetGameToVoting()
    {
        CurrentGameState = GameManagerVS.GameState.Voting;
        VotingScreen.SetActive(true);

        for (int i = 0; i < PlayerCount; i++)
        {
            GameObject Panel = Instantiate(PLPanelPrefab, ContentParent);
            Panel.GetComponent<PlayerPanelScript>().PanelId = i + 1;
            myNetwork.PutPanelsOnServer(Panel);
        }
        TimeLeft = SetTime;
        TimerOn = true;

    }

    [Command(requiresAuthority = false)]
    private void SetGameToNormal()
    {
        //VotingScreen.SetActive(false);
        CurrentGameState = GameManagerVS.GameState.Normal;

        foreach (Transform child in ContentParent)
        {
            Destroy(child.gameObject);
        }
        TimerOn = false;
        TimeLeft = SetTime;
        VotingScreen.SetActive(false);
      // PLManager.HAsVoted = false;

    }

    //[Command(requiresAuthority = false)]
    public void ChangeStateOfGame()
    {

        if (CurrentGameState == GameManagerVS.GameState.Normal)
        {

            CurrentGameState = GameManagerVS.GameState.Voting;
            VotingScreen.SetActive(true);
            SetGameToVoting();

        }

        else
        {

            CurrentGameState = GameManagerVS.GameState.Normal;
            TimerOn = false;
            VotingScreen.SetActive(false);
            SetGameToNormal();
        }
    }


    public void PlayerHasVoted()
    {
        PLManager.HAsVoted = true;

    }


    public void skipVote()
    {
        PlayerHasVoted();

        foreach (Transform transform in ContentParent)
        {
            transform.gameObject.GetComponent<PlayerPanelScript>().button.SetActive(false);
        }
    }

   
    public void VoteDone()
    {

        //    PlayerHasVoted();
        //    Debug.Log("KnoppieGedruk");
        //    List<Transform> MostVotes = new List<Transform>();
        //    int highestVotes = 0;

        //    foreach (Transform playerVotes in ContentParent)
        //    {
        //        int Total = playerVotes.gameObject.GetComponent<PlayerPanelScript>().AmountOfvotes;

        //        if (Total == highestVotes &&
        //            Total > 0)
        //        {
        //            MostVotes.Add(playerVotes);
        //            highestVotes = Total;
        //        }

        //        else if (Total > highestVotes)
        //        {
        //            MostVotes.Clear();
        //            MostVotes.Add(playerVotes);
        //            highestVotes = Total;
        //        }
        //    }

        //    Debug.Log(MostVotes.Count + " checking count");
        //    if (MostVotes.Count == 1)
        //    {
        //        int Pos = MostVotes[0].gameObject.GetComponent<PlayerPanelScript>().PanelId;

        //        if (Pos == PLManager.PlayerId)
        //        {
        //            PLManager.IsAlive = false;
        //            Debug.Log(PLManager.gameObject);
        //        }
        //    }

        //    else
        //    {
        //        Debug.Log("Nobody dies");
        //    }

        //    ChangeStateOfGame();
        //}

        if (!NetworkServer.active)
        {
            FunctionFromHost();
         

        }

        else
        {
            FuntionOnClients();
        }
    }

    [Command(requiresAuthority = false)]
    public void FunctionFromHost()
    {
        FuntionOnClients();
        
    }

    [ClientRpc]
    private void FuntionOnClients()
    {
        PlayerHasVoted();
        
        List<Transform> MostVotes = new List<Transform>();
        int highestVotes = 0;

        foreach (Transform playerVotes in ContentParent)
        {
            int Total = playerVotes.gameObject.GetComponent<PlayerPanelScript>().AmountOfvotes;

            if (Total == highestVotes &&
                Total > 0)
            {
                MostVotes.Add(playerVotes);
                highestVotes = Total;
            }

            else if (Total > highestVotes)
            {
                
                MostVotes.Clear();
                MostVotes.Add(playerVotes);
                highestVotes = Total;
            }
        }

        
        if (MostVotes.Count == 1)
        {
            int Pos = MostVotes[0].gameObject.GetComponent<PlayerPanelScript>().PanelId;

            if (Pos == PLManager.PlayerId)
            {
                PLManager.IsAlive = false;
                Debug.Log(PLManager.gameObject);
            }
        }

        else
        {
            
        }

        ChangeStateOfGame();
    }

    void TimerChanged(float oldV, float newV)
    {
        
    }
}
