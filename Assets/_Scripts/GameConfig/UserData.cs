using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int userLevel = 1;
    public Dictionary<string, Data.UnitData> ownPlants = new Dictionary<string, Data.UnitData>();
    public Dictionary<string, Data.UnitData> discoverZombies = new Dictionary<string, Data.UnitData>();

    public UserData()
    {

    }
}
