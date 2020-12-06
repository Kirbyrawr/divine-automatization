using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Delete")]
    public class DeleteNode : DANode<DeleteTask>
    {
        protected override string _nodeTitle => "Delete";

        protected override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Target
            var targetPathField = new DANodeTextField("Target Path", _task.targetPath);
            root.Add(targetPathField);

            return root;
        }
    }
}