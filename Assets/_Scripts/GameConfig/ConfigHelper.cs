using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class ConfigHelper
{
    const string UserDataKey = "UserData123";


    public static GameConfig gameConfig = null;

    public static GameConfig GameConfig
    {
        get
        {
            if (gameConfig == null)
            {
                string textAsset = Resources.Load<TextAsset>("Game Config/GameConfig").text;
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

        Data.UnitData firstUnit = GameConfig.plants["Peashooter"];

        defaultUserData.ownPlants.Add(firstUnit.unitName, firstUnit);

        return defaultUserData;
    }
}
