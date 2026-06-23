using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using Helpers.Collections;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewMaps;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.Sabotages;

[NetworkBehaviourWeaved(20)]
public class SabotageManager : NetworkBehaviour
{
	public enum SabotageIds
	{
		WellPoison,
		WolvesRitual,
		Portals,
		CursedNight,
		LaboratoryAutodoors,
		LaboratoryLights
	}

	private class SabotageInitialization
	{
		public Vector3 Position;

		public float ActivationDuration;

		public SabotageInitialization(Vector3 position, float activationDuration)
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			Position = position;
			ActivationDuration = activationDuration;
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Func<GameObject, bool> _003C_003E9__24_0;

		public static OnBeforeSpawned _003C_003E9__25_0;

		public static OnBeforeSpawned _003C_003E9__25_1;

		public static OnBeforeSpawned _003C_003E9__25_2;

		public static OnBeforeSpawned _003C_003E9__25_3;

		public static OnBeforeSpawned _003C_003E9__25_4;

		public static OnBeforeSpawned _003C_003E9__25_5;

		public static OnBeforeSpawned _003C_003E9__25_6;

		public static OnBeforeSpawned _003C_003E9__25_7;

		public static OnBeforeSpawned _003C_003E9__25_8;

		public static OnBeforeSpawned _003C_003E9__25_9;

		public static OnBeforeSpawned _003C_003E9__25_10;

		public static OnBeforeSpawned _003C_003E9__25_11;

		public static OnBeforeSpawned _003C_003E9__25_12;

		public static OnBeforeSpawned _003C_003E9__25_13;

		public static OnBeforeSpawned _003C_003E9__25_14;

		public static Predicate<PlayerController> _003C_003E9__32_0;

		public static Predicate<PlayerController> _003C_003E9__33_0;

		public static Func<Effect, bool> _003C_003E9__33_1;

		public static Func<Effect, bool> _003C_003E9__33_2;

		public static Func<Effect, bool> _003C_003E9__33_3;

		public static Func<Effect, bool> _003C_003E9__33_4;

		public static Func<Effect, bool> _003C_003E9__33_5;

		internal bool _003CUpdateCachedObjectsIfNeeded_003Eb__24_0(GameObject o)
		{
			return o.activeSelf && SabotageObjectsInfo.Any((KeyValuePair<string, SabotageInfo> j) => j.Key == ((Object)o).name);
		}

		internal void _003CInit_003Eb__25_0(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 3);
		}

		internal void _003CInit_003Eb__25_1(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WolvesRitual, 3);
		}

		internal void _003CInit_003Eb__25_2(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.Portals, 3);
		}

		internal void _003CInit_003Eb__25_3(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 4);
		}

		internal void _003CInit_003Eb__25_4(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.CursedNight, 3);
		}

		internal void _003CInit_003Eb__25_5(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.Portals, 3);
		}

		internal void _003CInit_003Eb__25_6(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 3);
		}

		internal void _003CInit_003Eb__25_7(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WolvesRitual, 3);
		}

		internal void _003CInit_003Eb__25_8(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 3);
		}

		internal void _003CInit_003Eb__25_9(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WolvesRitual, 4);
		}

		internal void _003CInit_003Eb__25_10(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.Portals, 3);
		}

		internal void _003CInit_003Eb__25_11(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.LaboratoryAutodoors, 4);
		}

		internal void _003CInit_003Eb__25_12(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.LaboratoryLights, 4);
		}

		internal void _003CInit_003Eb__25_13(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 4);
		}

		internal void _003CInit_003Eb__25_14(NetworkRunner _, NetworkObject no)
		{
			((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.CursedNight, 4);
		}

		internal bool _003CStartCursedNightTimer_003Eb__32_0(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsDead) && !NetworkBool.op_Implicit(o.IsWolf);
		}

		internal bool _003CFixedUpdateNetwork_003Eb__33_0(PlayerController o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			return !NetworkBool.op_Implicit(o.IsDead) && !NetworkBool.op_Implicit(o.IsWolf);
		}

		internal bool _003CFixedUpdateNetwork_003Eb__33_1(Effect o)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			return (int)o.GetEffectType() == 2;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__33_2(Effect o)
		{
			return o is DiseasedEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__33_3(Effect o)
		{
			return o is DownedEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__33_4(Effect o)
		{
			return o is WoundedEffect;
		}

		internal bool _003CFixedUpdateNetwork_003Eb__33_5(Effect o)
		{
			return o is PoisonEffect;
		}
	}

	public static Color SabotageColorWellPoison = new Color(0f, 1f, 0f);

	public static Color SabotageColorWolvesRitual = new Color(0f, 0f, 1f);

	public static Color SabotageColorPortals = new Color(1f, 1f, 0f);

	public static Color SabotageColorCursedNight = new Color(1f, 0f, 1f);

	public static Color SabotageColorLaboratoryAutodoors = new Color(0f, 1f, 1f);

	public static Color SabotageColorLaboratoryLights = new Color(0.25f, 0.25f, 0.25f);

	public static Dictionary<string, SabotageInfo> SabotageObjectsInfo = new Dictionary<string, SabotageInfo>();

	public static List<GameObject> CachedObjects = new List<GameObject>();

	public static bool ObjectsCached = false;

	public Dictionary<SabotageIds, SabotageSingle> Sabotages = new Dictionary<SabotageIds, SabotageSingle>();

	public Dictionary<int, SabotageObject> SabotageObjectsByIndex = new Dictionary<int, SabotageObject>();

	[Networked]
	[NetworkedWeaved(0, 1)]
	public unsafe TickTimer CursedNightTimer
	{
		get
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageManager.CursedNightTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			return (TickTimer)(*base.Ptr);
		}
		set
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.Ptr == null)
			{
				throw new InvalidOperationException("Error when accessing SabotageManager.CursedNightTimer. Networked properties can only be accessed when Spawned() has been called.");
			}
			Unsafe.Write(base.Ptr, value);
		}
	}

	public static SabotageManager Instance { get; private set; }

	public static Color GetSabotageColor(SabotageIds sabotageId)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(sabotageId switch
		{
			SabotageIds.WellPoison => SabotageColorWellPoison, 
			SabotageIds.WolvesRitual => SabotageColorWolvesRitual, 
			SabotageIds.Portals => SabotageColorPortals, 
			SabotageIds.CursedNight => SabotageColorCursedNight, 
			SabotageIds.LaboratoryAutodoors => SabotageColorLaboratoryAutodoors, 
			SabotageIds.LaboratoryLights => SabotageColorLaboratoryLights, 
			_ => Color.black, 
		});
	}

	public override void Spawned()
	{
		((NetworkBehaviour)this).Spawned();
		Instance = this;
	}

	public override void Despawned(NetworkRunner runner, bool hasState)
	{
		((NetworkBehaviour)this).Despawned(runner, hasState);
		Instance = null;
	}

	private void Awake()
	{
		SabotageObjectsInfo.Clear();
		SabotageObjectsInfo.Add("Well", new SabotageInfo("NALES_UI_SABOTAGE_WELL_POISON_WELL_NAME", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Logs (5)", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_CHURCH_LOGS_NAME", "NALES_UI_SABOTAGE_RITUAL_CHURCH_LOGS_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Ruins_03_LOD0", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_NAME", "NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Pot_01_LOD0", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_CAULDRON_NAME", "NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Arch_04_LOD0", new SabotageInfo("NALES_UI_SABOTAGE_PORTALS_PORTAL_NAME", "NALES_UI_SABOTAGE_PORTALS_PORTAL_ACTION", 4f));
		SabotageObjectsInfo.Add("Well_LOD0", new SabotageInfo("NALES_UI_SABOTAGE_WELL_POISON_WELL_NAME", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Fountain_01_LOD0", new SabotageInfo("NALES_UI_SABOTAGE_WELL_POISON_FOUNTAIN_NAME", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 2.5f));
		SabotageObjectsInfo.Add("NoticeBoard_01_LOD0", new SabotageInfo("NALES_UI_SABOTAGE_CURSED_NIGHT_NOTICE_BOARD_NAME", "NALES_UI_SABOTAGE_CURSED_NIGHT_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Book_13_LOD0", new SabotageInfo("NALES_UI_SABOTAGE_CURSED_NIGHT_BOOK_NAME", "NALES_UI_SABOTAGE_CURSED_NIGHT_ACTION", 2.5f));
		SabotageObjectsInfo.Add("PortalSpawn:Portal_Dungeon", new SabotageInfo("NALES_UI_SABOTAGE_PORTALS_PORTAL_NAME", "NALES_UI_SABOTAGE_PORTALS_PORTAL_ACTION", 4f));
		SabotageObjectsInfo.Add("WellHaddon", new SabotageInfo("NALES_UI_SABOTAGE_WELL_POISON_WELL_NAME", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 2.5f));
		SabotageObjectsInfo.Add("PillarHaddon", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_NAME", "NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_ACTION", 7f));
		SabotageObjectsInfo.Add("Queen", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_PIECE_NAME", "NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_ACTION", 3f));
		SabotageObjectsInfo.Add("King", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_PIECE_NAME", "NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_ACTION", 3f));
		SabotageObjectsInfo.Add("Wolfboss_skin", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_WOLF_STATUE_NAME", "NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_ACTION", 3f));
		SabotageObjectsInfo.Add("Chess_Board", new SabotageInfo("NALES_UI_SABOTAGE_RITUAL_CHESSBOARD_NAME", "NALES_UI_SABOTAGE_RITUAL_RUINS_PILLAR_ACTION", 3f));
		SabotageObjectsInfo.Add("PortalSpawn:Portal_Apartcan", new SabotageInfo("NALES_UI_SABOTAGE_PORTALS_PORTAL_NAME", "NALES_UI_SABOTAGE_PORTALS_PORTAL_ACTION", 4f));
		SabotageObjectsInfo.Add("SabotageConsole", new SabotageInfo("NALES_UI_SABOTAGE_LABORATORY_AUTODOORS_CONSOLE_NAME", "NALES_UI_SABOTAGE_LABORATORY_AUTODOORS_CONSOLE_ACTION", 3f));
		SabotageObjectsInfo.Add("LightsBattery", new SabotageInfo("NALES_UI_SABOTAGE_LABORATORY_BATTERY_NAME", "NALES_UI_SABOTAGE_LABORATORY_BLACKOUT_ACTION", 3f));
		SabotageObjectsInfo.Add("wood_bathtub", new SabotageInfo("NALES_UI_SABOTAGE_GOT_BATHTUB", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 3f));
		SabotageObjectsInfo.Add("Water (1)", new SabotageInfo("NALES_UI_SABOTAGE_GOT_SINK", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 3f));
		SabotageObjectsInfo.Add("Well (1)", new SabotageInfo("NALES_UI_SABOTAGE_WELL_POISON_WELL_NAME", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Fountain_4", new SabotageInfo("NALES_UI_SABOTAGE_WELL_POISON_FOUNTAIN_NAME", "NALES_UI_SABOTAGE_WELL_POISON_WELL_ACTION", 2.5f));
		SabotageObjectsInfo.Add("Table_05", new SabotageInfo("NALES_UI_SABOTAGE_GOT_GAME_TABLE", "NALES_UI_SABOTAGE_CURSED_NIGHT_ACTION", 3f));
		SabotageObjectsInfo.Add("Table_06", new SabotageInfo("NALES_UI_SABOTAGE_GOT_GAME_TABLE", "NALES_UI_SABOTAGE_CURSED_NIGHT_ACTION", 3f));
		SabotageObjectsInfo.Add("Wolves", new SabotageInfo("NALES_UI_SABOTAGE_GOT_DOGS", "NALES_UI_SABOTAGE_CURSED_NIGHT_ACTION", 3f));
		SabotageObjectsInfo.Add("Book_03", new SabotageInfo("NALES_UI_SABOTAGE_CURSED_NIGHT_BOOK_NAME", "NALES_UI_SABOTAGE_CURSED_NIGHT_ACTION", 3f));
	}

	public void ClearCachedObjects()
	{
		CachedObjects.Clear();
		ObjectsCached = false;
	}

	public void UpdateCachedObjectsIfNeeded()
	{
		if (ObjectsCached)
		{
			return;
		}
		GameObject[] source = Resources.FindObjectsOfTypeAll<GameObject>();
		CachedObjects.AddRange(source.Where((GameObject o) => o.activeSelf && SabotageObjectsInfo.Any((KeyValuePair<string, SabotageInfo> j) => j.Key == ((Object)o).name)));
		ObjectsCached = true;
	}

	public void Init()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_099c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1617: Unknown result type (might be due to invalid IL or missing references)
		//IL_163c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1661: Unknown result type (might be due to invalid IL or missing references)
		//IL_1686: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1314: Unknown result type (might be due to invalid IL or missing references)
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a36: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a72: Expected O, but got Unknown
		//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Expected O, but got Unknown
		//IL_16ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_16e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1712: Expected O, but got Unknown
		//IL_0ab5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0674: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1755: Unknown result type (might be due to invalid IL or missing references)
		//IL_1761: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af4: Expected O, but got Unknown
		//IL_1381: Unknown result type (might be due to invalid IL or missing references)
		//IL_1389: Unknown result type (might be due to invalid IL or missing references)
		//IL_1393: Unknown result type (might be due to invalid IL or missing references)
		//IL_13af: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c5: Expected O, but got Unknown
		//IL_0c9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Expected O, but got Unknown
		//IL_0eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef9: Expected O, but got Unknown
		//IL_17bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_17e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1805: Unknown result type (might be due to invalid IL or missing references)
		//IL_182a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1789: Unknown result type (might be due to invalid IL or missing references)
		//IL_178e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1794: Expected O, but got Unknown
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Expected O, but got Unknown
		//IL_1408: Unknown result type (might be due to invalid IL or missing references)
		//IL_1414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f48: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_146e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1493: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_143c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1441: Unknown result type (might be due to invalid IL or missing references)
		//IL_1447: Expected O, but got Unknown
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_0734: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Expected O, but got Unknown
		//IL_0fa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7b: Expected O, but got Unknown
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Expected O, but got Unknown
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Expected O, but got Unknown
		//IL_1872: Unknown result type (might be due to invalid IL or missing references)
		//IL_187a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1884: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b6: Expected O, but got Unknown
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0818: Unknown result type (might be due to invalid IL or missing references)
		//IL_083d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Expected O, but got Unknown
		//IL_0db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1905: Unknown result type (might be due to invalid IL or missing references)
		//IL_1525: Unknown result type (might be due to invalid IL or missing references)
		//IL_152d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1537: Unknown result type (might be due to invalid IL or missing references)
		//IL_1553: Unknown result type (might be due to invalid IL or missing references)
		//IL_1569: Expected O, but got Unknown
		//IL_0dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df7: Expected O, but got Unknown
		//IL_1059: Unknown result type (might be due to invalid IL or missing references)
		//IL_1061: Unknown result type (might be due to invalid IL or missing references)
		//IL_106b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1087: Unknown result type (might be due to invalid IL or missing references)
		//IL_109d: Expected O, but got Unknown
		//IL_192d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1932: Unknown result type (might be due to invalid IL or missing references)
		//IL_1938: Expected O, but got Unknown
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Expected O, but got Unknown
		//IL_15ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15eb: Expected O, but got Unknown
		//IL_0885: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c9: Expected O, but got Unknown
		//IL_1146: Unknown result type (might be due to invalid IL or missing references)
		//IL_116b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1190: Unknown result type (might be due to invalid IL or missing references)
		//IL_1114: Unknown result type (might be due to invalid IL or missing references)
		//IL_1119: Unknown result type (might be due to invalid IL or missing references)
		//IL_111f: Expected O, but got Unknown
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Expected O, but got Unknown
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Unknown result type (might be due to invalid IL or missing references)
		//IL_0940: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094b: Expected O, but got Unknown
		//IL_11d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1206: Unknown result type (might be due to invalid IL or missing references)
		//IL_121c: Expected O, but got Unknown
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Expected O, but got Unknown
		//IL_125f: Unknown result type (might be due to invalid IL or missing references)
		//IL_126b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1293: Unknown result type (might be due to invalid IL or missing references)
		//IL_1298: Unknown result type (might be due to invalid IL or missing references)
		//IL_129e: Expected O, but got Unknown
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Expected O, but got Unknown
		Instance.UpdateCachedObjectsIfNeeded();
		CursedNightTimer = TickTimer.None;
		if (!((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		int currentIndex = 0;
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.SabotageObject");
		List<SabotageInitialization> list = new List<SabotageInitialization>();
		NetworkPrefabId networkObject2 = NetworkObjectService.Instance.GetNetworkObject("LycansNewRoles.SabotageSingle");
		switch (MapManager.FindMapNameById(GameManager.Instance.MapID))
		{
		case "map_1":
		{
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(116.35f, 22.29f, 194.02f), 1.25f),
				new SabotageInitialization(new Vector3(150.1f, 22.1f, 118.25f), 1.25f),
				new SabotageInitialization(new Vector3(163.85f, 22.3f, 241.24f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit12 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit12.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WellPoison, sabotageInit12.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner12 = ((SimulationBehaviour)this).Runner;
			Quaternion? val23 = Quaternion.identity;
			object obj12 = _003C_003Ec._003C_003E9__25_0;
			if (obj12 == null)
			{
				OnBeforeSpawned val24 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 3);
				};
				_003C_003Ec._003C_003E9__25_0 = val24;
				obj12 = (object)val24;
			}
			runner12.Spawn(networkObject2, (Vector3?)null, val23, (PlayerRef?)null, (OnBeforeSpawned)obj12, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(112.98f, 22.19f, 174.06f), 1.25f),
				new SabotageInitialization(new Vector3(197.46f, 29.06f, 176.79f), 1.25f),
				new SabotageInitialization(new Vector3(155.47f, 22.33f, 140.9f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit13 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit13.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WolvesRitual, sabotageInit13.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner13 = ((SimulationBehaviour)this).Runner;
			Quaternion? val25 = Quaternion.identity;
			object obj13 = _003C_003Ec._003C_003E9__25_1;
			if (obj13 == null)
			{
				OnBeforeSpawned val26 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WolvesRitual, 3);
				};
				_003C_003Ec._003C_003E9__25_1 = val26;
				obj13 = (object)val26;
			}
			runner13.Spawn(networkObject2, (Vector3?)null, val25, (PlayerRef?)null, (OnBeforeSpawned)obj13, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(89.74f, 20.94f, 192.18f), 1.25f),
				new SabotageInitialization(new Vector3(166.05f, 20.96f, 269.8f), 1.25f),
				new SabotageInitialization(new Vector3(226.2f, 29.54f, 187.78f), 1.25f),
				new SabotageInitialization(new Vector3(177.78f, 20.8f, 127.9f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit14 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit14.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.Portals, sabotageInit14.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner14 = ((SimulationBehaviour)this).Runner;
			Quaternion? val27 = Quaternion.identity;
			object obj14 = _003C_003Ec._003C_003E9__25_2;
			if (obj14 == null)
			{
				OnBeforeSpawned val28 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.Portals, 3);
				};
				_003C_003Ec._003C_003E9__25_2 = val28;
				obj14 = (object)val28;
			}
			runner14.Spawn(networkObject2, (Vector3?)null, val27, (PlayerRef?)null, (OnBeforeSpawned)obj14, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			break;
		}
		case "map_2":
		{
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(385.98f, 23.01f, -89.88f), 1f),
				new SabotageInitialization(new Vector3(413.39f, 23.01f, -108.93f), 1f),
				new SabotageInitialization(new Vector3(422.6f, 23.01f, -142.91f), 1f),
				new SabotageInitialization(new Vector3(386.46f, 23.01f, -147.75f), 1f)
			};
			foreach (SabotageInitialization sabotageInit7 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit7.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WellPoison, sabotageInit7.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner7 = ((SimulationBehaviour)this).Runner;
			Quaternion? val13 = Quaternion.identity;
			object obj7 = _003C_003Ec._003C_003E9__25_3;
			if (obj7 == null)
			{
				OnBeforeSpawned val14 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 4);
				};
				_003C_003Ec._003C_003E9__25_3 = val14;
				obj7 = (object)val14;
			}
			runner7.Spawn(networkObject2, (Vector3?)null, val13, (PlayerRef?)null, (OnBeforeSpawned)obj7, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(403.4f, 23.01f, -133.61f), 1.25f),
				new SabotageInitialization(new Vector3(424.49f, 24.88f, -128.28f), 1.5f),
				new SabotageInitialization(new Vector3(401f, 34.18f, -145.13f), 1.5f)
			};
			foreach (SabotageInitialization sabotageInit8 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit8.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.CursedNight, sabotageInit8.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner8 = ((SimulationBehaviour)this).Runner;
			Quaternion? val15 = Quaternion.identity;
			object obj8 = _003C_003Ec._003C_003E9__25_4;
			if (obj8 == null)
			{
				OnBeforeSpawned val16 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.CursedNight, 3);
				};
				_003C_003Ec._003C_003E9__25_4 = val16;
				obj8 = (object)val16;
			}
			runner8.Spawn(networkObject2, (Vector3?)null, val15, (PlayerRef?)null, (OnBeforeSpawned)obj8, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			break;
		}
		case "map_dungeon":
		{
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(1.8f, 4.5f, 21.15f), 1.25f),
				new SabotageInitialization(new Vector3(14.85f, 4.5f, -41.4f), 1.25f),
				new SabotageInitialization(new Vector3(-32.4f, 0f, -36.9f), 1.25f),
				new SabotageInitialization(new Vector3(-29.7f, 0f, 35.41f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit15 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit15.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.Portals, sabotageInit15.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner15 = ((SimulationBehaviour)this).Runner;
			Quaternion? val29 = Quaternion.identity;
			object obj15 = _003C_003Ec._003C_003E9__25_5;
			if (obj15 == null)
			{
				OnBeforeSpawned val30 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.Portals, 3);
				};
				_003C_003Ec._003C_003E9__25_5 = val30;
				obj15 = (object)val30;
			}
			runner15.Spawn(networkObject2, (Vector3?)null, val29, (PlayerRef?)null, (OnBeforeSpawned)obj15, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			break;
		}
		case "map_haddoncans":
		{
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(43.11f, 0.2f, -42.44f), 1.25f),
				new SabotageInitialization(new Vector3(5.83f, 0.2f, 40.81f), 1.25f),
				new SabotageInitialization(new Vector3(-41.38f, 0.2f, 16.65f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit5 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit5.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WellPoison, sabotageInit5.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner5 = ((SimulationBehaviour)this).Runner;
			Quaternion? val9 = Quaternion.identity;
			object obj5 = _003C_003Ec._003C_003E9__25_6;
			if (obj5 == null)
			{
				OnBeforeSpawned val10 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 3);
				};
				_003C_003Ec._003C_003E9__25_6 = val10;
				obj5 = (object)val10;
			}
			runner5.Spawn(networkObject2, (Vector3?)null, val9, (PlayerRef?)null, (OnBeforeSpawned)obj5, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(-28.97f, 0.2f, -35.75f), 1.25f),
				new SabotageInitialization(new Vector3(44.65f, 0.2f, 30.51f), 1.25f),
				new SabotageInitialization(new Vector3(-23.28f, 0.2f, 30.53f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit6 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit6.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WolvesRitual, sabotageInit6.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner6 = ((SimulationBehaviour)this).Runner;
			Quaternion? val11 = Quaternion.identity;
			object obj6 = _003C_003Ec._003C_003E9__25_7;
			if (obj6 == null)
			{
				OnBeforeSpawned val12 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WolvesRitual, 3);
				};
				_003C_003Ec._003C_003E9__25_7 = val12;
				obj6 = (object)val12;
			}
			runner6.Spawn(networkObject2, (Vector3?)null, val11, (PlayerRef?)null, (OnBeforeSpawned)obj6, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			break;
		}
		case "map_apartcan":
		{
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(39.75f, 0f, -40.6f), 1.25f),
				new SabotageInitialization(new Vector3(43.15f, 0f, 43.66f), 1.25f),
				new SabotageInitialization(new Vector3(-39.77f, 0.97f, 24.51f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit9 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit9.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WellPoison, sabotageInit9.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner9 = ((SimulationBehaviour)this).Runner;
			Quaternion? val17 = Quaternion.identity;
			object obj9 = _003C_003Ec._003C_003E9__25_8;
			if (obj9 == null)
			{
				OnBeforeSpawned val18 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 3);
				};
				_003C_003Ec._003C_003E9__25_8 = val18;
				obj9 = (object)val18;
			}
			runner9.Spawn(networkObject2, (Vector3?)null, val17, (PlayerRef?)null, (OnBeforeSpawned)obj9, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(41.86f, 0.65f, -40.53f), 1.25f),
				new SabotageInitialization(new Vector3(40.76f, 0f, 35.02f), 1f),
				new SabotageInitialization(new Vector3(44.46f, 0f, 35.01f), 1f),
				new SabotageInitialization(new Vector3(24.18f, 0.89f, 8.53f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit10 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit10.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WolvesRitual, sabotageInit10.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner10 = ((SimulationBehaviour)this).Runner;
			Quaternion? val19 = Quaternion.identity;
			object obj10 = _003C_003Ec._003C_003E9__25_9;
			if (obj10 == null)
			{
				OnBeforeSpawned val20 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WolvesRitual, 4);
				};
				_003C_003Ec._003C_003E9__25_9 = val20;
				obj10 = (object)val20;
			}
			runner10.Spawn(networkObject2, (Vector3?)null, val19, (PlayerRef?)null, (OnBeforeSpawned)obj10, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(-49.22f, -0.13f, -15.33f), 1.25f),
				new SabotageInitialization(new Vector3(-9.86f, -0.14f, 28.3f), 1.25f),
				new SabotageInitialization(new Vector3(22.71f, -0.14f, -47.36f), 1.25f)
			};
			foreach (SabotageInitialization sabotageInit11 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit11.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.Portals, sabotageInit11.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner11 = ((SimulationBehaviour)this).Runner;
			Quaternion? val21 = Quaternion.identity;
			object obj11 = _003C_003Ec._003C_003E9__25_10;
			if (obj11 == null)
			{
				OnBeforeSpawned val22 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.Portals, 3);
				};
				_003C_003Ec._003C_003E9__25_10 = val22;
				obj11 = (object)val22;
			}
			runner11.Spawn(networkObject2, (Vector3?)null, val21, (PlayerRef?)null, (OnBeforeSpawned)obj11, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			break;
		}
		case "map_laboratory":
		{
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(-3.6f, 0.09f, 33.3f), 1f),
				new SabotageInitialization(new Vector3(3.3f, 0.06f, 0f), 1f),
				new SabotageInitialization(new Vector3(-51f, 4.89f, -22.2f), 1f),
				new SabotageInitialization(new Vector3(45.6f, 4.89f, 6.6f), 1f)
			};
			foreach (SabotageInitialization sabotageInit3 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit3.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.LaboratoryAutodoors, sabotageInit3.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner3 = ((SimulationBehaviour)this).Runner;
			Quaternion? val5 = Quaternion.identity;
			object obj3 = _003C_003Ec._003C_003E9__25_11;
			if (obj3 == null)
			{
				OnBeforeSpawned val6 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.LaboratoryAutodoors, 4);
				};
				_003C_003Ec._003C_003E9__25_11 = val6;
				obj3 = (object)val6;
			}
			runner3.Spawn(networkObject2, (Vector3?)null, val5, (PlayerRef?)null, (OnBeforeSpawned)obj3, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(0.34f, 7.23f, -4.2f), 1f),
				new SabotageInitialization(new Vector3(-4.8f, 0f, -26.4f), 1f),
				new SabotageInitialization(new Vector3(36f, 0f, 7.2f), 1f),
				new SabotageInitialization(new Vector3(-19.2f, 0f, 31.2f), 1f)
			};
			foreach (SabotageInitialization sabotageInit4 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit4.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.LaboratoryLights, sabotageInit4.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner4 = ((SimulationBehaviour)this).Runner;
			Quaternion? val7 = Quaternion.identity;
			object obj4 = _003C_003Ec._003C_003E9__25_12;
			if (obj4 == null)
			{
				OnBeforeSpawned val8 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.LaboratoryLights, 4);
				};
				_003C_003Ec._003C_003E9__25_12 = val8;
				obj4 = (object)val8;
			}
			runner4.Spawn(networkObject2, (Vector3?)null, val7, (PlayerRef?)null, (OnBeforeSpawned)obj4, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			break;
		}
		case "map_got":
		{
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(-12.33f, 0.31f, -18.79f), 1f),
				new SabotageInitialization(new Vector3(24.7f, 0.25f, 1.29f), 1f),
				new SabotageInitialization(new Vector3(-8.98f, 1.23f, 15.47f), 1f),
				new SabotageInitialization(new Vector3(-16.37f, 0.31f, 3.16f), 1f)
			};
			foreach (SabotageInitialization sabotageInit in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.WellPoison, sabotageInit.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner = ((SimulationBehaviour)this).Runner;
			Quaternion? val = Quaternion.identity;
			object obj = _003C_003Ec._003C_003E9__25_13;
			if (obj == null)
			{
				OnBeforeSpawned val2 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.WellPoison, 4);
				};
				_003C_003Ec._003C_003E9__25_13 = val2;
				obj = (object)val2;
			}
			runner.Spawn(networkObject2, (Vector3?)null, val, (PlayerRef?)null, (OnBeforeSpawned)obj, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			list = new List<SabotageInitialization>
			{
				new SabotageInitialization(new Vector3(-4.37f, 0.31f, -0.37f), 1f),
				new SabotageInitialization(new Vector3(0.51f, 0.31f, 20.7f), 1f),
				new SabotageInitialization(new Vector3(-22.62f, 3.9f, 16.18f), 1f),
				new SabotageInitialization(new Vector3(-27.68f, 0.3f, -27.75f), 1f)
			};
			foreach (SabotageInitialization sabotageInit2 in list)
			{
				((SimulationBehaviour)this).Runner.Spawn(networkObject, (Vector3?)sabotageInit2.Position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
				{
					SabotageObject component = ((Component)no).GetComponent<SabotageObject>();
					component.Init(currentIndex, SabotageIds.CursedNight, sabotageInit2.ActivationDuration);
					SabotageObjectsByIndex[currentIndex] = component;
				}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
				int num = currentIndex;
				currentIndex = num + 1;
			}
			NetworkRunner runner2 = ((SimulationBehaviour)this).Runner;
			Quaternion? val3 = Quaternion.identity;
			object obj2 = _003C_003Ec._003C_003E9__25_14;
			if (obj2 == null)
			{
				OnBeforeSpawned val4 = delegate(NetworkRunner _, NetworkObject no)
				{
					((Component)no).GetComponent<SabotageSingle>().Init(SabotageIds.CursedNight, 4);
				};
				_003C_003Ec._003C_003E9__25_14 = val4;
				obj2 = (object)val4;
			}
			runner2.Spawn(networkObject2, (Vector3?)null, val3, (PlayerRef?)null, (OnBeforeSpawned)obj2, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
			break;
		}
		}
	}

	public void Clean()
	{
		try
		{
			if (((SimulationBehaviour)this).Runner.IsServer)
			{
				foreach (SabotageObject value in SabotageObjectsByIndex.Values)
				{
					((SimulationBehaviour)this).Runner.Despawn(((Component)value).GetComponent<NetworkObject>(), false);
				}
				foreach (SabotageSingle value2 in Sabotages.Values)
				{
					((SimulationBehaviour)this).Runner.Despawn(((Component)value2).GetComponent<NetworkObject>(), false);
				}
			}
			SabotageObjectsByIndex.Clear();
			Sabotages.Clear();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Sabotages clean error: " + ex));
		}
	}

	public bool IsSabotageActive(SabotageIds sabotageId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			return (int)GameManager.State.Current == 2 && Sabotages.ContainsKey(sabotageId) && NetworkBool.op_Implicit(Sabotages[sabotageId].Active);
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("IsSabotageActive exception: " + ex));
			return false;
		}
	}

	[Rpc]
	public unsafe static void Rpc_Sabotage(NetworkRunner runner, int playerIndex, int sabotageObjectIndex, int sabotageType)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (NetworkBehaviourUtils.InvokeRpc)
			{
				NetworkBehaviourUtils.InvokeRpc = false;
			}
			else
			{
				if ((Object)(object)runner == (Object)null)
				{
					throw new ArgumentNullException("runner");
				}
				if ((int)runner.Stage == 4)
				{
					return;
				}
				if (runner.HasAnyActiveConnections())
				{
					int num = 32;
					SimulationMessage* ptr = SimulationMessage.Allocate(runner.Simulation, num);
					byte* data = SimulationMessage.GetData(ptr);
					int num2 = RpcHeader.Write(RpcHeader.Create(NetworkBehaviourUtils.GetRpcStaticIndexOrThrow("System.Void LycansNewRoles.PlayerCustom::Rpc_Sabotage(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")), data);
					*(int*)(data + num2) = playerIndex;
					num2 += 4;
					*(int*)(data + num2) = sabotageObjectIndex;
					num2 += 4;
					*(int*)(data + num2) = sabotageType;
					num2 += 4;
					((SimulationMessage)ptr).Offset = num2 * 12;
					((SimulationMessage)ptr).SetStatic();
					runner.SendRpc(ptr);
				}
			}
			PlayerCustom player = PlayerCustomRegistry.GetPlayer(playerIndex);
			if (runner.IsServer)
			{
				SabotageObject sabotageObject = Instance.SabotageObjectsByIndex[sabotageObjectIndex];
				float num3 = sabotageObject.ActivationDuration;
				if (sabotageType == 1)
				{
					num3 *= 5f;
				}
				player.StartSabotage(sabotageObject.SabotageObjectIndex, num3, sabotageType == 1);
			}
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("Rpc_Sabotage error: " + ex));
		}
	}

	[NetworkRpcStaticWeavedInvoker("System.Void LycansNewRoles.PlayerCustom::Rpc_Sabotage(Fusion.NetworkRunner,System.Int32,System.Int32,System.Int32)")]
	[Preserve]
	protected unsafe static void Rpc_Sabotage_0040Invoker(NetworkRunner runner, SimulationMessage* message)
	{
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		int playerIndex = *(int*)(data + num);
		num += 4;
		int sabotageObjectIndex = *(int*)(data + num);
		num += 4;
		int sabotageType = *(int*)(data + num);
		num += 4;
		NetworkBehaviourUtils.InvokeRpc = true;
		Rpc_Sabotage(runner, playerIndex, sabotageObjectIndex, sabotageType);
	}

	public void OnSabotageCompleted(int sabotageObjectIndex)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		SabotageObject sabotageObject = SabotageObjectsByIndex[sabotageObjectIndex];
		if (!NetworkBool.op_Implicit(sabotageObject.Completed))
		{
			sabotageObject.Completed = NetworkBool.op_Implicit(true);
			Sabotages.First((KeyValuePair<SabotageIds, SabotageSingle> o) => o.Key == (SabotageIds)sabotageObject.SabotageId).Value.AddStep();
		}
	}

	public void StartCursedNightTimer()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		int num = PlayerRegistry.CountWhere((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead) && !NetworkBool.op_Implicit(o.IsWolf)));
		float num2 = 100f / (float)num;
		CursedNightTimer = TickTimer.CreateFromSeconds(((SimulationBehaviour)this).Runner, num2);
	}

	public override void FixedUpdateNetwork()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		if (!((SimulationBehaviour)this).Runner.IsServer)
		{
			return;
		}
		TickTimer cursedNightTimer = CursedNightTimer;
		if (!((TickTimer)(ref cursedNightTimer)).Expired(((SimulationBehaviour)this).Runner))
		{
			return;
		}
		if (IsSabotageActive(SabotageIds.CursedNight))
		{
			PlayerController val = CollectionsUtil.Grab<PlayerController>(PlayerRegistry.Where((Predicate<PlayerController>)((PlayerController o) => !NetworkBool.op_Implicit(o.IsDead) && !NetworkBool.op_Implicit(o.IsWolf))).ToList(), 1).First();
			List<Effect> list = (from o in EffectManager.GetEffects()
				where (int)o.GetEffectType() == 2
				select o).ToList();
			list.Add(Plugin.NewEffects.First((Effect o) => o is DiseasedEffect));
			list.Add(Plugin.NewEffects.First((Effect o) => o is DownedEffect));
			list.Add(Plugin.NewEffects.First((Effect o) => o is WoundedEffect));
			list.Add(Plugin.NewEffects.First((Effect o) => o is PoisonEffect));
			Effect effect = CollectionsUtil.Grab<Effect>(list, 1).First();
			PlayerCustom.ApplyEffectToPlayer(val, effect, ((SimulationBehaviour)this).Runner);
			PlayerCustom.Rpc_Effect_On_Player(((SimulationBehaviour)this).Runner, val.Index, 1);
			GameManager.Rpc_BroadcastFollowSound(((SimulationBehaviour)this).Runner, NetworkString<_16>.op_Implicit("SUCCESS_SHOT"), ((Component)val).transform.position, 15f, 0.5f);
			StartCursedNightTimer();
		}
		else
		{
			CursedNightTimer = TickTimer.None;
		}
	}
}
