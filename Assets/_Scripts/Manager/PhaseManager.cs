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

    public void StartLevel()
    {
        phaseIndex = 0;

        DOVirtual.DelayedCall(GameConstant.TIME_START_MATCH, () => { StartPhase(); }).SetAutoKill();
    }

    public void StartPhase()
    {
        killCount = 0;
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
        StartBatch(amount, zombieName);
    }

    private void StartBatch(int amount, string zombieName)
    {
        for (int i = 0; i < amount; i++)
            OnZombieDispatcher?.Invoke(zombieName);
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

}
