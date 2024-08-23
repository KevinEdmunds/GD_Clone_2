using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using Org.BouncyCastle.Asn1.BC;
using Org.BouncyCastle.Bcpg;

public class PlayerHUD : NetworkBehaviour
{
    public GameObject imposterHUD, playerHUD;
    public PlayerType playerType;
    public PlayerActions playerActions;
    public Button ventButton, useButton, reportButton, killButton, sabotageButton;

    //public GameObject emptyGarbageCafeTrigger;
    private GameObject emptyGarbageCafePanel;
    private GameObject cardSwipeAdminPanel;
    private GameObject downloadWeaponsPanel;

    private Collider2D playerCollider;
    private Collider2D emptyGarbageCafeTrigger;
    private Collider2D cardSwipeAdminTrigger;
    private Collider2D downloadWeaponsTrigger;

    // Initialize HUD elements

    public override void OnStartClient()
    {
        if (isLocalPlayer) 
        { 
       
        base.OnStartClient();


        playerCollider = GetComponent<Collider2D>();

        GameObject emptyGarbageCafetrigger = GameObject.Find("EmptyGarbageCafeTrigger");
        GameObject cardSwipeAdmintrigger = GameObject.Find("CardSwipeAdminTrigger");
        GameObject downloadWeaponstrigger = GameObject.Find("DownloadWeaponsTrigger");

        emptyGarbageCafeTrigger = emptyGarbageCafetrigger.GetComponent<Collider2D>();
        cardSwipeAdminTrigger = cardSwipeAdmintrigger.GetComponent<Collider2D>();
        downloadWeaponsTrigger = downloadWeaponstrigger.GetComponent<Collider2D>();



        emptyGarbageCafePanel = GameObject.Find("EmptyGarbageCafePanel");
        emptyGarbageCafePanel.SetActive(false);

        cardSwipeAdminPanel = GameObject.Find("CardSwipeTaskPanel");
        cardSwipeAdminPanel.SetActive(false);

        downloadWeaponsPanel = GameObject.Find("DownloadTaskWeaponsPanel1");
        downloadWeaponsPanel.SetActive(false);
        }

    }

    void Start()
    {
        



        if (isLocalPlayer)
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent = this.transform;
            playerHUD.SetActive(true);
            ventButton.interactable = false;
            killButton.interactable = false;
            reportButton.interactable = false;
            useButton.interactable = false;
        }

        
       

    }

    public void KillButton()
    {
        playerActions.KillTarget();
    }

    public void UpdateHUD(bool isImposter)
    {
        imposterHUD.SetActive(isImposter);
    }

    public void ReportDeath()
    {
        //Link to command to call logic to start vote
    }

    public void UseButton()
    {
        if (isLocalPlayer)
        {
            if (!playerType.isImposter)
            {
                if (playerCollider.IsTouching(emptyGarbageCafeTrigger))
                {
                    emptyGarbageCafePanel.SetActive(true);
                }
                else if (playerCollider.IsTouching(cardSwipeAdminTrigger))
                {
                    
                    cardSwipeAdminPanel.SetActive(true);
                    
                }
                else if (playerCollider.IsTouching(downloadWeaponsTrigger))
                {
                    downloadWeaponsPanel.SetActive(true);
                }
                    
                
            }
        }
    }

    public void Sabotage()
    {
        //link to command to spawn "task" for player
    }

    public void Update()
    {
        if (isLocalPlayer)
        {
            if (!playerType.isImposter)
            {
                if (playerCollider.IsTouching(emptyGarbageCafeTrigger))
                {
                    useButton.interactable = true;
                
                }
                else
                {
                    useButton.interactable = false;
                }

                if (playerCollider.IsTouching(cardSwipeAdminTrigger))
                {
                    useButton.interactable = true;
                }
                else
                {
                    useButton.interactable = false;
                }

                if (playerCollider.IsTouching(downloadWeaponsTrigger))
                {
                    useButton.interactable = true;
                }
                else
                {
                    useButton.interactable = false;
                }
            }
        }
    }
}
