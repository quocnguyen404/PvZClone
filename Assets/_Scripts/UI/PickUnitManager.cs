using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUnitManager : MonoBehaviour
{
    [SerializeField] private PlantButtonHold buttonHolder = null;
    [SerializeField] private UnitButtonPanel pickPanel = null;

    private void Awake()
    {
        buttonHolder.Initialize();
        pickPanel.Initialize();
    }
}
