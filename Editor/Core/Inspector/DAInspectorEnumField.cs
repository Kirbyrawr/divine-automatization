using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DAInspectorEnumField : DAInspectorField
{
    private DAEnum _variable;

    public DAInspectorEnumField(string title, DAEnum variable)
    {
        _variable = variable;

        AddToClassList("inspector-field");

        Add(PropertiesButton());

        EnumField input = new EnumField(variable.value) { label = title, name = "input" };
        Add(input);

        RefreshField();

        input.RegisterValueChangedCallback((evt) =>
        {
            DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo(title);
            _variable.value = evt.newValue;
            DAEditor.Instance.graphView.Serialize();
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
        var input = this.Q<EnumField>("input");

        if (string.IsNullOrEmpty(_variable.reference))
        {
            input.SetValueWithoutNotify(_variable.value);
            input.SetEnabled(true);
        }
        else
        {
            //input.SetValueWithoutNotify(_variable.reference);
            input.SetEnabled(false);
        }
    }

    protected override DAPropertyData GetVariable()
    {
        return _variable;
    }
}
