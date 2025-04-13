using UnityEngine;

public class LevelSelectTile : Grid_Collider
{
    LevelLoader lvl;
    [SerializeField] int lvlSelect;
    [SerializeField] int sceneSkip;
    private void Start()
    {
        lvl =  FindAnyObjectByType<LevelLoader>();
    }

    protected override void _OnEnter(MainComponent main)
    {
        if (main.GetComponent<StateControl<Bubble_Gum_State>>() != null)
        {
            lvl.loadLevelSelectedScene(lvlSelect + sceneSkip);
        }
    }

    protected override void _OnExit(MainComponent main)
    {

    }
}
