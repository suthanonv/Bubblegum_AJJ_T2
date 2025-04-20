using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] LevelLoader lvlLoader;
    [SerializeField] EscMenuManager EscMenuManager;
    public void StartGame()
    {
        lvlLoader.LoadLevel(1);
    }

    public void OpenOptions()
    {
        EscMenuManager.OpenEscMenu();
        EscMenuManager.OpenSetting();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
