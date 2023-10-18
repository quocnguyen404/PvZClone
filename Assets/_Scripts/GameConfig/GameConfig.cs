using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{
    public Dictionary<string, Data.UnitData> plants = null;
    public Dictionary<string, Data.UnitData> zombies = null;
    public Dictionary<int, LevelConfig> levelConfigs = null;

    public GameConfig()
    {

    }
}
