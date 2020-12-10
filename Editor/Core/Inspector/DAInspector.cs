using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
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

        _title = new Label("TEST");
        _title.name = "title";
        header.Add(_title);

        _content = new VisualElement();
        _inspector.Add(_content);

        _editor.graphLayout.Add(_inspector);
    }

    public void SetContent(DANode node)
    {
        _content.Clear();
        _title.text = node.title;
        _content.Add(node.InspectorContent());
    }
}
