using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    [Title("Load Prefab Contents")]
    public class LoadPrefabContentsNode : DANode<LoadPrefabContentsTask>
    {
        protected override string _nodeTitle => "Load Prefab Contents";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();

            //Prefab
            var prefabField = new DAInspectorGameObjectField("Prefab", _task.prefab);
            root.Add(prefabField);

            return root;
        }
    }
}