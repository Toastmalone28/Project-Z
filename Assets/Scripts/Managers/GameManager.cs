using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<NPCController> playerList;
    public List<Group> groupList;

    private void Start()
    {
        if(instance == null)
            instance = this;

        InitializePlayerList();
        InitializeEvents();
        InitializeGroupList();
    }

    private void InitializeGroupList()
    {
        groupList = new List<Group>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Human"))
        {
            Group group = go.GetComponent<GroupHandler>().currentGroup;

            if (group != null)
            {
                groupList.Add(group);
            }
        }
    }

    private void InitializePlayerList()
    {
        playerList = new List<NPCController>();

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Human"))
        {
            playerList.Add(go.GetComponent<NPCController>());
        }
    }

    private void InitializeEvents()
    {
        foreach (NPCController npc in playerList)
        {
            npc.eventHandler.OnPlayerDeathEvent += OnPlayerDeath;
            npc.eventHandler.OnGroupUpdateEvent += OnGroupUpdate;
        }
    }

    private void OnPlayerDeath(NPCController npc)
    {
        npc.eventHandler.OnPlayerDeathEvent -= OnPlayerDeath;
        playerList.Remove(npc);
    }

    private void OnGroupUpdate(Group group)
    {
        if(group == null) 
            return;

        if (!groupList.Contains(group))
            groupList.Add(group);

        if (group.IsEmpty())
        {
            groupList.Remove(group);
        }
        else if(group.groupLeader == null)
        {
            group.groupLeader = group.GroupMembers[0];
        }
    }

}
