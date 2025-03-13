using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public string Name;
    private float _score;
    public float score
    {
        get { return _score; }
        set
        {
            _score = Mathf.Clamp01(value);
        }
    }
    public List<Consideration> considerations;

    public virtual void Awake()
    {
        score = 0f;
    }
    public abstract void Execute(NPCController npc);
}
