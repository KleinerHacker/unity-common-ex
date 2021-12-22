using System;
using System.Linq;
using System.Reflection;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;

namespace UnityCommonEx.Editor.common_ex.Scripts.Editor.Utils
{
    public static class PlayerSettingsEx
    {
        public static void AddDefine(string define)
        {
            foreach (var group in Enum.GetValues(typeof(BuildTargetGroup)).Cast<BuildTargetGroup>())
            {
                var definesStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
                var defines = definesStr.Split(',');
                if (defines.Contains(define))
                    return;

                try
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines.Append(define).ToArray());
                }
                catch (ArgumentException)
                {
                }
            }
        }

        public static void RemoveDefine(string define)
        {
            foreach (var group in Enum.GetValues(typeof(BuildTargetGroup)).Cast<BuildTargetGroup>())
            {
                var definesStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(group);
                var defines = definesStr.Split(',');
                if (!defines.Contains(define))
                    return;

                try
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(group, defines.Remove(define).ToArray());
                }
                catch (ArgumentException)
                {
                }
            }
        }
    }
}