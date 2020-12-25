using System.Collections.Generic;
using System.IO;

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
                File.Move(DAPath.FormatPath(entry.targetPath.GetValue(properties)), DAPath.FormatPath(entry.destinationPath.GetValue(properties)));
            }
            Finish(0);
        }
    }
}