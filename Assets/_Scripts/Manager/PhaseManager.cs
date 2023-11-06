using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public LevelConfig currentLevel
    {
        get
        {
            return ConfigHelper.GetCurrentLevelConfig();
        }
    }


    public System.Action<string> OnZombieDispatcher = null;
    public System.Action OnWin = null;

    private int phaseIndex = 0;
    private int maxPhaseAmount = 0;

    public void StartLevel()
    {
        phaseIndex = 0;
        maxPhaseAmount = currentLevel.phases.Count - 1;
    }

    public void StartPhase(PhaseData phase)
    {
        int index = 0;
        int zombieIndex = 0;
        phaseIndex = phase.phaseIndex;

        List<int> zombieAmount = phase.zombies.Keys.ToList();
        List<string> zombies = phase.zombies.Values.ToList();

        DOVirtual.DelayedCall(GameConstant.TIME_START_MATCH, () =>
        {


        }).SetAutoKill();
    }

    public void CallZombie(string zombieName, List<string> zombies)
    {
        //call first zombie
        OnZombieDispatcher?.Invoke(zombies[0]);

    }

    public void ZombieDie()
    {

    }

    public void CallNextPhase()
    {

    }
}
