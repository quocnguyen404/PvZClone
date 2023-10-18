using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUnitManager : MonoBehaviour
{
    [SerializeField] private PlantButtonHold leftPanel = null;
    [SerializeField] private UnitButtonPanel rightPanel = null;



    private void Awake()
    {
        rightPanel.OnUnitButtonClick = leftPanel.AddToHoldPanel;
        leftPanel.Initialize();
        rightPanel.Initialize();
    }



    public void RemoveFromHoldPanel()
    {

    }

    public void UpdataButtonHolder()
    {

    }
}
