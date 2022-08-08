using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Managers;

namespace Tests
{
    public class GlobalManager_Test
    {
        [UnityTest]
        public IEnumerator GlobalManagerCreated()
        {
            yield return null;
            IGlobalManager globalManagerObj = GameObject.FindObjectOfType<GlobalManager>();
            Assert.IsNotNull(globalManagerObj);
        }
    }
}
