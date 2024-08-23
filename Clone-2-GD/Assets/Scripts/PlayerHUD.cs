using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.UI;

public class PlayerHUD : NetworkBehaviour
{
    /*
    public GameObject imposterHUD, playerHUD;
   
    public PlayerActions playerActions;
    public Button ventButton, useButton, reportButton, killButton, sabotageButton;*/
    public PlayerType playerType;
    public Button useButton;

    private GameObject emptyGarbageCafePanel;
    private GameObject cardSwipeAdminPanel;
    private GameObject downloadWeaponsPanel;
    private GameObject uploadAdminPanel;
    private GameObject calibrateDisPanel;
    private GameObject sabotageReactor1Panel;
    private GameObject sabotageReactor2Panel;
    private GameObject sabotageOxygen1Panel;
    private GameObject sabotageOxygen2Panel;
    private GameObject sabotageMapPanel;
    

    private Collider2D playerCollider;
    private Collider2D emptyGarbageCafeTrigger;
    private Collider2D cardSwipeAdminTrigger;
    private Collider2D downloadWeaponsTrigger;
    private Collider2D uploadAdminTrigger;
    private Collider2D calibrateDisTrigger;
    private Collider2D sabotageReactor1Trigger;
    private Collider2D sabotageReactor2Trigger;
    private Collider2D sabotageOxygen1Trigger;
    private Collider2D sabotageOxygen2Trigger;


    private SabotageReactor sabotageReactor;
    private SabotageOxygen sabotageOxygen;
    

 

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (isLocalPlayer)
        {
            if (SceneManager.GetActiveScene().name!="Lobby")
            {
                playerCollider = GetComponent<Collider2D>();

                GameObject emptyGarbageCafetrigger = GameObject.Find("EmptyGarbageCafeTrigger");
                GameObject cardSwipeAdmintrigger = GameObject.Find("CardSwipeAdminTrigger");
                GameObject downloadWeaponstrigger = GameObject.Find("DownloadWeaponsTrigger");
                GameObject uploadAdmintrigger = GameObject.Find("UploadAdminTrigger");
                GameObject calibrateDistrigger = GameObject.Find("CalibrateDisTrigger");
                GameObject sabotageReactor1trigger = GameObject.Find("SabotageReactor1Trigger");
                GameObject sabotageReactor2trigger = GameObject.Find("SabotageReactor2Trigger");
                GameObject sabotageOxygen1trigger = GameObject.Find("SabotageOxygen1Trigger");
                GameObject sabotageOxygen2trigger = GameObject.Find("SabotageOxygen2Trigger");
                //  GameObject sabotageMapPanel = GameObject.Find("Canvas/SabotageMapPanel");
                sabotageReactor = GameObject.Find("TaskManager").GetComponent<SabotageReactor>();
                sabotageOxygen = GameObject.Find("TaskManager").GetComponent<SabotageOxygen>();






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

                if (calibrateDistrigger != null)
                {
                    calibrateDisTrigger = calibrateDistrigger.GetComponent<Collider2D>();
                }

                if (sabotageReactor1trigger != null)
                {
                    sabotageReactor1Trigger = sabotageReactor1trigger.GetComponent<Collider2D>();
                }

                if (sabotageReactor2trigger != null)
                {
                    sabotageReactor2Trigger = sabotageReactor2trigger.GetComponent<Collider2D>();
                }

                if (sabotageOxygen1trigger != null)
                {
                    sabotageOxygen1Trigger = sabotageOxygen1trigger.GetComponent<Collider2D>();
                }

                if (sabotageOxygen2trigger != null)
                {
                    sabotageOxygen2Trigger = sabotageOxygen2trigger.GetComponent<Collider2D>();
                }

                if(playerType.isImposter)
                {
                    if (sabotageMapPanel == null)
                    {
                        sabotageMapPanel = GameObject.Find("SabotageMapPanel");
                    }
                    else
                    {
                        Debug.LogError("Sabotage Map Panel!");
                    }
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

                calibrateDisPanel = GameObject.Find("CalibrateDisElectricalPanel");
                if (calibrateDisPanel != null)
                {
                    calibrateDisPanel.SetActive(false);
                }
                else
                {
                    Debug.LogError("CalibrateDisElectricalPanel not found!");
                }

                sabotageReactor1Panel = GameObject.Find("SabotageReactorPanel");
                if (sabotageReactor1Panel != null)
                {
                    sabotageReactor1Panel.SetActive(false);
                }
                else
                {
                    Debug.LogError("Sabotage Reactor 1 Panel not found!");
                }

                sabotageReactor2Panel = GameObject.Find("SabotageReactorPanel2");
                if (sabotageReactor2Panel != null)
                {
                    sabotageReactor2Panel.SetActive(false);
                }
                else
                {
                    Debug.LogError("Sabotage Reactor 2 Panel not found!");
                }

                sabotageOxygen1Panel = GameObject.Find("SabotageO2Panel1");
                if (sabotageOxygen1Panel != null)
                {
                    sabotageOxygen1Panel.SetActive(false);
                }
                else
                {
                    Debug.LogError("Sabotage Oxygen 1 Panel not found!");
                }

                sabotageOxygen2Panel = GameObject.Find("SabotageO2Panel2");
                if (sabotageOxygen2Panel != null)
                {
                    sabotageOxygen2Panel.SetActive(false);
                }
                else
                {
                    Debug.LogError("Sabotage Oxygen 2 Panel not found!");
                }

                sabotageMapPanel = GameObject.Find("SabotageMapPanel");
                if (sabotageMapPanel != null)
                {
                    sabotageMapPanel.SetActive(false);
                }
                else
                {
                    Debug.LogError("Sabotage Map Panel not found!");
                }
            }
        }
    }
    /*
    public void KillButton()
    {
        playerActions.KillTarget();
    }

    public void UpdateHUD(bool isImposter)
    {
        imposterHUD.SetActive(isImposter);
    }*/

    public void ReportDeath()
    {
        //Link to command to call logic to start vote
    }

    public void UseButton()
    {
        Debug.Log("the use button is working");
        if (isLocalPlayer)
        {
            Debug.Log("this is working too");
            if (!playerType.isImposter)
            {
                Debug.Log("even here");
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
                else if (playerCollider != null && calibrateDisTrigger != null && playerCollider.IsTouching(calibrateDisTrigger))
                {
                    calibrateDisPanel.SetActive(true);
                }
                
            }


            else if (playerCollider != null && sabotageReactor1Trigger != null && playerCollider.IsTouching(sabotageReactor1Trigger))
            {
                if (sabotageReactor.isReactorSabotaged)
                {
                    sabotageReactor1Panel.SetActive(true);
                }
            }

            else if (playerCollider != null && sabotageReactor2Trigger != null && playerCollider.IsTouching(sabotageReactor2Trigger))
            {
                if (sabotageReactor.isReactorSabotaged)
                {
                    sabotageReactor2Panel.SetActive(true);
                }
            }

            else if (playerCollider != null && sabotageOxygen1Trigger != null && playerCollider.IsTouching(sabotageOxygen1Trigger))
            {
                if (sabotageOxygen.sabotageOxygenActive)
                {
                    sabotageOxygen1Panel.SetActive(true);
                }
            }

            else if (playerCollider != null && sabotageOxygen2Trigger != null && playerCollider.IsTouching(sabotageOxygen2Trigger))
            {
                if (sabotageOxygen.sabotageOxygenActive)
                {
                    sabotageOxygen2Panel.SetActive(true);
                }
            }


        }
    }

    public void Sabotage()
    {
        //link to command to spawn "task" for player
        if (isLocalPlayer)
        {
            if (playerType != null && playerType.isImposter)
            {
                if (sabotageMapPanel != null)
                {
                    sabotageMapPanel.SetActive(true);
                    Debug.Log("SabotageMapPanel set to active.");
                }
                else
                {
                    Debug.LogError("sabotageMapPanel is null!");
                }
            }
            else
            {
                Debug.LogError("playerType is null or not an imposter!");
            }
        }
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
                    else if (playerCollider != null && calibrateDisTrigger != null && playerCollider.IsTouching(calibrateDisTrigger))
                    {
                        useButton.interactable = true;
                    }
                else
                {
                    useButton.interactable = false;
                }
            }
            
            else if (playerCollider != null && sabotageReactor1Trigger != null && playerCollider.IsTouching(sabotageReactor1Trigger))
            {
                if (sabotageReactor.isReactorSabotaged)
                {

                    useButton.interactable = true;
                }
            }
            else if (playerCollider != null && sabotageReactor2Trigger != null && playerCollider.IsTouching(sabotageReactor2Trigger))
            {
                if (sabotageReactor.isReactorSabotaged)
                {
                    useButton.interactable = true;
                }
            }

            else if (playerCollider != null && sabotageOxygen1Trigger != null && playerCollider.IsTouching(sabotageOxygen1Trigger))
            {
                if (sabotageOxygen.sabotageOxygenActive)
                {
                    useButton.interactable = true;
                }
            }

            else if (playerCollider != null && sabotageOxygen2Trigger != null && playerCollider.IsTouching(sabotageOxygen2Trigger))
            {
                if (sabotageOxygen.sabotageOxygenActive)
                {
                    useButton.interactable = true;
                }
            }
            else
            {
                useButton.interactable = false;
            }
        }
        }
}
