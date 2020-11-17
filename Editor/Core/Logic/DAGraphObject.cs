using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [CreateAssetMenu]
    public class DAGraphObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private DASerializer _serializer = new DASerializer();
        private DAGraphView _graphView;

        //Job Logic
        [System.NonSerialized]
        private Dictionary<string, object> _jobProperties;

        [System.NonSerialized]
        private DASerializer.NodeData _nodeData;

        [System.NonSerialized]
        private DATask _task;

        public void Setup(DAGraphView graphView)
        {
            _graphView = graphView;
            Undo.undoRedoPerformed += UndoRedoPerformed;
        }

        private void OnDestroy()
        {
            Undo.undoRedoPerformed -= UndoRedoPerformed;
        }

        public void OnBeforeSerialize()
        {
            Serialize();
        }

        public void OnAfterDeserialize()
        {
        }

        public void Serialize() => _serializer.Serialize(_graphView);
        public void Deserialize() => _serializer.Deserialize(_graphView);

        public void RegisterCompleteObjectUndo(string name)
        {
            Undo.RegisterCompleteObjectUndo(this, name);
        }

        private void UndoRedoPerformed()
        {
            _graphView.Clean();
            Deserialize();
        }

        public void Run(Dictionary<string, object> properties = null)
        {
            //Get tasks
            List<DATask> tasks = new List<DATask>();

            //Properties
            _jobProperties = new Dictionary<string, object>(properties);

            foreach (var serializedProperty in _serializer.SerializedProperties)
            {
                var property = (DAPropertyData)JsonUtility.FromJson(serializedProperty.data, System.Type.GetType(serializedProperty.dataType));
                var propertyValue = property.GetValue();

                if (propertyValue != null && !properties.ContainsKey(property.reference))
                {
                    _jobProperties.Add(property.reference, propertyValue);
                }
            }

            //Get start node
            _nodeData = _serializer.GetStartNode();

            //Run start task
            _task = (DATask)JsonUtility.FromJson(_nodeData.dataJson, System.Type.GetType(_nodeData.taskType));
            _task.OnFinish = NextTask;
            _task.Run(_jobProperties);
        }

        private void NextTask(int port)
        {
            //Remove callback
            _task.OnFinish = null;

            if (port != -1)
            {
                //Get next node and task and run it.
                _nodeData = _serializer.GetNode(_nodeData.connectedNodes[port]);
                _task = (DATask)JsonUtility.FromJson(_nodeData.dataJson, System.Type.GetType(_nodeData.taskType));
                _task.OnFinish += NextTask;
                _task.Run(_jobProperties);
            }
        }
    }
}