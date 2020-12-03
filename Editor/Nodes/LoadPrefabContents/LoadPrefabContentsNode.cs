using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Load Prefab Contents")]
    public class LoadPrefabContentsNode : DANode<LoadPrefabContentsTask>
    {
        protected override string _nodeTitle => "Load Prefab Contents";

        protected override void DrawContent()
        {
            //Prefab
            var prefabField = new DANodeGameObjectField("Prefab", _task.prefab, false);
            _content.Add(prefabField);
        }
    }
}