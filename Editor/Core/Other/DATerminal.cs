using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DATerminal
{
    public static Process Run(string command)
    {
        Process process = new Process();
        process.StartInfo.FileName = "sh.exe";
        process.StartInfo.Arguments = command;
        process.StartInfo.CreateNoWindow = true;

        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardError = true;

        process.Start();
        return process;
    }
}
