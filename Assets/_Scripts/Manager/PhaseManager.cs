using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public LevelConfig CurrentLevel
    {
        get
        {
            return ConfigHelper.GetCurrentLevelConfig();
        }
    }


    public System.Action<string> OnZombieDispatcher = null;
    public System.Action OnWin = null;

    private List<int> zombieAmount = null;

    private int phaseIndex = 0;
    private int maxPhaseAmount = 0;

    private int killCount = 0;
    private int maxKillCount = 0;



    public void StartLevel()
    {
        phaseIndex = 0;
        maxPhaseAmount = CurrentLevel.phases.Count - 1;
        zombieAmount = new List<int>();


        DOVirtual.DelayedCall(GameConstant.TIME_START_MATCH, () =>
        {
            StartPhase(CurrentLevel.phases[phaseIndex]);

        }).SetAutoKill();
    }

    private void StartPhase(PhaseData phase)
    {
        phaseIndex = phase.phaseIndex;
        killCount = 0;

        foreach (var zombie in phase.zombies)
        {
            zombieAmount.Add(zombie.Key);
            maxKillCount += zombie.Key;
        }

        StartCoroutine(IECallZombie(phase));
    }

    private IEnumerator IECallZombie(PhaseData phase)
    {
        int count = zombieAmount[phaseIndex];
        for (int i = 0; i < count; i++)
        {
            CallZombie(phase.zombies[count]);
            yield return Helper.GetWait(phase.timeBetweenSpawn);
        }
    }

    private void CallZombie(string zombieName)
    {
        //zombieCount++;
        OnZombieDispatcher?.Invoke(zombieName);
    }

    public void ZombieDie()
    {
        killCount++;
        Debug.Log(killCount);

        if (killCount >= maxKillCount)
        {
            killCount = 0;
            maxKillCount = 0;
            CallNextPhase();
        }
    }

    private void CallNextPhase()
    {
        if (phaseIndex >= maxPhaseAmount)
        {
            OnWin?.Invoke();
            StopAllCoroutines();
            return;
        }

        phaseIndex++;
        StartPhase(CurrentLevel.phases[phaseIndex]);
    }
}
