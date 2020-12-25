using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class CloneGitRepositoryTask : DATask
    {
        [System.Serializable]
        public class CloneData
        {
            public DAString url = new DAString();
            public DAString destinationPath = new DAString();
        }

        public List<CloneData> data = new List<CloneData>() { new CloneData() };

        public override void Run(Dictionary<string, object> properties)
        {
            EditorCoroutineUtility.StartCoroutine(Clone(properties), this);
        }

        private IEnumerator Clone(Dictionary<string, object> properties)
        {
            foreach (var entry in data)
            {
                var process = DATerminal.Run($"git clone {entry.url.GetValue(properties)} {entry.destinationPath.GetValue(properties)}");

                while (!process.HasExited)
                {
                    yield return 0;
                }
            }

            Finish(0);
        }
    }
}