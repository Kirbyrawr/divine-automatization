using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DAPort : Port
{
    protected DAPort(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type) : base(portOrientation, portDirection, portCapacity, type)
    {
    }

    public static Port Create(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type, IEdgeConnectorListener connectorListener)
    {
        var port = new DAPort(portOrientation, portDirection, portCapacity, type)
        {
            m_EdgeConnector = new EdgeConnector<Edge>(connectorListener),
        };

        port.AddManipulator(port.m_EdgeConnector);
        return port;
    }
}