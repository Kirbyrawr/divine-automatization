using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class MoveTask : DATask
    {
        [System.Serializable]
        public class MoveData
        {
            //File or folder path to rename.
            public DAString targetPath = new DAString();

            //New name to use.
            public DAString destinationPath = new DAString();
        }

        public List<MoveData> data = new List<MoveData>() { new MoveData() };

        public override void Run(Dictionary<string, object> properties)
        {
            foreach (var entry in data)
            {
                AssetDatabase.MoveAsset(entry.targetPath.GetValue(properties), entry.destinationPath.GetValue(properties));
            }
            Finish(0);
        }
    }
}