using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlant : Plant
{
    
    public override void TakeDamage(float damge)
    {
        base.TakeDamage(damge);

        if (currentHealth <= maxHealth * (1f / 3f))
            ator.SetPlantMove(UnitAnimator.PlantStateType.Idle2);
        else if (currentHealth <= maxHealth * (2f / 3f))
            ator.SetPlantMove(UnitAnimator.PlantStateType.Idle1);

        
    }
}
