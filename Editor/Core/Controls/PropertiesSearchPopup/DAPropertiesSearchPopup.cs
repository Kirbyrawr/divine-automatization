using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Kirbyrawr.DivineAutomatization;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class DAPropertiesSearchPopup : ScriptableObject, ISearchWindowProvider
{
    public Type acceptedType;
    public DANodeField field;
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
        tree.Add(new SearchTreeGroupEntry(new GUIContent("Select Property"), 0));

        //Add None entry
        var noneEntry = new SearchTreeEntry(new GUIContent("None", _icon));
        noneEntry.level = 1;
        tree.Add(noneEntry);

        foreach (var row in _editor.blackboard.PropertyRows)
        {
            var propertyData = row.Value.Property.GetData();
            if (propertyData.GetValueType() == acceptedType)
            {
                var name = $"{propertyData.name} ({propertyData.reference})";
                var entry = new SearchTreeEntry(new GUIContent(name, _icon));
                entry.userData = propertyData.reference;
                entry.level = 1;
                tree.Add(entry);
            }
        }

        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
    {
        field.SetReference((string)entry.userData);
        return true;
    }
}
