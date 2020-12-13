using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class CopyTask : DATask
    {
        [System.Serializable]
        public class CopyData
        {
            //File or folder path to rename.
            public DAString targetPath = new DAString();

            //New name to use.
            public DAString destinationPath = new DAString();
        }

        public List<CopyData> data = new List<CopyData>();

        public override void Run(Dictionary<string, object> properties)
        {
            foreach (var entry in data)
            {
                AssetDatabase.CopyAsset(entry.targetPath.GetValue(properties), entry.destinationPath.GetValue(properties));
            }
            Finish(0);
        }
    }
}