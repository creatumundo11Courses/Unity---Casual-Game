using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    public void OnGoalReached(Collider col)
    {
        if (col.TryGetComponent(out CharacterControllerPlayer characterControllerPlayer))
        {
            GameMode.Instance.OnGoalReached();
        }
    }
}
