using System;
using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEngine;
using UnityEngine.UIElements;

public class DAInspectorArrayElement<T> : VisualElement
{
    public VisualElement content;
    private List<T> _source;

    private DAEditor _editor;

    public DAInspectorArrayElement(int index, List<T> source)
    {
        _editor = DAEditor.Instance;
        _source = source;

        var box = new Box();
        box.AddToClassList("inspector-entry-box");

        //Header
        var header = new VisualElement();
        header.AddToClassList("inspector-entry-header");
        box.Add(header);

        //Header - Title
        var headerTitle = new Label($"#{index}");
        headerTitle.AddToClassList("inspector-entry-header-title");
        header.Add(headerTitle);

        //Header - Buttons
        var headerButtons = new VisualElement();
        headerButtons.AddToClassList("inspector-entry-header-buttons");
        header.Add(headerButtons);

        //Header - Buttons - Up
        var upButton = new Button();
        upButton.clickable.clicked += () => MoveElementUp((int)upButton.userData);
        upButton.userData = index;
        upButton.text = "";
        headerButtons.Add(upButton);
        if (index == 0)
        {
            upButton.SetEnabled(false);
        }

        //Header - Buttons - Down
        var downButton = new Button();
        downButton.clickable.clicked += () => MoveElementDown((int)downButton.userData);
        downButton.userData = index;
        downButton.text = "";
        headerButtons.Add(downButton);
        if (index == source.Count - 1)
        {
            downButton.SetEnabled(false);
        }

        //Header - Buttons - Remove
        var removeButton = new Button();
        removeButton.clickable.clicked += () => RemoveElement((int)removeButton.userData);
        removeButton.userData = index;
        removeButton.text = "";
        removeButton.tooltip = "Remove element";
        headerButtons.Add(removeButton);
        if (source.Count == 1)
        {
            removeButton.SetEnabled(false);
        }

        //Header - Buttons - Add
        var addButton = new Button();
        addButton.clickable.clicked += () => AddNewElement((int)addButton.userData);
        addButton.userData = index;
        addButton.text = "";
        addButton.tooltip = "Add new element";
        headerButtons.Add(addButton);

        content = new VisualElement();
        box.Add(content);

        Add(box);
    }

    public void AddToElement(VisualElement element)
    {
        content.Add(element);
    }

    //Buttons
    private void MoveElementUp(int index)
    {
        _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Remove element up");
        var origin = _source[index];
        var destination = _source[index - 1];
        _source[index] = destination;
        _source[index - 1] = origin;
        _editor.inspector.Refresh();
        _editor.graphView.Serialize();
    }

    private void MoveElementDown(int index)
    {
        _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Move element down");
        var origin = _source[index];
        var destination = _source[index + 1];
        _source[index] = destination;
        _source[index + 1] = origin;
        _editor.inspector.Refresh();
        _editor.graphView.Serialize();
    }

    private void AddNewElement(int index)
    {
        _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Add new element");
        _source.Insert(index + 1, (T)Activator.CreateInstance(typeof(T)));
        _editor.inspector.Refresh();
        _editor.graphView.Serialize();
    }

    private void RemoveElement(int index)
    {
        _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Remove element");
        _source.RemoveAt(index);
        _editor.inspector.Refresh();
        _editor.graphView.Serialize();
    }
}
