using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using ES3Types;
public static class ConfigHelper
{
    const string UserDataKey = "UserData123";
    
    public const int plantAmount = 44;
    
    public static GameConfig gameConfig = null;

    public static GameConfig GameConfig
    {
        get
        {
            if (gameConfig == null)
                LoadGameConfigSplashScreen();

            return gameConfig;
        }
    }

    public static void LoadGameConfigSplashScreen()
    {
        gameConfig = new GameConfig();
        gameConfig.plants = new Dictionary<string, Plant>();

        Plant[] plantsData = Resources.LoadAll<Plant>("Prefabs/Plant");

        foreach (Plant plant in plantsData)
        {
            gameConfig.plants.Add(plant.unitData.name, plant);
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
                userData = GetDefaultUserData();
                ES3.Save(UserDataKey, userData);
            }
            else
                userData = ES3.Load<UserData>(UserDataKey);

            return userData;
        }

        set => ES3.Save(UserDataKey, value);
    }

    public static LevelConfig GetLevelConfig(int level)
    {
        return GameConfig.levelConfigs[level];
    }

    public static UserData GetDefaultUserData()
    {
        UserData defaultUserData = new UserData();

        defaultUserData.userLevel = 1;
        defaultUserData.ownPlants = new Dictionary<string, Data.UnitData>();
        defaultUserData.discoverZombies = new Dictionary<string, Data.UnitData>();

        Data.UnitData firstUnit = GameConfig.plants["Pea Shooter"].unitData;

        defaultUserData.ownPlants.Add(firstUnit.unitName, firstUnit);

        return defaultUserData;
    }
}
