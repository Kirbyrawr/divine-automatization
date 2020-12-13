using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DAInspector
{
    private Label _title;
    private DAEditor _editor;

    private VisualElement _inspector;
    private VisualElement _content;

    public DAInspector(DAEditor editor)
    {
        _editor = editor;
        _inspector = new VisualElement();
        _inspector.styleSheets.Add(_editor.Styles["Inspector"]);
        _inspector.AddToClassList("inspector");
        _inspector.style.width = 350;

        var header = new VisualElement();
        header.AddToClassList("header");
        _inspector.Add(header);

        _title = new Label("N/A");
        _title.AddToClassList("node-title");
        header.Add(_title);

        _content = new ScrollView();
        _content.AddToClassList("inspector-content");
        _inspector.Add(_content);

        _editor.graphLayout.Add(_inspector);
    }

    public void SetContent(DANode node)
    {
        _editor.graphView.GraphObject.sessionData.selectedNode = node.viewDataKey;
        _editor.graphView.nodeSelected = node;
        _content.Clear();
        _title.text = _editor.graphView.nodeSelected.title;
        _content.Add(_editor.graphView.nodeSelected.InspectorContent());
    }

    public void Refresh()
    {
        if (_editor.graphView.nodeSelected == null) { return; }
        _content.Clear();
        _title.text = _editor.graphView.nodeSelected.title;
        _content.Add(_editor.graphView.nodeSelected.InspectorContent());
    }

    public void UndoRedoPerformed()
    {
        _editor.graphView.nodeSelected = (DANode)_editor.graphView.GetNodeByGuid(_editor.graphView.GraphObject.sessionData.selectedNode);
        Refresh();
    }
}
