using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class PlayerPanelScript : NetworkBehaviour, IPointerClickHandler
{
    [SyncVar(hook = nameof(PanelIDChanged))]
    public int PanelId;

    [SyncVar]
    public string ThePlayerName;

    [SerializeField]
    private Transform Content;

    [SerializeField]
    private GameManagerVS managerVS;
    [SerializeField]
    private TMP_Text PlayerName;

    [SyncVar(hook = nameof(SomebodyVotedForMe))]
    public int AmountOfvotes = 0;

    [SerializeField]
    public GameObject button, ShowDead, I_Voted, HowManyVotes, PlayerImage;

    [SerializeField]
    [SyncVar]
    private bool CorrespondingPlayerAlive;
    [SerializeField]
    [SyncVar]
    private bool PlayerHasVoted = false;

    // Start is called before the first frame update
    void Start()
    {
        
        //  Debug.Log("My id: " + netId);
        managerVS = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerVS>();
       
        ShowDead.SetActive(false);
        CorrespondingPlayerAlive = managerVS.PLManager.IsAlive;
       
        Content = GameObject.FindGameObjectWithTag("Content").transform;

        if (transform.parent == null)
        {
            transform.parent = Content;
        }
        I_Voted.SetActive(false);
        SetName(PanelId);
        IsDead();
        Vector3 Scale = new Vector3(1f, 1f, 1f);
        transform.localScale = Scale;
       
    }

    //[Command(requiresAuthority = false)]
    private void IsDead()
    {


        GameObject[] playerParent = GameObject.FindGameObjectsWithTag("Player");
        if (PanelId == managerVS.PLManager.PlayerId)
        {
            button.SetActive(false);
        }
        foreach (GameObject Player in playerParent)
        {
            PlayerManager playerManager = Player.gameObject.GetComponent<PlayerManager>();

            if(PanelId == playerManager.PlayerId)
            {
                if (playerManager.IsAlive == false)
                {
                    ShowDead.SetActive(true);
                    button.SetActive(false);
                    Image spriteRenderer = gameObject.GetComponent<Image>();
                    spriteRenderer.color = Color.grey;
                    return;
                }
            }
        }
        
        
        //if (CorrespondingPlayerAlive == false &&
        //    PanelId == managerVS.PLManager.PlayerId)
        //{
        //    ShowDead.SetActive(true);
        //    Image spriteRenderer = gameObject.GetComponent<Image>();
        //    spriteRenderer.color = Color.grey;
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
      

        if (managerVS.PLManager.IsAlive == false &&
            PanelId == managerVS.PLManager.PlayerId)
        {
            I_Voted.SetActive(false);
        }

        if (PlayerHasVoted)
        {
            I_Voted.SetActive(true);
        }
    }

    void PanelIDChanged(int oldV, int newV)
    {

    }

    void SomebodyVotedForMe(int oldVal, int newVAl)
    {

    }


 //  [Command(requiresAuthority = false)]
    public void SetName(int PLId)
    {
        GameObject[] playerParent = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject MightBeThisPlayer in playerParent)
        {
            PlayerManager playerManager = MightBeThisPlayer.gameObject.GetComponent<PlayerManager>();
            PlayerLobbyManager playerType = MightBeThisPlayer.gameObject.GetComponent<PlayerLobbyManager>();
            if (PLId == playerManager.PlayerId)
            {
               // CMdSyncPlayerName(playerType);
                PlayerName.text = playerType.playerName;
                Image spriteRenderer = PlayerImage.GetComponent<Image>();
                spriteRenderer.color = playerManager.gameObject.GetComponent<SpriteRenderer>().color;

            }
        }
        
    }

    //[Command(requiresAuthority = false)]
    //private void CMdSyncPlayerName(PlayerLobbyManager playerType)
    //{
    //    ThePlayerName = playerType.playerName;
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        
        //if (managerVS.PLManager.HAsVoted == false &&
        //    CorrespondingPlayerAlive == true)
        //{
            
        //    AmountOfvotes += 1;
        //    managerVS.PlayerHasVoted();
        //}
        
    }

   // [Command(requiresAuthority = false)]
    public void VoteForPlayer(PlayerPanelScript playerTar)
    {
        
        if (managerVS.PLManager.HasVoted == false &&
            playerTar.CorrespondingPlayerAlive == true)
        {
            //Debug.Log("Voted");
            // AmountOfvotes += 1;
            CMdIncreaseVote();
           // managerVS.PlayerHasVoted();
             managerVS.skipVote();

            //foreach (Transform transform in Content)
            //{
            //    transform.gameObject.GetComponent<PlayerPanelScript>().button.SetActive(false);

            //    if (transform.gameObject.GetComponent<PlayerPanelScript>().PanelId == managerVS.PLManager.PlayerId)
            //    {
            //        ThisPlayerVoted();
            //    }
            //}
        }
 
    }


    [Command(requiresAuthority = false)]
    public void CMdIncreaseVote()
    {
        AmountOfvotes += 1;
    }

    [Command(requiresAuthority = false)]
    public void CMdThisPlayerVoted()
    {
        PlayerHasVoted = true;
    }
   
}
