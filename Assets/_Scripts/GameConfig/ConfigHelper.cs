using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigHelper
{
    private static GameConfig gameConfig = null;
    public static GameConfig GameConfig
    {
        get
        {
            if (gameConfig == null)
                gameConfig = JsonConvert.DeserializeObject<GameConfig>(Resources.Load<TextAsset>("GameConfig").text);

            return gameConfig;
        }
    }

    public static LevelConfig GetLevelConfig(int level)
    {
        return GameConfig.levels[level];
    }

    public static Dictionary<string, Data.UnitData> GetUserOwnPlants()
    {
        return GameConfig.plantsData;
    }
}
