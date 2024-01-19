using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class ConfigHelper
{
    public static GameConfig gameConfig = null;
    public static GameConfig GameConfig
    {
        get
        {
            if (gameConfig == null)
            {
                string textAsset = Resources.Load<TextAsset>(GameConstant.GAMECONFIG_PATH).text;
                gameConfig = JsonConvert.DeserializeObject<GameConfig>(textAsset);
            }

            return gameConfig;
        }
    }

    private const string UserDataKey = "UserData123";
    private static UserData userData = null;
    public static UserData UserData
    {
        get
        {
            //ES3.DeleteKey(UserDataKey);

            if (!ES3.KeyExists(UserDataKey))
            {
                //userData = GetTestUserData();
                userData = GetDefaultUserData();
                ES3.Save(UserDataKey, userData);
            }
            else if (userData == null)
                userData = ES3.Load<UserData>(UserDataKey);

            return userData;
        }

        set
        {
            userData = value;
            ES3.Save(UserDataKey, userData);
        }
    }

    public static LevelConfig GetCurrentLevelConfig()
    {
        if (UserData.userLevel > GameConfig.levelConfigs.Count)
            UserData.userLevel = 1;

        return GameConfig.levelConfigs[UserData.userLevel];
    }

    public static LevelConfig GetLevelConfig(int level)
    {
        return GameConfig.levelConfigs[level];
    }

    public static void LevelUp()
    {
        UserData.userLevel++;
    }

    public static void AddGold(int amount)
    {
        UserData.gold += amount;
    }

    public static void AddNewPlant(string plantName)
    {
        UserData.ownPlants.Add(plantName, GameConfig.plants[plantName]);
    }

    public static UserData GetDefaultUserData()
    {
        UserData defaultUserData = new UserData
        {
            userLevel = 1,
            ownPlants = new Dictionary<string, Data.UnitData>(),
            discoverZombies = new Dictionary<string, Data.UnitData>(),
            gold = 0,
        };

        Data.UnitData firstUnit = GameConfig.plants["PeaShooter"];
        defaultUserData.ownPlants.Add(firstUnit.unitName, firstUnit);

        return defaultUserData;
    }

    public static UserData GetTestUserData()
    {
        UserData testUserData = new UserData
        {
            userLevel = 1,
            ownPlants = new Dictionary<string, Data.UnitData>(),
            discoverZombies = new Dictionary<string, Data.UnitData>()
        };

        foreach (var plant in GameConfig.plants)
            testUserData.ownPlants.Add(plant.Key, plant.Value);

        return testUserData;
    }
}
