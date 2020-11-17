using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kirbyrawr.DivineAutomatization
{
    public abstract class DATask
    {
        [System.NonSerialized]
        public System.Action<int> OnFinish;

        /// <summary>
        /// Run the task.
        /// </summary>
        /// <returns>Returns the next port index, 
        /// if the task only have one port it will return 0</returns>
        public abstract void Run(Dictionary<string, object> properties);
        public void Finish(int port)
        {
            OnFinish(port);
        }
    }
}