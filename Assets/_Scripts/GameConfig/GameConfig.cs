using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{
    public Dictionary<string, Plant> plants = null;
    public Dictionary<string, Zombie> zombies = null;
    public Dictionary<int, LevelConfig> levelConfigs = null;

    public GameConfig()
    {

    }
}
