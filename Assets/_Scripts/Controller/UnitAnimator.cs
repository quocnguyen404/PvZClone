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

    public void ZInitialize()
    {
        SetZombieMove(ZombieStateType.Idle);
    }

    public void PInitialize()
    {
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
        if (movement == pCurrentState)
            return;

        Debug.Log(movement.ToString());

        pCurrentState = movement;

        ator.Play(movement.ToString());
    }


    public void Reset()
    {
        ator.Rebind();
    }
}
