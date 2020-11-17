﻿using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [HideInInspector]
    public class EndNode : DANode<EndTask>
    {
        protected override string _nodeTitle => "End";

        public override void Setup(DAGraphView graphView, DAEdgeConnectorListener edgeConnectorListener)
        {
            base.Setup(graphView, edgeConnectorListener);
            m_CollapseButton.style.display = DisplayStyle.None;
            capabilities = Capabilities.Movable | Capabilities.Selectable;
            _content.style.SetPadding(0, 0, 0, 0);
        }

        protected override void DrawPorts(DAEdgeConnectorListener edgeConnectorListener)
        {
            //Output
            var inputPort = DAPort.Create(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, null, edgeConnectorListener);
            inputPort.portName = "Finish";
            inputContainer.Add(inputPort);

            RefreshExpandedState();
            RefreshPorts();
        }

        protected override void DrawContent()
        {
        }
    }
}