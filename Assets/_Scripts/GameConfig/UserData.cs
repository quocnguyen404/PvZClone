using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int userLevel = 1;
    public Dictionary<string, Data.UnitData> ownPlants;
    public Dictionary<string, Data.UnitData> discoverZombies;
    public int gold = 0;
    public bool isMusicOn = true;
    public bool isSoundOn = true;
    public float soundVolume = 1f;
    public float musicVolume = 1f;
    public UserData()
    {

    }
}
