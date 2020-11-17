using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Delete")]
    public class DeleteNode : DANode<DeleteTask>
    {
        protected override string _nodeTitle => "Delete";

        protected override void DrawContent()
        {
            //Target
            var targetPathField = new DANodeTextField("Target Path", _task.targetPath);
            _content.Add(targetPathField);
        }
    }
}