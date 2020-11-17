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
        public DAString sshURL = new DAString();
        public DAString destinationPath = new DAString();

        public override void Run(Dictionary<string, object> properties)
        {
            EditorCoroutineUtility.StartCoroutine(Clone(properties), this);
        }

        private IEnumerator Clone(Dictionary<string, object> properties)
        {
            var process = DATerminal.Run($"git clone {properties[sshURL.GetValue(properties)]}");

            while (!process.HasExited)
            {
                yield return 0;
            }

            Finish(0);
        }
    }
}