using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Kirbyrawr.DivineAutomatization;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Linq;

public class DAEnumSearchPopup : ScriptableObject, ISearchWindowProvider
{
    public class EnumData
    {
        public string Namespace;
        public Type type;
    }
    public DAEnum enumEditing;
    public System.Action OnSelectEntryEvent;

    private DAEditor _editor;
    private Texture2D _icon;
    private List<EnumData> _enums;
    private Dictionary<string, SearchTreeGroupEntry> _groups;

    public void Setup(DAEditor editor)
    {
        _editor = editor;
        // Transparent icon to trick search window into indenting items
        _icon = new Texture2D(1, 1);
        _icon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        _icon.Apply();

        _enums = new List<EnumData>();
        foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsEnum)
                {
                    var enumData = new EnumData();
                    enumData.Namespace = !string.IsNullOrEmpty(type.Namespace) ? type.Namespace : "Uncategorized";
                    enumData.type = type;
                    _enums.Add(enumData);
                }
            }
        }

        _enums.Sort((a, b) => { return a.Namespace.CompareTo(b.Namespace); });
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> tree = new List<SearchTreeEntry>();
        _groups = new Dictionary<string, SearchTreeGroupEntry>();

        tree.Add(new SearchTreeGroupEntry(new GUIContent("Select Enum"), 0));

        foreach (var enumData in _enums)
        {
            if (!_groups.ContainsKey(enumData.Namespace))
            {
                var group = new SearchTreeGroupEntry(new GUIContent(enumData.Namespace), 1);
                tree.Add(group);
                _groups.Add(enumData.Namespace, group);
            }

            var entry = new SearchTreeEntry(new GUIContent(enumData.type.Name, _icon));
            entry.userData = enumData.type;
            entry.level = 2;
            tree.Add(entry);
        }

        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
    {
        _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Set Enum Type");
        enumEditing.type = (Type)entry.userData;
        OnSelectEntryEvent.Invoke();
        return true;
    }
}
