using System;
using Fusion;
using HarmonyLib;
using UnityEngine;

namespace LycansNewRoles;

[HarmonyPatch(typeof(PlayerEffectsManager), "GlowingChanged")]
internal class GlowingChangedColorPatch
{
	private static bool Prefix(Changed<PlayerEffectsManager> changed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			PlayerEffectsManager behaviour = changed.Behaviour;
			PlayerController value = Traverse.Create((object)behaviour).Field<PlayerController>("_playerController").Value;
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(value.Ref);
			SkinnedMeshRenderer value2 = Traverse.Create((object)value.PlayerEffectManager).Field<SkinnedMeshRenderer>("skinnedMeshRenderer").Value;
			Light value3 = Traverse.Create((object)value.PlayerEffectManager).Field<Light>("glowingLight").Value;
			if (NetworkBool.op_Implicit(behaviour.Glowing) && !NetworkBool.op_Implicit(value.IsWolf))
			{
				Color color = ColorManager.GetColor(player.ColorIndex);
				((Renderer)value2).material.SetColor(Shader.PropertyToID("_EmissionColor"), color * 3f);
				((Renderer)value2).material.EnableKeyword("_EMISSION");
				value3.color = color;
				((Behaviour)value3).enabled = true;
				player.UpdateVisibility();
				return false;
			}
			((Renderer)value2).material.DisableKeyword("_EMISSION");
			((Behaviour)value3).enabled = false;
			player.UpdateVisibility();
			return false;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("GlowingChangedColorPatch error: " + ex));
			return true;
		}
	}
}
