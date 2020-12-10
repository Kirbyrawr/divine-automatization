using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class DAInspectorField : VisualElement
{
    protected string _reference;

    public abstract void SetReference(string reference);

    protected virtual Button PropertiesButton()
    {
        Button propertiesButton = new Button() { name = "properties-button" };
        propertiesButton.clicked += () => OpenPropertiesPopup(GetVariable(), propertiesButton.worldBound.position);
        propertiesButton.text = "";
        return propertiesButton;
    }

    protected virtual void OpenPropertiesPopup(DAPropertyData data, Vector2 position)
    {
        var searchPopup = DAEditor.Instance.searchPropertiesPopup;
        searchPopup.acceptedType = data.GetValueType();
        searchPopup.field = this;

        var actualScreenPosition = position;
        actualScreenPosition.x += 202;
        actualScreenPosition.y += 34;
        SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(actualScreenPosition), 400, 300), searchPopup);
    }

    protected abstract DAPropertyData GetVariable();
}
