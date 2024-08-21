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
    [SerializeField]
    private Transform Content;

    [SerializeField]
    private GameManagerVS managerVS;
    [SerializeField]
    private TMP_Text PlayerName;

    [SyncVar(hook = nameof(SomebodyVotedForMe))]
    public int AmountOfvotes = 0;

    [SerializeField]
    public GameObject button, ShowDead, I_Voted;

    [SerializeField]
    [SyncVar]
    private bool CorrespondingPlayerAlive;

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
    }

    //[Command(requiresAuthority = false)]
    private void IsDead()
    {
        GameObject[] PlayersInGame = GameObject.FindGameObjectsWithTag("Player");
        if (PanelId == managerVS.PLManager.PlayerId)
        {
            button.SetActive(false);
        }
        foreach (GameObject Player in PlayersInGame)
        {
            PlayerManager playerManager = Player.GetComponent<PlayerManager>();

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
        if (managerVS.PLManager.HAsVoted == true)
        {
            I_Voted.SetActive(true);
        }

        if (managerVS.PLManager.IsAlive == false &&
            PanelId == managerVS.PLManager.PlayerId)
        {
            I_Voted.SetActive(false);
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
        Debug.Log("WeesKind");
        PlayerName.text = "Player " + PLId.ToString();
    }

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
        
        if (managerVS.PLManager.HAsVoted == false &&
            playerTar.CorrespondingPlayerAlive == true)
        {
            Debug.Log("Voted");
            // AmountOfvotes += 1;
            IncreaseVote();
            managerVS.PlayerHasVoted();

            foreach (Transform transform in Content)
            {
                transform.gameObject.GetComponent<PlayerPanelScript>().button.SetActive(false);
            }
        }


         
    }

    [Command(requiresAuthority = false)]
    public void IncreaseVote()
    {
        AmountOfvotes += 1;
    }


   
}
