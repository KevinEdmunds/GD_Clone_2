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

    
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            managerVS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerVS>();
            managerVS.AddPlayerCount();
            managerVS.PLManager = this;
            AssignPlayerID();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.T))
        {
            TestChanges();
        }
    }

    private void TestChanges()
    {
       if (managerVS.CurrentGameState == GameManagerVS.GameState.Normal)
        {
            managerVS.CurrentGameState = GameManagerVS.GameState.Voting;
        }

       else
        {
            managerVS.CurrentGameState = GameManagerVS.GameState.Normal;
        }
    }

    private void AssignPlayerID()
    {
        PlayerId = managerVS.PlayerCount + 1;
    }

    void UpdatePlayerID(int OldValue, int NewValue)
    {
        Debug.Log(NewValue);
    }
}
