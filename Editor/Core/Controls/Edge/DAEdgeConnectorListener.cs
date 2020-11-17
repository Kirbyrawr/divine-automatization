using System.Collections.Generic;
using Kirbyrawr.DivineAutomatization;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using Edge = UnityEditor.Experimental.GraphView.Edge;

public class DAEdgeConnectorListener : IEdgeConnectorListener
{
    private GraphViewChange _graphViewChange;
    private List<Edge> _edgesToCreate;
    private List<GraphElement> _edgesToDelete;

    public DAEdgeConnectorListener()
    {
        _edgesToCreate = new List<Edge>();
        _edgesToDelete = new List<GraphElement>();
        _graphViewChange.edgesToCreate = _edgesToCreate;
    }

    public void OnDropOutsidePort(Edge edge, Vector2 position)
    {
    }

    public void OnDrop(GraphView graphView, Edge edge)
    {
        ((DAGraphView)graphView).GraphObject.RegisterCompleteObjectUndo("Connect");

        _edgesToCreate.Clear();
        _edgesToCreate.Add(edge);
        _edgesToDelete.Clear();
        if (edge.input.capacity == Port.Capacity.Single)
        {
            foreach (Edge connection in edge.input.connections)
            {
                if (connection != edge)
                {
                    _edgesToDelete.Add(connection);
                }
            }
        }
        if (edge.output.capacity == Port.Capacity.Single)
        {
            foreach (Edge connection2 in edge.output.connections)
            {
                if (connection2 != edge)
                {
                    _edgesToDelete.Add(connection2);
                }
            }
        }
        if (_edgesToDelete.Count > 0)
        {
            graphView.DeleteElements(_edgesToDelete);
        }
        List<Edge> edgesToCreate = _edgesToCreate;
        if (graphView.graphViewChanged != null)
        {
            edgesToCreate = graphView.graphViewChanged(_graphViewChange).edgesToCreate;
        }
        foreach (Edge item in edgesToCreate)
        {
            graphView.AddElement(item);
            edge.input.Connect(item);
            edge.output.Connect(item);
        }
    }
}
