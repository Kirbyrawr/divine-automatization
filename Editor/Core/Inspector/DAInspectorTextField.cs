using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DAInspectorTextField : DAInspectorField
{
    private DAString _variable;

    public DAInspectorTextField(string title, DAString variable)
    {
        _variable = variable;

        AddToClassList("inspector-field");

        Add(PropertiesButton());
        TextField input = new TextField() { label = title, name = "input" };
        input.isDelayed = true;
        Add(input);

        RefreshField();

        input.RegisterValueChangedCallback((evt) =>
        {
            DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo(title);
            _variable.value = evt.newValue;
        });
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
    }

    protected override DAPropertyData GetVariable()
    {
        return _variable;
    }
}
