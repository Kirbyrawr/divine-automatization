using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Open URL")]
    public class OpenURLNode : DANode<OpenURLTask>
    {
        protected override string _nodeTitle => "Open URL";

        protected override void DrawContent()
        {
            DANodeTextField urlField = new DANodeTextField("URL", _task.url);
            _content.Add(urlField);
        }
    }
}