using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerHUD : NetworkBehaviour
{
    public GameObject imposterHUD, playerHUD;
    public PlayerType playerType;
    public PlayerActions playerActions;
    public Button ventButton, useButton, reportButton, killButton, sabotageButton;

    private GameObject emptyGarbageCafePanel;
    private GameObject cardSwipeAdminPanel;
    private GameObject downloadWeaponsPanel;
    private GameObject uploadAdminPanel;

    private Collider2D playerCollider;
    private Collider2D emptyGarbageCafeTrigger;
    private Collider2D cardSwipeAdminTrigger;
    private Collider2D downloadWeaponsTrigger;
    private Collider2D uploadAdminTrigger;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (isLocalPlayer)
        {
            playerCollider = GetComponent<Collider2D>();

            GameObject emptyGarbageCafetrigger = GameObject.Find("EmptyGarbageCafeTrigger");
            GameObject cardSwipeAdmintrigger = GameObject.Find("CardSwipeAdminTrigger");
            GameObject downloadWeaponstrigger = GameObject.Find("DownloadWeaponsTrigger");
            GameObject uploadAdmintrigger = GameObject.Find("UploadAdminTrigger");

            if (emptyGarbageCafetrigger != null)
            {
                emptyGarbageCafeTrigger = emptyGarbageCafetrigger.GetComponent<Collider2D>();
            }
            else
            {
                Debug.LogError("EmptyGarbageCafeTrigger not found!");
            }

            if (cardSwipeAdmintrigger != null)
            {
                cardSwipeAdminTrigger = cardSwipeAdmintrigger.GetComponent<Collider2D>();
            }
            else
            {
                Debug.LogError("CardSwipeAdminTrigger not found!");
            }

            if (downloadWeaponstrigger != null)
            {
                downloadWeaponsTrigger = downloadWeaponstrigger.GetComponent<Collider2D>();
            }
            else
            {
                Debug.LogError("DownloadWeaponsTrigger not found!");
            }

            if (uploadAdmintrigger != null)
            {
                uploadAdminTrigger = uploadAdmintrigger.GetComponent<Collider2D>();
            }
            else
            {
                Debug.LogError("UploadWeaponsTrigger not found!");
            }

            emptyGarbageCafePanel = GameObject.Find("EmptyGarbageCafePanel");
            if (emptyGarbageCafePanel != null)
            {
                emptyGarbageCafePanel.SetActive(false);
            }
            else
            {
                Debug.LogError("EmptyGarbageCafePanel not found!");
            }

            cardSwipeAdminPanel = GameObject.Find("CardSwipeTaskPanel");
            if (cardSwipeAdminPanel != null)
            {
                cardSwipeAdminPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("CardSwipeTaskPanel not found!");
            }

            downloadWeaponsPanel = GameObject.Find("DownloadTaskWeaponsPanel1");
            if (downloadWeaponsPanel != null)
            {
                downloadWeaponsPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("DownloadTaskWeaponsPanel1 not found!");
            }

            uploadAdminPanel = GameObject.Find("UploadTaskWeaponsPanel");
            if (uploadAdminPanel != null)
            {
                uploadAdminPanel.SetActive(false);
            }
            else
            {
                Debug.LogError("UploadTaskWeaponsPanel not found!");
            }
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
                if (playerCollider != null && emptyGarbageCafeTrigger != null && playerCollider.IsTouching(emptyGarbageCafeTrigger))
                {
                    emptyGarbageCafePanel.SetActive(true);
                }
                else if (playerCollider != null && cardSwipeAdminTrigger != null && playerCollider.IsTouching(cardSwipeAdminTrigger))
                {
                    cardSwipeAdminPanel.SetActive(true);
                }
                else if (playerCollider != null && downloadWeaponsTrigger != null && playerCollider.IsTouching(downloadWeaponsTrigger))
                {
                    downloadWeaponsPanel.SetActive(true);
                }
                else if (playerCollider != null && uploadAdminTrigger != null && playerCollider.IsTouching(uploadAdminTrigger))
                {
                    uploadAdminPanel.SetActive(true);
                }
            }
        }
    }

    public void Sabotage()
    {
        //link to command to spawn "task" for player
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (!playerType.isImposter)
            {
                if (playerCollider != null && emptyGarbageCafeTrigger != null && playerCollider.IsTouching(emptyGarbageCafeTrigger))
                {
                    useButton.interactable = true;
                }
                else if (playerCollider != null && cardSwipeAdminTrigger != null && playerCollider.IsTouching(cardSwipeAdminTrigger))
                {
                    useButton.interactable = true;
                }
                else if (playerCollider != null && downloadWeaponsTrigger != null && playerCollider.IsTouching(downloadWeaponsTrigger))
                {
                    useButton.interactable = true;
                }
                else if (playerCollider != null && uploadAdminTrigger != null && playerCollider.IsTouching(uploadAdminTrigger))
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
