using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Switch Platform")]
    public class SwitchPlatformNode : DANode<SwitchPlatformTask>
    {
        protected override string _nodeTitle => "Switch Platform";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            //Target
            var buildTargetGroupField = new DAInspectorEnumField("Build Target Group", _task.buildTargetGroup);
            entrySection.AddToSection(buildTargetGroupField);

            //Name
            var buildTarget = new DAInspectorEnumField("Build Target", _task.buildTarget);
            entrySection.AddToSection(buildTarget);

            return root;
        }
    }
}