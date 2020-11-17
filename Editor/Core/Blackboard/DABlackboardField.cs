using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DABlackboardPropertyField : BlackboardField
{
    public DABlackboardProperty Property { get; private set; }

    private new DABlackboard blackboard;
    private DABlackboardPropertyRow _row;

    public DABlackboardPropertyField(DABlackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public void Setup(DABlackboardPropertyRow row, DABlackboardProperty property)
    {
        _row = row;
        Property = property;
        text = Property.TypeString;
        typeText = Property.TypeString;

        blackboard.editTextRequested += ((Blackboard, element, value) =>
        {
            if (element == this)
            {
                blackboard.daGraphView.GraphObject.RegisterCompleteObjectUndo("Property Name");
                Property.GetData().name = value;
                text = value;
                blackboard.daGraphView.Serialize();
            }
        });

        this.AddManipulator(new ContextualMenuManipulator(BuildContextualMenu));
    }

    private void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        evt.menu.AppendAction("Rename", delegate
        {
            this.OpenTextEditor();
        }, DropdownMenuAction.AlwaysEnabled);

        evt.menu.AppendSeparator();

        evt.menu.AppendAction("Delete", (action) =>
        {
            blackboard.RemoveProperty(_row);
        }, DropdownMenuAction.AlwaysEnabled);
    }

}
