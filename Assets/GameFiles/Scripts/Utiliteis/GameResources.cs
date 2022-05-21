using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameResources
{

    public enum volumeType
    {
        BGM,
        MASTER,
        SFX,
        UI
    }

    public enum pickupType
    {
      Gold = 0, 
      Explosive = 1,
      Empty = 2
    }

    public enum uiGroupName
    {
        HUD,
        TITLE,
        GAMEOVER,
        SETTINGS,
        INGAME
    }

    public enum PowerUpType
    {
        LIFE = 0,
        DUALGUN = 1,
        TRIPPLEGUN = 2
    }

    public static int _finalLevel { get; set; }
    public static int _finalGold { get; set; }

    public static Vector3 playerStartPos = new Vector3(100, 20, 30);
    


}
