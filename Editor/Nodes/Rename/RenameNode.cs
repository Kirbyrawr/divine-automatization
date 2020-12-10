using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Rename")]
    public class RenameNode : DANode<RenameTask>
    {
        protected override string _nodeTitle => "Rename";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Target
            var targetPathField = new DAInspectorTextField("Target Path", _task.targetPath);
            root.Add(targetPathField);

            //Name
            var newNameField = new DAInspectorTextField("New Name", _task.newName);
            root.Add(newNameField);

            return root;
        }
    }
}