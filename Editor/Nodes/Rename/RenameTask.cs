using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class RenameTask : DATask
    {
        //File or folder path to rename.
        public DAString targetPath = new DAString();

        //New name to use.
        public DAString newName = new DAString();

        public override void Run(Dictionary<string, object> properties)
        {
            AssetDatabase.RenameAsset(targetPath.GetValue(properties), newName.GetValue(properties));
            Finish(0);
        }
    }
}