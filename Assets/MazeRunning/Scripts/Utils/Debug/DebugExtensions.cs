using System;
using Object = UnityEngine.Object;

namespace MazeRunning.Utils.Debug
{
    public static class DebugExtensions
    {
        public static void Log(this object self, object msg)
        {
#if UNITY_EDITOR
            if (self is Object context)
                UnityEngine.Debug.Log(msg, context);
            else
                UnityEngine.Debug.Log(msg);
#endif
        }

        public static void LogWarning(this object self, object msg)
        {
#if UNITY_EDITOR
            if (self is Object context)
                UnityEngine.Debug.LogWarning(msg, context);
            else
                UnityEngine.Debug.LogWarning(msg);
#endif
        }

        public static void LogError(this object self, object msg)
        {
#if UNITY_EDITOR
            if (self is Object context)
                UnityEngine.Debug.LogError(msg, context);
            else
                UnityEngine.Debug.LogError(msg);
#endif
        }

        public static void LogException(this object self, Exception msg)
        {
#if UNITY_EDITOR
            if (self is Object context)
                UnityEngine.Debug.LogException(msg, context);
            else
                UnityEngine.Debug.LogException(msg);
#endif
        }
    }
}