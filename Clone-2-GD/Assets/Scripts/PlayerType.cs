using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerType : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetPlayerRole))]
    public bool isImposter = false;

    [SyncVar(hook = nameof(SetAliveState))]
    public bool isAlive = true;

    public PlayerActions playerActions;
    public PlayerHUD playerHUD;

    // Hook method for when isImposter changes
    void SetPlayerRole(bool oldState, bool newState)
    {
        if (isLocalPlayer)
        {
            playerHUD.UpdateHUD(newState); // Pass the newState (isImposter) to the HUD
        }
    }

    // Hook method for when isAlive changes
    void SetAliveState(bool oldState, bool newState)
    {
        GetComponent<SpriteRenderer>().color = newState ? Color.white : Color.black;
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        CmdSetRole(); // Command to set the player's role on the server
    }

    // Command to assign the player's role on the server
    [Command]
    void CmdSetRole()
    {
        int role = Random.Range(0, 2);

        isImposter = (role == 1); // This will trigger the SyncVar hook
    }
}
