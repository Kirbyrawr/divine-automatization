using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class DANodeField : VisualElement
{
    protected string _reference;

    public abstract void SetReference(string reference);
    protected virtual void OpenPropertiesPopup(DAPropertyData data)
    {
        var searchPopup = DAEditor.Instance.searchPropertiesPopup;
        searchPopup.acceptedType = data.GetValueType();
        searchPopup.field = this;
        SearchWindow.Open(new SearchWindowContext(Vector2.zero, 400), DAEditor.Instance.searchPropertiesPopup);
    }
}
