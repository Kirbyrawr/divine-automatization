using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEngine;
using UnityEngine.UIElements;

public class DABlackboardStringProperty : DABlackboardProperty<DAString>
{
    public override string TypeString => "String";

    public DABlackboardStringProperty(DABlackboard blackboard) : base(blackboard)
    {
        TextField defaultValueField = new TextField("Default Value");
        defaultValueField.name = "defaultValue";
        defaultValueField.RegisterValueChangedCallback((evt) =>
        {
            blackboard.daGraphView.GraphObject.RegisterCompleteObjectUndo("Default Value");
            defaultValueField.value = evt.newValue;
            _data.value = evt.newValue;
            blackboard.daGraphView.Serialize();
        });
        Add(defaultValueField);
        RefreshValues();
    }

    protected override void RefreshValues()
    {
        base.RefreshValues();
        this.Q<TextField>("defaultValue").SetValueWithoutNotify(_data.value);
    }
}
