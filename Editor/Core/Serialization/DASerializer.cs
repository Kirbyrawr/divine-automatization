using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class DASerializer
    {
        [System.Serializable]
        public class NodeData
        {
            public string type;
            public string taskType;
            public Rect position;
            public bool expanded;
            public string dataJson;
            public string guid;
            public List<string> inputPortsGUID = new List<string>();
            public List<string> outputPortsGUID = new List<string>();
            public List<string> connectedNodes = new List<string>();
        }

        [System.Serializable]
        public class EdgeData
        {
            public string guid;
            public string inputPortGUID;
            public string outputPortGUID;
        }

        [System.Serializable]
        public class PropertyData
        {
            public string blackboardType;
            public bool expanded;
            public string dataType;
            public string reference;
            public string data;
        }

        public List<NodeData> SerializedNodes { get => _serializedNodes; }
        public List<EdgeData> SerializedEdges { get => _serializedEdges; }
        public List<PropertyData> SerializedProperties { get => _serializedProperties; }

        [SerializeField]
        private NodeData _startNode;

        [SerializeField]
        private List<NodeData> _serializedNodes = new List<NodeData>();

        [SerializeField]
        private List<EdgeData> _serializedEdges = new List<EdgeData>();

        [SerializeField]
        private List<PropertyData> _serializedProperties = new List<PropertyData>();

        public DASerializer()
        {
            _startNode = CreateSerializedNode<StartNode>(new Vector2(-200, 0), 0, 1);
            _serializedNodes.Add(_startNode);
            _serializedNodes.Add(CreateSerializedNode<EndNode>(new Vector2(300, 0), 1, 0));
        }

        private NodeData CreateSerializedNode<T>(Vector2 position, int input, int output) where T : DANode
        {
            NodeData nodeData = new NodeData();
            nodeData.guid = Guid.NewGuid().ToString();
            nodeData.position = new Rect(position.x, position.y, 0, 0);
            nodeData.type = typeof(T).AssemblyQualifiedName;
            nodeData.taskType = typeof(T).BaseType.GetGenericArguments()[0].AssemblyQualifiedName;
            nodeData.expanded = true;
            nodeData.dataJson = "{}";

            //Inputs
            for (int i = 0; i < input; i++)
            {
                nodeData.inputPortsGUID.Add(Guid.NewGuid().ToString());
            }

            //Outputs
            for (int i = 0; i < output; i++)
            {
                nodeData.outputPortsGUID.Add(Guid.NewGuid().ToString());
            }

            return nodeData;
        }

        public void Serialize(DAGraphView graphView)
        {
            if (graphView == null) { return; }

            _serializedNodes.Clear();
            _serializedEdges.Clear();
            _serializedProperties.Clear();

            //Nodes
            var nodes = graphView.nodes.ToList().Cast<DANode>();
            foreach (var node in nodes)
            {
                NodeData nodeData = new NodeData();

                nodeData.type = node.GetType().AssemblyQualifiedName;
                nodeData.taskType = node.GetTask().GetType().AssemblyQualifiedName;
                node.style.position = Position.Absolute;
                nodeData.position = node.GetPosition();
                nodeData.expanded = node.expanded;
                nodeData.guid = node.viewDataKey;
                nodeData.dataJson = node.Serialize();

                //Save connected nodes to run the job properly.
                foreach (var nextNode in node.GetNextNodes())
                {
                    nodeData.connectedNodes.Add(nextNode.viewDataKey);
                }

                //Input Ports
                foreach (var inputPort in node.inputContainer.Query<Port>().ToList())
                {
                    nodeData.inputPortsGUID.Add(inputPort.viewDataKey);
                }

                //Output Ports
                foreach (var outputPort in node.outputContainer.Query<Port>().ToList())
                {
                    nodeData.outputPortsGUID.Add(outputPort.viewDataKey);
                }

                _serializedNodes.Add(nodeData);

                if (node.GetType() == typeof(StartNode))
                {
                    _startNode = nodeData;
                }
            }

            //Edges
            var edges = graphView.edges.ToList();
            foreach (var edge in edges)
            {
                EdgeData edgeData = new EdgeData();
                edgeData.guid = edge.viewDataKey;
                edgeData.inputPortGUID = edge.input.viewDataKey;
                edgeData.outputPortGUID = edge.output.viewDataKey;
                _serializedEdges.Add(edgeData);
            }

            //Properties
            var blackboard = (DABlackboard)graphView.GetBlackboard();
            foreach (var row in blackboard.PropertyRows)
            {
                PropertyData propertyData = new PropertyData();
                propertyData.blackboardType = row.Value.Property.GetType().AssemblyQualifiedName;
                propertyData.expanded = row.Value.expanded;
                propertyData.dataType = row.Value.Property.GetData().GetType().AssemblyQualifiedName;
                propertyData.reference = row.Value.Property.GetData().reference;
                propertyData.data = row.Value.Property.Serialize();
                _serializedProperties.Add(propertyData);
            }
        }

        public void Deserialize(DAGraphView graphView)
        {
            if (graphView == null) { return; }

            //Nodes
            foreach (var nodeData in _serializedNodes)
            {
                DANode node = (DANode)Activator.CreateInstance(Type.GetType(nodeData.type));
                node.viewDataKey = nodeData.guid;
                node.SetPosition(nodeData.position);
                node.expanded = nodeData.expanded;
                node.Deserialize(nodeData.dataJson);
                node.Setup(graphView, DAEditor.Instance.edgeConnectorListener);

                //Ports
                for (int i = 0; i < node.inputContainer.Query<Port>().ToList().Count; i++)
                {
                    node.inputContainer[i].viewDataKey = nodeData.inputPortsGUID[i];
                }

                for (int i = 0; i < node.outputContainer.Query<Port>().ToList().Count; i++)
                {
                    node.outputContainer[i].viewDataKey = nodeData.outputPortsGUID[i];
                }

                graphView.AddNode(node);
            }

            //Edges
            foreach (var edgeData in _serializedEdges)
            {
                Edge edge = new Edge();
                edge.viewDataKey = edgeData.guid;

                edge.input = graphView.GetPortByGuid(edgeData.inputPortGUID);
                edge.input.Connect(edge);

                edge.output = graphView.GetPortByGuid(edgeData.outputPortGUID);
                edge.output.Connect(edge);

                graphView.AddElement(edge);
            }

            //Properties
            var blackboard = (DABlackboard)graphView.GetBlackboard();
            foreach (var propertyData in _serializedProperties)
            {
                DABlackboardPropertyField field = new DABlackboardPropertyField(blackboard);
                DABlackboardProperty property = (DABlackboardProperty)Activator.CreateInstance(System.Type.GetType(propertyData.blackboardType), blackboard);
                DABlackboardPropertyRow row = new DABlackboardPropertyRow(field, property);
                row.expanded = propertyData.expanded;
                row.Property.Deserialize(propertyData.data);
                row.Field.text = row.Property.GetData().name;
                blackboard.AddProperty(row);
            }
        }

        public NodeData GetStartNode()
        {
            return _startNode;
        }

        public NodeData GetNode(string guid)
        {
            foreach (var node in _serializedNodes)
            {
                if (guid == node.guid)
                {
                    return node;
                }
            }

            return null;
        }
    }
}