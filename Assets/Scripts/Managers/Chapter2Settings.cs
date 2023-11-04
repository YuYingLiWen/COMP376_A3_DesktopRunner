﻿
using UnityEngine;

public class Chapter2Settings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
