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

        protected override void DrawContent()
        {
            //Target
            var buildTargetGroupField = new DANodeEnumField("Build Target Group", _task.buildTargetGroup);
            _content.Add(buildTargetGroupField);

            //Name
            var buildTarget = new DANodeEnumField("Build Target", _task.buildTarget);
            _content.Add(buildTarget);
        }
    }
}