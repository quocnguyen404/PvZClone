using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private int killCount = 0;
    private int killToCallNextPhase = 0;
    private int currentPhase = 0;
    private int maxPhase = 0;

    public System.Action<int, string> OnZombieDispatcher = null;
    public System.Action OnWin = null;

    public void StartLevel()
    {
        LevelConfig currentLevel = ConfigHelper.GetCurrentLevelConfig();
        maxPhase = currentLevel.phases.Count - 1;
        currentPhase = 0;
        killCount = 0;
        killToCallNextPhase = 0;

        this.DelayCall(1f, () =>
        {
            StartPhase(currentLevel.phases[currentPhase]);
        });
    }

    public void StartPhase(PhaseData phase)
    {
        currentPhase = phase.phaseIndex;
        killToCallNextPhase = phase.zombieAmount;

        float callTime = phase.timeBetweenSpawn;


        foreach (var zombie in phase.zombies)
        {

            CallNextZombie(callTime, () => { OnZombieDispatcher?.Invoke(zombie.Key, zombie.Value); });
            callTime += phase.timeBetweenSpawn;
        }
    }

    public void ZombieDie()
    {
        killCount++;

        if (killToCallNextPhase >= killCount)
        {
            CallNextPhase();
        }

    }

    public void CallNextPhase()
    {
        killCount = 0;
        killToCallNextPhase = 0;
        currentPhase++;

        StopCoroutine("IEDelayCall");

        if (currentPhase > maxPhase)
        {
            StopAllCoroutines();
            OnWin?.Invoke();
            return;
        }

        StartPhase(ConfigHelper.GetCurrentLevelConfig().phases[currentPhase]);
    }

    private void CallNextZombie(float time, System.Action callback)
    {
        StartCoroutine(IEDelay(time, callback));
    }

    private IEnumerator IEDelay(float time, System.Action callback)
    {
        yield return Helper.GetWait(time);

        callback?.Invoke();
    }
}
