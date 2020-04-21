using Forge.Networking.Unity.Messages;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Forge.Networking.Unity
{
	public static class EntitySpawner
	{
		public static IUnityEntity SpawnEntityFromData(IEngineFacade engine, int id, string prefabAddress, Vector3 pos, Quaternion rot, Vector3 scale)
		{
			var go = SpawnGameObjectWithInfo(engine, prefabAddress, pos, rot, scale);
			return SetupNetworkEntity(engine, go, id, prefabAddress);
		}

		public static IUnityEntity SpawnEntityFromMessage(IEngineFacade engine, SpawnEntityMessage message)
		{
			GameObject go = SpawnGameObject(message, engine);
			return SetupNetworkEntity(engine, go, message.Id, message.PrefabAddress);
		}

		private static GameObject SpawnGameObject(SpawnEntityMessage message, IEngineFacade engine)
		{
			return SpawnGameObjectWithInfo(engine, message.PrefabAddress, message.Position, message.Rotation, message.Scale);
		}

		private static GameObject SpawnGameObjectWithInfo(IEngineFacade engine, string prefabAddress, Vector3 pos, Quaternion rot, Vector3 scale)
		{
			var prefab = engine.PrefabManager.GetPrefabByAddress(prefabAddress);
			var gameObject = GameObject.Instantiate(prefab, pos, rot);
			gameObject.transform.localScale = scale;
			return gameObject;
		}

		private static IUnityEntity SetupNetworkEntity(IEngineFacade engine, GameObject go, int id, string prefabAddress)
		{
			var entity = go.gameObject.AddComponent<NetworkEntity>();
			entity.Id = id;
			entity.PrefabAddress = prefabAddress;
			engine.EntityRepository.Add(entity);
			return entity;
		}
	}
}
