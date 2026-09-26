using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using Fusion;
using HarmonyLib;
using HarmonyLib.Tools;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewItems;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.NewMaps;
using LycansNewRoles.NewPrimaryRoles;
using LycansNewRoles.PowerObjects;
using LycansNewRoles.Sabotages;
using Photon.Voice.Fusion;
using UnityEngine;

namespace LycansNewRoles;

[BepInPlugin("LycansNewRoles", "Lycans New Roles", "0.358")]
public class Plugin : BaseUnityPlugin
{
	public static NetworkObject NetworkObject;

	public static List<Effect> NewEffects = new List<Effect>();

	public static MinimapComponent Minimap;

	public static List<AssetBundle> NewHatsBundles = new List<AssetBundle>();

	public static List<GameObject> NewHats = new List<GameObject>();

	public static List<AssetBundle> PetBundles = new List<AssetBundle>();

	public static List<string> PetNames = new List<string>();

	public static List<AssetBundle> SkinBundles = new List<AssetBundle>();

	public static List<CharacterSkin> Skins = new List<CharacterSkin>();

	public static AssetBundle NewRolesCoreBundle;

	public static AssetBundle NewMapsCoreBundle;

	internal static ManualLogSource Logger;

	public static string Location;

	public static Dictionary<int, string> NewMapPathById = new Dictionary<int, string>();

	public const string SoundSurvivalistDying = "SurvivalistDying";

	public const string SoundBombTicking = "BombTicking";

	public const string SoundBombExplosion = "BombExplosion";

	public const string SoundPowerAvailable = "PowerAvailable";

	public const string SoundUseTrappedItem = "UseTrappedItem";

	public const string SoundAngelHeal = "AngelHeal";

	public const string SoundBeastAwakening = "BeastAwakening";

	public const string SoundBeastHeartBeatFast = "BeastHeartBeatFast";

	public const string SoundBeastHeartBeatMid = "BeastHeartBeatMid";

	public const string SoundBeastHeartBeatSlow = "BeastHeartBeatSlow";

	public const string SoundDiamondEffect = "DiamondEffect";

	public const string SoundTrapDisarm = "TrapDisarm";

	public const string SoundMercenaryShot = "BountyHunterShot";

	public const string SoundGrenadeEffect = "GrenadeEffect";

	public const string SoundGrenadeThrow = "GrenadeThrow";

	public const string SoundChaos = "ChaosEffect";

	public const string SoundZombie1 = "Zombie1";

	public const string SoundZombie2 = "Zombie2";

	public const string SoundZombie3 = "Zombie3";

	public const string SoundZombieAttack = "ZombieAttack";

	public const string SoundVoodooReanimation = "VoodooRez";

	public const string SoundPredatorKill = "PredatorKill";

	public const string SoundExorcism = "Exorcism";

	public const string SoundSpiritSummoned = "SpiritSummoned";

	public const string SoundMegaFart = "MegaFart";

	public const string SoundPossessorDance = "PossessorDance";

	public const string SoundKilledByCrystalBallGuess = "SeerCorrectGuessSound";

	public const string SoundSleepingGasDetonate = "SleepingGasBreak";

	public const string SoundScoutAlert = "ScoutAlert";

	public const string SoundIsolation = "Isolation";

	public const string SoundMayor = "Mayor";

	public const string SoundAlchemistTransform = "AlchemistT";

	public const string SoundInquisitorFire = "InquisitorFire";

	public const string SoundBanish = "Banish";

	public const string SoundRadar = "Radar";

	public const string SoundCultistSkull = "CultistSkull";

	public const string SoundCultistCapture = "CultistCapture";

	public const string SoundParasite = "Parasite";

	public const string SoundRuneTrigger = "RuneTrigger";

	public const string SoundRuneExplosion = "RuneExplosion";

	public const string SoundInventorDeviceActivation = "InventorDeviceActivation";

	public const string SoundInventorScrapAlert = "InventorScrapAlert";

	public const string SoundAcrobatJump = "AcrobatJump";

	public const string SoundBurp = "Burp";

	public const string SoundTinyCrush = "TinyCrush";

	public static bool PlayerIllusionCreated = false;

	public static GameConfig CustomConfig => ((Object)(object)NetworkObject != (Object)null) ? ((Component)NetworkObject).GetComponent<GameConfig>() : null;

	private void Awake()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		try
		{
			Logger = ((BaseUnityPlugin)this).Logger;
			Logger.LogInfo((object)"Plugin LycansNewRoles is loaded!");
			TranslationManager.Instance.Initialize();
			Harmony val = new Harmony("lycans.nalesplugin");
			HarmonyFileLog.Enabled = true;
			val.PatchAll();
			Logger.LogInfo((object)"Harmony patched!");
			Location = ((BaseUnityPlugin)this).Info.Location;
			if ((Object)(object)NewRolesCoreBundle == (Object)null)
			{
				try
				{
					string text = Path.GetDirectoryName(Location) + "/resources/lycansnewroles";
					NewRolesCoreBundle = AssetBundle.LoadFromFile(text);
					Object[] array = NewRolesCoreBundle.LoadAllAssets();
					Object[] array2 = array;
					foreach (Object val2 in array2)
					{
						LycansUtility.DebugLog("Resource: " + val2.name);
					}
				}
				catch (Exception ex)
				{
					Logger.LogError((object)("Error loading bundle: " + ex));
				}
			}
			if ((Object)(object)NewMapsCoreBundle == (Object)null)
			{
				try
				{
					string text2 = Path.GetDirectoryName(Location) + "/resources/lycansnewmaps";
					NewMapsCoreBundle = AssetBundle.LoadFromFile(text2);
					Object[] array3 = NewMapsCoreBundle.LoadAllAssets();
					Object[] array4 = array3;
					foreach (Object val3 in array4)
					{
						LycansUtility.DebugLog("Resource: " + val3.name);
					}
				}
				catch (Exception ex2)
				{
					Logger.LogError((object)("Error loading bundle: " + ex2));
				}
			}
			string path = Path.GetDirectoryName(Location) + "/resources/";
			List<string> source = Directory.EnumerateFiles(path).ToList();
			int num = 3;
			NewMapPathById.Clear();
			foreach (string item4 in source.Where((string o) => !o.Contains(".json")))
			{
				string text3 = item4.Split('/').Last();
				if (text3.ToLower().StartsWith("map_"))
				{
					NewMapPathById.Add(num, text3.ToLower());
					num++;
				}
				else if (item4.ToLower().Contains("/hats_"))
				{
					Logger.LogInfo((object)("Adding hats bundle: " + item4));
					AssetBundle item = AssetBundle.LoadFromFile(Path.GetDirectoryName(Location) + "/resources/" + text3);
					NewHatsBundles.Add(item);
				}
				else if (item4.ToLower().Contains("/pets_"))
				{
					Logger.LogInfo((object)("Adding pets bundle: " + item4));
					AssetBundle item2 = AssetBundle.LoadFromFile(Path.GetDirectoryName(Location) + "/resources/" + text3);
					PetBundles.Add(item2);
				}
				else if (item4.ToLower().Contains("/skins_"))
				{
					Logger.LogInfo((object)("Adding skins bundle: " + item4));
					AssetBundle item3 = AssetBundle.LoadFromFile(Path.GetDirectoryName(Location) + "/resources/" + text3);
					SkinBundles.Add(item3);
				}
			}
		}
		catch (Exception ex3)
		{
			Logger.LogError((object)("Error initializing plugin: " + ex3));
		}
	}

	public static void CreatePrefabs()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_20c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_2110: Unknown result type (might be due to invalid IL or missing references)
		//IL_212c: Unknown result type (might be due to invalid IL or missing references)
		//IL_30cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_310d: Unknown result type (might be due to invalid IL or missing references)
		//IL_379b: Unknown result type (might be due to invalid IL or missing references)
		//IL_3821: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			NewEffects.Clear();
			Logger.LogInfo((object)"CreatePrefabs...");
			GameObject val = new GameObject("Manager");
			val.AddComponent<NetworkObject>();
			val.AddComponent<GameConfig>();
			RegisterGameObject(val, "LycansNewRoles.GlobalManager");
			GameObject val2 = new GameObject("SabotageManager");
			val2.AddComponent<NetworkObject>();
			val2.AddComponent<SabotageManager>();
			RegisterGameObject(val2, "LycansNewRoles.SabotageManager");
			GameObject val3 = new GameObject("SabotageSingle");
			val3.AddComponent<NetworkObject>();
			val3.AddComponent<SabotageSingle>();
			RegisterGameObject(val3, "LycansNewRoles.SabotageSingle");
			GameObject val4 = new GameObject("SabotageObject");
			val4.AddComponent<NetworkObject>();
			val4.AddComponent<NetworkTransform>();
			val4.AddComponent<SabotageObject>();
			RegisterGameObject(val4, "LycansNewRoles.SabotageObject");
			GameObject val5 = new GameObject("BeastManager");
			val5.AddComponent<NetworkObject>();
			val5.AddComponent<BeastManager>();
			RegisterGameObject(val5, "LycansNewRoles.BeastManager");
			GameObject val6 = new GameObject("CultistManager");
			val6.AddComponent<NetworkObject>();
			val6.AddComponent<CultistManager>();
			RegisterGameObject(val6, "LycansNewRoles.CultistManager");
			GameObject val7 = new GameObject("VoodooManager");
			val7.AddComponent<NetworkObject>();
			val7.AddComponent<VoodooManager>();
			RegisterGameObject(val7, "LycansNewRoles.VoodooManager");
			GameObject val8 = new GameObject("DraftManager");
			val8.AddComponent<NetworkObject>();
			val8.AddComponent<DraftManager>();
			RegisterGameObject(val8, "LycansNewRoles.DraftManager");
			GameObject val9 = new GameObject("GameManagerCustom");
			val9.AddComponent<NetworkObject>();
			val9.AddComponent<GameManagerCustom>();
			RegisterGameObject(val9, "LycansNewRoles.GameManagerCustom");
			GameObject val10 = new GameObject("PlayerCustomRegistry");
			val10.AddComponent<NetworkObject>();
			val10.AddComponent<PlayerCustomRegistry>();
			RegisterGameObject(val10, "LycansNewRoles.PlayerCustomRegistry");
			GameObject val11 = new GameObject("PlayerCustom");
			val11.AddComponent<NetworkObject>();
			val11.AddComponent<NetworkTransform>();
			val11.AddComponent<PlayerCustom>();
			RegisterGameObject(val11, "LycansNewRoles.PlayerCustom");
			GameObject val12 = new GameObject("ItemCustom");
			val12.AddComponent<NetworkObject>();
			val12.AddComponent<NetworkTransform>();
			val12.AddComponent<ItemCustom>();
			RegisterGameObject(val12, "LycansNewRoles.ItemCustom");
			AddEffectToList("LycansNewRoles.EffectChasing", typeof(ChasingEffect), 3f);
			AddEffectToList("LycansNewRoles.EffectSprinting", typeof(SprintEffect), 5f);
			AddEffectToList("LycansNewRoles.EffectReverting", typeof(RevertingEffect), 15f);
			AddEffectToList("LycansNewRoles.EffectDisoriented", typeof(DisorientedEffect), 3f);
			AddEffectToList("LycansNewRoles.EffectUndetected", typeof(UndetectedEffect), 15f);
			AddEffectToList("LycansNewRoles.EffectStarvationActive", typeof(StarvationActiveEffect), 150f);
			AddEffectToList("LycansNewRoles.EffectBlind", typeof(BlindEffect), 3.5f);
			AddEffectToList("LycansNewRoles.EffectIllusion", typeof(IllusionEffect), 15f);
			AddEffectToList("LycansNewRoles.EffectDisguised", typeof(DisguisedEffect), 300f);
			AddEffectToList("LycansNewRoles.EffectDiseased", typeof(DiseasedEffect), 300f);
			AddEffectToList("LycansNewRoles.EffectPossessed", typeof(PossessedEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectDowned", typeof(DownedEffect), 10f);
			AddEffectToList("LycansNewRoles.EffectWounded", typeof(WoundedEffect), 30f);
			AddEffectToList("LycansNewRoles.EffectPoisoned", typeof(PoisonEffect), 45f);
			AddEffectToList("LycansNewRoles.EffectEmpowered", typeof(EmpoweredEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectExorcismActive", typeof(ExorcismEffect), 15f);
			AddEffectToList("LycansNewRoles.EffectTelepathy", typeof(TelepathyEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectNauseated", typeof(NauseatedEffect), 2f);
			AddEffectToList("LycansNewRoles.EffectPhasing", typeof(PhasingEffect), 3f);
			AddEffectToList("LycansNewRoles.EffectDying", typeof(DyingEffect), 45f);
			AddEffectToList("LycansNewRoles.EffectBomb", typeof(BombEffect), 20f);
			AddEffectToList("LycansNewRoles.EffectPanic", typeof(PanicEffect), 3f);
			AddEffectToList("LycansNewRoles.EffectFleeing", typeof(FleeingEffect), 4f);
			AddEffectToList("LycansNewRoles.EffectAngel", typeof(AngelEffect), 12f);
			AddEffectToList("LycansNewRoles.EffectTracking", typeof(TrackingEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectParalyzed", typeof(ParalyzedEffect), 4f);
			AddEffectToList("LycansNewRoles.EffectResistance", typeof(ResistanceEffect), 10f);
			AddEffectToList("LycansNewRoles.EffectPredator", typeof(PredatorEffect), 45f);
			AddEffectToList("LycansNewRoles.EffectResurrected", typeof(ResurrectedEffect), 300f);
			AddEffectToList("LycansNewRoles.EffectPetrified", typeof(PetrifiedEffect), 60f);
			AddEffectToList("LycansNewRoles.EffectPortal", typeof(PortalEffect), 8f);
			AddEffectToList("LycansNewRoles.EffectWeakened", typeof(WeakenedEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectTrapResistance", typeof(TrapResistanceEffect), 1f);
			AddEffectToList("LycansNewRoles.EffectSpiritResistance", typeof(SpiritResistanceEffect), 15f);
			AddEffectToList("LycansNewRoles.EffectSleepy", typeof(SleepyEffect), 7f);
			AddEffectToList("LycansNewRoles.EffectAsleep", typeof(AsleepEffect), 30f);
			AddEffectToList("LycansNewRoles.EffectIsolation", typeof(IsolationEffect), 1f);
			AddEffectToList("LycansNewRoles.EffectSneaky", typeof(SneakyEffect), 7f);
			AddEffectToList("LycansNewRoles.EffectResilience", typeof(ResilienceEffect), 8f);
			AddEffectToList("LycansNewRoles.EffectEndurance", typeof(EnduranceEffect), 8f);
			AddEffectToList("LycansNewRoles.EffectEscaping", typeof(EscapingEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectBanished", typeof(BanishedEffect), 8f);
			AddEffectToList("LycansNewRoles.EffectRecuperating", typeof(RecuperatingEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectBurning", typeof(BurningEffect), 8f);
			AddEffectToList("LycansNewRoles.EffectTenacity", typeof(TenacityEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectHubris", typeof(HubrisEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectSpotter", typeof(SpotterEffect), 20f);
			AddEffectToList("LycansNewRoles.EffectPurifierBurn", typeof(PurifierBurnEffect), 8f);
			AddEffectToList("LycansNewRoles.EffectDetected", typeof(DetectedEffect), 3f);
			AddEffectToList("LycansNewRoles.EffectKidnapperSilence", typeof(KidnapperSilenceEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectCaptured", typeof(CapturedEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectHidden", typeof(HiddenEffect), 2f);
			AddEffectToList("LycansNewRoles.EffectTournamentWinner", typeof(TournamentWinner), 3600f);
			AddEffectToList("LycansNewRoles.EffectTournamentLoser", typeof(TournamentLoser), 3600f);
			AddEffectToList("LycansNewRoles.EffectMoleClock", typeof(MoleClockEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectJump", typeof(JumpEffect), 3600f);
			AddEffectToList("LycansNewRoles.EffectStrengthened", typeof(StrenghtenedEffect), 30f);
			AddEffectToList("LycansNewRoles.EffectSanctuary", typeof(SanctuaryEffect), 3600f);
			GameObject val13 = AddEffectToList("LycansNewRoles.EffectDeafness", typeof(DeafnessEffect), 60f);
			GameObject val14 = AddEffectToList("LycansNewRoles.EffectMidas", typeof(MidasEffect), 60f);
			GameObject val15 = AddEffectToList("LycansNewRoles.EffectVampire", typeof(VampireEffect), 35f);
			GameObject val16 = AddEffectToList("LycansNewRoles.EffectVampireTarget", typeof(VampireTargetEffect), 1f);
			GameObject val17 = AddEffectToList("LycansNewRoles.EffectTiny", typeof(TinyEffect), 35f);
			GameObject val18 = AddEffectToList("LycansNewRoles.EffectHaunted", typeof(HauntedEffect), 70f);
			GameObject val19 = AddEffectToList("LycansNewRoles.EffectNearsighted", typeof(NearsightedEffect), 60f);
			GameObject val20 = AddEffectToList("LycansNewRoles.EffectAssassin", typeof(AssassinEffect), 20f);
			GameObject val21 = AddEffectToList("LycansNewRoles.EffectStinking", typeof(StinkingEffect), 60f);
			GameObject val22 = AddEffectToList("LycansNewRoles.EffectMute", typeof(MuteEffect), 100f);
			GameObject val23 = AddEffectToList("LycansNewRoles.EffectClairvoyance", typeof(ClairvoyanceEffect), 75f);
			GameObject val24 = AddEffectToList("LycansNewRoles.EffectChaos", typeof(ChaosEffect), 60f);
			GameObject val25 = AddEffectToList("LycansNewRoles.EffectEnergized", typeof(EnergizedEffect), 60f);
			GameObject val26 = AddEffectToList("LycansNewRoles.EffectConfused", typeof(ConfusedEffect), 45f);
			GameObject val27 = AddEffectToList("LycansNewRoles.EffectStealthing", typeof(CamouflageEffect), 90f);
			GameObject val28 = AddEffectToList("LycansNewRoles.EffectImmune", typeof(ImmuneEffect), 80f);
			List<Effect> list = new List<Effect>();
			list.Add((Effect)(object)val13.GetComponent<DeafnessEffect>());
			list.Add((Effect)(object)val14.GetComponent<MidasEffect>());
			list.Add((Effect)(object)val15.GetComponent<VampireEffect>());
			list.Add((Effect)(object)val17.GetComponent<TinyEffect>());
			list.Add((Effect)(object)val18.GetComponent<HauntedEffect>());
			list.Add((Effect)(object)val19.GetComponent<NearsightedEffect>());
			list.Add((Effect)(object)val20.GetComponent<AssassinEffect>());
			list.Add((Effect)(object)val21.GetComponent<StinkingEffect>());
			list.Add((Effect)(object)val22.GetComponent<MuteEffect>());
			list.Add((Effect)(object)val23.GetComponent<ClairvoyanceEffect>());
			list.Add((Effect)(object)val24.GetComponent<ChaosEffect>());
			list.Add((Effect)(object)val25.GetComponent<EnergizedEffect>());
			list.Add((Effect)(object)val26.GetComponent<ConfusedEffect>());
			list.Add((Effect)(object)val27.GetComponent<CamouflageEffect>());
			list.Add((Effect)(object)val28.GetComponent<ImmuneEffect>());
			AddPotionEffectsToList(list);
			Traverse.Create((object)EffectManager.GetEffects().First((Effect o) => o is FlatulenceEffect)).Field<float>("duration").Value = 75f;
			Traverse.Create((object)EffectManager.GetEffects().First((Effect o) => o is GlowingEffect)).Field<float>("duration").Value = 90f;
			Traverse.Create((object)EffectManager.GetEffects().First((Effect o) => o is ParanoiaEffect)).Field<float>("duration").Value = 90f;
			Traverse.Create((object)EffectManager.GetEffects().First((Effect o) => o is TeleportationEffect)).Field<float>("duration").Value = 75f;
			GameObject val29 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MagicScroll"));
			val29.AddComponent<NetworkObject>();
			val29.AddComponent<NetworkTransform>();
			MagicScrollItem magicScrollItem = val29.AddComponent<MagicScrollItem>();
			((Object)val29).name = "LycansNewRoles.ItemMagicScroll";
			Traverse.Create((object)magicScrollItem).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)magicScrollItem).Field("resetDelay").SetValue((object)1);
			Traverse.Create((object)magicScrollItem).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)magicScrollItem).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("magicscrollsprite"));
			Traverse.Create((object)magicScrollItem).Field<MeshRenderer[]>("meshRenderers").Value = val29.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)magicScrollItem).Field<Collider>("itemCollider").Value = val29.GetComponentInChildren<Collider>();
			RegisterGameObject(val29, "LycansNewRoles.ItemMagicScroll");
			GameObject val30 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PhasingDiamond"));
			val30.AddComponent<NetworkObject>();
			val30.AddComponent<NetworkTransform>();
			PhasingDiamondItem phasingDiamondItem = val30.AddComponent<PhasingDiamondItem>();
			((Object)val30).name = "LycansNewRoles.ItemPhasingDiamond";
			Traverse.Create((object)phasingDiamondItem).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)phasingDiamondItem).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)phasingDiamondItem).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("phasingdiamondsprite"));
			Traverse.Create((object)phasingDiamondItem).Field<MeshRenderer[]>("meshRenderers").Value = val30.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)phasingDiamondItem).Field<Collider>("itemCollider").Value = val30.GetComponentInChildren<Collider>();
			RegisterGameObject(val30, "LycansNewRoles.ItemPhasingDiamond");
			GameObject val31 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("Grenade"));
			val31.AddComponent<NetworkObject>();
			val31.AddComponent<NetworkTransform>();
			GrenadeItem grenadeItem = val31.AddComponent<GrenadeItem>();
			((Object)val31).name = "LycansNewRoles.ItemGrenade";
			Traverse.Create((object)grenadeItem).Field("initialQuantity").SetValue((object)2);
			Traverse.Create((object)grenadeItem).Field("resetDelay").SetValue((object)15);
			Traverse.Create((object)grenadeItem).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)grenadeItem).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("grenadesprite"));
			Traverse.Create((object)grenadeItem).Field<MeshRenderer[]>("meshRenderers").Value = val31.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)grenadeItem).Field<Collider>("itemCollider").Value = val31.GetComponentInChildren<Collider>();
			RegisterGameObject(val31, "LycansNewRoles.ItemGrenade");
			GameObject val32 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("GrenadeActive"));
			val32.AddComponent<NetworkObject>();
			val32.AddComponent<NetworkTransform>();
			val32.AddComponent<GrenadeActive>();
			((Object)val32).name = "LycansNewRoles.ItemGrenadeActive";
			RegisterGameObject(val32, "LycansNewRoles.ItemGrenadeActive");
			GameObject val33 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SleepingGasItem"));
			val33.AddComponent<NetworkObject>();
			val33.AddComponent<NetworkTransform>();
			SleepingGasItem sleepingGasItem = val33.AddComponent<SleepingGasItem>();
			((Object)val33).name = "LycansNewRoles.ItemSleepingGas";
			Traverse.Create((object)sleepingGasItem).Field("triggerDelay").SetValue((object)4f);
			Traverse.Create((object)sleepingGasItem).Field("animationDuration").SetValue((object)4f);
			Traverse.Create((object)sleepingGasItem).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)sleepingGasItem).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)sleepingGasItem).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("sleepinggasitemsprite"));
			Traverse.Create((object)sleepingGasItem).Field<MeshRenderer[]>("meshRenderers").Value = val33.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)sleepingGasItem).Field<Collider>("itemCollider").Value = val33.GetComponentInChildren<Collider>();
			RegisterGameObject(val33, "LycansNewRoles.ItemSleepingGas");
			GameObject val34 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SleepingGasPlaced"));
			val34.AddComponent<NetworkObject>();
			val34.AddComponent<NetworkTransform>();
			val34.AddComponent<SleepingGasPlaced>();
			((Object)val34).name = "LycansNewRoles.ItemSleepingGasPlaced";
			RegisterGameObject(val34, "LycansNewRoles.ItemSleepingGasPlaced");
			GameObject val35 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("Molotov"));
			val35.AddComponent<NetworkObject>();
			val35.AddComponent<NetworkTransform>();
			MolotovItem molotovItem = val35.AddComponent<MolotovItem>();
			((Object)val35).name = "LycansNewRoles.ItemMolotove";
			Traverse.Create((object)molotovItem).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)molotovItem).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)molotovItem).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("molotovsprite"));
			Traverse.Create((object)molotovItem).Field<MeshRenderer[]>("meshRenderers").Value = val35.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)molotovItem).Field<Collider>("itemCollider").Value = val35.GetComponentInChildren<Collider>();
			RegisterGameObject(val35, "LycansNewRoles.ItemMolotove");
			GameObject val36 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MolotovActive"));
			val36.AddComponent<NetworkObject>();
			val36.AddComponent<NetworkTransform>();
			val36.AddComponent<MolotovActive>();
			((Object)val36).name = "LycansNewRoles.ItemMolotovActive";
			RegisterGameObject(val36, "LycansNewRoles.ItemMolotovActive");
			GameObject val37 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MolotovFire"));
			val37.AddComponent<NetworkObject>();
			val37.AddComponent<NetworkTransform>();
			val37.AddComponent<MolotovFire>();
			((Object)val37).name = "LycansNewRoles.ItemMolotovFire";
			RegisterGameObject(val37, "LycansNewRoles.ItemMolotovFire");
			GameObject val38 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("Radar"));
			val38.AddComponent<NetworkObject>();
			val38.AddComponent<NetworkTransform>();
			RadarItem radarItem = val38.AddComponent<RadarItem>();
			((Object)val38).name = "LycansNewRoles.ItemRadar";
			Traverse.Create((object)radarItem).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)radarItem).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)radarItem).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("radarsprite"));
			Traverse.Create((object)radarItem).Field<MeshRenderer[]>("meshRenderers").Value = val38.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)radarItem).Field<Collider>("itemCollider").Value = val38.GetComponentInChildren<Collider>();
			RegisterGameObject(val38, "LycansNewRoles.ItemRadar");
			GameObject val39 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("RadarActive"));
			val39.AddComponent<NetworkObject>();
			val39.AddComponent<NetworkTransform>();
			val39.AddComponent<RadarActive>();
			((Object)val39).name = "LycansNewRoles.ItemRadarActive";
			RegisterGameObject(val39, "LycansNewRoles.ItemRadarActive");
			GameObject val40 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AccessoryBoots"));
			val40.AddComponent<NetworkObject>();
			val40.AddComponent<NetworkTransform>();
			AccessoryBoots accessoryBoots = val40.AddComponent<AccessoryBoots>();
			((Object)val40).name = "LycansNewRoles.AccessoryBoots";
			Traverse.Create((object)accessoryBoots).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)accessoryBoots).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)accessoryBoots).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("bootssprite"));
			Traverse.Create((object)accessoryBoots).Field<MeshRenderer[]>("meshRenderers").Value = val40.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)accessoryBoots).Field<Collider>("itemCollider").Value = val40.GetComponent<Collider>();
			accessoryBoots.Light = val40.GetComponentInChildren<Light>();
			RegisterGameObject(val40, "LycansNewRoles.AccessoryBoots");
			GameObject val41 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AccessoryHorn"));
			val41.AddComponent<NetworkObject>();
			val41.AddComponent<NetworkTransform>();
			AccessoryHorn accessoryHorn = val41.AddComponent<AccessoryHorn>();
			((Object)val41).name = "LycansNewRoles.AccessoryHorns";
			Traverse.Create((object)accessoryHorn).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)accessoryHorn).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)accessoryHorn).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("hornsprite"));
			Traverse.Create((object)accessoryHorn).Field<MeshRenderer[]>("meshRenderers").Value = val41.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)accessoryHorn).Field<Collider>("itemCollider").Value = val41.GetComponent<Collider>();
			accessoryHorn.Light = val41.GetComponentInChildren<Light>();
			RegisterGameObject(val41, "LycansNewRoles.AccessoryHorns");
			GameObject val42 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AccessoryRing"));
			val42.AddComponent<NetworkObject>();
			val42.AddComponent<NetworkTransform>();
			AccessoryRing accessoryRing = val42.AddComponent<AccessoryRing>();
			((Object)val42).name = "LycansNewRoles.AccessoryRing";
			Traverse.Create((object)accessoryRing).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)accessoryRing).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)accessoryRing).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("ringsprite"));
			Traverse.Create((object)accessoryRing).Field<MeshRenderer[]>("meshRenderers").Value = val42.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)accessoryRing).Field<Collider>("itemCollider").Value = val42.GetComponent<Collider>();
			accessoryRing.Light = val42.GetComponentInChildren<Light>();
			RegisterGameObject(val42, "LycansNewRoles.AccessoryRing");
			GameObject val43 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AccessoryMagnifier"));
			val43.AddComponent<NetworkObject>();
			val43.AddComponent<NetworkTransform>();
			AccessoryMagnifier accessoryMagnifier = val43.AddComponent<AccessoryMagnifier>();
			((Object)val43).name = "LycansNewRoles.AccessoryMagnifier";
			Traverse.Create((object)accessoryMagnifier).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)accessoryMagnifier).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)accessoryMagnifier).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("magnifiersprite"));
			Traverse.Create((object)accessoryMagnifier).Field<MeshRenderer[]>("meshRenderers").Value = val43.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)accessoryMagnifier).Field<Collider>("itemCollider").Value = val43.GetComponent<Collider>();
			accessoryMagnifier.Light = val43.GetComponentInChildren<Light>();
			RegisterGameObject(val43, "LycansNewRoles.AccessoryMagnifier");
			GameObject val44 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AccessoryCrystalBall"));
			val44.AddComponent<NetworkObject>();
			val44.AddComponent<NetworkTransform>();
			AccessoryCrystalBall accessoryCrystalBall = val44.AddComponent<AccessoryCrystalBall>();
			((Object)val44).name = "LycansNewRoles.AccessoryCrystalBall";
			Traverse.Create((object)accessoryCrystalBall).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)accessoryCrystalBall).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)accessoryCrystalBall).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("crystalballsprite"));
			Traverse.Create((object)accessoryCrystalBall).Field<MeshRenderer[]>("meshRenderers").Value = val44.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)accessoryCrystalBall).Field<Collider>("itemCollider").Value = val44.GetComponent<Collider>();
			accessoryCrystalBall.Light = val44.GetComponentInChildren<Light>();
			RegisterGameObject(val44, "LycansNewRoles.AccessoryCrystalBall");
			GameObject val45 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AccessoryBackpack"));
			val45.AddComponent<NetworkObject>();
			val45.AddComponent<NetworkTransform>();
			AccessoryBackpack accessoryBackpack = val45.AddComponent<AccessoryBackpack>();
			((Object)val45).name = "LycansNewRoles.AccessoryBackpack";
			Traverse.Create((object)accessoryBackpack).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)accessoryBackpack).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)accessoryBackpack).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("backpacksprite"));
			Traverse.Create((object)accessoryBackpack).Field<MeshRenderer[]>("meshRenderers").Value = val45.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)accessoryBackpack).Field<Collider>("itemCollider").Value = val45.GetComponent<Collider>();
			accessoryBackpack.Light = val45.GetComponentInChildren<Light>();
			RegisterGameObject(val45, "LycansNewRoles.AccessoryBackpack");
			GameObject val46 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AccessorySpellbook"));
			val46.AddComponent<NetworkObject>();
			val46.AddComponent<NetworkTransform>();
			AccessorySpellbook accessorySpellbook = val46.AddComponent<AccessorySpellbook>();
			((Object)val46).name = "LycansNewRoles.AccessoryBackpack";
			Traverse.Create((object)accessorySpellbook).Field("initialQuantity").SetValue((object)1);
			Traverse.Create((object)accessorySpellbook).Field("destroyWhenEmpty").SetValue((object)true);
			Traverse.Create((object)accessorySpellbook).Field("sprite").SetValue((object)NewRolesCoreBundle.LoadAsset<Sprite>("spellbooksprite"));
			Traverse.Create((object)accessorySpellbook).Field<MeshRenderer[]>("meshRenderers").Value = val46.GetComponentsInChildren<MeshRenderer>().ToArray();
			Traverse.Create((object)accessorySpellbook).Field<Collider>("itemCollider").Value = val46.GetComponent<Collider>();
			accessorySpellbook.Light = val46.GetComponentInChildren<Light>();
			RegisterGameObject(val46, "LycansNewRoles.AccessorySpellbook");
			GameObject val47 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerAstralSpirit"));
			val47.AddComponent<NetworkObject>();
			val47.AddComponent<PlayerAstralSpiritComponent>();
			PlayerAstralSpiritNetworkCharacterController playerAstralSpiritNetworkCharacterController = val47.AddComponent<PlayerAstralSpiritNetworkCharacterController>();
			((NetworkCharacterControllerPrototypeCustom)playerAstralSpiritNetworkCharacterController).acceleration = 500f;
			((NetworkCharacterControllerPrototypeCustom)playerAstralSpiritNetworkCharacterController).braking = 500f;
			val47.AddComponent<PlayerAstralSpiritInputHandler>();
			((Component)val47.transform.Find("Camera")).gameObject.AddComponent<PlayerAstralSpiritCameraHandler>();
			((Object)val47).name = "LycansNewRoles.GameObjectAstralSpirit";
			RegisterGameObject(val47, "LycansNewRoles.GameObjectAstralSpirit");
			GameObject val48 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerSummonedSpirit"));
			val48.AddComponent<NetworkObject>();
			val48.AddComponent<PlayerSummonedSpiritComponent>();
			PlayerSummonedSpiritNetworkCharacterController playerSummonedSpiritNetworkCharacterController = val48.AddComponent<PlayerSummonedSpiritNetworkCharacterController>();
			((NetworkCharacterControllerPrototypeCustom)playerSummonedSpiritNetworkCharacterController).acceleration = 500f;
			((NetworkCharacterControllerPrototypeCustom)playerSummonedSpiritNetworkCharacterController).braking = 500f;
			val48.AddComponent<PlayerSummonedSpiritInputHandler>();
			((Component)val48.transform.Find("Camera")).gameObject.AddComponent<PlayerSummonedSpiritCameraHandler>();
			((Object)val48).name = "LycansNewRoles.GameObjectSummonedSpirit";
			RegisterGameObject(val48, "LycansNewRoles.GameObjectSummonedSpirit");
			GameObject val49 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerSpecter"));
			val49.AddComponent<NetworkObject>();
			val49.AddComponent<PlayerSummonedSpiritComponent>();
			PlayerSummonedSpiritNetworkCharacterController playerSummonedSpiritNetworkCharacterController2 = val49.AddComponent<PlayerSummonedSpiritNetworkCharacterController>();
			((NetworkCharacterControllerPrototypeCustom)playerSummonedSpiritNetworkCharacterController2).acceleration = 500f;
			((NetworkCharacterControllerPrototypeCustom)playerSummonedSpiritNetworkCharacterController2).braking = 500f;
			val49.AddComponent<PlayerSummonedSpiritInputHandler>();
			((Component)val49.transform.Find("Camera")).gameObject.AddComponent<PlayerSummonedSpiritCameraHandler>();
			((Object)val49).name = "LycansNewRoles.GameObjectSpecter";
			RegisterGameObject(val49, "LycansNewRoles.GameObjectSpecter");
			GameObject val50 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerCultistSpirit"));
			val50.AddComponent<NetworkObject>();
			val50.AddComponent<PlayerSummonedSpiritComponent>();
			PlayerSummonedSpiritNetworkCharacterController playerSummonedSpiritNetworkCharacterController3 = val50.AddComponent<PlayerSummonedSpiritNetworkCharacterController>();
			((NetworkCharacterControllerPrototypeCustom)playerSummonedSpiritNetworkCharacterController3).acceleration = 500f;
			((NetworkCharacterControllerPrototypeCustom)playerSummonedSpiritNetworkCharacterController3).braking = 500f;
			val50.AddComponent<PlayerSummonedSpiritInputHandler>();
			((Component)val50.transform.Find("Camera")).gameObject.AddComponent<PlayerSummonedSpiritCameraHandler>();
			((Object)val50).name = "LycansNewRoles.GameObjectCultistSpirit";
			RegisterGameObject(val50, "LycansNewRoles.GameObjectCultistSpirit");
			GameObject val51 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PurifierActive"));
			val51.AddComponent<NetworkObject>();
			val51.AddComponent<NetworkTransform>();
			val51.AddComponent<PurifierActive>();
			((Object)val51).name = "LycansNewRoles.ItemPurifierActive";
			RegisterGameObject(val51, "LycansNewRoles.ItemPurifierActive");
			GameObject val52 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PurifierFire"));
			val52.AddComponent<NetworkObject>();
			val52.AddComponent<NetworkTransform>();
			val52.AddComponent<PurifierFire>();
			((Object)val52).name = "LycansNewRoles.ItemPurifierFire";
			RegisterGameObject(val52, "LycansNewRoles.ItemPurifierFire");
			AddGameObjectToList("LycansNewRoles.GameObjectDeceiverIllusion", typeof(DeceiverIllusionComponent));
			PlayerCustom.PlayerCustomColliderPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerCustomCollider"));
			PlayerCustom.PlayerCustomColliderPrefab.SetActive(false);
			FootstepComponent.FootstepVillagerPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("FootstepBlue"));
			FootstepComponent.FootstepVillagerPrefab.SetActive(false);
			FootstepComponent.FootstepWolfPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("FootstepRed"));
			FootstepComponent.FootstepWolfPrefab.SetActive(false);
			PlayerCustom.StinkingParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("StinkingParticleSystem"));
			PlayerCustom.StinkingParticleSystemPrefab.SetActive(false);
			PlayerDyingComponent.DyingPlayerInfoPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("DyingPlayerInfo"));
			PlayerDyingComponent.DyingPlayerInfoPrefab.SetActive(false);
			PlayerBombIconComponent.BombIconPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("BombIcon"));
			PlayerBombIconComponent.BombIconPrefab.SetActive(false);
			PlayerHeldItemComponent.HeldItemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("HeldItem"));
			PlayerHeldItemComponent.HeldItemPrefab.SetActive(false);
			PlayerCustom.BombExplosionParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("BombExplosionParticleSystem"));
			PlayerCustom.BombExplosionParticleSystemPrefab.SetActive(false);
			UIGenericChoicePanel.GenericChoiceButtonPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("GenericChoiceButton"));
			UIGenericChoicePanel.GenericChoiceButtonPrefab.SetActive(false);
			PlayerCustom.DownedParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("DownedParticleSystem"));
			PlayerCustom.DownedParticleSystemPrefab.SetActive(false);
			PlayerAngelIconComponent.AngelIconPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AngelShieldIcon"));
			PlayerAngelIconComponent.AngelIconPrefab.SetActive(false);
			UIDraftPanel.DraftChoiceRolePrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("DraftChoiceRole"));
			UIDraftPanel.DraftChoiceRolePrefab.AddComponent<DraftChoiceRoleComponent>();
			UIDraftPanel.DraftChoiceRolePrefab.SetActive(false);
			UIModInstallationPanel.PlayerPanelPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerStatePlayer"));
			UIModInstallationPanel.PlayerPanelPrefab.AddComponent<UIModInstallationPlayer>();
			UIModInstallationPanel.PlayerPanelPrefab.SetActive(false);
			UILastGameSummaryPanel.PlayerKillPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("LastGameKill"));
			UILastGameSummaryPanel.PlayerKillPrefab.AddComponent<UILastGameSummaryKill>();
			UILastGameSummaryPanel.PlayerKillPrefab.SetActive(false);
			PlayerMercenaryTargetIconComponent.TargetIconPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("BountyHunterTargetIcon"));
			PlayerMercenaryTargetIconComponent.TargetIconPrefab.SetActive(false);
			GrenadeActive.GrenadeExplosionParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("GrenadeExplosionParticleSystem"));
			GrenadeActive.GrenadeExplosionParticleSystemPrefab.SetActive(false);
			MolotovActive.MolotovExplosionParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MolotovFire"));
			MolotovActive.MolotovExplosionParticleSystemPrefab.SetActive(false);
			PlayerCustom.ChaosParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("ChaosParticleSystem"));
			PlayerCustom.ChaosParticleSystemPrefab.SetActive(false);
			PlayerResurrectedComponent.ResurrectedEffectPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerZombie"));
			PlayerResurrectedComponent.ResurrectedEffectPrefab.SetActive(false);
			PlayerCustom.ExorcisedParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("ExorcisedParticleSystem"));
			PlayerCustom.ExorcisedParticleSystemPrefab.SetActive(false);
			PlayerCustom.PriestShieldParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PriestShieldParticleSystem"));
			PlayerCustom.PriestShieldParticleSystemPrefab.SetActive(false);
			ExorcistDetector.ActivationParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("ExorcistActivation"));
			ExorcistDetector.ActivationParticleSystemPrefab.SetActive(false);
			PlayerCustom.TeleportParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("TeleportParticleSystem"));
			PlayerCustom.TeleportParticleSystemPrefab.SetActive(false);
			PlayerSummonedSpiritComponent.SpiritCreationPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SpiritCreation"));
			PlayerSummonedSpiritComponent.SpiritCreationPrefab.SetActive(false);
			PlayerTargetArrowComponent.PlayerTargetArrowPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerTargetArrow"));
			PlayerTargetArrowComponent.PlayerTargetArrowPrefab.SetActive(false);
			PlayerCustom.MegaFartParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MegaFartEffect"));
			PlayerCustom.MegaFartParticleSystemPrefab.SetActive(false);
			UIDetectivePanel.DetectiveNotePrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("DetectiveNotesSingle"));
			UIDetectivePanel.DetectiveNotePrefab.SetActive(false);
			PlayerCustom.TruesightParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("TruesightParticleSystem"));
			Transform child = PlayerCustom.TruesightParticleSystemPrefab.transform.GetChild(0);
			child.localScale = new Vector3(0.00075f, 0.00075f, 0.00075f);
			child.localPosition = new Vector3(-0.00045f, 0.0009f, 0.00175f);
			Transform child2 = PlayerCustom.TruesightParticleSystemPrefab.transform.GetChild(1);
			child2.localScale = new Vector3(0.00075f, 0.00075f, 0.00075f);
			child2.localPosition = new Vector3(0.00045f, 0.0009f, 0.00175f);
			PlayerCustom.TruesightParticleSystemPrefab.SetActive(false);
			PlayerCustom.KilledByCrystalBallGuessParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SeerCorrectGuess"));
			PlayerCustom.KilledByCrystalBallGuessParticleSystemPrefab.SetActive(false);
			PlayerCustom.MidasParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MidasParticleSystem"));
			PlayerCustom.MidasParticleSystemPrefab.SetActive(false);
			PlayerCustom.VampireParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("VampireParticleSystem"));
			PlayerCustom.VampireParticleSystemPrefab.SetActive(false);
			PlayerCustom.SpeedParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SpeedParticleSystem"));
			PlayerCustom.SpeedParticleSystemPrefab.SetActive(false);
			PlayerCustom.HauntedParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("HauntedParticleSystem"));
			PlayerCustom.HauntedParticleSystemPrefab.SetActive(false);
			PlayerCustom.AsleepParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AsleepParticleSystem"));
			PlayerCustom.AsleepParticleSystemPrefab.SetActive(false);
			PlayerCustom.TrappedItemExplosionParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("TrappedItemExplosionParticleSystem"));
			PlayerCustom.TrappedItemExplosionParticleSystemPrefab.SetActive(false);
			PlayerCustom.MayorParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MayorParticleSystem"));
			PlayerCustom.MayorParticleSystemPrefab.SetActive(false);
			UIMayorPanelForMayor.MayorActionPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MayorPanelForMayorAction"));
			UIMayorPanelForMayor.MayorActionPrefab.AddComponent<UIMayorAction>();
			UIMayorPanelForMayor.MayorActionPrefab.SetActive(false);
			PlayerCustom.AlchemistTransformParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AlchemistTransformParticleSystem"));
			PlayerCustom.AlchemistTransformParticleSystemPrefab.SetActive(false);
			DiscipleAnchor.ActivationParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("DiscipleAnchorActivation"));
			DiscipleAnchor.ActivationParticleSystemPrefab.SetActive(false);
			PlayerCustom.MolotovBurnParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MolotovBurnParticleSystem"));
			PlayerCustom.MolotovBurnParticleSystemPrefab.SetActive(false);
			PlayerCustom.PurifierBurnParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PurifierBurnParticleSystem"));
			PlayerCustom.PurifierBurnParticleSystemPrefab.SetActive(false);
			PlayerCustom.BanishedParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("BanishedParticleSystem"));
			PlayerCustom.BanishedParticleSystemPrefab.SetActive(false);
			PlayerSpotterLightComponent.SpotterLightEffectPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerSpotterLights"));
			PlayerSpotterLightComponent.SpotterLightEffectPrefab.SetActive(false);
			PlayerCustom.KidnapperKidnapParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("KidnapperKidnapEffect"));
			PlayerCustom.KidnapperKidnapParticleSystemPrefab.SetActive(false);
			PlayerHeartSeethroughComponent.SeethroughPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("PlayerSeethrough"));
			PlayerHeartSeethroughComponent.SeethroughPrefab.SetActive(false);
			HostParasite.ParasiteExplosionParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("ParasiteExplosion"));
			HostParasite.ParasiteExplosionParticleSystemPrefab.SetActive(false);
			RunemasterRune.ActivationParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("RunemasterRuneExplosion"));
			RunemasterRune.ActivationParticleSystemPrefab.SetActive(false);
			PlayerCustom.ConfusedParticleSystemPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("ConfusedParticleSystem"));
			PlayerCustom.ConfusedParticleSystemPrefab.SetActive(false);
			UISpectatorChoicePanel.SpectatorChoiceButtonPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SpectatorChoicePanelPlayer"));
			UISpectatorChoicePanel.SpectatorChoiceButtonPrefab.SetActive(false);
			PlayerAskForSpeechIconComponent.AskForSpeechIconPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AskForSpeechIcon"));
			PlayerAskForSpeechIconComponent.AskForSpeechIconPrefab.SetActive(false);
			MinimapPlayerComponent.MinimapPlayerPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapPlayer"));
			MinimapPlayerComponent.MinimapPlayerPrefab.AddComponent<MinimapPlayerComponent>();
			MinimapPlayerComponent.MinimapPlayerPrefab.SetActive(false);
			MinimapSabotageComponent.MinimapSabotagePrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapSabotage"));
			MinimapSabotageComponent.MinimapSabotagePrefab.AddComponent<MinimapSabotageComponent>();
			MinimapSabotageComponent.MinimapSabotagePrefab.SetActive(false);
			MinimapInvestigatorTargetComponent.MinimapInvestigatorTargetPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapInvestigatorHint"));
			MinimapInvestigatorTargetComponent.MinimapInvestigatorTargetPrefab.AddComponent<MinimapInvestigatorTargetComponent>();
			MinimapInvestigatorTargetComponent.MinimapInvestigatorTargetPrefab.SetActive(false);
			MinimapExorcistDetectorComponent.MinimapExorcistDetectorPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapExorcistDetector"));
			MinimapExorcistDetectorComponent.MinimapExorcistDetectorPrefab.AddComponent<MinimapExorcistDetectorComponent>();
			MinimapExorcistDetectorComponent.MinimapExorcistDetectorPrefab.SetActive(false);
			MinimapTeleporterBeaconComponent.MinimapTeleporterBeaconPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapTeleporterBeacon"));
			MinimapTeleporterBeaconComponent.MinimapTeleporterBeaconPrefab.AddComponent<MinimapTeleporterBeaconComponent>();
			MinimapTeleporterBeaconComponent.MinimapTeleporterBeaconPrefab.SetActive(false);
			MinimapMerchantCoinComponent.MinimapMerchantCoinPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapMerchantCoin"));
			MinimapMerchantCoinComponent.MinimapMerchantCoinPrefab.AddComponent<MinimapMerchantCoinComponent>();
			MinimapMerchantCoinComponent.MinimapMerchantCoinPrefab.SetActive(false);
			MinimapDetectivePositionComponent.MinimapDetectivePositionPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapTarget"));
			MinimapDetectivePositionComponent.MinimapDetectivePositionPrefab.AddComponent<MinimapDetectivePositionComponent>();
			MinimapDetectivePositionComponent.MinimapDetectivePositionPrefab.SetActive(false);
			MinimapDeathPositionComponent.MinimapDeathPositionPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapDeathPosition"));
			MinimapDeathPositionComponent.MinimapDeathPositionPrefab.AddComponent<MinimapDeathPositionComponent>();
			MinimapDeathPositionComponent.MinimapDeathPositionPrefab.SetActive(false);
			MinimapSleepingGasComponent.MinimapSleepingGasPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapSleepingGas"));
			MinimapSleepingGasComponent.MinimapSleepingGasPrefab.AddComponent<MinimapSleepingGasComponent>();
			MinimapSleepingGasComponent.MinimapSleepingGasPrefab.SetActive(false);
			MinimapScoutRadarComponent.MinimapScoutRadarPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapScoutRadar"));
			MinimapScoutRadarComponent.MinimapScoutRadarPrefab.AddComponent<MinimapScoutRadarComponent>();
			MinimapScoutRadarComponent.MinimapScoutRadarPrefab.SetActive(false);
			MinimapHermitHideoutComponent.MinimapHermitHideoutPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapHermitHideout"));
			MinimapHermitHideoutComponent.MinimapHermitHideoutPrefab.AddComponent<MinimapHermitHideoutComponent>();
			MinimapHermitHideoutComponent.MinimapHermitHideoutPrefab.SetActive(false);
			MinimapHostParasiteComponent.MinimapHostParasitePrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapHostParasite"));
			MinimapHostParasiteComponent.MinimapHostParasitePrefab.AddComponent<MinimapHostParasiteComponent>();
			MinimapHostParasiteComponent.MinimapHostParasitePrefab.SetActive(false);
			MinimapRunemasterRuneComponent.MinimapRunemasterRunePrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapRunemasterRune"));
			MinimapRunemasterRuneComponent.MinimapRunemasterRunePrefab.AddComponent<MinimapRunemasterRuneComponent>();
			MinimapRunemasterRuneComponent.MinimapRunemasterRunePrefab.SetActive(false);
			MinimapAcrobatSpotComponent.MinimapAcrobatSpotPrefab = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapAcrobatSpot"));
			MinimapAcrobatSpotComponent.MinimapAcrobatSpotPrefab.AddComponent<MinimapAcrobatSpotComponent>();
			MinimapAcrobatSpotComponent.MinimapAcrobatSpotPrefab.SetActive(false);
			GameObject val53 = Object.Instantiate<GameObject>(NewMapsCoreBundle.LoadAsset<GameObject>("MinimapParent"), ((Component)Traverse.Create((object)GameManager.Instance).Field<GameUI>("gameUI").Value).transform.Find("Canvas"));
			Minimap = val53.AddComponent<MinimapComponent>();
			Minimap.SetState(MinimapComponent.MinimapState.Inactive);
			GameObject val54 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("ExorcistDetector"));
			val54.AddComponent<NetworkObject>();
			val54.AddComponent<NetworkTransform>();
			val54.AddComponent<ExorcistDetector>();
			((Object)val54).name = "LycansNewRoles.GameObjectExorcistDetector";
			RegisterGameObject(val54, "LycansNewRoles.GameObjectExorcistDetector");
			GameObject val55 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SpiritSpell"));
			val55.AddComponent<NetworkObject>();
			val55.AddComponent<NetworkTransform>();
			val55.AddComponent<SpiritSpell>();
			val55.AddComponent<SelfDestroyingObjectComponent>();
			((Object)val55).name = "LycansNewRoles.GameObjectSpiritSpell";
			RegisterGameObject(val55, "LycansNewRoles.GameObjectSpiritSpell");
			GameObject val56 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MerchantCoin"));
			val56.AddComponent<NetworkObject>();
			val56.AddComponent<NetworkTransform>();
			val56.AddComponent<MerchantCoin>();
			((Object)val56).name = "LycansNewRoles.GameObjectMerchantCoin";
			RegisterGameObject(val56, "LycansNewRoles.GameObjectMerchantCoin");
			GameObject val57 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("ScoutRadar"));
			val57.AddComponent<NetworkObject>();
			val57.AddComponent<NetworkTransform>();
			val57.AddComponent<ScoutRadar>();
			((Object)val57).name = "LycansNewRoles.GameObjectScoutRadar";
			RegisterGameObject(val57, "LycansNewRoles.GameObjectScoutRadar");
			GameObject val58 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("InvestigatorHint"));
			val58.AddComponent<NetworkObject>();
			val58.AddComponent<NetworkTransform>();
			val58.AddComponent<InvestigatorHint>();
			((Object)val58).name = "LycansNewRoles.GameObjectInvestigatorHint";
			RegisterGameObject(val58, "LycansNewRoles.GameObjectInvestigatorHint");
			GameObject val59 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("SurvivalistHint"));
			val59.AddComponent<NetworkObject>();
			val59.AddComponent<NetworkTransform>();
			val59.AddComponent<SurvivalistHint>();
			((Object)val59).name = "LycansNewRoles.GameObjectSurvivalistHint";
			RegisterGameObject(val59, "LycansNewRoles.GameObjectSurvivalistHint");
			GameObject val60 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("DiscipleAnchor"));
			val60.AddComponent<NetworkObject>();
			val60.AddComponent<NetworkTransform>();
			val60.AddComponent<DiscipleAnchor>();
			((Object)val60).name = "LycansNewRoles.GameObjectDiscipleAnchor";
			RegisterGameObject(val60, "LycansNewRoles.GameObjectDiscipleAnchor");
			GameObject val61 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("MagicianBeacon"));
			val61.AddComponent<NetworkObject>();
			val61.AddComponent<NetworkTransform>();
			val61.AddComponent<MagicianBeacon>();
			((Object)val61).name = "LycansNewRoles.GameObjectMagicianBeaconName";
			RegisterGameObject(val61, "LycansNewRoles.GameObjectMagicianBeaconName");
			GameObject val62 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("CultistSkull"));
			val62.AddComponent<NetworkObject>();
			val62.AddComponent<NetworkTransform>();
			val62.AddComponent<CultistSkull>();
			((Object)val62).name = "LycansNewRoles.GameObjectCultistSkull";
			RegisterGameObject(val62, "LycansNewRoles.GameObjectCultistSkull");
			GameObject val63 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("CultistSkullSpirit"));
			val63.AddComponent<NetworkObject>();
			val63.AddComponent<NetworkTransform>();
			val63.AddComponent<CultistSkullSpirit>();
			((Object)val63).name = "LycansNewRoles.GameObjectCultistSkullSpirit";
			RegisterGameObject(val63, "LycansNewRoles.GameObjectCultistSkullSpirit");
			GameObject val64 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("HermitHideout"));
			val64.AddComponent<NetworkObject>();
			val64.AddComponent<NetworkTransform>();
			val64.AddComponent<HermitHideout>();
			val64.layer = LayerMask.NameToLayer("NoInteract");
			((Object)val64).name = "LycansNewRoles.GameObjectHermitHideout";
			RegisterGameObject(val64, "LycansNewRoles.GameObjectHermitHideout");
			GameObject val65 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("HostParasite"));
			val65.AddComponent<NetworkObject>();
			val65.AddComponent<NetworkTransform>();
			val65.AddComponent<HostParasite>();
			((Object)val65).name = "LycansNewRoles.GameObjectHostParasite";
			RegisterGameObject(val65, "LycansNewRoles.GameObjectHostParasite");
			GameObject val66 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("RunemasterRune"));
			val66.AddComponent<NetworkObject>();
			val66.AddComponent<NetworkTransform>();
			val66.AddComponent<RunemasterRune>();
			((Object)val66).name = "LycansNewRoles.GameObjectRunemasterRune";
			RegisterGameObject(val66, "LycansNewRoles.GameObjectRunemasterRune");
			GameObject val67 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("AcrobatSpot"));
			val67.AddComponent<NetworkObject>();
			val67.AddComponent<NetworkTransform>();
			val67.AddComponent<AcrobatSpot>();
			((Object)val67).name = "LycansNewRoles.GameObjectAcrobatSpot";
			RegisterGameObject(val67, "LycansNewRoles.GameObjectAcrobatSpot");
			GameObject val68 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("InventorDeviceSmoke"));
			val68.AddComponent<NetworkObject>();
			val68.AddComponent<NetworkTransform>();
			val68.AddComponent<InventorSmoke>();
			((Object)val68).name = "LycansNewRoles.GameObjectInventorSmoke";
			RegisterGameObject(val68, "LycansNewRoles.GameObjectInventorSmoke");
			InventorSmoke.InventorSmokeForOthersPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("InventorDeviceSmoke"));
			InventorSmoke.InventorSmokeForOthersPrefab.SetActive(false);
			InventorSmoke.InventorSmokeForInventorPrefab = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("InventorDeviceSmokeForInventor"));
			InventorSmoke.InventorSmokeForInventorPrefab.SetActive(false);
			GameObject val69 = Object.Instantiate<GameObject>(NewRolesCoreBundle.LoadAsset<GameObject>("InventorScrap"));
			val69.AddComponent<NetworkObject>();
			val69.AddComponent<NetworkTransform>();
			val69.AddComponent<InventorScrap>();
			((Object)val69).name = "LycansNewRoles.GameObjectInventorScrap";
			RegisterGameObject(val69, "LycansNewRoles.GameObjectInventorScrap");
			PlayerNewAnimationsComponent.CustomAnimatorController = NewRolesCoreBundle.LoadAsset<RuntimeAnimatorController>("PlayerCustomAnimatorController");
			PlayerCustom.SeeThroughShaderHuman = NewRolesCoreBundle.LoadAsset<Shader>("SeeThroughShaderBlue");
			PlayerCustom.SeeThroughShaderWolf = NewRolesCoreBundle.LoadAsset<Shader>("SeeThroughShaderRed");
			PlayerCustom.CamouflageLevel1Shader = NewRolesCoreBundle.LoadAsset<Shader>("CamouflageLevel1Shader");
			PlayerCustom.CamouflageLevel2Shader = NewRolesCoreBundle.LoadAsset<Shader>("CamouflageLevel2Shader");
			PlayerCustom.CamouflageLevel3Shader = NewRolesCoreBundle.LoadAsset<Shader>("CamouflageLevel3Shader");
			HostParasite.CamouflageShader = NewRolesCoreBundle.LoadAsset<Shader>("HostParasiteCamouflageShader");
			AddSoundIfNeeded("SurvivalistDying");
			AddSoundIfNeeded("BombTicking");
			AddSoundIfNeeded("BombExplosion");
			AddSoundIfNeeded("PowerAvailable");
			AddSoundIfNeeded("UseTrappedItem");
			AddSoundIfNeeded("AngelHeal");
			AddSoundIfNeeded("BeastAwakening");
			AddSoundIfNeeded("BeastHeartBeatFast");
			AddSoundIfNeeded("BeastHeartBeatMid");
			AddSoundIfNeeded("BeastHeartBeatSlow");
			AddSoundIfNeeded("DiamondEffect");
			AddSoundIfNeeded("TrapDisarm");
			AddSoundIfNeeded("BountyHunterShot");
			AddSoundIfNeeded("GrenadeEffect");
			AddSoundIfNeeded("GrenadeThrow");
			AddSoundIfNeeded("ChaosEffect");
			AddSoundIfNeeded("Zombie1");
			AddSoundIfNeeded("Zombie2");
			AddSoundIfNeeded("Zombie3");
			AddSoundIfNeeded("ZombieAttack");
			AddSoundIfNeeded("VoodooRez");
			AddSoundIfNeeded("PredatorKill");
			AddSoundIfNeeded("Exorcism");
			AddSoundIfNeeded("SpiritSummoned");
			AddSoundIfNeeded("MegaFart");
			AddSoundIfNeeded("PossessorDance");
			AddSoundIfNeeded("SeerCorrectGuessSound");
			AddSoundIfNeeded("SleepingGasBreak");
			AddSoundIfNeeded("ScoutAlert");
			AddSoundIfNeeded("Isolation");
			AddSoundIfNeeded("Mayor");
			AddSoundIfNeeded("AlchemistT");
			AddSoundIfNeeded("InquisitorFire");
			AddSoundIfNeeded("Banish");
			AddSoundIfNeeded("Radar");
			AddSoundIfNeeded("CultistSkull");
			AddSoundIfNeeded("CultistCapture");
			AddSoundIfNeeded("Parasite");
			AddSoundIfNeeded("RuneTrigger");
			AddSoundIfNeeded("RuneExplosion");
			AddSoundIfNeeded("InventorDeviceActivation");
			AddSoundIfNeeded("InventorScrapAlert");
			AddSoundIfNeeded("AcrobatJump");
			AddSoundIfNeeded("Burp");
			AddSoundIfNeeded("TinyCrush");
			foreach (GameObject newHat in NewHats)
			{
				Object.Destroy((Object)(object)newHat);
			}
			NewHats.Clear();
			foreach (AssetBundle newHatsBundle in NewHatsBundles)
			{
				string[] allAssetNames = newHatsBundle.GetAllAssetNames();
				string[] array = allAssetNames;
				foreach (string text in array)
				{
					Logger.LogInfo((object)("Load hat: " + text));
					GameObject val70 = newHatsBundle.LoadAsset<GameObject>(text);
					Logger.LogInfo((object)("Loaded hat: " + (object)val70));
					GameObject val71 = Object.Instantiate<GameObject>(val70);
					val71.SetActive(false);
					NewHats.Add(val71);
					Logger.LogInfo((object)("Added hat: " + (object)val71));
				}
			}
			PetNames.Clear();
			foreach (AssetBundle petBundle in PetBundles)
			{
				string[] allAssetNames2 = petBundle.GetAllAssetNames();
				string[] array2 = allAssetNames2;
				foreach (string text2 in array2)
				{
					Logger.LogInfo((object)("Load pet: " + text2));
					GameObject val72 = petBundle.LoadAsset<GameObject>(text2);
					Logger.LogInfo((object)("Loaded pet: " + (object)val72));
					GameObject val73 = Object.Instantiate<GameObject>(val72);
					val73.AddComponent<NetworkObject>();
					val73.AddComponent<PlayerPetComponent>();
					NetworkCharacterControllerPrototype val74 = val73.AddComponent<NetworkCharacterControllerPrototype>();
					val74.Controller.center = new Vector3(0f, 0.8f, 0f);
					val74.Controller.height = 1.5f;
					val74.Controller.radius = 0.25f;
					val74.Controller.stepOffset = 0.07f * val73.transform.localScale.x;
					val74.Controller.slopeLimit = 45f;
					val74.Controller.skinWidth = 0.025f;
					val74.maxSpeed = 5f;
					val74.acceleration = 50f;
					val74.braking = 50f;
					string text3 = "Pet" + PetNames.Count + 1;
					PetNames.Add(text3);
					((Object)val73).name = text3;
					val73.SetActive(false);
					RegisterGameObject(val73, text3);
					Logger.LogInfo((object)("Added pet: " + ((object)val73)?.ToString() + " with name " + text3));
				}
			}
			foreach (CharacterSkin skin in Skins)
			{
				Object.Destroy((Object)(object)skin.SkinMeshRenderer);
				Object.Destroy((Object)(object)skin.Metarig);
			}
			Skins.Clear();
			Skins.Add(new CharacterSkin());
			if ((Object)(object)PlayerController.Local != (Object)null)
			{
				AddOriginalSkin(PlayerController.Local);
			}
			foreach (AssetBundle skinBundle in SkinBundles)
			{
				string[] allAssetNames3 = skinBundle.GetAllAssetNames();
				foreach (string item in allAssetNames3.Where((string o) => o.EndsWith(".prefab")))
				{
					Logger.LogInfo((object)("Load skin: " + item));
					GameObject val75 = skinBundle.LoadAsset<GameObject>(item);
					Logger.LogInfo((object)("Loaded skin: " + (object)val75));
					CharacterSkin characterSkin = new CharacterSkin();
					characterSkin.SkinMeshRenderer = Object.Instantiate<GameObject>(((Component)val75.transform.Find("SkinnedMeshRenderer")).gameObject);
					characterSkin.DefaultShader = ((Renderer)characterSkin.SkinMeshRenderer.GetComponent<SkinnedMeshRenderer>()).material.shader;
					characterSkin.Metarig = Object.Instantiate<GameObject>(((Component)val75.transform.Find("metarig")).gameObject);
					characterSkin.Avatar = val75.GetComponent<Animator>().avatar;
					characterSkin.IsOriginalSkin = false;
					characterSkin.SkinMeshRenderer.SetActive(true);
					characterSkin.Metarig.SetActive(true);
					string assetTexturePrefix = item.Replace(".prefab", "");
					foreach (string item2 in allAssetNames3.Where((string o) => o.StartsWith(assetTexturePrefix + "_skin_")))
					{
						characterSkin.SkinTextures.Add(skinBundle.LoadAsset<Texture>(item2));
						Logger.LogInfo((object)("Added skin texture: " + item2));
					}
					characterSkin.SkinPetrified = skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_petrified.png");
					characterSkin.SkinZombified = skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_zombie.png");
					characterSkin.SkinPoisoned = skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_poison.png");
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_blue.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_green.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_yellow.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_red.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_cyan.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_pink.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_orange.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_gray.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_purple.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_brown.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_lemon.png"));
					characterSkin.TopTextures.Add(skinBundle.LoadAsset<Texture>(assetTexturePrefix + "_darkblue.png"));
					Skins.Add(characterSkin);
					Logger.LogInfo((object)("Added skin: " + ((object)characterSkin.SkinMeshRenderer)?.ToString() + " as index " + Skins.IndexOf(characterSkin)));
				}
			}
			if (!MapManager.NewMapsByIdInfo.ContainsKey(1))
			{
				GameObject val76 = NewMapsCoreBundle.LoadAsset<GameObject>("minimap1");
				val76.SetActive(true);
				MapManager.NewMapsByIdInfo[1] = new CustomMap(1, "map_1", val76.transform.Find("OffsetMultiplier"), val76.transform.Find("CameraOffset"), val76.transform.Find("Rotation"), val76.transform.localScale);
			}
			if (!MapManager.NewMapsByIdInfo.ContainsKey(2))
			{
				GameObject val77 = NewMapsCoreBundle.LoadAsset<GameObject>("minimap2");
				val77.SetActive(true);
				MapManager.NewMapsByIdInfo[2] = new CustomMap(2, "map_2", val77.transform.Find("OffsetMultiplier"), val77.transform.Find("CameraOffset"), val77.transform.Find("Rotation"), val77.transform.localScale);
			}
			List<Transform> list2 = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value.ToList();
			foreach (int key in NewMapPathById.Keys)
			{
				list2.Add(list2[0]);
			}
			Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value = list2.ToArray();
			Physics.IgnoreLayerCollision(25, 3);
			Physics.IgnoreLayerCollision(26, 7);
			Physics.IgnoreLayerCollision(27, 3);
			Physics.IgnoreLayerCollision(27, 25);
		}
		catch (Exception ex)
		{
			Logger.LogError((object)("Error starting plugin: " + ex));
		}
	}

	public static void AddOriginalSkin(PlayerController playerController)
	{
		if (!Skins.Any((CharacterSkin o) => o.IsOriginalSkin))
		{
			if (!Skins.Any())
			{
				Skins.Add(new CharacterSkin());
			}
			CharacterSkin characterSkin = new CharacterSkin();
			characterSkin.SkinMeshRenderer = Object.Instantiate<GameObject>(((Component)Traverse.Create((object)playerController).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value).gameObject);
			characterSkin.DefaultShader = ((Renderer)characterSkin.SkinMeshRenderer.GetComponent<SkinnedMeshRenderer>()).material.shader;
			characterSkin.Metarig = Object.Instantiate<GameObject>(((Component)playerController.FindVillagerMetarig()).gameObject);
			characterSkin.Avatar = ((Component)((Component)playerController.FindVillagerMetarig()).transform.parent).GetComponent<Animator>().avatar;
			characterSkin.IsOriginalSkin = true;
			Skins[0] = characterSkin;
		}
	}

	public static void CreatePlayerIllusionIfNeeded()
	{
		if (PlayerIllusionCreated)
		{
			return;
		}
		GameObject val = Object.Instantiate<GameObject>(((Component)PlayerController.Local).gameObject);
		SkinnedMeshRenderer value = Traverse.Create((object)val.GetComponent<PlayerController>()).Field<SkinnedMeshRenderer>("villagerMeshRenderer").Value;
		Object.DestroyImmediate((Object)(object)((Component)val.GetComponent<PlayerController>().FindVillagerHandLeft().Find("MidasParticleSystem(Clone)(Clone)")).gameObject);
		Object.DestroyImmediate((Object)(object)((Component)val.GetComponent<PlayerController>().FindVillagerHandRight().Find("MidasParticleSystem(Clone)(Clone)")).gameObject);
		Object.DestroyImmediate((Object)(object)((Component)val.GetComponent<PlayerController>().hatsContainer.transform.parent.Find("TruesightParticleSystem(Clone)(Clone)")).gameObject);
		DestroyComponentIfExists<PlayerController>(val);
		DestroyComponentIfExists<PlayerInteract>(val);
		DestroyComponentIfExists<AudioListener>(val);
		DestroyComponentIfExists<VoiceNetworkObject>(val);
		DestroyComponentIfExists<CharacterInputHandler>(val);
		DestroyComponentIfExists<CharacterMovementHandler>(val);
		DestroyComponentIfExists<NetworkCharacterControllerPrototypeCustom>(val);
		DestroyComponentIfExists<PlayerEffectsManager>(val);
		DestroyComponentIfExists<PlayerGroundDetection>(val);
		DestroyComponentIfExists<PlayerFootstepsComponent>(val);
		DestroyComponentIfExists<PlayerPhasingComponent>(val);
		DestroyComponentIfExists<PlayerBombTickingComponent>(val);
		DestroyComponentIfExists<PlayerBombIconComponent>(val);
		DestroyComponentIfExists<PlayerSurvivalistHeartbeatComponent>(val);
		DestroyComponentIfExists<PlayerMercenaryTargetIconComponent>(val);
		DestroyComponentIfExists<PlayerHeldItemComponent>(val);
		DestroyComponentIfExists<PlayerPredatorComponent>(val);
		DestroyComponentIfExists<PlayerAngelIconComponent>(val);
		DestroyComponentIfExists<PlayerResurrectedComponent>(val);
		DestroyComponentIfExists<PlayerSpotterLightComponent>(val);
		DestroyComponentIfExists<PlayerDyingComponent>(val);
		DestroyComponentIfExists<PlayerTargetArrowComponent>(val);
		DestroyComponentIfExists<KnockbackComponent>(val);
		DestroyComponentIfExists<ForcedRotationComponent>(val);
		DestroyComponentIfExists<GravityComponent>(val);
		DestroyComponentIfExists<PlayerNewAnimationsComponent>(val);
		DestroyComponentIfExists<PlayerGlowingChangesComponent>(val);
		DestroyComponentIfExists<PlayerHeartSeethroughComponent>(val);
		DestroyComponentIfExists<PlayerLocalEachSecondTimerComponent>(val);
		DestroyComponentIfExists<PlayerAskForSpeechIconComponent>(val);
		PlayerIllusionNetworkCharacterController playerIllusionNetworkCharacterController = val.AddComponent<PlayerIllusionNetworkCharacterController>();
		((NetworkCharacterControllerPrototypeCustom)playerIllusionNetworkCharacterController).acceleration = 500f;
		((NetworkCharacterControllerPrototypeCustom)playerIllusionNetworkCharacterController).braking = 500f;
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < val.transform.childCount; i++)
		{
			Transform child = val.transform.GetChild(i);
			if (((Object)((Component)child).gameObject).name == "Body")
			{
				((Component)child.Find("Villager")).gameObject.SetActive(true);
			}
			else
			{
				list.Add(((Component)child).gameObject);
			}
		}
		ParticleSystem[] componentsInChildren = val.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem val2 in componentsInChildren)
		{
			val2.Stop();
		}
		((Component)value).gameObject.SetActive(true);
		((Renderer)value).enabled = true;
		for (int num = list.Count - 1; num >= 0; num--)
		{
			GameObject val3 = list[num];
			Object.DestroyImmediate((Object)(object)val3);
		}
		val.AddComponent<MagicianIllusion>();
		RegisterGameObject(val, "LycansNewRoles.GameObjectMagicianIllusionName");
		GameObject val4 = Object.Instantiate<GameObject>(val);
		DestroyComponentIfExists<MagicianIllusion>(val4);
		((Component)val4.transform.Find("Body").Find("Villager")).gameObject.SetActive(false);
		((Component)val4.transform.Find("Body").Find("Werewolf")).gameObject.SetActive(true);
		((Component)val4.transform.Find("Body").Find("Werewolf").Find("WerewolfModel")).gameObject.SetActive(true);
		WolfIllusion wolfIllusion = val4.AddComponent<WolfIllusion>();
		RegisterGameObject(val4, "LycansNewRoles.GameObjectWolfIllusion");
		GameObject val5 = Object.Instantiate<GameObject>(((Component)PlayerController.Local).gameObject);
		MolotovEntity molotovEntity = val5.AddComponent<MolotovEntity>();
		Object.DestroyImmediate((Object)(object)((Component)val5.GetComponent<PlayerController>().FindVillagerHandLeft().Find("MidasParticleSystem(Clone)(Clone)")).gameObject);
		Object.DestroyImmediate((Object)(object)((Component)val5.GetComponent<PlayerController>().FindVillagerHandRight().Find("MidasParticleSystem(Clone)(Clone)")).gameObject);
		Object.DestroyImmediate((Object)(object)((Component)val5.GetComponent<PlayerController>().hatsContainer.transform.parent.Find("TruesightParticleSystem(Clone)(Clone)")).gameObject);
		DestroyComponentIfExists<PlayerController>(val5);
		DestroyComponentIfExists<PlayerInteract>(val5);
		DestroyComponentIfExists<AudioListener>(val5);
		DestroyComponentIfExists<VoiceNetworkObject>(val5);
		DestroyComponentIfExists<CharacterInputHandler>(val5);
		DestroyComponentIfExists<CharacterMovementHandler>(val5);
		DestroyComponentIfExists<NetworkCharacterControllerPrototypeCustom>(val5);
		DestroyComponentIfExists<PlayerEffectsManager>(val5);
		DestroyComponentIfExists<PlayerGroundDetection>(val5);
		DestroyComponentIfExists<PlayerFootstepsComponent>(val5);
		DestroyComponentIfExists<PlayerPhasingComponent>(val5);
		DestroyComponentIfExists<PlayerBombTickingComponent>(val5);
		DestroyComponentIfExists<PlayerBombIconComponent>(val5);
		DestroyComponentIfExists<PlayerSurvivalistHeartbeatComponent>(val5);
		DestroyComponentIfExists<PlayerMercenaryTargetIconComponent>(val5);
		DestroyComponentIfExists<PlayerHeldItemComponent>(val5);
		DestroyComponentIfExists<PlayerPredatorComponent>(val5);
		DestroyComponentIfExists<PlayerAngelIconComponent>(val5);
		DestroyComponentIfExists<PlayerResurrectedComponent>(val5);
		DestroyComponentIfExists<PlayerSpotterLightComponent>(val5);
		DestroyComponentIfExists<PlayerDyingComponent>(val5);
		DestroyComponentIfExists<PlayerTargetArrowComponent>(val5);
		DestroyComponentIfExists<KnockbackComponent>(val5);
		DestroyComponentIfExists<ForcedRotationComponent>(val5);
		DestroyComponentIfExists<GravityComponent>(val5);
		DestroyComponentIfExists<PlayerNewAnimationsComponent>(val5);
		DestroyComponentIfExists<PlayerGlowingChangesComponent>(val5);
		DestroyComponentIfExists<PlayerHeartSeethroughComponent>(val5);
		DestroyComponentIfExists<PlayerAskForSpeechIconComponent>(val5);
		list = new List<GameObject>();
		for (int k = 0; k < val5.transform.childCount; k++)
		{
			Transform child2 = val5.transform.GetChild(k);
			list.Add(((Component)child2).gameObject);
		}
		for (int num2 = list.Count - 1; num2 >= 0; num2--)
		{
			GameObject val6 = list[num2];
			Object.DestroyImmediate((Object)(object)val6);
		}
		val5.SetActive(false);
		MolotovEntity.MolotovEntityPrefab = val5;
		PlayerCustom.HatShader = ((Renderer)((Component)PlayerController.Local.hats.transform.GetChild(0)).GetComponentInChildren<MeshRenderer>()).material.shader;
		PlayerCustom.RegularWolfShader = ((Renderer)Traverse.Create((object)PlayerController.Local).Field<SkinnedMeshRenderer>("wolfMeshRenderer").Value).material.shader;
		PlayerIllusionCreated = true;
	}

	private static void DestroyComponentIfExists<T>(GameObject obj) where T : Component
	{
		T val = default(T);
		if (obj.TryGetComponent<T>(ref val))
		{
			Object.DestroyImmediate((Object)(object)val);
		}
	}

	private static void RegisterGameObject(GameObject prefab, string name)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		prefab.SetActive(false);
		NetworkObjectService.Instance.RegisterNetworkObject(prefab, name);
	}

	private static NetworkObject CreateObjectInCenter(string name)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = Traverse.Create((object)GameManager.Instance).Field<Transform[]>("mapSpawns").Value[GameManager.Instance.MapID - 1].position;
		NetworkPrefabId networkObject = NetworkObjectService.Instance.GetNetworkObject(name);
		NetworkObject val = ((SimulationBehaviour)GameManager.Instance).Runner.Spawn(networkObject, (Vector3?)position, (Quaternion?)Quaternion.identity, (PlayerRef?)null, (OnBeforeSpawned)delegate(NetworkRunner _, NetworkObject no)
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			((Component)no).transform.position = position;
		}, (NetworkObjectPredictionKey?)null, true, (NetworkObject)null);
		((Component)val).transform.position = position;
		return val;
	}

	private static void AddSoundIfNeeded(string assetName)
	{
		AudioBank value = Traverse.Create((object)AudioManager.Instance).Field<AudioBank>("soundBank").Value;
		Dictionary<string, AudioClip> value2 = Traverse.Create((object)value).Field<Dictionary<string, AudioClip>>("dictionary").Value;
		if (!value2.ContainsKey(assetName))
		{
			AudioClip value3 = NewRolesCoreBundle.LoadAsset<AudioClip>(assetName);
			value2.Add(assetName, value3);
			Logger.LogInfo((object)("Added sound: " + assetName));
		}
	}

	private static GameObject AddEffectToList(string globalManagerName, Type componentType, float duration)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		GameObject val = new GameObject(globalManagerName);
		val.AddComponent<NetworkObject>();
		val.AddComponent(componentType);
		Traverse.Create((object)val.GetComponent(componentType)).Field("duration").SetValue((object)duration);
		RegisterGameObject(val, globalManagerName);
		NewEffects.Add(val.GetComponent<Effect>());
		return val;
	}

	private static void AddPotionEffectsToList(List<Effect> effects)
	{
		EffectManager value = Traverse.Create(typeof(EffectManager)).Field<EffectManager>("_instance").Value;
		Traverse val = Traverse.Create((object)value).Field("effects");
		List<Effect> list = new List<Effect>((Effect[])val.GetValue());
		list.AddRange(effects);
		val.SetValue((object)list.ToArray());
		NewEffects.AddRange(effects);
	}

	private static GameObject AddGameObjectToList(string globalManagerName, Type componentType)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		GameObject val = new GameObject(globalManagerName);
		val.AddComponent<NetworkObject>();
		val.AddComponent<NetworkTransform>();
		val.AddComponent(componentType);
		RegisterGameObject(val, globalManagerName);
		return val;
	}
}
