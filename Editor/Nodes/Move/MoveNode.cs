using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Move")]
    public class MoveNode : DANode<MoveTask>
    {
        protected override string _nodeTitle => "Move";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            for (int i = 0; i < _task.data.Count; i++)
            {
                var entry = _task.data[i];
                var element = new DAInspectorArrayElement<MoveTask.MoveData>(i, _task.data);

                //Target
                var targetPathField = new DAInspectorTextField("Target Path", entry.targetPath);
                element.AddToElement(targetPathField);

                //Name
                var destinationPathField = new DAInspectorTextField("Destination Path", entry.destinationPath);
                element.AddToElement(destinationPathField);

                entrySection.AddToSection(element);
            }

            return root;
        }
    }
}