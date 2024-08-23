using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class StartGameManager : NetworkBehaviour
{
    public List<bool> isImposterList = new List<bool>();

    int playerCount;
    [SerializeField]
    List<bool> croppedList = new List<bool>();

    private List<bool> GetShortenedRoleList(int playerCount)
    {
        List<bool> shortList = new List<bool>();
        for(int i=0; i<playerCount; i++)
        {
            shortList.Add(isImposterList[i]);
        }

        return shortList;
    }

    private void Start()
    {
        croppedList = GetShortenedRoleList(NetworkServer.connections.Count);
        croppedList = ShuffleList(croppedList);
        AssignPlayerRoles();
    }

    public List<bool> ShuffleList(List<bool> list)
    {
        System.Random rng = new System.Random();
        return list.OrderBy(x => rng.Next()).ToList();
    }
    
    [Command(requiresAuthority =false)]
    public void AssignPlayerRoles()
    {
        //Debug.Log("ASSIGNING PLAYER ROLES");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i=0; i<players.Length;i++)
        {
            players[i].GetComponent<PlayerType>().SetPlayerRole(croppedList[i]);
        }
    }
}
