using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class LoadPrefabContentsTask : DATask
    {
        //Prefab
        public DAGameObject prefab = new DAGameObject();

        public override void Run(Dictionary<string, object> properties)
        {
            PrefabUtility.LoadPrefabContents(AssetDatabase.GetAssetPath(prefab.GetValue(properties)));
            Finish(0);
        }
    }
}