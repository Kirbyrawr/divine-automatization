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

        protected override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Target
            var buildTargetGroupField = new DANodeEnumField("Build Target Group", _task.buildTargetGroup);
            root.Add(buildTargetGroupField);

            //Name
            var buildTarget = new DANodeEnumField("Build Target", _task.buildTarget);
            root.Add(buildTarget);

            return root;
        }
    }
}