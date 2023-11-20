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
        LostHeadWalkState,
        LostHeadAttackState,
        LostHeadAttack,
        LostHeadWalk,
        Die,
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


    public void SetZombieMove(ZombieStateType movement)
    {
        if (movement == zCurrentState)
            return;

        Debug.Log(movement.ToString());

        zCurrentState = movement;

        ator.Play(movement.ToString());
    }

    public void SetZombieLostHead()
    {
        switch (zCurrentState)
        {
            case ZombieStateType.Walk:

                SetZombieMove(ZombieStateType.LostHeadWalkState);
                break;

            case ZombieStateType.Attack:
                SetZombieMove(ZombieStateType.LostHeadAttackState);
                break;
        }
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
