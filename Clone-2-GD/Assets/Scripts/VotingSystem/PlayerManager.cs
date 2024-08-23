using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;



//Communicates with the GameManager
public class PlayerManager : NetworkBehaviour
{
    [SerializeField]
    private GameManagerVS managerVS;
    [SerializeField]
    [SyncVar(hook = nameof(UpdatePlayerID))]
    public int PlayerId;
    [SyncVar(hook = nameof(HasVotedChanged))]
    public bool HasVoted = false;
    [SyncVar(hook = nameof(HasBeenKilled))]
    public bool IsAlive = true;
    [SerializeField]
    private PlayerType playerType;
    public bool buffer = true;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            IsAlive = playerType.isAlive;
            managerVS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerVS>();
            managerVS.CMdAddPlayerCount();
            managerVS.PlayerSpawned = true;
            managerVS.PLManager = this;
            // transform.parent = managerVS.playerParent;

            AssignPlayerID(managerVS.PlayerCount);
          //  UpdatePlayerId();
            // PutOnServer();
            Debug.Log("Spawned");
            buffer = false;
            //CmdPutOnServer();
            //PositionPlayer();
        }
        
    }

    [Command(requiresAuthority = false)]
    private void UpdatePlayerId()
    {
        PlayerId += 1;
        Debug.Log("Hello");
    }


    //private void PositionPlayer()
    //{
    //    Debug.Log("Waars jy");
    //    transform.parent = managerVS.playerParent;
    //}

    [Command(requiresAuthority = false)]
    private void CmdPutOnServer()
    {
        NetworkServer.Spawn(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.T))
        {
           
            TestChanges();
        }

        if (isLocalPlayer && 
            managerVS.CurrentGameState == GameManagerVS.GameState.Normal)
        {
            HasVoted = false;
        }
    }

    private void TestChanges()
    {
        managerVS.ChangeStateOfGame();
    }

    [Command(requiresAuthority = false)]
    private void AssignPlayerID(int Value)
    {

        PlayerId = Value + 1;
        //  NetworkServer.Spawn(gameObject);
    }



    void UpdatePlayerID(int OldValue, int NewValue)
    {
        Debug.Log(NewValue);
    }

    void HasVotedChanged(bool oldT, bool newT)
    {

    }

    void HasBeenKilled(bool oldT, bool newT)
    {

    }

    [Command(requiresAuthority =false)]
    public void ThisPlayerVoted()
    {
        HasVoted = true;
        Debug.Log("HetGestem");
    }

    [Command(requiresAuthority = false)]
    public void ResetVoted()
    {
        HasVoted = false;
        Debug.Log("ResetedVote");
    }

    [Command(requiresAuthority = false)]
    public void ThisPlayerDied()
    {
        IsAlive = false;
    }

    [Command(requiresAuthority = false)]
    public void CmdCheckStatus()
    {
        IsAlive = playerType.isAlive;
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeStatus()
    {
        playerType.isAlive = IsAlive;
    }
}
