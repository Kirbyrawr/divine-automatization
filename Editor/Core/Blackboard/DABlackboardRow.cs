using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DABlackboardPropertyRow : BlackboardRow
{
    public DABlackboardPropertyField Field { get; private set; }
    public DABlackboardProperty Property { get; private set; }

    public DABlackboardPropertyRow(DABlackboardPropertyField field, DABlackboardProperty property) : base(field, property)
    {
        Field = field;
        Property = property;

        if (property != null)
        {
            field.Setup(this, property);
        }
    }
}
