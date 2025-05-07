using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] LevelLoader lvlLoader;
    [SerializeField] EscMenuManager EscMenuManager;
    Load_Condition LoadCondition;
    void Start()
    {
        LoadCondition = FindAnyObjectByType<Load_Condition>();
    }
    public void StartGame()
    {
        LoadCondition.LoadingType();
    }

    public void OpenOptions()
    {
        if (FindFirstObjectByType<Cloud>() != null) return;
        EscMenuManager.OpenEscMenu();
        EscMenuManager.OpenSetting();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
