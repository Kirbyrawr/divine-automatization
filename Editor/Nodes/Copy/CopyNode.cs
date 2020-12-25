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

            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            for (int i = 0; i < _task.data.Count; i++)
            {
                var entry = _task.data[i];
                var element = new DAInspectorArrayElement<CopyTask.CopyData>(i, _task.data);

                //Target
                var targetPathField = new DAInspectorPathField("Target Path", entry.targetPath);
                element.AddToElement(targetPathField);

                //Name
                var destinationPathField = new DAInspectorPathField("Destination Path", entry.destinationPath);
                element.AddToElement(destinationPathField);

                entrySection.AddToSection(element);
            }

            return root;
        }


    }
}