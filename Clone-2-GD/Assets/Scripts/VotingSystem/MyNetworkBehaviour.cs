using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class MyNetworkBehaviour : NetworkManager
{
    [SerializeField]
    private GameObject GMVSObj, VotingScreen;
    [SerializeField]
    private GameManagerVS managerVS;
    [SerializeField]
    private Transform Canvas;
    
    // Start is called before the first frame update
    void Start()
    {
    // VotingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetKeyUp(KeyCode.P))
        //{
        //    PlaceManagerOnServer();
        //}
    }

    private void PlaceManagerOnServer()
    {
        //GameObject MGonServer = Instantiate(GMVSObj);
        //managerVS = MGonServer.GetComponent<GameManagerVS>();
        //NetworkServer.Spawn(MGonServer);

    }

    public override void OnStartServer()
    {
      //  GameObject VSObj = Instantiate(VotingScreen, Canvas);
     //   NetworkServer.Spawn(VSObj);
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

      
       
        
    }
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        //Debug.Log("Hierso");
        //managerVS.CMdRemovePlayer();
        //base.OnServerDisconnect(conn);
       
    }


    public void PutPanelsOnServer(GameObject panel)
    {
        NetworkServer.Spawn(panel);
    }

}
