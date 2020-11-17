using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class DABlackboardGameObjectProperty : DABlackboardProperty<DAGameObject>
{
    public override string TypeString => "GameObject";

    public DABlackboardGameObjectProperty(DABlackboard blackboard) : base(blackboard)
    {
        ObjectField defaultValueField = new ObjectField("Default Value");
        defaultValueField.objectType = typeof(GameObject);
        defaultValueField.allowSceneObjects = false;
        defaultValueField.name = "defaultValue";
        defaultValueField.RegisterValueChangedCallback((evt) =>
        {
            blackboard.daGraphView.GraphObject.RegisterCompleteObjectUndo("Default Value");
            defaultValueField.value = evt.newValue;
            _data.value = (GameObject)evt.newValue;
            blackboard.daGraphView.Serialize();
        });
        Add(defaultValueField);
        RefreshValues();
    }

    protected override void RefreshValues()
    {
        base.RefreshValues();
        this.Q<ObjectField>("defaultValue").SetValueWithoutNotify(_data.value);
    }
}
