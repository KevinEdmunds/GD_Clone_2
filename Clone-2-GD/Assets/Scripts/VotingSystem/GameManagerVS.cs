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
    [SerializeField]
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
        myNetwork = GameObject.FindGameObjectWithTag("NetworkMan").GetComponent<MyNetworkBehaviour>();
        // NetworkServer.Spawn(gameObject);
        CurrentGameState = GameState.Normal;

        // Debug.Log("Server is: " + isServer);

        ProceedButton.SetActive(false);

        VotingScreen.SetActive(false);
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
            ProceedButton.SetActive(false);
            VotingScreen.SetActive(false);
        }

        else
        {
            
            VotingScreen.SetActive(true);
            ProceedButton.SetActive(false);

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
        VotingScreen.SetActive(false);
        ProceedButton.SetActive(false);
        CurrentGameState = GameManagerVS.GameState.Normal;

        foreach (Transform child in ContentParent)
        {
            Destroy(child.gameObject);
        }
        TimerOn = false;
        TimeLeft = SetTime;
        VotingScreen.SetActive(false);
        PLManager.HasVoted = false;

    }

    //public void CheckHostForChange()//Call this to run CRPCChangeStateOfGame()
    //{
    //    if (!NetworkServer.active)
    //    {
    //        HstCSoG();
    //    }

    //    else
    //    {
    //        CRPCChangeStateOfGame();
    //    }

    //}

    //[Command(requiresAuthority = false)]
    //private void HstCSoG()
    //{
    //    CRPCChangeStateOfGame();
    //}

  
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
        Debug.Log("Player has initiated the voting process.");

        PLManager.ThisPlayerVoted(); 
        StartCoroutine(WaitForVoteConfirmation());
    }

    private IEnumerator WaitForVoteConfirmation()
    {
        // Wait until the player's vote is confirmed
        while (!PLManager.HasVoted)
        {
            yield return null; // Wait for the next frame
        }

        // Once the vote is confirmed
        foreach (Transform transform in ContentParent)
        {
            PlayerPanelScript panelScript = transform.gameObject.GetComponent<PlayerPanelScript>();
            panelScript.button.SetActive(false);
          //  Debug.Log($"Disabling vote button for Player Panel ID: {panelScript.PanelId}");

            if (panelScript.PanelId == PLManager.PlayerId)
            {
              //  Debug.Log($"Executing CMdThisPlayerVoted for Player Panel ID: {panelScript.PanelId}");
                panelScript.CMdThisPlayerVoted();
            }
        }

        CheckIfEveryOneVoted();
    }


    private void CheckIfEveryOneVoted()
    {

        //Debug.Log("Start of the check");
        bool everyOneVoted = true;
        GameObject[] playerParent = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject transform in playerParent)
        {
            PlayerManager playerManager = transform.gameObject.GetComponent<PlayerManager>();
           // Debug.Log("Player " + playerManager.PlayerId + " voting status is: " + playerManager.HasVoted);

            if (!playerManager.HasVoted)
            {
                everyOneVoted = false;
            }
        }

        if (everyOneVoted)
        {
           // Debug.Log("done");
            VoteDone();
        }
    }

   
    
    public void VoteDone()
    {
        if (!NetworkServer.active)
        {
            PBFromHost();

        }

        else
        {
            CRPCSetProceedActiveF();
        }

    }

   [Command(requiresAuthority = false)]
   public void PBFromHost()
    {
        CRPCSetProceedActiveF();
    }
    
    [ClientRpc]
    private void CRPCSetProceedActiveF()
    {
       
        ProceedButton.SetActive(true);
        TimeLeft = 0;
    }

    public void PressProceedDone()
    {

        if (!NetworkServer.active)
        {
            CmdKilledFromHost();
         

        }

        else
        {
            CRpcFuntionOnClients();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdKilledFromHost()
    {
        CRpcFuntionOnClients();
        
    }

    [ClientRpc]
    private void CRpcFuntionOnClients()
    {
        //   PlayerHasVoted();
        PLManager.ResetVoted();
        ProceedButton.SetActive(false);
       // VotingScreen.SetActive(false);
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
                PLManager.ThisPlayerDied();
                PLManager.CmdChangeStatus();
                Debug.Log(PLManager.gameObject);
                CheckIfKilledIsHost(PLManager.PlayerId);
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

    //[Command(requiresAuthority =false)]
    private void SetSpaceOn()
    {
        DisplayTime = SetSpaceTimer;
        TimerDPOn = true;
    }

   // [Command(requiresAuthority = false)]
    private void SetSpaceOf()
    {
        Debug.Log("Waar is Jy in die lewe?");
       // TimerDPOn = false;
        EjectPlayerScreen.SetActive(false);
        CmdCheckAmountofPlayersAlive();
    }

    // [Command(requiresAuthority = false)]

    public void CheckIfKilledIsHost(int value)
    {

        if (!NetworkServer.active)
        {
            CmdKilledHost(value);


        }

        else
        {
            KillPlayerScreen(value);
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdKilledHost(int value)
    {
        KillPlayerScreen(value);
    }

    [ClientRpc]
    private void KillPlayerScreen(int ID)
    {
        GameObject[] PLEjected = GameObject.FindGameObjectsWithTag("Player");
        EjectPlayerScreen.SetActive(true);
        bool isTheImposter = PLEjected[ID-1].GetComponent<PlayerType>().isImposter;

        Debug.Log(ID);
        Debug.Log("Was Imposter: " + PLEjected[ID-1].GetComponent<PlayerType>().gameObject.name);
        if (isTheImposter)
        {
            Debug.Log("Mimic");
            EjectedMessage.text = PLEjected[ID-1].GetComponent<PlayerLobbyManager>().playerName + " was the imposter";
            SetSpaceOn();
        }

        else
        {
            Debug.Log("Not mimic");
            EjectedMessage.text = PLEjected[ID-1].GetComponent<PlayerLobbyManager>().playerName + " was not the imposter";
            SetSpaceOn();
        }
        
        
        

        //EjectedMessage.text = PLEjected.GetComponent<PlayerManager>().;
      //  ChangeStateOfGame();
    }

   // [Command(requiresAuthority = false)]
    private void CmdCheckAmountofPlayersAlive()
    {
        Debug.Log("Start Checking Players Alive");
        PlayerLeftAlive = 0;
        int AddPlayer = 0;
        CheckImposterDead();
        Debug.Log(CheckImposterDead());

       if (!CheckImposterDead())
        {
            Debug.Log("Check how many are Left");
            GameObject[] playerParent = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject PlayerThis in playerParent)
            {
                PlayerType playerType = PlayerThis.gameObject.GetComponent<PlayerType>();

                if (playerType.isAlive == true)
                {
                    AddPlayer += 1;
                }
            }

            Debug.Log(AddPlayer);
            PlayerLeftAlive = AddPlayer;


            if (PlayerLeftAlive <= 2)
            {
                Debug.Log("Ons was hier");
                ImposterWon();
                return;
            }

            else
            {
                Debug.Log("Ons was daar");
               

                if (isServer)
                {
                    ChangeStateOfGame();
                }
                
                return;
            }
        }

        else
        {
            CrewWon();
            return;
        }
       
    }

    
   // [Command(requiresAuthority = false)]
    private bool CheckImposterDead()
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
            return true;
        }

        else
        {
            return false;
        }
    }

    private void CrewWon()
    {
        CrewWonScreen.SetActive(true);
    }

    private void ImposterWon()
    {
        ImposterWonScreen.SetActive(true);
    }

    void TimerChanged(float oldV, float newV)
    {
        
    }
}
