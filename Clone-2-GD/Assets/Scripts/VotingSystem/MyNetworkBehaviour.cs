using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class MyNetworkBehaviour : NetworkManager
{
    [SerializeField]
    private GameObject GMVSObj;
    [SerializeField]
    private GameManagerVS managerVS;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaceManagerOnServer();
        }
    }

    private void PlaceManagerOnServer()
    {
        //GameObject MGonServer = Instantiate(GMVSObj);
        //managerVS = MGonServer.GetComponent<GameManagerVS>();
        //NetworkServer.Spawn(MGonServer);

    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        

    }

    public override void OnStopServer()
    {
        base.OnStopServer();
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
    }


}
