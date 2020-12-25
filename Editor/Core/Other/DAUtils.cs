using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class DAUtils
{
    public static List<Type> GetAllNodesAvailable()
    {
        List<Type> types = new List<Type>();

        foreach (var t in typeof(DANode).Assembly.GetTypes())
        {
            if (t.IsClass && !t.IsAbstract)
            {
                Type baseType = t.BaseType;
                while (baseType != null)
                {
                    if (baseType.UnderlyingSystemType == typeof(DANode) && t.GetCustomAttributes(true)[0].GetType() != typeof(HideInInspector))
                    {
                        types.Add(t);
                        baseType = null;
                    }
                    else
                    {
                        baseType = baseType.BaseType;
                    }
                }
            }
        }

        return types;
    }

    public static string GenerateShortID()
    {
        string id = "";
        System.Random rng = new System.Random();
        while (id.Length < 8)
        {
            if (id.Length >= 3)
            {
                int num = rng.Next(0, 26);
                id += (char)('A' + num);
            }
            else
            {
                id += rng.Next(0, 10);
            }
        }

        return id;
    }

    public static string GetPathOfScriptableObject(ScriptableObject scriptableObject)
    {
        MonoScript ms = MonoScript.FromScriptableObject(scriptableObject);
        return AssetDatabase.GetAssetPath(ms).Replace(scriptableObject.GetType().Name + ".cs", "");
    }
}
