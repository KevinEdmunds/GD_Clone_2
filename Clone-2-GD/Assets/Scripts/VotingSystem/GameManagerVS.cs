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
    [SyncVar]
    public int PlayerLeftAlive = 0;

    //[SerializeField]
    //public Transform playerParent;

    
    [SerializeField]
    [SyncVar(hook = nameof(GameStateChanged))]
    public GameState CurrentGameState;


    [SerializeField]
    public PlayerManager PLManager;

    [SerializeField]
    private MyNetworkBehaviour myNetwork;

    [SerializeField]
    private TMP_Text PlayerNumbers, PlayerIDText, TimerText, EjectedMessage;



    [SerializeField]
    private GameObject VotingScreen, PLPanelPrefab, EjectPlayerScreen, ProceedButton, ImposterWonScreen, CrewWonScreen;
    [SerializeField]
    private Transform ContentParent;

    public bool PlayerSpawned = false;

    [SerializeField]
    [SyncVar(hook = nameof(TimerChanged))]
    private float TimeLeft, DisplayTime;
    [SerializeField]
    private float SetTime, minutes, seconds, SetSpaceTimer;
    [SyncVar]
    private bool TimerOn = false, TimerDPOn = false;
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
        
       // Debug.Log("Server is: " + isServer);

        //  VotingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
       // PlayerNumbers.text = PlayerCount.ToString();

        //if (PlayerSpawned == true)
        //{
        //    PlayerIDText.text = PLManager.PlayerId.ToString();
        //}

        if (TimerOn == true)
        {
            Countdown();
        }


        if (DisplayTime > 0 &&
            TimerDPOn == true)
        {
            DisplayTime -= Time.deltaTime;

            if (DisplayTime < 0)
            {
                SetSpaceOf();
            }
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
    public void CMdAddPlayerCount()
    {
        PlayerCount += 1;

    }

    [Command(requiresAuthority = false)]
    public void CMdRemovePlayer()
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
    private void CmdSetGameToVoting()
    {
        CurrentGameState = GameManagerVS.GameState.Voting;
        VotingScreen.SetActive(true);
        ProceedButton.SetActive(false);

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
    private void CmdSetGameToNormal()
    {
        //VotingScreen.SetActive(false);
        ProceedButton.SetActive(false);
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
       // Debug.Log("Eenkeer");
        if (CurrentGameState == GameManagerVS.GameState.Normal)
        {
           // Debug.Log("Normal");
            CurrentGameState = GameManagerVS.GameState.Voting;
            VotingScreen.SetActive(true);
            ProceedButton.SetActive(false);
            PLManager.CmdCheckStatus();
            CmdSetGameToVoting();

        }

        else
        {
            // Debug.Log("Voting");
            ProceedButton.SetActive(false);
            CurrentGameState = GameManagerVS.GameState.Normal;
            TimerOn = false;
            VotingScreen.SetActive(false);
            CmdSetGameToNormal();
        }
    }

    //[Command(requiresAuthority = false)]
    public void PlayerHasVoted()
    {
       PLManager.ThisPlayerVoted();
    }

    public int GetPlayerCount()
    {
        return PlayerCount;
    }

    public void skipVote()
    {
        PlayerHasVoted();
       

        foreach (Transform transform in ContentParent)
        {
            transform.gameObject.GetComponent<PlayerPanelScript>().button.SetActive(false);

            if (transform.gameObject.GetComponent<PlayerPanelScript>().PanelId == PLManager.PlayerId)
            {
                transform.gameObject.GetComponent<PlayerPanelScript>().CMdThisPlayerVoted();

            }
        }

        CheckIfEveryOneVoted();
    }

    private void CheckIfEveryOneVoted()
    {
        bool everyOneVoted = true;
        GameObject[] playerParent = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject transform in playerParent)
        {
            PlayerManager playerManager = transform.gameObject.GetComponent<PlayerManager>();

            if (!playerManager.HAsVoted)
            {
                everyOneVoted = false;
            }
        }

        if (everyOneVoted)
        {
            VoteDone();
        }
    }

   
    public void VoteDone()
    {
        ShowVotes();
        ProceedButton.SetActive(true);
            
    }

    private void ShowVotes()
    {
        //foreach (Transform transform in ContentParent)
        //{
        //    int Totalvotes = transform.gameObject.GetComponent<PlayerPanelScript>().AmountOfvotes;
        //    Transform Parent = transform.gameObject.GetComponent<PlayerPanelScript>().HowManyVotes.transform;

        //    for (int i = 0; i < Totalvotes; i++)
        //    {
        //        Parent.GetChild(i).gameObject.SetActive(true);
        //    }
        //}
    }

    public void PressProceedDone()
    {

        if (!NetworkServer.active)
        {
            CmdFunctionFromHost();
         

        }

        else
        {
            CRpcFuntionOnClients();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdFunctionFromHost()
    {
        CRpcFuntionOnClients();
        
    }

    [ClientRpc]
    private void CRpcFuntionOnClients()
    {
     //   PlayerHasVoted();
        
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
                PLManager.CmdChangeStatus();
                Debug.Log(PLManager.gameObject);
                KillPlayerScreen(PLManager.gameObject);
            }
        }

        else
        {
            NobodyWasKilledScreen();
        }

       // CmdCheckAmountofPlayersAlive();
        
       //  KillPlayerScreen();

       // ChangeStateOfGame();
    }

    private void NobodyWasKilledScreen()
    {
        EjectPlayerScreen.SetActive(true);
        EjectedMessage.text = "Nobody was Ejected";
        SetSpaceOn();
        
    }

    [Command(requiresAuthority =false)]
    private void SetSpaceOn()
    {
        DisplayTime = SetSpaceTimer;
        TimerDPOn = true;
    }

    [Command(requiresAuthority = false)]
    private void SetSpaceOf()
    {
        TimerDPOn = false;
        EjectPlayerScreen.SetActive(false);
        CmdCheckAmountofPlayersAlive();
    }

    [Command(requiresAuthority = false)]
    private void KillPlayerScreen(GameObject PLEjected)
    {
        EjectPlayerScreen.SetActive(true);
        bool isTheImposter = PLEjected.GetComponent<PlayerType>().isImposter;

        if (isTheImposter)
        {
            EjectedMessage.text = PLEjected.GetComponent<PlayerLobbyManager>().playerName + " was the imposter";
            SetSpaceOn();
        }

        else
        {
            EjectedMessage.text = PLEjected.GetComponent<PlayerLobbyManager>().playerName + " was not the imposter";
            SetSpaceOn();
        }
        
        
        

        //EjectedMessage.text = PLEjected.GetComponent<PlayerManager>().;
      //  ChangeStateOfGame();
    }

    [Command(requiresAuthority = false)]
    private void CmdCheckAmountofPlayersAlive()
    {
        PlayerLeftAlive = 0;
        int AddPlayer = 0;
        CheckImposterAlive();

        GameObject[] playerParent = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject PlayerThis in playerParent)
        {
          PlayerType playerType =  PlayerThis.gameObject.GetComponent<PlayerType>();
            
            if (playerType.isAlive == true)
            {
                AddPlayer += 1;
            }
        }

        PlayerLeftAlive = AddPlayer;
       

        if (PlayerLeftAlive <= 2)
        {
            ImposterWon();
        }

        else
        {
            ChangeStateOfGame();
        }
    }

    
    [Command(requiresAuthority = false)]
    private void CheckImposterAlive()
    {
        bool ImposterIsDead = true;
        GameObject[] playerParent = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject PlayerThis in playerParent)
        {
            PlayerType playerType = PlayerThis.gameObject.GetComponent<PlayerType>();

            if (playerType.isAlive == true &&
                playerType.isImposter == true)
            {
                ImposterIsDead = false;
            }

        }

        if (ImposterIsDead)
        {
            CrewWon();
        }
    }

    private void CrewWon()
    {
        CrewWonScreen.SetActive(true);
    }

    private void ImposterWon()
    {
        ImposterWonScreen.SetActive(false);
    }

    void TimerChanged(float oldV, float newV)
    {
        
    }
}
