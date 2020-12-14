using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    public class OpenURLNode : DANode<OpenURLTask>
    {
        protected override string _nodeTitle => "Open URL";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();
            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            for (int i = 0; i < _task.urls.Count; i++)
            {
                var url = _task.urls[i];

                var element = new DAInspectorArrayElement<DAString>(i, _task.urls);
                DAInspectorTextField urlField = new DAInspectorTextField("URL", url);
                element.AddToElement(urlField);
                entrySection.AddToSection(element);
            }

            return root;
        }
    }
}