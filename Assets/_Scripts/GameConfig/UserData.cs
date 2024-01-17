using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int userLevel = 1;
    public Dictionary<string, Data.UnitData> ownPlants;
    public Dictionary<string, Data.UnitData> discoverZombies;
    public int gold = 0;
    public UserData()
    {

    }
}
