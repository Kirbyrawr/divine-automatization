using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Move")]
    public class MoveNode : DANode<MoveTask>
    {
        protected override string _nodeTitle => "Move";

        protected override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Target
            var targetPathField = new DANodeTextField("Target Path", _task.targetPath);
            root.Add(targetPathField);

            //Name
            var destinationPathField = new DANodeTextField("Destination Path", _task.destinationPath);
            root.Add(destinationPathField);

            return root;
        }
    }
}