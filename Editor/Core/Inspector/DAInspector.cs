using System.Collections;
using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEngine;
using UnityEngine.UIElements;

public class DAInspector
{
    private DAEditor _editor;
    private VisualElement _inspector;

    public DAInspector(DAEditor editor)
    {
        _editor = editor;
        _inspector = new VisualElement();
        _inspector.style.width = 300;
        _editor.graphLayout.Add(_inspector);
    }

    public void SetContent(VisualElement content)
    {
        _inspector.Clear();
        _inspector.Add(content);
    }
}
