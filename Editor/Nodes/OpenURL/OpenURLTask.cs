using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class OpenURLTask : DATask
    {
        public DAString url = new DAString();

        public override void Run(Dictionary<string, object> properties)
        {
            Application.OpenURL(url.GetValue(properties));
            Finish(0);
        }
    }
}