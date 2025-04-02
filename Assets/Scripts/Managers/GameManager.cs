using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<NPCController> playerList;

    private void Start()
    {
        if(instance == null)
            instance = this;

        InitializePlayerList();
        InitializeEvents();
    }

    private void InitializePlayerList()
    {
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
        }
    }

    private void OnPlayerDeath(NPCController npc)
    {
        npc.eventHandler.OnPlayerDeathEvent -= OnPlayerDeath;
        playerList.Remove(npc);
    }
}
