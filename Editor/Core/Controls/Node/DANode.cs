using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kirbyrawr.DivineAutomatization
{
    public abstract class DANode : Node
    {
        protected DAGraphView _graphView;

        public abstract void Setup(DAGraphView graphView, DAEdgeConnectorListener edgeConnectorListener);

        public override Rect GetPosition()
        {
            if (base.resolvedStyle.position == Position.Absolute)
            {
                return new Rect(base.resolvedStyle.left, base.resolvedStyle.top, base.layout.width, base.layout.height);
            }
            return base.layout;
        }

        public abstract string Serialize();
        public abstract void Deserialize(string json);
        public abstract DATask GetTask();
        public virtual List<Node> GetNextNodes()
        {
            List<Node> nodes = new List<Node>();

            foreach (var port in outputContainer.Query<Port>().ToList())
            {
                var connections = port.connections;

                if (connections?.Count() > 0)
                {
                    nodes.Add(connections.First().input.node);
                }
            }

            return nodes;
        }
    }

    public abstract class DANode<T> : DANode where T : DATask
    {
        protected T _task;
        protected abstract string _nodeTitle { get; }
        protected VisualElement _content;

        public override void Setup(DAGraphView graphView, DAEdgeConnectorListener edgeConnectorListener)
        {
            _graphView = graphView;

            if (_task == null)
            {
                _task = Activator.CreateInstance<T>();
            }

            title = _nodeTitle;

            DrawContentContainer();
            DrawPorts(edgeConnectorListener);
            DrawContent();
        }

        protected void DrawContentContainer()
        {
            var contents = this.Q<VisualElement>("contents");

            _content = new VisualElement();
            _content.name = "content";
            _content.style.SetPadding(12, 12, 12, 12);
            contents.Add(_content);
        }

        protected virtual void DrawPorts(DAEdgeConnectorListener edgeConnectorListener)
        {
            //Input
            var inputPort = DAPort.Create(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, null, edgeConnectorListener);
            inputPort.portName = "Entry";
            inputContainer.Add(inputPort);

            //Output
            var outputPort = DAPort.Create(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, null, edgeConnectorListener);
            outputPort.portName = "Next";
            outputContainer.Add(outputPort);

            RefreshExpandedState();
            RefreshPorts();
        }

        protected abstract void DrawContent();

        public override string Serialize()
        {
            if (_task == null)
            {
                return "";
            }
            else
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Converters.Add(new DAGameObjectConverter());
                settings.Formatting = Formatting.None;
                return JsonConvert.SerializeObject(_task, settings);
            }
        }

        public override void Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                _task = null;
            }
            else
            {
                _task = JsonConvert.DeserializeObject<T>(json, new DAGameObjectConverter());
            }
        }

        public override DATask GetTask()
        {
            return _task;
        }
    }
}