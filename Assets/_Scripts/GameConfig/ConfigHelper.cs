using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class ConfigHelper
{
    private const string UserDataKey = "UserData123";

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

    private static UserData userData = null;
    public static UserData UserData
    {
        get
        {
            ES3.DeleteKey(UserDataKey);

            if (!ES3.KeyExists(UserDataKey))
            {
                userData = GetTestUserData();
                ES3.Save(UserDataKey, userData);
            }
            else
                userData = ES3.Load<UserData>(UserDataKey);

            return userData;
        }

        set => ES3.Save(UserDataKey, value);
    }

    public static LevelConfig GetCurrentLevelConfig()
    {
        return GameConfig.levelConfigs[UserData.userLevel];
    }

    public static LevelConfig GetLevelConfig(int level)
    {
        return GameConfig.levelConfigs[level];
    }

    public static UserData GetDefaultUserData()
    {
        UserData defaultUserData = new UserData
        {
            userLevel = 1,
            ownPlants = new Dictionary<string, Data.UnitData>(),
            discoverZombies = new Dictionary<string, Data.UnitData>()
        };

        Data.UnitData firstUnit = GameConfig.plants["Peashooter"];
        Data.UnitData secondUnit = GameConfig.plants["Sunflower"];
        Data.UnitData thirdUnit = GameConfig.plants["Wall-nut"];

        defaultUserData.ownPlants.Add(firstUnit.unitName, firstUnit);
        defaultUserData.ownPlants.Add(secondUnit.unitName, secondUnit);
        defaultUserData.ownPlants.Add(thirdUnit.unitName, thirdUnit);

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
        {
            testUserData.ownPlants.Add(plant.Key, plant.Value);
        }

        return testUserData;
    }
}
