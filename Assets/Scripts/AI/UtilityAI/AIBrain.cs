using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public bool finishedDeciding;
    public Action bestAction;
    private NPCController npcController;

    private void Awake()
    {
        npcController = GetComponent<NPCController>();
    }
    private void Update()
    {
        if (bestAction is null)
        {
            DecideBestAction(npcController.availableActions);
        }
    }

    //Loop through available actions
    //Return highest scoring action
    public void DecideBestAction(List<Action> availableActions)
    {
        float score = 0f;
        int nextBestActionIndex = 0;

        for (int i = 0; i < availableActions.Count; i++)
        {
            if(ScoreAction(availableActions[i]) > score)
            {
                nextBestActionIndex = i;
                score = availableActions[i].score;
            }
        }

        bestAction = availableActions[nextBestActionIndex];
        finishedDeciding = true;
    }

    //Loop through all considerations of the action
    //Score all considerations and average them
    public float ScoreAction(Action action)
    {
        float score = 1f;
        foreach (Consideration consideration in action.considerations)
        {
            float considerationScore = consideration.ScoreConsideration(npcController);
            score *= considerationScore;

            if(score == 0f)
            {
                action.score = 0f;
                return action.score;
            }
        }

        //Averaging scheme of overall score
        float originalScore = score;
        float modFactor = 1 - (1 / action.considerations.Count);
        float makeupValue = (1 - originalScore) * modFactor;
        action.score = originalScore + (makeupValue * originalScore);

        return action.score;
    }
}
