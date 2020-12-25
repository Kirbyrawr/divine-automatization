using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class AssetDatabaseRefreshTask : DATask
    {
        public override void Run(Dictionary<string, object> properties)
        {
            AssetDatabase.Refresh();
            Finish(0);
        }
    }
}