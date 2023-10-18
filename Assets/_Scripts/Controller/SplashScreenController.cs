using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{


    public void LoadDataSplashScene()
    {
        ConfigHelper.LoadGameConfigSplashScreen();

        while (ConfigHelper.gameConfig == null)
        {

        }
    }
}
