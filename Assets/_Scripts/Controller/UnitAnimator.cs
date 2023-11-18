using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] protected Animator ator = null;

    public enum MovementType
    {
        Idle,
        Move,
    }

    public void SetMove(MovementType movement)
    {
        ator.SetFloat("MovementType", (int)movement);
    }

    public void SetTriggger(string actionName)
    {
        ator.SetTrigger(actionName);
    }

    public void Stop()
    {
        ator.StopPlayback();
    }
    public void Reset()
    {
        ator.Rebind();
    }
}
