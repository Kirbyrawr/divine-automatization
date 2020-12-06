using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kirbyrawr.DivineAutomatization;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DABlackboardEnumProperty : DABlackboardProperty<DAEnum>
{
    public override string TypeString => "Enum";

    public DABlackboardEnumProperty(DABlackboard blackboard) : base(blackboard)
    {
        EnumField defaultValueField = new EnumField("Default Value", (System.Enum)System.Enum.Parse(_data.type, "Empty"));
        defaultValueField.name = "defaultValue";
        defaultValueField.RegisterValueChangedCallback((evt) =>
        {
            blackboard.daGraphView.GraphObject.RegisterCompleteObjectUndo("Default Value");
            defaultValueField.value = evt.newValue;
            _data.value = evt.newValue;
            blackboard.daGraphView.Serialize();
        });
        defaultValueField.SetValueWithoutNotify(_data.value);
        Add(defaultValueField);

        VisualElement enumTypeContainer = new VisualElement();
        enumTypeContainer.style.flexShrink = 1;
        enumTypeContainer.style.flexDirection = FlexDirection.Row;

        TextField typeField = new TextField("Type");
        typeField.name = "type";
        typeField.style.flexGrow = 1;
        typeField.style.flexShrink = 1;
        typeField.ElementAt(1).SetEnabled(false);
        enumTypeContainer.Add(typeField);

        Button selectTypeButton = new Button();
        selectTypeButton.style.flexShrink = 0;
        selectTypeButton.text = "▼";
        selectTypeButton.clickable.clicked += () => ShowEnumPopup(selectTypeButton.worldBound.position);
        enumTypeContainer.Add(selectTypeButton);

        Add(enumTypeContainer);

        RefreshValues();
    }

    private void ShowEnumPopup(Vector2 position)
    {
        position.x += 200;
        position.y += 38;
        DAEditor.Instance.searchEnumPopup.enumEditing = _data;
        DAEditor.Instance.searchEnumPopup.OnSelectEntryEvent = OnSelectEnumType;
        SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(position) * UnityEditor.EditorGUIUtility.pixelsPerPoint, 400), DAEditor.Instance.searchEnumPopup);
    }

    private void OnSelectEnumType()
    {
        _blackboard.daGraphView.GraphObject.RegisterCompleteObjectUndo("Set Enum Type");
        DAEditor.Instance.searchEnumPopup.OnSelectEntryEvent = null;
        _data.value = (Enum)System.Enum.GetValues(_data.type).GetValue(0);
        this.Q<EnumField>("defaultValue").Init(_data.value);
        _blackboard.daGraphView.Serialize();
        RefreshValues();
    }

    protected override void RefreshValues()
    {
        base.RefreshValues();
        this.Q<EnumField>("defaultValue").Init(_data.value);
        this.Q<TextField>("type").SetValueWithoutNotify(_data.type.FullName);
    }
}