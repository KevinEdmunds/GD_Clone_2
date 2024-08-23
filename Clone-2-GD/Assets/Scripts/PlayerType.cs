using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerType : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetPlayerRole))]
    public bool isImposter=false;

    [SyncVar(hook = nameof(SetAliveState))]
    public bool isAlive = true;

    public PlayerActions playerActions;
    public PlayerHUD playerHUD;

    void SetAliveState(bool oldState, bool newState)
    {
        Debug.Log("This player is dead");
        this.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        CmdSetRole();
    }

    void SetPlayerRole(bool oldState, bool newState)
    {
        Debug.Log("Setting player role to " + (newState ? "Imposter" : "Crewmate"));

        // Update HUD based on the new role
        if (isLocalPlayer)
        {
            playerHUD.UpdateHUD(newState); // Pass the newState (isImposter) to the HUD
        }
    }

    [Command]
    void CmdSetRole()
    {
        int role = Random.Range(0, 2);
        Debug.Log("Assigning role: " + (role == 0 ? "Crewmate" : "Imposter"));

        isImposter = (role == 1);
    }
}
