using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Open URL")]
    public class OpenURLNode : DANode<OpenURLTask>
    {
        protected override string _nodeTitle => "Open URL";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            DAInspectorTextField urlField = new DAInspectorTextField("URL", _task.url);
            root.Add(urlField);

            return root;
        }
    }
}