using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace HackathonA
{
    public class DebugLoger
    {
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void LogError(string error)
        {
            UnityEngine.Debug.LogError(error);
        }

        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string error)
        {
            UnityEngine.Debug.Log(error);
        }
    }
}
