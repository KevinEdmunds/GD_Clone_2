using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManagerVS : NetworkBehaviour
{
    [SerializeField]
    [SyncVar(hook = nameof(PlayerCountChanged))]
    public int PlayerCount = 0;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    
        
       
    }

    [Command(requiresAuthority = false)]
   public void AddPlayerCount()
    {
        PlayerCount += 1;

        Debug.Log(PlayerCount);
    }

    void PlayerCountChanged(int OldValue, int NewValue)
    {

    }
}
