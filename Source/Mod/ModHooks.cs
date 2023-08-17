using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

public class ModHooks
{
    private static readonly List<Action> _onLoadActions = new List<Action>();
    private static readonly List<Action> _onSaveActions = new List<Action>();
    private static readonly List<Action> _onUpdateActions = new List<Action>();
    private static readonly List<Action> _onGUIActions = new List<Action>();
    private static readonly List<Action> _onApplicationQuitActions = new List<Action>();

    public static void AddOnLoadAction(Action action)
    {
        if (action != null)
        {
            _onLoadActions.Add(action);
        }
        else
        {
            Debug.Log("Tried to add null OnLoad action.");
        }
    }

    public static void AddOnSaveAction(Action action)
    {
        if (action != null)
        {
            _onSaveActions.Add(action);
        }
        else
        {
            Debug.Log("Tried to add null OnSave action.");
        }
    }

    public static void AddOnUpdateAction(Action action)
    {
        if (action != null)
        {
            _onUpdateActions.Add(action);
        }
        else
        {
            Debug.Log("Tried to add null OnUpdate action.");
        }
    }

    public static void AddOnGUIAction(Action action)
    {
        if (action != null)
        {
            _onGUIActions.Add(action);
        }
        else
        {
            Debug.Log("Tried to add null OnGUI action.");
        }
    }

    public static void AddOnApplicationQuitAction(Action action)
    {
        if (action != null)
        {
            _onApplicationQuitActions.Add(action);
        }
        else
        {
            Debug.Log("Tried to add null OnApplicationQuit action.");
        }
    }

    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("Load")]
    public class Load_Patch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (var action in _onLoadActions)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error executing OnLoad action: {ex}");
                }
            }
        }
    }

    [HarmonyPatch(typeof(SaveLoadManager))]
    [HarmonyPatch("Save")]
    public class Save_Patch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (var action in _onSaveActions)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error executing OnSave action: {ex}");
                }
            }
        }
    }

    [HarmonyPatch(typeof(uGUI_MainMenu))]
    [HarmonyPatch("Update")]
    public class Update_Patch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (var action in _onUpdateActions)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error executing OnUpdate action: {ex}");
                }
            }
        }
    }

    [HarmonyPatch(typeof(uGUI))]
    [HarmonyPatch("Update")]
    public class GUI_Patch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (var action in _onGUIActions)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error executing OnGUI action: {ex}");
                }
            }
        }
    }

    [HarmonyPatch(typeof(Application))]
    [HarmonyPatch("Quit")]
    public class Quit_Patch
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            foreach (var action in _onApplicationQuitActions)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error executing OnApplicationQuit action: {ex}");
                }
            }
        }
    }
}