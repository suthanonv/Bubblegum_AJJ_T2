using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class UndoAndRedo<T> : MonoBehaviour
{
    protected Stack<T> undoStack = new Stack<T>();
    protected Stack<T> redoStack = new Stack<T>();

    public virtual void RegisterState(T snapshot)
    {
        undoStack.Push(snapshot);
        redoStack.Clear();
    }

    public virtual T UndoState(Func<T> getCurrentSnapshot)
    {
        if (undoStack.Count == 0)
        {
            Debug.LogWarning($"[{gameObject.name}] Undo stack is empty.");
            return default(T); 
        }

        T current = getCurrentSnapshot();
        redoStack.Push(current);

        return undoStack.Pop();
    }

    public virtual T RedoState(Func<T> getCurrentSnapshot)
    {
        if (redoStack.Count == 0)
        {
            Debug.LogWarning($"[{gameObject.name}] Redo stack is empty.");
            return default(T); 
        }

        T current = getCurrentSnapshot();
        undoStack.Push(current);

        return redoStack.Pop();
    }


    public void Clear()
    {
        undoStack.Clear();
        redoStack.Clear();
    }

    public int UndoCount => undoStack.Count;
    public int RedoCount => redoStack.Count;


   

}
