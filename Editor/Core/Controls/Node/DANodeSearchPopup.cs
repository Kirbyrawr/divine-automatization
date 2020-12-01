using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Kirbyrawr.DivineAutomatization;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Searcher;

public class SearcherNodeEntry : SearcherItem
{
    public Type type;
    public SearcherNodeEntry(string name, string help = "", List<SearcherItem> children = null) : base(name, help, children)
    {
    }
}

public class DANodeSearchPopup : ScriptableObject
{
    private DAEditor _editor;

    public void Setup(DAEditor editor)
    {
        _editor = editor;
    }

    public Searcher LoadSearchWindow()
    {
        List<SearcherItem> root = new List<SearcherItem>();

        foreach (var item in DAUtils.GetAllNodesAvailable())
        {
            var name = ((TitleAttribute)item.GetCustomAttributes(typeof(TitleAttribute), false)[0]).Title;
            var entry = new SearcherNodeEntry(name);
            entry.type = item;
            root.Add(entry);
        }

        var nodeDatabase = SearcherDatabase.Create(root, string.Empty, false);

        return new Searcher(nodeDatabase, new SearcherAdapter("Create Node"));
    }

    public bool OnSearcherSelectEntry(SearcherItem entry, Vector2 screenMousePosition)
    {
        if (entry != null)
        {
            var windowRoot = _editor.rootVisualElement;
            var windowMousePosition = windowRoot.ChangeCoordinatesTo(windowRoot.parent, screenMousePosition); //- m_EditorWindow.position.position);
            var graphMousePosition = _editor.graphView.contentViewContainer.WorldToLocal(windowMousePosition);

            var nodeEntry = (SearcherNodeEntry)entry;
            _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Add " + nodeEntry.Name);
            _editor.graphView.CreateAndAddNode(nodeEntry.type, graphMousePosition);
            return true;
        }
        else
        {
            return false;
        }
    }
}
