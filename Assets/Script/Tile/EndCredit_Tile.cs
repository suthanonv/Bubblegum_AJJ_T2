public class EndCredit_Tile : Grid_Collider
{

    protected override void _OnEnter(MainComponent main)
    {
        FindAnyObjectByType<LevelLoader>().LoadLevel(0);
    }

    protected override void _OnExit(MainComponent main)
    {


    }
}
