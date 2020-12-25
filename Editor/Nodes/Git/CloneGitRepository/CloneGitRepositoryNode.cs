using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Clone Git Repository")]
    public class CloneGitRepositoryNode : DANode<CloneGitRepositoryTask>
    {
        protected override string _nodeTitle => "Clone Git Repository";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            for (int i = 0; i < _task.data.Count; i++)
            {
                var entry = _task.data[i];
                var element = new DAInspectorArrayElement<CloneGitRepositoryTask.CloneData>(i, _task.data);

                DAInspectorTextField urlField = new DAInspectorTextField("URL", entry.url);
                urlField.tooltip = "Only SSH URLS are valid.";
                element.AddToElement(urlField);

                DAInspectorTextField destinationPath = new DAInspectorTextField("Destination Path", entry.destinationPath);
                element.AddToElement(destinationPath);

                entrySection.AddToSection(element);
            }

            return root;
        }
    }
}