using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DAPath : MonoBehaviour
{
    public static readonly Dictionary<string, Environment.SpecialFolder> specialFolders = new Dictionary<string, Environment.SpecialFolder>()
    {
        {"<APPLICATION_DATA>", Environment.SpecialFolder.ApplicationData},
        {"<COMMON_APPLICATION_DATA>", Environment.SpecialFolder.CommonApplicationData},
        {"<DESKTOP>", Environment.SpecialFolder.Desktop},
        {"<LOCAL_APPLICATION_DATA>", Environment.SpecialFolder.LocalApplicationData},
        {"<MY_DOCUMENTS>", Environment.SpecialFolder.MyDocuments},
        {"<PERSONAL>", Environment.SpecialFolder.Personal},
        {"<PROGRAM_FILES>", Environment.SpecialFolder.ProgramFiles},
        {"<PROGRAMS>", Environment.SpecialFolder.Programs},
        {"<USER_PROFILE>", Environment.SpecialFolder.UserProfile}
    };

    public static string FormatPath(string path)
    {
        if (string.IsNullOrEmpty(path)) { return Directory.GetCurrentDirectory(); }

        var formattedPath = "";

        foreach (var folder in specialFolders)
        {
            if (path.StartsWith(folder.Key))
            {
                formattedPath = path.Replace(folder.Key, Environment.GetFolderPath(folder.Value));
                break;
            }
        }

        if (IsValidPath(formattedPath))
        {
            return Path.GetFullPath(formattedPath);
        }
        else if (IsValidDirectoryInfoPath(path))
        {
            return new DirectoryInfo(path).FullName;
        }
        else
        {
            return "Path not valid";
        }
    }

    public static bool IsValidPath(string path)
    {
        try
        {
            Path.GetFullPath(path);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public static bool IsValidDirectoryInfoPath(string path)
    {
        try
        {
            new DirectoryInfo(path);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
