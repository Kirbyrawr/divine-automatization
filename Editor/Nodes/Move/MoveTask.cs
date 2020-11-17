using System.Collections.Generic;
using UnityEditor;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class MoveTask : DATask
    {
        //File or folder path to move.
        public DAString targetPath = new DAString();

        //New name to use.
        public DAString destinationPath = new DAString();

        public override void Run(Dictionary<string, object> properties)
        {
            AssetDatabase.MoveAsset(targetPath.GetValue(properties), destinationPath.GetValue(properties));
            Finish(0);
        }
    }
}