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

            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            for (int i = 0; i < _task.data.Count; i++)
            {
                var entry = _task.data[i];
                var element = new DAInspectorArrayElement<RenameTask.RenameData>(i, _task.data);

                //Target
                var targetPathField = new DAInspectorTextField("Target Path", entry.targetPath);
                element.AddToElement(targetPathField);

                //Name
                var newNameField = new DAInspectorTextField("New Name", entry.newName);
                element.AddToElement(newNameField);

                entrySection.AddToSection(element);
            }

            return root;
        }
    }
}