using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Load Prefab Contents")]
    public class LoadPrefabContentsNode : DANode<LoadPrefabContentsTask>
    {
        protected override string _nodeTitle => "Load Prefab Contents";

        protected override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Prefab
            var prefabField = new DANodeGameObjectField("Prefab", _task.prefab, false);
            root.Add(prefabField);

            return root;
        }
    }
}