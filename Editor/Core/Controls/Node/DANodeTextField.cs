using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DANodeTextField : DANodeField
{
    private DAString _variable;

    public DANodeTextField(string title, DAString variable)
    {
        this.AddToClassList("DANodeField");

        _variable = variable;

        Label label = new Label(title) { name = "label" };
        Add(label);

        TextField input = new TextField() { name = "input" };
        input.isDelayed = true;
        Add(input);

        RefreshField();

        input.RegisterValueChangedCallback<string>((evt) =>
        {
            DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo(title);
            _variable.value = evt.newValue;
        });

        Button propertiesButton = new Button() { name = "properties-button" };
        propertiesButton.clicked += () => OpenPropertiesPopup(_variable, propertiesButton.worldBound.position);
        propertiesButton.text = "...";
        Add(propertiesButton);
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
}
