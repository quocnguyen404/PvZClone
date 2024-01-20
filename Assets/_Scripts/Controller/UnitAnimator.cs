using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] protected Animator ator = null;
    [SerializeField] protected ZombieStateType zCurrentState;
    [SerializeField] protected PlantStateType pCurrentState;

    public System.Action OnAgentStop = null;

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


    /*
     * Wall plant have 3 idle
     * Night plant have 2 idle -> Idle: normal idle -> Idle1: sleep idle
     * Night plant ready is wake up
    */
    public enum PlantStateType
    {
        Idle, //normal idle
        Idle1, //sleep
        Idle2,
        Ready, //ready or wakeup
        Attack, //attack or explose
    }

    public void ZInitialize()
    {
        SetZombieMove(ZombieStateType.Idle);
    }

    public void PInitialize()
    {
        SetPlantMove(PlantStateType.Idle);
    }

    public void OnTurnOff()
    {
        SetZombieMove(ZombieStateType.Idle);
        SetPlantMove(PlantStateType.Idle);
        OnAgentStop = null;
    }

    public ZombieStateType GetZombieState() => zCurrentState;

    public void SetZombieMove(ZombieStateType movement)
    {
        if (movement == zCurrentState)
            return;

        //Debug.Log(movement.ToString());

        zCurrentState = movement;

        ator.Play(movement.ToString());
    }

    public void SetZombieMove(ZombieStateType movement, System.Action action)
    {
        if (movement == zCurrentState)
            return;

        //Debug.Log(movement.ToString());

        zCurrentState = movement;

        float animLength = GetAnimationClipsLength(movement.ToString());

        ator.Play(movement.ToString(), 0);
        DOVirtual.DelayedCall(animLength, () => { action?.Invoke(); }).SetAutoKill();
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
                OnAgentStop?.Invoke();
                break;
        }
    }

    public void SetPlantMove(PlantStateType movement)
    {
        if (movement == pCurrentState)
            return;

        //Debug.Log(movement.ToString());

        pCurrentState = movement;

        ator.Play(movement.ToString());
    }

    public void SetPlantMove(PlantStateType movement, System.Action action)
    {
        if (movement == pCurrentState)
            return;

        //Debug.Log(movement.ToString());

        pCurrentState = movement;

        float animLength = GetAnimationClipsLength(movement.ToString());

        ator.Play(movement.ToString(), 0);

        DOVirtual.DelayedCall(animLength, () => { action?.Invoke(); }).SetAutoKill();
    }

    public float GetAnimationClipsLength(string animName)
    {
        //may get bugs
        foreach (AnimationClip clip in ator.runtimeAnimatorController.animationClips)
        {
            if (animName == clip.name)
                return clip.length;
        }

        return 0;
    }

    public void TriggerZombieMove(ZombieStateType movement)
    {
        if (movement == zCurrentState)
            return;

        //Debug.Log($"Trigger Zombie {movement.ToString()}");

        zCurrentState = movement;

        ator.SetTrigger(movement.ToString());
    }

    public void TriggerPlantMove(PlantStateType movement)
    {
        if (movement == pCurrentState)
            return;

        //Debug.Log($"Trigger Plant {movement.ToString()}");

        pCurrentState = movement;

        ator.SetTrigger(movement.ToString());
    }



    public void Reset()
    {
        ator.Rebind();
    }
}
