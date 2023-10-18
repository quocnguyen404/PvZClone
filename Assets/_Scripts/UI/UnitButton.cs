using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Button button = null;

    private Image icon;
    private TMP_Text cost;
    public Data.UnitData unitData;
}
