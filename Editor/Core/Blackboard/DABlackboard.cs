using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DABlackboard : Blackboard
{
    public DAGraphView daGraphView;
    public ScrollView PropertiesScrollView { get; private set; }
    public Dictionary<string, DABlackboardPropertyRow> PropertyRows { get; private set; }
    private BlackboardSection _propertiesSection;

    public DABlackboard(DAGraphView graph)
    {
        daGraphView = graph;
        SetPosition(new Rect(0, 0, 350, 500));
        Setup();
    }

    private void Setup()
    {
        PropertyRows = new Dictionary<string, DABlackboardPropertyRow>();
        this.addItemRequested += ShowAddMenu;
        _propertiesSection = new BlackboardSection();
        _propertiesSection.title = "Properties";

        PropertiesScrollView = new ScrollView();
        _propertiesSection.Add(PropertiesScrollView);
        Add(_propertiesSection);
    }

    public void ShowAddMenu(Blackboard blackboard)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("string"), false, () => CreateProperty(typeof(DABlackboardStringProperty)));
        menu.AddItem(new GUIContent("enum"), false, () => CreateProperty(typeof(DABlackboardEnumProperty)));
        menu.AddItem(new GUIContent("GameObject"), false, () => CreateProperty(typeof(DABlackboardGameObjectProperty)));
        menu.ShowAsContext();
    }

    public void AddProperty(DABlackboardPropertyRow row)
    {
        PropertiesScrollView.Add(row);
        PropertyRows.Add(row.Property.GetData().reference, row);
    }

    public DABlackboardPropertyRow CreateProperty(System.Type type)
    {
        daGraphView.GraphObject.RegisterCompleteObjectUndo("Add Property");
        DABlackboardPropertyField field = new DABlackboardPropertyField(this);
        DABlackboardProperty property = (DABlackboardProperty)System.Activator.CreateInstance(type, this);
        DABlackboardPropertyRow row = new DABlackboardPropertyRow(field, property);
        PropertiesScrollView.Add(row);
        PropertyRows.Add(property.GetData().reference, row);
        return row;
    }

    public void RemoveProperty(DABlackboardPropertyRow row)
    {
        daGraphView.GraphObject.RegisterCompleteObjectUndo("Delete Property");
        PropertyRows.Remove(row.Field.Property.GetData().reference);
        PropertiesScrollView.Remove(row);
        daGraphView.Serialize();
    }
}
