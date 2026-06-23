using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

public static class ImprovedTrapsExtensions
{
	public static bool TrapCanBeDisarmed(this Trap trap)
	{
		Traverse<Animator> val = Traverse.Create((object)trap).Field<Animator>("animator");
		return !val.Value.GetBool("Closing") && !val.Value.GetBool("Breaking");
	}
}
