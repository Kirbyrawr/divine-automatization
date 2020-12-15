using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class RenameTask : DATask
    {
        [System.Serializable]
        public class RenameData
        {
            //File or folder path to rename.
            public DAString targetPath = new DAString();

            //New name to use.
            public DAString newName = new DAString();
        }

        public List<RenameData> data = new List<RenameData>() { new RenameData() };

        public override void Run(Dictionary<string, object> properties)
        {
            foreach (var entry in data)
            {
                AssetDatabase.RenameAsset(entry.targetPath.GetValue(properties), entry.newName.GetValue(properties));
            }

            Finish(0);
        }
    }
}