using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] protected Animator ator = null;

    public enum ZombieStateType
    {
        Move,
        Attack,
    }

    public enum PlantStateType
    {
        Idle,
        Idle1,
        Idle2,
        Ready,
        Attack,
    }

    public void SetZombieMove(ZombieStateType movement)
    {
        ator.SetFloat("MovementType", (int)movement);
    }

    public void SetPlantMove(PlantStateType movement)
    {
        ator.SetFloat("IdleType", (int)movement);
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
