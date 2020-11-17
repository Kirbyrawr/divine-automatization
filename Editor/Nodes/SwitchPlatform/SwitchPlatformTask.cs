using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kirbyrawr.DivineAutomatization
{
    [System.Serializable]
    public class SwitchPlatformTask : DATask
    {
        public DAEnum buildTargetGroup = new DAEnum(BuildTargetGroup.Android);
        public DAEnum buildTarget = new DAEnum(BuildTarget.Android);

        public override void Run(Dictionary<string, object> properties)
        {
            EditorUserBuildSettings.SwitchActiveBuildTargetAsync((BuildTargetGroup)buildTargetGroup.GetValue(properties), (BuildTarget)buildTarget.GetValue(properties));
            Finish(0);
        }
    }
}