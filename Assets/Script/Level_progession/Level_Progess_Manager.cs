using System.Collections.Generic;
using UnityEngine;
public class Level_Progess_Manager : MonoBehaviour
{


    private void Start()
    {

    }

    [ContextMenu("Reset Progession")]
    private void ResetProgess()
    {

    }

    private void OnApplicationQuit()
    {

    }
}


public class All_Level_Progession
{

    Dictionary<string, LevelProgession> SceneNameToValue = new Dictionary<string, LevelProgession>();

}




public class LevelProgession
{
    string SceneName;
    public bool CanMovingThough = true;
}
