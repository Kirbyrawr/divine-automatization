using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class CopyTask : DATask
    {
        //File or folder path to rename.
        public DAString targetPath = new DAString();

        //New name to use.
        public DAString destinationPath = new DAString();

        public override void Run(Dictionary<string, object> properties)
        {
            AssetDatabase.CopyAsset(targetPath.GetValue(properties), destinationPath.GetValue(properties));
            Finish(0);
        }
    }
}