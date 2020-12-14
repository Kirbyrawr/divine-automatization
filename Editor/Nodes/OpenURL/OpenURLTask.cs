using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class OpenURLTask : DATask
    {
        public List<DAString> urls = new List<DAString>() { new DAString() };

        public override void Run(Dictionary<string, object> properties)
        {
            foreach (var url in urls)
            {
                Application.OpenURL(url.GetValue(properties));
            }

            Finish(0);
        }
    }
}