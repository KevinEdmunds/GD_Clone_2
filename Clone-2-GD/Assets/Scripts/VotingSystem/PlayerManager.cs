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
    public bool HAsVoted = false, buffer = true;
    [SyncVar(hook = nameof(HasBeenKilled))]
    public bool IsAlive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            managerVS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerVS>();
            managerVS.AddPlayerCount();
            managerVS.PlayerSpawned = true;
            managerVS.PLManager = this;
            AssignPlayerID();
           // PutOnServer();
            Debug.Log("Spawned");
            buffer = false;
        }
        
    }

    [Command(requiresAuthority = false)]
    private void PutOnServer()
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
            HAsVoted = false;
        }
    }

    private void TestChanges()
    {
        managerVS.ChangeStateOfGame();
    }

  
    private void AssignPlayerID()
    {
       
            PlayerId = managerVS.PlayerCount+1;
        
        
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

   
}
