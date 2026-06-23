using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(24)]
public class PlayerIllusionNetworkCharacterController : NetworkCharacterControllerPrototypeCustom
{
	public override void Spawned()
	{
		Traverse.Create((object)this).Method("CacheController", Array.Empty<object>()).GetValue();
	}
}
