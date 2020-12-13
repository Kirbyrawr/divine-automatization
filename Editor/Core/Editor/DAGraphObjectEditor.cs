using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [CustomEditor(typeof(DAGraphObject))]
    public class DAGraphObjectEditor : Editor
    {
        private DAGraphObject _graphObject;

        public void OnEnable()
        {
            _graphObject = (DAGraphObject)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            //Edit Job
            var editJobButton = new Button(EditJob);
            editJobButton.text = "Edit Job";
            root.Add(editJobButton);

            //Run Job
            var runJobButton = new Button(RunJob);
            runJobButton.text = "Run Job";
            root.Add(runJobButton);

            return root;
        }

        private void RunJob()
        {
            _graphObject.Run(new Dictionary<string, object>());
        }

        private void EditJob()
        {
            DAEditor.Init(_graphObject);
        }
    }
}