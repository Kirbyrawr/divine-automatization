using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Copy")]
    public class CopyNode : DANode<CopyTask>
    {
        protected override string _nodeTitle => "Copy";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Target
            var targetPathField = new DAInspectorTextField("Target Path", _task.targetPath);
            root.Add(targetPathField);

            //Name
            var destinationPathField = new DAInspectorTextField("Destination Path", _task.destinationPath);
            root.Add(destinationPathField);

            return root;
        }
    }
}