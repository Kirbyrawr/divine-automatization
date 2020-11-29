using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DANodeEnumField : DANodeField
{
    private DAEnum _variable;

    public DANodeEnumField(string title, DAEnum variable)
    {
        this.AddToClassList("DANodeField");

        _variable = variable;

        Label label = new Label(title) { name = "label" };
        Add(label);

        EnumField input = new EnumField(variable.value) { name = "input" };
        Add(input);

        RefreshField();

        input.RegisterValueChangedCallback<Enum>((evt) =>
        {
            DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo(title);
            _variable.value = evt.newValue;
            DAEditor.Instance.graphView.Serialize();
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
}
