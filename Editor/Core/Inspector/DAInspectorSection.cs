using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DAInspectorSection : VisualElement
{
    public VisualElement content;

    public DAInspectorSection(string title) : base()
    {
        var header = new Box();
        header.AddToClassList("inspector-section-header");
        Add(header);

        var titleLabel = new Label(title);
        header.Add(titleLabel);

        content = new VisualElement();
        content.AddToClassList("inspector-section-content");
        Add(content);
    }

    public void AddToSection(VisualElement element)
    {
        content.Add(element);
    }
}
