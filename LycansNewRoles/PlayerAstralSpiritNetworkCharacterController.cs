using System;
using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[NetworkBehaviourWeaved(24)]
public class PlayerAstralSpiritNetworkCharacterController : NetworkCharacterControllerPrototypeCustom
{
	public override void Spawned()
	{
		((NetworkCharacterControllerPrototypeCustom)this).Spawned();
		Traverse.Create((object)this).Method("CacheController", Array.Empty<object>()).GetValue();
	}
}
