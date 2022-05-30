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



    public enum uiGroupName
    {
        HUD,
        TITLE,
        GAMEOVER,
        SETTINGS,
        INGAME
    }

    public enum spawnType
    {
        GUARDING,
        PATROL
    }

    public static List<GameObject> entities = new List<GameObject>();

    public static Transform playerTransform { get; set; }
    public static Player _player { get; set; }
    
    public static int nunberOfEntities { get; set; }

    public static Vector3 playerStartPos = new Vector3(100, 20, 30);

    

    public static int currentLevel { get; set; }

    public static LevelStartSettings currentLevelStartSettings { get; set; }
    
    public static bool gameIsRunning { get; set; }


}
