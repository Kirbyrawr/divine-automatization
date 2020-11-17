using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Rename")]
    public class RenameNode : DANode<RenameTask>
    {
        protected override string _nodeTitle => "Rename";

        protected override void DrawContent()
        {
            //Target
            var targetPathField = new DANodeTextField("Target Path", _task.targetPath);
            _content.Add(targetPathField);

            //Name
            var newNameField = new DANodeTextField("New Name", _task.newName);
            _content.Add(newNameField);
        }
    }
}