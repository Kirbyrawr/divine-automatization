using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Copy")]
    public class CopyNode : DANode<CopyTask>
    {
        protected override string _nodeTitle => "Copy";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            var entrySection = new DAInspectorSection("Data");
            root.Add(entrySection);

            for (int i = 0; i < _task.data.Count; i++)
            {
                var entry = _task.data[i];

                var box = new Box();
                box.AddToClassList("inspector-entry-box");

                //Header
                var header = new VisualElement();
                header.AddToClassList("inspector-entry-header");
                box.Add(header);

                //Header - Title
                var headerTitle = new Label($"#{i}");
                headerTitle.AddToClassList("inspector-entry-header-title");
                header.Add(headerTitle);

                //Header - Buttons
                var headerButtons = new VisualElement();
                headerButtons.AddToClassList("inspector-entry-header-buttons");
                header.Add(headerButtons);

                //Header - Buttons - Up
                var upButton = new Button();
                upButton.text = "";
                headerButtons.Add(upButton);

                //Header - Buttons - Down
                var downButton = new Button();
                downButton.text = "";
                headerButtons.Add(downButton);

                //Header - Buttons - Remove
                var removeButton = new Button();
                removeButton.text = "";
                removeButton.tooltip = "Remove element";
                headerButtons.Add(removeButton);

                //Header - Buttons - Add
                var addButton = new Button();
                addButton.clickable.clicked += () => AddNewElement((int)addButton.userData);
                addButton.userData = i;
                addButton.text = "";
                addButton.tooltip = "Add new element";
                headerButtons.Add(addButton);

                //Target
                var targetPathField = new DAInspectorTextField("Target Path", entry.targetPath);
                box.Add(targetPathField);

                //Name
                var destinationPathField = new DAInspectorTextField("Destination Path", entry.destinationPath);
                box.Add(destinationPathField);

                entrySection.AddToSection(box);
            }

            return root;
        }

        public void AddNewElement(int index)
        {
            _editor.graphView.GraphObject.RegisterCompleteObjectUndo("Add new element");
            _task.data.Insert(index + 1, new CopyTask.CopyData());
            _editor.inspector.Refresh();
            _editor.graphView.Serialize();
        }
    }
}