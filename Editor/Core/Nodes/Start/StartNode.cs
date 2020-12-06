using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [HideInInspector]
    public class StartNode : DANode<StartTask>
    {
        protected override string _nodeTitle => "Start";

        public override void Setup(DAEditor editor, DAEdgeConnectorListener edgeConnectorListener)
        {
            base.Setup(editor, edgeConnectorListener);
            m_CollapseButton.style.display = DisplayStyle.None;
            capabilities = Capabilities.Movable | Capabilities.Selectable;
        }

        protected override void DrawPorts(DAEdgeConnectorListener edgeConnectorListener)
        {
            //Output
            var outputPort = DAPort.Create(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, null, edgeConnectorListener);
            outputPort.portName = "Next";
            outputContainer.Add(outputPort);

            RefreshExpandedState();
            RefreshPorts();
        }
    }
}