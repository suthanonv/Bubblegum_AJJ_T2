using UnityEngine;

public class StickyGum_behave : StateBehaviour<Bubble_Gum_State>
{
    public override Bubble_Gum_State state => Bubble_Gum_State.Stick;


    All_Sticky_Gum_Holder _stick_Gum_Holder;

    StateControl<Bubble_Gum_State> _mainGumState;


    Grouping _attach_Moveable_List;

    MainComponent_Transform _mainComponent_Transform;
    private void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        _stick_Gum_Holder = FindAnyObjectByType<All_Sticky_Gum_Holder>(FindObjectsInactive.Include);
        _mainGumState = this.transform.parent.GetComponent<StateControl<Bubble_Gum_State>>();
        _mainComponent_Transform = this.transform.parent.GetComponent<MainComponent_Transform>();
        _attach_Moveable_List = this.transform.GetComponentInChildren<Grouping>();


    }



    public override void OnEnterState()
    {
        SoundManager.PlaySound(SoundType.BBG_Stick, 1f);

        base.OnEnterState();
        _stick_Gum_Holder.Add_Sticky_Gum(_mainGumState);
        if (_mainComponent_Transform.currentTile_index == Vector2Int.zero)
        {
            if (_attach_Moveable_List._AddingSelf == true)
            {
                _attach_Moveable_List._AddingSelf = false;
            }
        }
    }

    public override void OnExitState()
    {
        SoundManager.PlaySound(SoundType.BBG_Unstick, 1f);

        base.OnExitState();


        _stick_Gum_Holder.Remove_Sticky_Gum(_mainGumState);

        foreach (Transform i in this.transform)
        {
            if (i.gameObject.TryGetComponent<Grouping>(out Grouping attached))
            {
                attached.Reset_List();
            }
        }

    }
}
