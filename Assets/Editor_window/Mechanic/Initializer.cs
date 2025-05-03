using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Initializer
{
    public static void ExecuteInitialize()
    {
        List<bbg_IInitialize> initializables = new List<bbg_IInitialize>();
        Dictionary<int, List<Action>> ExecuteOrder = new Dictionary<int, List<Action>>();

        // Find all MonoBehaviours that implement bbg_IInitialize
        foreach (var mono in GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
        {
            if (mono is bbg_IInitialize initializer)
            {
                initializables.Add(initializer);
            }
        }

        // Group all initialize actions by layer
        foreach (var init in initializables)
        {
            int layer = init.InitializeLayer();
            Action action = init.Initialize();

            if (!ExecuteOrder.ContainsKey(layer))
            {
                ExecuteOrder[layer] = new List<Action>();
            }

            ExecuteOrder[layer].Add(action);
        }

        // Execute actions starting from key 0 and going upward
        int maxLayer = ExecuteOrder.Keys.Max();

        for (int i = 0; i <= maxLayer; i++)
        {
            if (ExecuteOrder.ContainsKey(i))
            {
                foreach (Action execute in ExecuteOrder[i])
                {
                    execute?.Invoke();
                }
            }
        }
    }


}
