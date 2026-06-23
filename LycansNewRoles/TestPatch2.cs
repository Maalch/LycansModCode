using Fusion;
using HarmonyLib;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerRegistry), "Server_Remove")]
public class TestPatch2
{
	private unsafe static void Prefix(PlayerRef pRef)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		PlayerRef val = pRef;
		LycansUtility.DebugLog("BUG2, Server_Remove " + ((object)(*(PlayerRef*)(&val))/*cast due to constrained. prefix*/).ToString());
	}
}
