using UnityEngine;
using System;

public static class Logger
{
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(FormatMessage(message));
    }

    public static void LogWarning(string message)
    {
        UnityEngine.Debug.LogWarning(FormatMessage(message));
    }

    public static void LogError(string message)
    {
        UnityEngine.Debug.LogError(FormatMessage(message));
    }

    private static string FormatMessage(string message)
    {
        return $"[{DateTime.Now:HH:mm:ss}] {message}";
    }
}