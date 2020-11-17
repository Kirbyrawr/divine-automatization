using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Kirbyrawr.DivineAutomatization;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class DANodeSearchPopup : ScriptableObject, ISearchWindowProvider
{
    private DAEditor _editor;
    private Texture2D _icon;

    public void Setup(DAEditor editor)
    {
        _editor = editor;
        // Transparent icon to trick search window into indenting items
        _icon = new Texture2D(1, 1);
        _icon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        _icon.Apply();
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> tree = new List<SearchTreeEntry>();
        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Node"), 0));

        foreach (var item in DAUtils.GetAllNodesAvailable())
        {
            var name = ((TitleAttribute)item.GetCustomAttributes(typeof(TitleAttribute), false)[0]).Title;
            var entry = new SearchTreeEntry(new GUIContent(name, _icon));
            entry.userData = item;
            entry.level = 1;
            tree.Add(entry);
        }

        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
    {
        _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Add " + entry.name);
        _editor.graphView.CreateAndAddNode((Type)entry.userData, _editor.rootVisualElement.ChangeCoordinatesTo(_editor.rootVisualElement, context.screenMousePosition - _editor.position.position));
        return true;
    }
}
