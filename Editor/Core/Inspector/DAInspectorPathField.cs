using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DAInspectorPathField : DAInspectorField
{
    private DAString _variable;

    public DAInspectorPathField(string title, DAString variable)
    {
        _variable = variable;

        AddToClassList("inspector-field");

        Add(PropertiesButton());

        var pathField = new VisualElement();
        pathField.AddToClassList("path-field");
        Add(pathField);

        var titleLabel = new Label(title);
        titleLabel.AddToClassList("unity-base-field__label");
        titleLabel.AddToClassList("unity-label");
        pathField.Add(titleLabel);

        var pathButton = new Button();
        pathButton.AddToClassList("inspector-field-path-main-button");
        pathButton.clicked += () => OpenPathEditor(pathButton);
        if (string.IsNullOrEmpty(_variable.value))
        {
            pathButton.text = "/";
        }
        else
        {
            pathButton.text = _variable.value;
        }
        pathButton.tooltip = DAPath.FormatPath(_variable.value);
        pathField.Add(pathButton);

        Button edit = new Button() { text = "" };
        edit.AddToClassList("inspector-field-path-edit");
        edit.clickable.clicked += () => OpenPathEditor(edit);
        pathField.Add(edit);

        RefreshField();
    }

    public override void SetReference(string reference)
    {
        DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo("Set Property");
        _variable.value = null;
        _variable.reference = reference;
        RefreshField();
        DAEditor.Instance.graphView.Serialize();
    }

    private void RefreshField()
    {
        /*
        var input = this.Q<TextField>("input");

        if (string.IsNullOrEmpty(_variable.reference))
        {
            input.SetValueWithoutNotify(_variable.value);
            input.SetEnabled(true);
        }
        else
        {
            input.SetValueWithoutNotify(_variable.reference);
            input.SetEnabled(false);
        }
        */
    }

    protected override DAPropertyData GetVariable()
    {
        return _variable;
    }

    private void OpenPathEditor(VisualElement element)
    {
        var pathEditor = ScriptableObject.CreateInstance<DAPathEditor>();
        var rect = element.worldBound;
        rect.position += DAEditor.Instance.position.position;
        pathEditor.Open(rect, _variable, () => DAEditor.Instance.inspector.Refresh());
    }
}
