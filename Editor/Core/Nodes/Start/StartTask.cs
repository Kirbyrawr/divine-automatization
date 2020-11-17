using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kirbyrawr.DivineAutomatization
{
    public class StartTask : DATask
    {
        public override void Run(Dictionary<string, object> properties)
        {
            Finish(0);
        }
    }
}