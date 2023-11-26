using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    public void SetPlantMove(PlantStateType movement, System.Action action)
    {
        if (movement == pCurrentState)
            return;

        Debug.Log(movement.ToString());

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

    public  void TriggerZombieMove(ZombieStateType movement)
    {
        if (movement == zCurrentState)
            return;

        Debug.Log($"Trigger Zombie {movement.ToString()}");

        zCurrentState = movement;

        ator.SetTrigger(movement.ToString());
    }

    public void TriggerPlantMove(PlantStateType movement)
    {
        if (movement == pCurrentState)
            return;

        Debug.Log($"Trigger Zombie {movement.ToString()}");

        pCurrentState = movement;

        ator.SetTrigger(movement.ToString());
    }

    public void Reset()
    {
        ator.Rebind();
    }
}
