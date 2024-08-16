using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestingPlayer : NetworkBehaviour
{
    [SerializeField]
    private GameManagerVS managerVS;
    [SerializeField]
    [SyncVar(hook = nameof(UpdatePlayerID))]
    private int PlayerId;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            managerVS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerVS>();
            managerVS.AddPlayerCount();
            AssignPlayerID();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AssignPlayerID()
    {
        PlayerId = managerVS.PlayerCount;
    }

    void UpdatePlayerID(int OldValue, int NewValue)
    {
        Debug.Log(NewValue);
    }
}
