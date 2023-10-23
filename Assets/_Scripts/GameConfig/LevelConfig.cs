using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig
{
    public Chapter chapter = Chapter.Day;
    public int levelIndex;
    public int zombiesAmount = 4;

    public Dictionary<int, string> zombies;

    public LevelConfig()
    {

    }
}
