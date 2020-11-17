using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kirbyrawr.DivineAutomatization
{
    public class EndTask : DATask
    {
        public override void Run(Dictionary<string, object> properties)
        {
            Finish(-1);
        }
    }
}