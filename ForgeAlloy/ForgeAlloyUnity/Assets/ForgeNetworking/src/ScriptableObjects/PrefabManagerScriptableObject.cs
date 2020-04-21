using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Forge.Networking.Unity
{
	[CreateAssetMenu(fileName = "ForgeNetworkingExampleData",
		menuName = "Forge Networking/Scriptable Objects/PrefabManager", order = 1)]
	public class PrefabManagerScriptableObject : ScriptableObject, IPrefabManager
	{
		[SerializeField]
		private AssetReference[] _prefabs = new AssetReference[0];

		Dictionary<string, AssetReference> loadedAssetsByAddress = new Dictionary<string, AssetReference>();

		public GameObject GetPrefabByAddress(string address)
		{
			return (GameObject)loadedAssetsByAddress[address].Asset;
		}

		public void LoadAssetByAddress(string address)
		{
			LoadAssetReference(new AssetReference(address));
		}

		public void LoadAssetReference(AssetReference assetReference)
		{
			assetReference.LoadAssetAsync<GameObject>().Completed += (obj) =>
			{
				loadedAssetsByAddress.Add(assetReference.AssetGUID, assetReference);
			};
		}

		public void LoadAllAssetReferences()
		{
			for (int i = 0; i < _prefabs.Length; i++)
			{
				LoadAssetReference(_prefabs[i]);
			}
		}
	}
}
