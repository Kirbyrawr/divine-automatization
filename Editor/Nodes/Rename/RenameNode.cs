using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Rename")]
    public class RenameNode : DANode<RenameTask>
    {
        protected override string _nodeTitle => "Rename";

        protected override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Target
            var targetPathField = new DANodeTextField("Target Path", _task.targetPath);
            root.Add(targetPathField);

            //Name
            var newNameField = new DANodeTextField("New Name", _task.newName);
            root.Add(newNameField);

            return root;
        }
    }
}