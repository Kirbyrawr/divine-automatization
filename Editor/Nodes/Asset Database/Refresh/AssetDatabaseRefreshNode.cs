using UnityEngine;
using UnityEngine.UIElements;

namespace Kirbyrawr.DivineAutomatization
{
    public class AssetDatabaseRefreshNode : DANode<AssetDatabaseRefreshTask>
    {
        protected override string _nodeTitle => "Asset Database Refresh";

        public override VisualElement InspectorContent()
        {
            VisualElement root = new VisualElement();
            return root;
        }
    }
}