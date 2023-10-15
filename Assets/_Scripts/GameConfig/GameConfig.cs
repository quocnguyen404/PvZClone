using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public Dictionary<string, Data.UnitData> plantsData = null;
    public Dictionary<string, Data.UnitData> zombiesData = null;
    public Dictionary<int, LevelConfig> levels = null;
}
