using System;
using UnityEngine;

namespace LycansNewRoles.NewMaps.Components;

public class LootCustom : MonoBehaviour
{
	private void Start()
	{
		try
		{
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("LootCustom start error: " + ex));
		}
	}
}
