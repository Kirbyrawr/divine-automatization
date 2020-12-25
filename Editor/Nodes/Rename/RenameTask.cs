using System.Collections.Generic;
using System.IO;

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
                var newPath = Path.Combine(new FileInfo(DAPath.FormatPath(entry.targetPath.GetValue(properties))).FullName, entry.newName.GetValue(properties));
                File.Move(DAPath.FormatPath(entry.targetPath.GetValue(properties)), newPath);
            }

            Finish(0);
        }
    }
}