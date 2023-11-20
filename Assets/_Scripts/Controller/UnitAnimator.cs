using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] protected Animator ator = null;

    [SerializeField] protected ZombieStateType zCurrentState;
    [SerializeField] protected PlantStateType pCurrentState;
    public enum ZombieStateType
    {
        Idle,
        Walk,
        Attack,
        WalkDie,
        AttackDie,
        BombDie,
    }

    public enum PlantStateType
    {
        Idle,
        Idle1,
        Idle2,
        Ready,
        Attack,
    }

    public void Initialize()
    {
        SetZombieMove(ZombieStateType.Idle);
        SetPlantMove(PlantStateType.Idle);
    }

    public void SetZombieDie()
    {
        switch (zCurrentState)
        {
            case ZombieStateType.Walk:

                SetZombieMove(ZombieStateType.WalkDie);
                return;

            case ZombieStateType.Attack:

                return;

            default:
                return;
        }
    }

    public void SetZombieMove(ZombieStateType movement)
    {
        if (zCurrentState == ZombieStateType.Walk)
            return;

        zCurrentState = movement;

        ator.SetFloat("ZombieState", (int)movement);
    }


    public void SetPlantMove(PlantStateType movement)
    {
        ator.SetFloat("PlantState", (int)movement);
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
