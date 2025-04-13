using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectTile : Grid_Collider
{
    LevelLoader lvl;
    [SerializeField] int lvlSelect;
    private void Start()
    {
        lvl =  FindAnyObjectByType<LevelLoader>();
    }

    protected override void _OnEnter(MainComponent main)
    {
        if (main.GetComponent<StateControl<Bubble_Gum_State>>() != null)
        {
            lvl.loadLevelSelectedScene(lvlSelect + SceneManager.GetActiveScene().buildIndex);
        }
    }

    protected override void _OnExit(MainComponent main)
    {

    }
}
