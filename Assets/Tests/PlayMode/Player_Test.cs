using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Managers;
using Entities;
using Entities.Modules;
using UnityEditor;

namespace Tests
{
    public class Player_Test
    {
        GameObject playerObj;
        IGlobalManager globalManagerObj;
        IModuleHandler handler;

        [UnityTest, Order(0)]
        public IEnumerator _FindGlobalManager()
        {
            yield return null;
            globalManagerObj = GameObject.FindObjectOfType<GlobalManager>();
        }

        [UnityTest, Order(1)]
        public IEnumerator CanShoot()
        {
            yield return null;

            CreatePlayerIfNull();

            IWeaponModule weaponModule = handler.GetModule<IWeaponModule>();
            Assert.IsNotNull(weaponModule);

            weaponModule.Shoot();
            yield return null;

            GameObject bulletObj = GameObject.Find("Bullet");
            Assert.IsNotNull(bulletObj);
        }

        [UnityTest, Order(2)]
        public IEnumerator CanMove()
        {
            yield return null;

            CreatePlayerIfNull();

            IMovementModule movement = handler.GetModule<IMovementModule>();
            Assert.IsNotNull(movement);

            movement.SetDirection(Vector2.left);

            yield return null;
            Assert.Less(playerObj.transform.position.x, 0);
        }

        void CreatePlayerIfNull()
        {
            if (playerObj != null)
                return;

            playerObj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Entity/Player.prefab"));
            Assert.NotNull(playerObj);

            Entity player = playerObj.GetComponent<Entity>();
            Assert.NotNull(player);

            player.Init(globalManagerObj);
            handler = player.GetHandler();
            Assert.IsNotNull(handler);
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(playerObj);
        }
    }
}
