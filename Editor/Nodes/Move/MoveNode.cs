using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Move")]
    public class MoveNode : DANode<MoveTask>
    {
        protected override string _nodeTitle => "Move";

        protected override void DrawContent()
        {
            //Target
            var targetPathField = new DANodeTextField("Target Path", _task.targetPath);
            _content.Add(targetPathField);

            //Name
            var destinationPathField = new DANodeTextField("Destination Path", _task.destinationPath);
            _content.Add(destinationPathField);
        }
    }
}