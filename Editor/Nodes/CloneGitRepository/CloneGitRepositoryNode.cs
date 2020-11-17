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

        protected override void DrawContent()
        {
            DANodeTextField sshURLField = new DANodeTextField("URL (SSH)", _task.sshURL);
            _content.Add(sshURLField);

            DANodeTextField destinationPath = new DANodeTextField("Destination Path", _task.destinationPath);
            _content.Add(destinationPath);
        }
    }
}