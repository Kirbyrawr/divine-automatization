using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class DeleteTask : DATask
    {
        //File or folder path to delete.
        public DAString targetPath = new DAString();

        public override void Run(Dictionary<string, object> properties)
        {
            AssetDatabase.DeleteAsset(targetPath.GetValue(properties));
            Finish(0);
        }
    }
}