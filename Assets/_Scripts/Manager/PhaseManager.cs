using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public LevelConfig CurrentLevel => ConfigHelper.GetCurrentLevelConfig();

    public System.Action<string> OnZombieDispatcher = null;
    public System.Action OnWin = null;

    private int phaseIndex = 0;

    private int batchKeyIndex = 0;
    private List<int> batchKey = null;

    private int killCount = 0;
    private int batchKillCount = 0;

    public void StartLevel()
    {
        phaseIndex = 0;

        DOVirtual.DelayedCall(GameConstant.TIME_START_MATCH, () => { StartPhase(); }).SetAutoKill();
    }

    public void StartPhase()
    {
        killCount = 0;
        batchKillCount = 0;
        batchKeyIndex = 0;
        batchKey = new List<int>();

        foreach (Batch batch in CurrentLevel.phases[phaseIndex].batchs)
            batchKey.Add(batch.amount);

        CallBatch();
    }

    private void CallBatch()
    {
        killCount = 0;
        int amount = CurrentLevel.phases[phaseIndex].batchs[batchKeyIndex].amount;
        string zombieName = CurrentLevel.phases[phaseIndex].batchs[batchKeyIndex].name;
        float timeBetweenSpawn = CurrentLevel.phases[phaseIndex].timeBetweenSpawn;
        StartBatch(amount, zombieName, timeBetweenSpawn);
    }

    private void StartBatch(int amount, string zombieName, float time)
    {
        for (int i = 0; i < amount; i++)
        {
            try
            {
                CallZombie(time, () => { OnZombieDispatcher?.Invoke(zombieName); });
            }
            catch
            {
                Debug.Log("Bacth is null");
            }
        }


        //try to start next batch if zombie in this batch is not die after a specify time
        //add a variable that count zombie in current calling batch
        //batchsAmount = 
    }

    public void ZombieDie()
    {
        killCount++;

        if (killCount == CurrentLevel.phases[phaseIndex].batchs[batchKeyIndex].amount)
            EndABatch();
    }

    private void EndABatch()
    {
        if (batchKeyIndex + 1 == batchKey.Count)
        {
            EndAPhase();
            return;
        }
        else
            batchKeyIndex++;

        CallBatch();
    }

    private void EndAPhase()
    {
        if (phaseIndex + 1 == CurrentLevel.phases.Count)
        {
            OnWin?.Invoke();
            return;
        }
        else
            phaseIndex++;

        StartPhase();
    }


    private void CallZombie(float time, System.Action action)
    {
        StartCoroutine(IECallZombie(time, action));
    }

    private IEnumerator IECallZombie(float time, System.Action action)
    {
        action?.Invoke();

        yield return Helper.GetWait(time);
    }
}
