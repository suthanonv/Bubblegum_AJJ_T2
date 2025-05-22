using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputMove_Holder : MonoBehaviour
{
    List<Base_Movement> moveObject = new List<Base_Movement>();
    Action<Vector2Int, List<Base_Movement>, Action> moveCall;
    Action _onfinishmove;

    List<LayerInfo> allLayer = new List<LayerInfo>();
    int currentLayerIndex;

    private void Start()
    {
        FindAnyObjectByType<Input_handle>().AddMovementListener(MoveAll_call);
    }

    #region Add/remove moveable class
    public void Add_movealbe(Base_Movement move)
    {
        if (moveObject.Contains(move)) return;
        moveObject.Add(move);
    }

    public void Remove_moveable(Base_Movement move)
    {
        moveObject.Remove(move);
        LayerInfo getLayer = allLayer.FirstOrDefault(i => i.Direction == move.GetDirection(InputDirection) && i.LayerIndex == move.Movelayer);
        if (getLayer != null) getLayer.Data.ObjectToMove.Remove(move);

    }
    #endregion

    #region Adding/call move function
    public void Add_moveCall_Listener(Action<Vector2Int, List<Base_Movement>, Action> newFunc)
    {
        moveCall += newFunc;
    }
    #endregion

    #region Add callback
    public void OnFinishMove_AddListener(Action func)
    {
        _onfinishmove += func;
    }
    #endregion
    Vector2Int InputDirection;

    public void MoveAll_call(Vector2Int Direction)
    {
        InputDirection = Direction;
        GenLayer(Direction);
        currentLayerIndex = 0;
        ExecuteNextLayer();
    }

    void GenLayer(Vector2Int input)
    {
        allLayer = new List<LayerInfo>();

        foreach (Base_Movement move in moveObject)
        {
            Vector2Int dir = move.GetDirection(input);
            int layerIndex = move.Movelayer;

            LayerInfo getLayer = allLayer.FirstOrDefault(i => i.Direction == dir && i.LayerIndex == layerIndex);

            if (getLayer != null)
            {
                getLayer.Data.ObjectToMove.Add(move);
            }
            else
            {
                LayerInfo layerInfo = new LayerInfo(layerIndex, dir);
                layerInfo.Data.ObjectToMove.Add(move);
                allLayer.Add(layerInfo);
            }
        }

        // Sort layers in ascending order
        allLayer = allLayer.OrderBy(i => i.LayerIndex).ToList();

        // Link to next layer if needed
        for (int i = 0; i < allLayer.Count - 1; i++)
        {
            allLayer[i].Data.NextLayer = allLayer[i + 1];
        }
    }

    void ExecuteNextLayer()
    {
        if (currentLayerIndex >= allLayer.Count)
        {
            _onfinishmove?.Invoke(); // all done
            return;
        }

        LayerInfo layer = allLayer[currentLayerIndex];
        currentLayerIndex++;

        ExcuteLayer(layer, ExecuteNextLayer); // move current layer and wait for callback
    }

    void ExcuteLayer(LayerInfo layer, Action callBack)
    {
        moveCall?.Invoke(InputDirection, layer.Data.ObjectToMove, callBack);
    }
}

public class LayerInfo
{
    public LayerInfo(int index, Vector2Int direction)
    {
        LayerIndex = index;
        Direction = direction;
        Data = new moveLayerData();
    }

    public int LayerIndex;
    public Vector2Int Direction;
    public moveLayerData Data;
}

public class moveLayerData
{
    public List<Base_Movement> ObjectToMove = new List<Base_Movement>();
    public LayerInfo NextLayer;
}
