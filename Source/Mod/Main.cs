using BepInEx;
using UnityEngine;

[BepInPlugin("com.example.multitools", "MultiTools", "1.0.0")]
public class Main : BaseUnityPlugin
{
    void Awake()
    {
        Debug.Log("MultiTools plugin loaded!");
    }
}

