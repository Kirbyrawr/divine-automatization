using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
sealed class TitleAttribute : System.Attribute
{

    private readonly string title;

    public TitleAttribute(string title)
    {
        this.title = title;
    }

    public string Title
    {
        get { return title; }
    }
}