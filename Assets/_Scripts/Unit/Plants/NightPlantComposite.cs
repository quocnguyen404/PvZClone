using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build;
using UnityEngine;

public class NightPlantComposite
{
    public UnitAnimator ator = null;
    public bool IsSleeping { get; private set; }

    public void Initialize(UnitAnimator ator)
    {
        this.ator = ator;

        if (GameUtilities.GetDate() == Date.Day)
            Sleep();
    }

    public void Sleep()
    {
        IsSleeping = true;
        ator.SetPlantMove(UnitAnimator.PlantStateType.Idle2);
    }

    public void WakeUp()
    {
        ator.SetPlantMove(UnitAnimator.PlantStateType.Ready, () => { IsSleeping = false; });

    }
}
