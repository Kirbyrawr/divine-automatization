using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DANodeGameObjectField : DANodeField
{
    private DAGameObject _variable;

    public DANodeGameObjectField(string title, DAGameObject variable, bool allowSceneObjects)
    {
        this.AddToClassList("DANodeField");

        _variable = variable;

        Label label = new Label(title) { name = "label" };
        Add(label);

        ObjectField input = new ObjectField() { name = "input", objectType = typeof(GameObject), allowSceneObjects = false };
        Add(input);

        RefreshField();

        input.RegisterValueChangedCallback((evt) =>
        {
            DAEditor.Instance.graphView.GraphObject.RegisterCompleteObjectUndo(title);
            _variable.value = (GameObject)evt.newValue;
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
        var input = this.Q<ObjectField>("input");

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
