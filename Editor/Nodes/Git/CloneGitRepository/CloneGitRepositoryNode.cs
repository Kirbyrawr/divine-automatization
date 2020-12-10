using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Clone Git Repository")]
    public class CloneGitRepositoryNode : DANode<CloneGitRepositoryTask>
    {
        protected override string _nodeTitle => "Clone Git Repository";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            DAInspectorTextField sshURLField = new DAInspectorTextField("URL (SSH)", _task.sshURL);
            root.Add(sshURLField);

            DAInspectorTextField destinationPath = new DAInspectorTextField("Destination Path", _task.destinationPath);
            root.Add(destinationPath);

            return root;
        }
    }
}