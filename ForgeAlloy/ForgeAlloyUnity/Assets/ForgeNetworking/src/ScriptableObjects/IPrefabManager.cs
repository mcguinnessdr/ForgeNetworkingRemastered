using UnityEngine;

namespace Forge.Networking.Unity
{
	public interface IPrefabManager
	{
		GameObject GetPrefabByAddress(string address);
	}
}
