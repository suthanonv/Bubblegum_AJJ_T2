using UnityEngine;
public class Wining_Tile : Grid_Collider
{

    private void Start()
    {
        FindAnyObjectByType<Wining_Check>().Add_WiningTile(this);
    }

    protected override void _OnEnter(MainComponent main)
    {
        if (main.GetComponent<StateControl<Bubble_Gum_State>>() == null) _iswin = false;

        else _iswin = true;

        //Debug.Log(_iswin);
    }

    protected override void _OnExit(MainComponent main)
    {
        _iswin = false;
        //Debug.Log(_iswin);

    }


    bool _iswin = false;

    public bool IsWin => _iswin;

}
