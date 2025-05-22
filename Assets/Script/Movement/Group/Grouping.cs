using System.Collections.Generic;
using UnityEngine;

public class Grouping : MonoBehaviour
{
    private List<Grouping> _groupInfo = new List<Grouping>();
    private I_move _thisMove;

    public I_move Get_Move => _thisMove;


    [SerializeField] List<MainComponent> BaseGroup;

    List<Grouping> _baseGroup = new List<Grouping>();


    [Tooltip("Need adding self move script into the list in the set up")]
    [SerializeField] bool _addingSelf = true;


    public bool _AddingSelf { get { return _addingSelf; } set { _addingSelf = value; } }
    private void Start()
    {
        _thisMove = GetComponent<I_move>();
        BaseSetUp();
        Reset_List();
    }

    public void BaseSetUp()
    {
        foreach (var item in BaseGroup)
        {
            if (item.TryFindComponent_InChild<Grouping>(out Grouping attachedd))
            {
                _baseGroup.Add(attachedd);
            }
        }
        if (_baseGroup.Contains(this) == false)
        {
            _baseGroup.Add(this);
        }
    }

    void SetUP()
    {
        if (_addingSelf)
            this.AddNewObjectIntoGroup(_baseGroup);
    }

    public void Reset_List(HashSet<Grouping> visited = null)
    {
        if (visited == null)
            visited = new HashSet<Grouping>();

        // Prevent infinite recursion in case of cyclic references
        if (visited.Contains(this))
            return;

        visited.Add(this);

        // Create a copy to prevent modifying the list while iterating
        List<Grouping> tempList = new List<Grouping>(_groupInfo);

        // Process children first
        foreach (Grouping i_Move in tempList)
        {
            foreach (Grouping i_ in i_Move.GetGroup())
            {
                i_.Reset_List(visited);
            }
        }

        // Now reset the list
        _groupInfo.Clear();
        SetUP();
    }

    public void RemoveObjectFromGroup(Grouping moveable)
    {
        _groupInfo.Remove(moveable);
    }

    public void AddNewObjectIntoGroup(List<Grouping> newList)
    {
        foreach (Grouping move in newList)
        {
            if (!_groupInfo.Contains(move))
            {
                _groupInfo.Add(move);
            }
        }

        // Instead of redundant loops, just make a copy of push_able_List
        List<Grouping> allList = new List<Grouping>(_groupInfo);

        // Update each item with the new list
        foreach (Grouping e in _groupInfo)
        {
            e.SetSameGroup(allList);
        }
    }

    public List<Grouping> GetGroup() => new List<Grouping>(_groupInfo);

    public bool IsInSameGroup(Grouping i)
    {
        return _groupInfo.Contains(i);
    }

    public void SetSameGroup(List<Grouping> Origin)
    {
        _groupInfo = new List<Grouping>(Origin);

    }
}
