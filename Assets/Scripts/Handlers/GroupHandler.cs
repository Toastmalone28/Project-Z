using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHandler : MonoBehaviour
{
    public Group currentGroup;

    public bool isInvited = false;
    private Group invitedBy = null;
    public float invitationCooldown {  get; private set; }

    private EventHandler eventHandler;

    private void Awake()
    {
        eventHandler = GetComponent<EventHandler>();
    }


    private void FixedUpdate()
    {
        invitationCooldown -= Time.deltaTime;
    }

    public void InviteToGroup(Group newGroup)
    {
        invitedBy = newGroup;
        isInvited = true;
        newGroup.RecentlyInvited.Add(gameObject);
    }

    public void AcceptGroupInvitation()
    {
        currentGroup = invitedBy;
        isInvited = false;
        invitedBy = null;

        currentGroup.AddMember(gameObject.GetComponent<NPCController>());

        invitationCooldown = 999f;
    }
    public void DeclineGroupInvitation()
    {
        isInvited = false;
        invitedBy = null;

        invitationCooldown = 999f;
    }
}

public class Group : MonoBehaviour
{
    public string Name;
    public NPCController groupLeader;

    private List<NPCController> groupMembers = new List<NPCController>();
    public List<NPCController> GroupMembers
    {
        get
        {
            groupMembers.RemoveAll(obj => !obj);
            return groupMembers;
        }
    }

    private List<GameObject> recentlyInvited = new List<GameObject>();
    public List<GameObject> RecentlyInvited
    {
        get
        {
            recentlyInvited.RemoveAll(obj => !obj);
            return recentlyInvited;
        }
    }

    public void AddMember(NPCController newMember)
    {
        if (GroupMembers.Contains(newMember))
            return;

        if (newMember.groupHandler.currentGroup != null)
            return;

        GroupMembers.Add(newMember);
        newMember.groupHandler.currentGroup = this;
    }

    public void RemoveMember(NPCController member)
    {
        if (!GroupMembers.Contains(member)) 
            return;

        GroupMembers.Remove(member);
        member.groupHandler.currentGroup = null;
    }

    public bool IsEmpty()
    {
        return GroupMembers.Count == 0;
    }
}