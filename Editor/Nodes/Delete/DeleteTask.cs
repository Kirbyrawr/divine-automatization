using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class DeleteTask : DATask
    {
        [System.Serializable]
        public class DeleteData
        {
            //File or folder path to delete.
            public DAString targetPath = new DAString();
        }

        public List<DeleteData> data = new List<DeleteData>() { new DeleteData() };

        public override void Run(Dictionary<string, object> properties)
        {
            foreach (var entry in data)
            {
                AssetDatabase.DeleteAsset(entry.targetPath.GetValue(properties));
            }

            Finish(0);
        }
    }
}