using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Delete")]
    public class DeleteNode : DANode<DeleteTask>
    {
        protected override string _nodeTitle => "Delete";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Target
            var targetPathField = new DAInspectorTextField("Target Path", _task.targetPath);
            root.Add(targetPathField);

            return root;
        }
    }
}