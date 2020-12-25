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

            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            for (int i = 0; i < _task.data.Count; i++)
            {
                var entry = _task.data[i];
                var element = new DAInspectorArrayElement<DeleteTask.DeleteData>(i, _task.data);

                //Target
                var targetPathField = new DAInspectorPathField("Target Path", entry.targetPath);
                element.AddToElement(targetPathField);
                entrySection.AddToSection(element);
            }

            return root;
        }
    }
}