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
    public System.Action<Vector3> OnWin = null;

    private int phaseIndex = 0;

    private int batchKeyIndex = 0;
    private List<int> batchKey = null;

    private int killCount = 0;
    private int batchKillCount = 0;

    private Vector3 lastZombDiePos;

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
        //killCount = 0;
        int amount = CurrentLevel.phases[phaseIndex].batchs[batchKeyIndex].amount;
        string zombieName = CurrentLevel.phases[phaseIndex].batchs[batchKeyIndex].name;
        float timeCallNext = CurrentLevel.phases[phaseIndex].batchs[batchKeyIndex].timeCallNext;
        float timeBetweenSpawn = CurrentLevel.phases[phaseIndex].timeBetweenSpawn;
        StartBatch(amount, zombieName, timeBetweenSpawn, timeCallNext);
    }

    Tween nextBatchTween = null;

    private void StartBatch(int amount, string zombieName, float time, float timeCallNext)
    {
        batchKillCount += amount;
        for (int i = 0; i < amount; i++)
        {
            try
            {
                DelayedCall(time, () => { OnZombieDispatcher?.Invoke(zombieName); });
            }
            catch
            {
                Debug.Log("Bacth is null");
            }
        }

        if (nextBatchTween != null)
            nextBatchTween.Kill();

        if (timeCallNext < 0)
            return; ;

        nextBatchTween = DOVirtual.DelayedCall(timeCallNext, () =>
        {
            if (batchKeyIndex + 1 >= batchKey.Count)
                return;

            batchKeyIndex++;
            CallBatch();
        });

        //try to start next batch if zombie in this batch is not die after a specify time
        //add a variable that count zombie in current calling batch
        //batchsAmount = 
    }

    public void ZombieDie(Vector3 lastPos)
    {
        killCount++;
        lastZombDiePos = lastPos;

        if (nextBatchTween != null)
            nextBatchTween.Kill();

        if (killCount == batchKillCount)
            EndABatch();
    }

    private void EndABatch()
    {
        if (batchKeyIndex + 1 >= batchKey.Count)
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
        if (phaseIndex + 1 >= CurrentLevel.phases.Count)
        {
            OnWin?.Invoke(lastZombDiePos);
            return;
        }
        else
            phaseIndex++;

        StartPhase();
    }

    private void DelayedCall(float time, System.Action action)
    {
        try
        {
            StartCoroutine(IEDelayedCall(time, action));
        }
        catch
        {
            Debug.LogError("Null");
        }
    }

    private IEnumerator IEDelayedCall(float time, System.Action action)
    {
        action?.Invoke();

        yield return Helper.GetWait(time);
    }
}
