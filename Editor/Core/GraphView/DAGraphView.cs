using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [SerializeField]
    public class DAGraphView : GraphView
    {
        public DAGraphObject GraphObject { get; set; }
        public DAEditor Editor { get; private set; }

        public DANode nodeSelected;
        private DANodeSearchPopup _nodeSearcher;

        public DAGraphView(DAEditor editor)
        {
            Setup(editor);
        }

        private void Setup(DAEditor editor)
        {
            Editor = editor;
            Editor.graphLayout.Add(this);
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());
            this.AddManipulator(new SelectionDropper());
            this.RegisterCallback<KeyDownEvent>(OnSpaceDown);

            _nodeSearcher = ScriptableObject.CreateInstance<DANodeSearchPopup>();
            _nodeSearcher.Setup(Editor);

            deleteSelection = OnDeleteSelection;
            nodeCreationRequest = OpenSearchWindow;

            this.style.flexGrow = 1;

            var grid = new GridBackground();
            grid.name = "grid";
            grid.StretchToParentSize();
            Insert(0, grid);
        }

        private void OnDeleteSelection(string operationName, AskUser askUser)
        {
            GraphObject.RegisterCompleteObjectUndo("Delete Selection");
            for (int i = selection.Count - 1; i >= 0; i--)
            {
                RemoveElement((GraphElement)selection[i]);
            }
            Serialize();
        }

        private void OnSpaceDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Space && !evt.shiftKey && !evt.altKey && !evt.ctrlKey && !evt.commandKey)
            {
                if (nodeCreationRequest == null)
                    return;

                Vector2 referencePosition;
                referencePosition = evt.imguiEvent.mousePosition;
                Vector2 screenPoint = Editor.position.position + referencePosition;

                nodeCreationRequest(new NodeCreationContext() { screenMousePosition = screenPoint });
            }
        }

        private void OpenSearchWindow(NodeCreationContext c)
        {
            if (EditorWindow.focusedWindow == Editor)
            {
                SearcherWindow.Show(Editor, _nodeSearcher.LoadSearchWindow(),
                    item => _nodeSearcher.OnSearcherSelectEntry(item, c.screenMousePosition - Editor.position.position),
                    c.screenMousePosition - Editor.position.position, null);
            }
        }

        public void AddNode(DANode node)
        {
            node.styleSheets.Add(Editor.Styles["Node"]);
            AddElement(node);
        }

        public DANode CreateAndAddNode(Type type, Vector2 position)
        {
            DANode node = (DANode)Activator.CreateInstance(type);
            node.styleSheets.Add(Editor.Styles["Node"]);
            node.Setup(Editor, Editor.edgeConnectorListener);
            node.SetPosition(new Rect(position.x, position.y, 0, 0));
            AddNode(node);
            return node;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach((port) =>
            {
                //Check if its not the same.
                if (startPort != port &&
                        startPort.node != port.node &&
                        startPort.direction != port.direction)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }

        public override Blackboard GetBlackboard()
        {
            return Editor.blackboard;
        }

        public DAInspector GetInspector()
        {
            return Editor.inspector;
        }

        public void Serialize()
        {
            GraphObject.Serialize();
        }

        public void Deserialize()
        {
            GraphObject.Deserialize();
        }

        public void Clean()
        {
            foreach (var item in graphElements.ToList())
            {
                RemoveElement(item);
            }

            DABlackboard blackboard = (DABlackboard)GetBlackboard();

            var keys = blackboard.PropertyRows.Keys.ToList();
            foreach (var reference in keys)
            {
                var row = blackboard.PropertyRows[reference];
                blackboard.PropertiesScrollView.Remove(row);
                blackboard.PropertyRows.Remove(reference);
            }
        }
    }
}