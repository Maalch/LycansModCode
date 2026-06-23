using System.Collections.Generic;
using Fusion;
using LycansNewRoles.NewEffects;
using LycansNewRoles.NewItems.Accessories;
using LycansNewRoles.NewMaps;
using UnityEngine;

namespace LycansNewRoles;

public static class BalancingValues
{
	public struct ModifiedEffectData(int realIndex, int blueMageCooldownSeconds, float? durationOnTransformedWolf, float? durationOnZombie)
	{
		public int RealIndex = realIndex;

		public int BlueMageCooldownSeconds = blueMageCooldownSeconds;

		public float? DurationOnTransformedWolf = durationOnTransformedWolf;

		public float? DurationOnZombie = durationOnZombie;
	}

	public const float EffectDurationStunned = 5f;

	public const float EffectDurationChasing = 3f;

	public const float EffectDurationSprinting = 5f;

	public const float EffectDurationReverting = 15f;

	public const float EffectDurationDisoriented = 3f;

	public const float EffectDurationInfected = 10f;

	public const float EffectDurationUndetected = 15f;

	public const float EffectDurationStarvationActive = 150f;

	public const float EffectDurationBlind = 3.5f;

	public const float EffectDurationIllusion = 15f;

	public const float EffectDurationDisguised = 300f;

	public const float EffectDurationDiseased = 300f;

	public const float EffectDurationBomb = 20f;

	public const float EffectDurationPanic = 3f;

	public const float EffectDurationFleeing = 4f;

	public const float EffectDurationDowned = 10f;

	public const float EffectDurationWounded = 30f;

	public const float EffectDurationPoisoned = 45f;

	public const float EffectDurationExorcised = 15f;

	public const float EffectDurationPhasing = 3f;

	public const float EffectDurationDying = 45f;

	public const float EffectDurationAngel = 12f;

	public const float EffectDurationResistance = 10f;

	public const float EffectDurationPortal = 8f;

	public const float EffectDurationSpiritResistance = 15f;

	public const float EffectDurationSneaky = 7f;

	public const float EffectDurationSpotter = 20f;

	public const float EffectDurationPotionDeaf = 60f;

	public const float EffectDurationPotionMidas = 60f;

	public const float EffectDurationPotionVampire = 35f;

	public const float EffectDurationPotionTiny = 35f;

	public const float EffectDurationPotionHaunted = 70f;

	public const float EffectDurationPotionNearsighted = 60f;

	public const float EffectDurationPotionAssassin = 20f;

	public const float EffectDurationPotionStinking = 60f;

	public const float EffectDurationPotionMute = 100f;

	public const float EffectDurationPotionClairvoyance = 75f;

	public const float EffectDurationPotionChaos = 60f;

	public const float EffectDurationPotionEnergized = 60f;

	public const float EffectDurationPotionConfused = 30f;

	public const float EffectDurationPotionCamouflage = 90f;

	public const float EffectDurationPotionImmune = 80f;

	public const float AgentEliminationRange = 2.5f;

	public const float AgentEliminationSneakyDuration = 8f;

	public const float NecromancerMarkCastTime = 2f;

	public const float NecromancerResurrectCastTime = 2f;

	public const int NecromancerChargePerSecondInWolfForm = 150;

	public const int NecromancerChargeLossPerSecondWhenResurrected = 300;

	public const int NecromancerMinimumChargeToResurrect = 10000;

	public const int WarlockShapeshiftCastTime = 1;

	public const float DeceiverScaryEffectDistance = 40f;

	public const float DeceiverIllusionDelayInitialMinSeconds = 2f;

	public const float DeceiverIllusionDelayInitialMaxSeconds = 5f;

	public const float DeceiverIllusionDelayBetweenMinSeconds = 15f;

	public const float DeceiverIllusionDelayBetweenMaxSeconds = 30f;

	public const float DeceiverIllusionDurationMinSeconds = 8f;

	public const float DeceiverIllusionDurationMaxSeconds = 15f;

	public const float DeceiverTrickRange = 10f;

	public const float DeceiverDrunkChangeVoteChance = 0.65f;

	public const float DeceiverDrunkStunChanceEverySecondWhenTalking = 0.15f;

	public const float DeceiverDrunkStunDurationInMeetings = 8f;

	public const float DeceiverDrunkStunDurationOutsideMeetings = 2f;

	public const int VillageIdiotStartingBoredom = 2500;

	public const int VillageIdiotBoredomIncreasePerSecond = 200;

	public const float VillageIdiotBoredomReductionPerSecondAtMinDistance = 3000f;

	public const float VillageIdiotBoredomReductionPerSecondAtMaxDistance = 0f;

	public const float VillageIdiotBoredomReductionMultiplierForWolf = 2f;

	public const float VillageIdiotHumanRangeForBoredom = 30f;

	public const float VillageIdiotWolfRangeForBoredom = 40f;

	public const int VillageIdiotBoredomReductionPerCollect = 1700;

	public const int VillageIdiotBoredomReductionPerSecondInWolfForm = 400;

	public const float VillageIdiotHungerPerSecondPerBoredom = 0.0002f;

	public const float SpyMaximumDistanceForBestTarget = 80f;

	public const float SpySpyingPerSecondWithoutBonus = 10f;

	public const float SpySpyingPerSecondWithTargetVisible = 32f;

	public const float SpySpyingPerSecondWithSpyImmobile = 18f;

	public const float SpySpyingPerSecondWithTargetVisibleAndSpyImmobile = 63f;

	public const float ScientistAnalysisPowerAtMinDistance = 70f;

	public const float ScientistAnalysisPowerAtMaxDistance = 20f;

	public const float ScientistAnalysisMultiplierIfNotRealWolf = 0.2f;

	public const float ScientistAnalysisMultiplierForTransform = 8f;

	public const float ScientistAnalysisMultiplierForKill = 8f;

	public const float ScientistAnalysisMultiplierIfWolfVisible = 4f;

	public const float ScientistAnalysisMultiplierIfWolfVisibleButWounded = 0.4f;

	public const float ScientistAnalysisMultiplierIfScientistCrouched = 0.8f;

	public const float ScientistAnalysisMultiplierIfScientistInvisible = 0.4f;

	public const float ScientistAnalysisMultiplierIfScientistFast = 0.3f;

	public const float ScientistAnalysisMultiplierIfScientistFeigningDeath = 0.5f;

	public const float ScientistAnalysisRangePeriodic = 40f;

	public const float ScientistAnalysisRangeTransform = 40f;

	public const float ScientistAnalysisRangeKill = 30f;

	public const float ScientistCreateGadgetCooldown = 60f;

	public const float BeastMarkRange = 7f;

	public const float BeastTransformationTime = 7f;

	public const float BeastKillRange = 3f;

	public const float BeastHealthLossPercentagePerSecond = 0.0115f;

	public const float BeastTrapHealthLossPercentagePerSecond = 0.025f;

	public const float BeastHealthGainPercentagePerKill = 0.15f;

	public const float BeastMovementSpeed = 1.25f;

	public const float BeastNegativeEffectsDurationPercentage = 0.5f;

	public const float BeastHealthLossPercentageOnShot = 0.1f;

	public const float BeastHealthLossPercentageOnBomb = 0.25f;

	public const float BeastTrapDurationSeconds = 5f;

	public const int BeastDamageBonusOnDoors = 50;

	public const int BeastActiveFogEndDistance = 65;

	public static Color BeastActiveFogColor = new Color(1f, 0f, 0f, 1f);

	public const float BeastVoiceRange = 40f;

	public const float BeastGlobalKnockbackMultiplier = 0.6f;

	public const float BeastSleepingGasStacksMultiplier = 0.5f;

	public const float MercenarySleepDurationOnHuman = 30f;

	public const float MercenarySleepDurationOnWolf = 8f;

	public const float MercenaryParalysisCooldown = 60f;

	public const int MercenaryBonusPointsOnSuccessfulShot = 200;

	public const int MercenaryCooldownBetweenTargets = 90;

	public const float VoodooReanimateRange = 10f;

	public const float VoodooReanimateCastTime = 2f;

	public const float VoodooHealthLossPerZombiePerPlayerCount = 0.015f;

	public const float ZombieMovementSpeed = 1.1f;

	public const float ZombieKillRange = 2.5f;

	public const float ZombieAttackOnWolfDownedDuration = 7f;

	public const float ZombieAttackOnWolfKnockbackPower = 7f;

	public const float ZombieAttackOnWolfKnockbackReductionPerSecond = 7f;

	public const float ZombieKillVoodooCooldownMultiplication = 0.3f;

	public const float ZombieMoaningRange = 20f;

	public const float ZombieHealthLossPerSecondWhenRunning = 25f;

	public const float ZombieHealthRegenPerSecondWhenNotRunning = 4f;

	public static Color ZombieFogColor = new Color(0.75f, 0f, 0.75f, 1f);

	public const float KidnapperAbductRange = 5f;

	public const float KidnapperAbductTimer = 3f;

	public const float KidnapperCooldownAfterGameStart = 75f;

	public const float KidnapperCooldownAfterDayStart = 15f;

	public const float KidnapperKidnapBlindRadius = 10f;

	public const float KidnapperKidnapBlindDuration = 3f;

	public const float KidnapperSilenceHealthAdditionalHungerOutsideMeetings = 0.25f;

	public const float KidnapperSilenceHealthAdditionalHungerDuringMeetings = 0.1f;

	public const float CultistSkullCreationCastTime = 1f;

	public const float CultistGoal = 10000f;

	public const float CultistChargeGainPerSecondPerSkull = 15f;

	public const float CultistSpiritMoveSpeed = 1.4f;

	public const float CultistSpiritCaptureCooldown = 5f;

	public const float CultistInvokedSkullCreationInterval = 6f;

	public const float CultistInvokedSkullCreationTimer = 4f;

	public const float CultistInvokedSkullLifetime = 45f;

	public const float CultistInvokedSkullMoveSpeed = 2.75f;

	public const float CultistInvokedSkullMoveSpeedWhenSlowed = 0.8f;

	public const float CultistInvokedSkullSlowDuration = 2f;

	public const int CultistActiveFogEndDistance = 65;

	public static Color CultistActiveFogColor = new Color(0f, 0.6f, 0.6f, 1f);

	public const float CultistPortalOnPlayerChance = 0.25f;

	public const float WarlockWolfCurseRange = 10f;

	public const float WarlockWolfCurseDormantMinimumDelay = 1f;

	public const float WarlockWolfCurseDormantMaximumDelay = 20f;

	public const float WarlockWolfCurseDormantMinimumDuration = 30f;

	public const float WarlockWolfCurseDormantMaximumDuration = 60f;

	public const int PossessorProgressPerSecond = 400;

	public const float PossessorMarkRange = 5f;

	public const float SaboteurTrapHeldItemRange = 6f;

	public const float SaboteurPoisonHealthDecrease = 1.2f;

	public const int SaboteurPoisonFogEndDistance = 20;

	public static Color SaboteurPoisonFogColor = new Color(0f, 0.5f, 0f, 1f);

	public const float SaboteurTrappedGadgetHealthLossPercentage = 0.25f;

	public const float SaboteurWoundedMoveSpeedMultiplier = 0.65f;

	public const float SaboteurItemLightIntensityNotHeld = 2f;

	public const float SaboteurItemLightIntensityHeld = 0.5f;

	public const float SaboteurItemLightRangeNotHeld = 2f;

	public const float SaboteurItemLightRangeHeld = 0.15f;

	public const int SaboteurChargesGainPerSecond = 160;

	public const float TraitorSabotageDelayMultiplier = 1f;

	public const float BomberCreateBombRange = 6f;

	public const float BomberGiveBombRange = 2.5f;

	public const float BomberBombDamagePercentageOnHuman = 0.35f;

	public const float BomberBombDamagePercentageOnWolf = 0.35f;

	public const float BomberBombDamagePercentageOnBeast = 0.15f;

	public const float BomberBombGlowingDuration = 180f;

	public const float BomberBombWoundedDurationOnHuman = 180f;

	public const float BomberBombWoundedDurationOnWolf = 30f;

	public const float BomberBombStartMinimumDelay = 5f;

	public const float BomberBombStartMaximumDelay = 10f;

	public const float BomberBombTickingSoundRange = 20f;

	public const float BomberBombExplosionSoundRange = 60f;

	public const float PoacherShotSoundRange = 20f;

	public const float PoacherMarkSoundRange = 30f;

	public const float RitualistChargePerSecondAtMinDistance = 600f;

	public const float RitualistChargePerSecondAtMaxDistance = 100f;

	public const float RitualistChargePerSecondRange = 25f;

	public const float RitualistMuteDuration = 55f;

	public const float RitualistDeafDuration = 20f;

	public const float RitualistParanoiaDuration = 12f;

	public const float RitualistFlatulenceDuration = 20f;

	public const float RitualistNearsightedDuration = 25f;

	public const float RitualistConfusedDuration = 15f;

	public const float RitualistStunnedDuration = 10f;

	public const float PredatorScaryEffectRangeNotCrouched = 30f;

	public const float PredatorScaryEffectRangeCrouched = 5f;

	public const float PredatorHeartbeatRange = 20f;

	public const float PredatorEmpoweredDurationOnKill = 45f;

	public const float PredatorEffectMovementSpeed = 1.15f;

	public const float PredatorEffectScaryEffectRangeMultiplier = 0.75f;

	public const float PredatorHealthAfterBeingShotIfPrey = 0.5f;

	public const float SneakySpeedMultiplier = 1.5f;

	public const float SneakSilentTransformationDelayMultiplier = 2f;

	public const float TrackerPlaceRadarCastTime = 6f;

	public const float TrackerRadarRadius = 30f;

	public const int TrackerRadarDuration = 30;

	public const int TrackerChargesGainPerSecond = 100;

	public const int HostEggHatchDuration = 15;

	public const float HostParasiteDestroyRange = 0.1f;

	public const float HostParasiteDetectRange = 20f;

	public const float HostParasiteTargetDamagePercentage = 0.2f;

	public const float HostParasiteTargetPoisonDuration = 15f;

	public const float HostParasiteNearbyPlayerRange = 12f;

	public const float HostParasiteNearbyPlayerMaximumDamagePercentage = 0.2f;

	public const float HostParasiteNearbyPlayerMaximumPoisonDuration = 15f;

	public static Dictionary<PlayerCustom.PlayerSecondaryRole, int> SecondaryRoleMaxAmountInDraft = new Dictionary<PlayerCustom.PlayerSecondaryRole, int>
	{
		{
			PlayerCustom.PlayerSecondaryRole.BothAlcoholic,
			3
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothSprinter,
			3
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothInfected,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothTeleporter,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothEngineer,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothPolitician,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothMetabolic,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothIllusionist,
			3
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothSherif,
			1
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothGambler,
			1
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothMedium,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothAstral,
			3
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothScavenger,
			1
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothBlueMage,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothActor,
			3
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothScribe,
			3
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothCarabineer,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothForger,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothImitator,
			2
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothMerchant,
			1
		},
		{
			PlayerCustom.PlayerSecondaryRole.BothTinkerer,
			3
		}
	};

	public const float AlcoholicPotionHealWolf = 0.45f;

	public const float AlcoholicPotionHealBeast = 0.2f;

	public const float SprinterSprintingMoveSpeedMultiplier = 1.75f;

	public const float SprinterChasingMoveSpeedMultiplier = 1.5f;

	public const float EngineerTrappedDuration = 4f;

	public const float EngineerTrapDisarmTime = 1f;

	public const int EngineerDoorBashBonusDamageWolf = 40;

	public const int EngineerDoorBashBonusDamageNonWolf = 20;

	public const int EngineerSmokeRadiusBoosted = 250;

	public const int EngineerSmokeRadiusNotBoosted = 250;

	public const float EngineerGrenadeKnockbackMultiplier = 0.6f;

	public const float EngineerSleepingGasStacksMultiplier = 0.4f;

	public const int InfectedWolfRadius = 30;

	public const float GamblerSwitchRange = 5f;

	public const float GamblerHealOnHumanForm = 0.2f;

	public const float GamblerHealOnWolfForm = 0.5f;

	public const float GamblerHealOnBeast = 0.15f;

	public const float MetabolicWolfStarvationRange = 10f;

	public const float MetabolicVillagerFeedMultiplier = 1.4f;

	public const float MetabolicStarvationAdditionalHunger = 0.8f;

	public const float MetabolicPositiveEffectsDurationMultiplier = 1.25f;

	public const float MetabolicNegativeEffectsDurationMultiplier = 0.8f;

	public const float PoliticianDenyVoteRange = 10f;

	public const float SherifInitialCooldown = 240f;

	public const float SherifKillRange = 3f;

	public const float ResistanceMinimumHealthAfterBeingShot = 0.6f;

	public const float SprinterSprintingAdditionalHunger = 4f;

	public const float ScavengerHumanFormHealPercentage = 0.25f;

	public const float ScavengerWolfFormHealPercentage = 0.75f;

	public const float BlueMageCastRange = 7f;

	public const float BlueMageMinimumCooldownAfterNewSpell = 10f;

	public const float ActorInWolfFormHungerMultiplier = 0.25f;

	public const float CarabineerKnockbackPower = 14f;

	public const float CarabineerKnockbackReductionPerSecond = 9.5f;

	public const float CarabineerDisorientedDurationOnWolf = 3f;

	public const float CarabineerForcedRotationPower = 900f;

	public const float CarabineerForcedRotationReductionPerSecond = 700f;

	public const float CarabineerResilienceDuration = 3f;

	public const float CarabineerKnockdownDurationOnHuman = 4f;

	public const float ForgerCopyItemRange = 10f;

	public const float ForgerStealItemRange = 3f;

	public const float AstralActiveBaseHunger = 0.2f;

	public const float AstralActiveExtraHungerPerDistance = 0.01f;

	public const float AstralMoveSpeedHumanForm = 3f;

	public const float AstralMoveSpeedWolfForm = 9f;

	public const float AstralFearRadiusMultiplier = 0.35f;

	public const int ImitatorSecondaryRolesToChoose = 4;

	public const float MerchantMinimumDistanceToCoin = 40f;

	public const int MerchantGainPerCoinForNonWolf = 3;

	public const int MerchantGainPerCoinForWolf = 2;

	public const int MerchantStartingCoins = 10;

	public const int MerchantOffersCount = 4;

	public const int MerchantRerollCost = 3;

	public const float MerchantDeliveryTime = 5f;

	public const float TinkererBootsInvisibilityDurationHumanForm = 3f;

	public const float TinkererBootsInvisibilityDurationWolfForm = 6f;

	public const float TinkererHornHealPercentage = 0.25f;

	public const float TinkererPowerInteractionRange = 10f;

	public const float TinkererRingQuicknessDurationHumanForm = 20f;

	public const float TinkererRingQuicknessDurationWolfForm = 10f;

	public const float TinkererMagnifierBlindDurationHumanForm = 6f;

	public const float TinkererMagnifierBlindDurationWolfForm = 3f;

	public const float TinkererCrystalBallTruesightDurationHumanForm = 45f;

	public const float TinkererCrystalBallTruesightDurationWolfForm = 45f;

	public const int MerchantPriestProtectionCost = 20;

	public const int MerchantHealCost = 20;

	public const float MerchantHealPercentage = 0.5f;

	public const float PeasantChargePerSecondAtMinDistance = 330f;

	public const float PeasantChargePerSecondAtMaxDistance = 70f;

	public const float PeasantChargePerSecondRange = 30f;

	public const float PeasantMinimumChargeToUse = 2500f;

	public const float PeasantChargeLossPerSecondWhenActive = 1250f;

	public const float ExorcistCastTime = 3f;

	public const float ExorcistBlindDuration = 1.5f;

	public const float ExorcistResilienceDuration = 5f;

	public const int ExorcistFogEndDistance = 20;

	public static Color ExorcistFogColor = new Color(0.5f, 0.5f, 0f, 1f);

	public const float ExorcistDetectorKnockbackPower = 5f;

	public const float ExorcistDetectorKnockbackReductionPerSecond = 5f;

	public const float ExorcistDetectorMinimumDuration = 80f;

	public const float ExorcistDetectorMaximumDuration = 210f;

	public const float ExorcistDetectorDistanceFromMeetingForMaximumDuration = 80f;

	public const float AvengerAttackRange = 3.5f;

	public const float AvengerAttackDownedDuration = 5f;

	public const float AvengerAttackResilienceDuration = 8f;

	public const float AvengerOnWolfKnockbackPower = 6f;

	public const float AvengerOnWolfKnockbackReductionPerSecond = 6f;

	public const float AvengerRadiusForChargeBonus = 30f;

	public const float AvengerChargePerSecondIfNearbyWolf = 100f;

	public const int AvengerChargeIncreaseOnNearbyWolfKill = 2500;

	public const int InvestigatorHintsAmountOnNewDay = 3;

	public const float InvestigatorMinimumPlaceDistanceFromEachOther = 30f;

	public const float InvestigatorMinimumPlaceDistanceFromPlayer = 30f;

	public const float InvestigatorMaximumPlaceDistanceFromPlayer = 100f;

	public const float InvestigatorHintDurationAtMinimumDistance = 30f;

	public const float InvestigatorHintDurationAtMaximumDistance = 100f;

	public const float InvestigatorDurationRandomnessMinimum = 0.7f;

	public const float InvestigatorDurationRandomnessMaximum = 1.3f;

	public const int InvestigatorStartingCharge = 50;

	public const int InvestigatorHintChargePercentage = 25;

	public const int InvestigatorPlayerTargetChargePercentage = 35;

	public const float InvestigatorPlayerTargetInsteadOfHintChance = 0.6f;

	public const float InvestigatorPlayerTargetMinimumDistance = 25f;

	public const float InvestigatorPlayerTargetInteractRange = 3f;

	public const int InvestigatorAmountOfIntel = 2;

	public static Dictionary<PlayerDetectiveIntel.PlayerDetectiveIntelType, int> InvestigatorChanceByIntelType = new Dictionary<PlayerDetectiveIntel.PlayerDetectiveIntelType, int>
	{
		{
			PlayerDetectiveIntel.PlayerDetectiveIntelType.OneIsEvil,
			3
		},
		{
			PlayerDetectiveIntel.PlayerDetectiveIntelType.DifferentSides,
			1
		},
		{
			PlayerDetectiveIntel.PlayerDetectiveIntelType.NotWolf,
			0
		},
		{
			PlayerDetectiveIntel.PlayerDetectiveIntelType.TransformationsAndDetransformations,
			2
		},
		{
			PlayerDetectiveIntel.PlayerDetectiveIntelType.WolvesAndSoloRolesRemaining,
			2
		}
	};

	public const float SurvivalistSaveCastTime = 4f;

	public const float SurvivalistHungerPercentageWhenDowned = 0.095f;

	public const float SurvivalistHungerPercentageWhenSaved = 0.395f;

	public const float SurvivalistAlertRange = 40f;

	public const float SurvivalistHeartbeatRange = 15f;

	public const float SurvivalistBuffRange = 2.5f;

	public const int SurvivalistMaterialsPerHint = 2000;

	public const float SurvivalistHintCreationChancePerSecond = 0.035f;

	public const int SurvivalistHintLifetimeSeconds = 90;

	public const float SurvivalistSelfCastTime = 3f;

	public const float SurvivalistDyingDurationOnNonElite = 50f;

	public const float SurvivalistDyingDurationOnElite = 35f;

	public const float PriestProtectRange = 2.5f;

	public const float PriestProtectDownedDuration = 2f;

	public const float PriestProtectEliteDownedDuration = 1f;

	public const float PriestProtectionKnockbackPower = 6f;

	public const float PriestProtectionKnockbackReductionPerSecond = 3f;

	public const float PriestProtectionEliteKnockbackPower = 4f;

	public const float PriestProtectionEliteKnockbackReductionPerSecond = 2f;

	public const float PriestProtectionForcedRotationPower = 4000f;

	public const float PriestProtectionForcedRotationReductionPerSecond = 3000f;

	public const float PriestProtectionFleeingDuration = 6f;

	public const float PriestProtectionEliteFleeingDuration = 4f;

	public const float PriestResilienceDuration = 6f;

	public const float AngelHealPercentage = 0.2f;

	public const float AngelEffectWolfOnMinimapRadius = 40f;

	public const float AngelDebuffOnWolfDuration = 15f;

	public const float GhostMoveSpeed = 1.3f;

	public const float SpecterMoveSpeed = 1.8f;

	public const float SpecterInteractionRange = 2.5f;

	public const float SpecterLeftClickCooldown = 25f;

	public const float SpecterLeftClickCooldownInMeetings = 40f;

	public const float SpecterRightClickCooldown = 120f;

	public const float SpecterPowerOnWolfDuration = 15f;

	public const float SpecterDebuffOnVillagerDuration = 25f;

	public const float SpecterDebuffOnVillagerDurationInMeetings = 8f;

	public const float SpecterDebuffOnVillagerImmunityDuration = 50f;

	public const float SummonerSpiritAttackKnockbackPower = 15f;

	public const float SummonerSpiritAttackKnockbackReductionPerSecond = 21f;

	public const float SummonerSpiritAttackDamagePercentage = 0.05f;

	public const float SummonerSpiritAttackWoundedDuration = 6f;

	public const float SummonerSpiritAttackCooldown = 5f;

	public const float SummonerSpiritAttackResilienceDuration = 5f;

	public const float SummonerSpiritAttackResistanceDuration = 20f;

	public const float SummonerSpiritSpellMaximumRange = 20f;

	public const float SummonerSpiritSpellDamagePercentage = 0.05f;

	public const float SummonerSpiritSpellWoundedDuration = 6f;

	public const float SummonerSpiritSpellCooldown = 20f;

	public const float SummonerSpiritSpellResilienceDuration = 5f;

	public const float SummonerSpiritSpellResistanceDuration = 20f;

	public const float SummonerInteractionRange = 2.5f;

	public const float ScoutPlaceRadarCastTime = 2f;

	public const float ScoutRadarRadius = 36f;

	public const float ScoutRadarMinimumDuration = 70f;

	public const float ScoutRadarMaximumDuration = 140f;

	public const float ScoutRadarDistanceFromMeetingForMaximumDuration = 60f;

	public const float MagicianBeaconCastTime = 1f;

	public const int MagicianIllusionDuration = 7;

	public const float MagicianIllusionMoveSpeed = 3f;

	public const float MagicianIllusionExplosionBlindDuration = 2f;

	public const int MysticCastTime = 1;

	public const int MysticPowerChargePerSecondAtMaximumHealth = 25;

	public const int MysticPowerChargePerSecondAtZeroHealth = 150;

	public const float MysticRepulsorRadius = 30f;

	public const int MysticRepulsorDuration = 15;

	public const float MysticRepulsorBanishDuration = 8f;

	public static Color MysticRepulsorFogColor = Color.cyan;

	public static float MysticRepulsorFogEndDistanceAtZeroStack = 40f;

	public static float MysticRepulsorFogEndDistanceAtMaxStacks = 15f;

	public const int MysticRepulsorStacksMaximum = 1000;

	public const int MysticRepulsorStacksPerSecond = 175;

	public const float MysticRepulsorStacksMultiplierAtZeroRange = 1f;

	public const float MysticRepulsorStacksMultiplierAtMaximumRange = 0.35f;

	public const float MysticRepulsorMovementSpeedAtZeroStack = 0.9f;

	public const float MysticRepulsorMovementSpeedAtMaxStacks = 0.25f;

	public const float ShadowRadiusForChargeBonus = 30f;

	public const float ShadowAuraRadius = 20f;

	public const float ShadowChargePerSecondDefault = 35f;

	public const float ShadowChargePerSecondBonusIfNearbyWolf = 215f;

	public const int ShadowChargeIncreaseOnNearbyWolfKill = 10000;

	public const int ShadowChargeLossOnActivation = 2000;

	public const float ShadowChargeLossPerSecondWhenActive = 1500f;

	public const float ShadowMinimumDistanceForInvisibility = 40f;

	public const float ShadowMinimumDistanceForLevel3Camouflage = 25f;

	public const float ShadowMinimumDistanceForLevel2Camouflage = 10f;

	public const int HermitHideoutsAmountOnNewDay = 4;

	public const int HermitHideoutDuration = 80;

	public const int HermitHideoutFadeMultiplierWhenInside = 3;

	public const int HermitHideoutCreationInterval = 20;

	public const float HermitHideoutRadius = 12f;

	public const float HermitHiddenRaycastRange = 2f;

	public const int HermitHideoutDurationIncreaseOnPower = 20;

	public const int HermitHideoutMaximumDurationForYellowIcon = 60;

	public const int HermitHideoutMaximumDurationForRedIcon = 30;

	public const int HermitHideoutMinimumPlaceDistanceFromEachOther = 30;

	public const int RunemasterMaximumRunes = 8;

	public const float RunemasterCastTime = 2f;

	public const float RunemasterTriggerTime = 2.5f;

	public const int RunemasterRuneDuration = 150;

	public const float RunemasterRuneExplosionVisualAreaMultiplier = 4.5f;

	public const float RunemasterRuneExplosionAreaWithOneWolf = 12f;

	public const float RunemasterRuneExplosionForcedRotationPowerWithOneWolf = 3000f;

	public const float RunemasterRuneExplosionForcedRotationReductionPerSecond = 2000f;

	public const float RunemasterRuneExplosionConfusionDurationWithOneWolf = 12f;

	public const float RunemasterRuneExplosionEffectsPowerMultiplierAtMaximumRange = 0.35f;

	public const float RunemasterRuneExplosionMinimumPowerMultiplierForForcedRotation = 0.5f;

	public const float RunemasterRuneExplosionAreaAdditionalMultiplierPerExtraWolf = 0.5f;

	public const float RunemasterRuneExplosionForcedRotationAdditionalMultiplierPerExtraWolf = 0.25f;

	public const float RunemasterRuneExplosionEffectDurationAdditionalMultiplierPerExtraWolf = 0.25f;

	public const float RunemasterRuneExplosionBaseResilienceDuration = 8f;

	public const float RunemasterSelectedRuneWolfDetectionRange = 10f;

	public const float AvatarWolfOnMinimapRadius = 40f;

	public const float AvatarHealthPercentageToCreateItem = 0.25f;

	public const float BulletsBaseMultiplier = 0.25f;

	public const float BulletsAdditionalMultiplierPerEliteRole = 0.5f;

	public const float AlchemistWhitePotionHealPercent = 0.2f;

	public const float AlchemistWhitePotionDurationMultiplier = 0.6f;

	public const int AlchemistMaterialsGainForBulletRecharge = 100;

	public const int SpotterMaterialsGainForBulletRecharge = 150;

	public const float SpotterVoiceRangeBoosted = 70f;

	public const float SpotterMinimapRangeUnboosted = 25f;

	public const float SpotterMinimapRangeBoosted = 45f;

	public const float SpotterWolvesLightRange = 250f;

	public const float SpotterWolvesLightIntensity = 4f;

	public const float SpotterMovementSpeedMultiplierBoosted = 1.13f;

	public const int PurifierMaterialsGainForBulletRecharge = 150;

	public const float PurifierYAngleIncrease = 0.1f;

	public const float PurifierLaunchPower = 25f;

	public const float PurifierMovementReductionPercentagePerSecond = 0.5f;

	public const int PurifierFireDurationMilliseconds = 9000;

	public const float PurifierEntityExistenceMillisecondsForMaximumBurnDuration = 5f;

	public const float PurifierMinimumBurnDurationAtMinimumRadius = 3f;

	public const float PurifierMaximumBurnDurationAtMaximumRadius = 7f;

	public const float PurifierBurnMovementSpeedMultiplierHumanForm = 1.2f;

	public const float PurifierBurnMovementSpeedMultiplierWolfForm = 0.7f;

	public const float PurifierBurnHealthIncreaseHumanForm = 1.5f;

	public const float PurifierBurnHealthDecreaseWolfForm = 0.8f;

	public const float PurifierConfusionDurationOnWolf = 5f;

	public static Color PurifierBurnFogColor = Color.green;

	public const float PurifierBurnFogMaximumDistance = 25f;

	public const float PurifierBurnDurationIfKilledByWolf = 15f;

	public static int VillageIdiotTrapItemCooldown = 75;

	public static int VillageIdiotBombCooldown = 110;

	public static int VillageIdiotCurseCooldown = 120;

	public const float MegaFartChancePerFart = 0.0035f;

	public const float MegaFartKnockbackPowerOnHuman = 25f;

	public const float MegaFartKnockbackReductionPerSecondOnHuman = 10f;

	public const float MegaFartKnockbackPowerOnWolf = 15f;

	public const float MegaFartKnockbackReductionPerSecondOnWolf = 8f;

	public const float MidasEffectValueMultiplier = 2f;

	public const float VampireBaseRadius = 25f;

	public const float VampireEffectCurrentPercentage = 0.03f;

	public const float TinyMovementSpeed = 0.6f;

	public const float HauntedMinimumDelayServer = 3f;

	public const float HauntedMaximumDelayServer = 6f;

	public const float HauntedHealthGainPercentage = 0.25f;

	public const float HauntedHealthLossPercentage = 0.2f;

	public const float HauntedDoorRadius = 40f;

	public const float HauntedLanternFlickerRadius = 30f;

	public const int HauntedLanternFlickersCount = 20;

	public const float HauntedForcedRotationPower = 3000f;

	public const float HauntedForcedRotationReductionPerSecond = 2000f;

	public static Dictionary<HauntedEffect.HauntedPossibleEffect, int> HauntedChanceByEffect = new Dictionary<HauntedEffect.HauntedPossibleEffect, int>
	{
		{
			HauntedEffect.HauntedPossibleEffect.HealthGain,
			2
		},
		{
			HauntedEffect.HauntedPossibleEffect.HealthLoss,
			1
		},
		{
			HauntedEffect.HauntedPossibleEffect.OpenDoors,
			3
		},
		{
			HauntedEffect.HauntedPossibleEffect.CloseDoors,
			3
		},
		{
			HauntedEffect.HauntedPossibleEffect.Teleport,
			2
		},
		{
			HauntedEffect.HauntedPossibleEffect.GainItem,
			2
		},
		{
			HauntedEffect.HauntedPossibleEffect.RandomEffect,
			4
		},
		{
			HauntedEffect.HauntedPossibleEffect.Sound,
			3
		},
		{
			HauntedEffect.HauntedPossibleEffect.FlickerLanterns,
			2
		},
		{
			HauntedEffect.HauntedPossibleEffect.ForcedRotation,
			1
		}
	};

	public const int NearsightedFogEndDistance = 12;

	public static Color NearsightedFogColor = new Color(0f, 0f, 0.1f, 1f);

	public const float AssassinRange = 3.5f;

	public const float AssassinOnWolfDownedDuration = 6f;

	public const float AssassinOnWolfKnockbackPower = 6f;

	public const float AssassinOnWolfKnockbackReductionPerSecond = 6f;

	public const float AssassinResilienceDuration = 9f;

	public const float MidasPetrifyRange = 3f;

	public const float MidasDurationOnHuman = 30f;

	public const float MidasDurationOnWolf = 8f;

	public static Color PetrifiedFogColor = new Color(0.75f, 0.75f, 0f, 1f);

	public const float DisorientedMovementSpeed = 0.5f;

	public const float StunnedMovementSpeed = 0.5f;

	public const float StinkingEffectRadiusOnNonWolf = 5f;

	public const float StinkingEffectRadiusOnWolf = 15f;

	public const float NauseatedMovementSpeed = 0.85f;

	public const int NauseatedFogEndDistance = 30;

	public static Color NauseatedFogColor = new Color(0f, 0.5f, 0f, 1f);

	public const float PhasingMovementSpeedHumanForm = 1.5f;

	public const float PhasingMovementSpeedWolfForm = 1.25f;

	public const float BombMovementSpeed = 1.15f;

	public const float FleeingMovementSpeed = 1.75f;

	public const float PanicMovementSpeed = 0.65f;

	public const float ChaosMinimumDelayServer = 6f;

	public const float ChaosMaximumDelayServer = 12f;

	public static Dictionary<ChaosEffect.ChaosPossibleEffect, int> ChaosChanceByEffect = new Dictionary<ChaosEffect.ChaosPossibleEffect, int>
	{
		{
			ChaosEffect.ChaosPossibleEffect.CreateTraps,
			2
		},
		{
			ChaosEffect.ChaosPossibleEffect.CreateGrenade,
			4
		},
		{
			ChaosEffect.ChaosPossibleEffect.UseScrollOnNearbyPlayer,
			6
		},
		{
			ChaosEffect.ChaosPossibleEffect.LockNearbyDoor,
			1
		},
		{
			ChaosEffect.ChaosPossibleEffect.UnlockNearbyDoor,
			2
		},
		{
			ChaosEffect.ChaosPossibleEffect.CreateSmoke,
			4
		},
		{
			ChaosEffect.ChaosPossibleEffect.UseDiamondOnNearbyPlayer,
			1
		}
	};

	public const float ChaosMaximumPlayerRange = 15f;

	public const float ChaosMaximumDoorRange = 20f;

	public const int ChaosMinimumTraps = 1;

	public const int ChaosMaximumTraps = 3;

	public const float ChaosMaximumTrapRange = 2f;

	public const float ChaosMaximumGrenadeRange = 2f;

	public const float ChaosSmokeDuration = 8f;

	public const float ChaosGrenadeKnockbackMultiplier = 0.7f;

	public const float TruesightItemLightIntensity = 2f;

	public const float TruesightItemLightRange = 2f;

	public const float IsolationFogEndDistance = 40f;

	public static Color IsolationFogColor = new Color(0.35f, 0.35f, 0.35f, 1f);

	public const float EnergizedHealPerSecondIfNoChargeablePower = 0.6f;

	public const float EnergizedCooldownSecondsReductionPerSecond = 1f;

	public const float ClairvoyanceMinimapRadius = 40f;

	public const int ConfusionInitialDelayMilliseconds = 1500;

	public const int ConfusionMinimumDelayMilliseconds = 5000;

	public const int ConfusionMaximumDelayMilliseconds = 8000;

	public const float ResilienceHungerMultiplier = 0.25f;

	public const float LoverVillagerHungerRateBase = 0.5f;

	public const float LoverVillagerHungerRatePerLivingPlayer = 0.02f;

	public const float LoverVillagerHealPercentageOnWolfKill = 0.4f;

	public const int LoverFogEndDistance = 55;

	public static Color LoverFogColor = new Color(1f, 0.4f, 1f, 1f);

	public const float MercenaryHuntSneakyDuration = 10f;

	public const float EscapingSpeedMultiplier = 1.1f;

	public const float WolfKillRange = 1.75f;

	public const float LootCorpseCastTime = 3f;

	public const float CultistSkullForbiddenAreaRadius = 10f;

	public static Dictionary<int, List<Vector3>> CultistSkullForbiddenAreasByMapId = new Dictionary<int, List<Vector3>> { 
	{
		1,
		new List<Vector3>
		{
			new Vector3(111.98f, 22.08f, 153.42f),
			new Vector3(135.76f, 22.08f, 153.86f),
			new Vector3(104.75f, 22.17f, 217.89f),
			new Vector3(148.08f, 22.28f, 254.41f),
			new Vector3(176.49f, 22.7f, 222.09f),
			new Vector3(184.45f, 22.58f, 228.51f),
			new Vector3(196.93f, 22.28f, 242.07f)
		}
	} };

	public const float CultistSkullMinimumDistanceFromCorpses = 3f;

	public const float CultistSkullMinimumDistanceFromOtherSkulls = 10f;

	public const float MagicScrollDurationMultiplier = 0.75f;

	public const float GrenadeFuseTime = 2f;

	public const float GrenadeYAngleIncrease = 0.1f;

	public const float GrenadeLaunchPower = 25f;

	public const float GrenadeMovementReductionPercentagePerSecond = 0.5f;

	public const float GrenadeMovementReductionPercentagePerSecondWhenGrounded = 60f;

	public const float GrenadeArea = 15f;

	public const float GrenadeKnockbackMultiplierAtMaximumDistance = 0.4f;

	public const float GrenadeMaximumKnockback = 20f;

	public const float GrenadeKnockbackMultiplierOnCrouched = 0.5f;

	public const float GrenadeKnockbackReductionPerSecond = 9f;

	public const float GrenadeKnockbackMultiplierIfNotVisible = 0.6f;

	public const float GrenadeMaximumDisorientedDuration = 8f;

	public const float GrenadeDisorientedDurationMultiplierAtMaximumDistance = 0.4f;

	public const float MolotovYAngleIncrease = 0.1f;

	public const float MolotovLaunchPower = 25f;

	public const float MolotovMovementReductionPercentagePerSecond = 0.5f;

	public const int MolotovFireDurationMilliseconds = 12000;

	public const float MolotovEntityExistenceMillisecondsForMaximumBurnDuration = 6f;

	public const float MolotovMinimumBurnDurationAtMinimumRadius = 2f;

	public const float MolotovMaximumBurnDurationAtMaximumRadius = 8f;

	public const float MolotovBurnMovementSpeedMultiplier = 0.7f;

	public const float MolotovBurnHealthDecreaseHumanForm = 2.2f;

	public const float MolotovBurnHealthDecreaseWolfForm = 1.5f;

	public static Color MolotovBurnFogColor = Color.red;

	public const float MolotovBurnFogMaximumDistance = 30f;

	public const float RadarDelayBeforeActivation = 2f;

	public const float RadarDuration = 23f;

	public const float RadarYAngleIncrease = 0.1f;

	public const float RadarLaunchPower = 25f;

	public const float RadarMovementReductionPercentagePerSecond = 0.5f;

	public const float RadarMovementReductionPercentagePerSecondWhenGrounded = 60f;

	public const float RadarAreaOnHumans = 15f;

	public const float RadarAreaOnWolves = 40f;

	public const float SleepingGasPrepareTime = 4f;

	public const float SleepingGasDetonateSoundRange = 30f;

	public const float SleepingGasDurationForSleepingGas = 20f;

	public const float SleepingGasDurationForCamouflage = 20f;

	public const float SleepingGasDurationForPoisonGas = 50f;

	public const float SleepingGasInitialScaleRatio = 0.2f;

	public const float SleepingGasDurationForMaximumScaleRatio = 4f;

	public const float SleepingGasParticleSystemScalePerRadius = 0.175f;

	public static Color SleepingGasColorForSleepingGas = new Color(0.5f, 0f, 0.5f, 0.7f);

	public static Color SleepingGasColorForCamouflage = new Color(0f, 0.5f, 1f, 0.7f);

	public static Color SleepingGasColorForPoisonGas = new Color(0f, 1f, 0f, 0.7f);

	public const float SleepingGasMaximumRadiusForSleepingGas = 20f;

	public const float SleepingGasMaximumRadiusForCamouflage = 20f;

	public const float SleepingGasMaximumRadiusForPoisonGas = 30f;

	public const float SleepingGasSleepyMovementSpeedAtZeroStack = 0.85f;

	public const float SleepingGasSleepyMovementSpeedAtMaxStacks = 0.25f;

	public const float SleepingGasSleepyDuration = 7f;

	public const float SleepingGasCamouflageDuration = 2f;

	public const float SleepingGasFlatulenceDuration = 5.5f;

	public static Color SleepingGasSleepyFogColor = Color.white;

	public static float SleepingGasSleepyFogEndDistanceAtZeroStack = 60f;

	public static float SleepingGasSleepyFogEndDistanceAtMaxStacks = 5f;

	public const float SleepingGasApplyEffectInterval = 1f;

	public const int SleepingGasSleepStacksMaximum = 1000;

	public const int SleepingGasSleepStacksPerHalfSecondOnHuman = 150;

	public const int SleepingGasSleepStacksPerHalfSecondOnWolf = 150;

	public const float SleepingGasSleepStacksMultiplierAtZeroRange = 1.7f;

	public const float SleepingGasSleepStacksMultiplierAtMaximumRange = 0.6f;

	public const float SleepingGasSleepDurationOnHuman = 30f;

	public const float SleepingGasSleepDurationOnWolf = 12f;

	public const float RingMovementSpeedForVillagerInDanger = 1.15f;

	public const float RingMovementSpeedForTransformedWolf = 1.15f;

	public const float HornHealthIncreaseInHumanForm = 0.3f;

	public const float HornHealthIncreaseForLoverVillager = 0.15f;

	public const float HornHealthIncreaseInWolfForm = 0.2f;

	public const float BootsDetectionRadiusMultiplierInWolfForm = 0.85f;

	public const int MagnifierWolfFootstepsDuration = 90;

	public const int MagnifierVillagerFootstepsDuration = 10;

	public const float CrystalBallWolfDetectionRange = 25f;

	public const float SpellbookCooldown = 30f;

	public const float SpellbookNegativeEffectsDurationMultiplier = 0.7f;

	public const float SpellbookForcedRotationPower = 2000f;

	public const float SpellbookForcedRotationReductionPerSecond = 2000f;

	public static Dictionary<AccessorySpellbook.PossibleEffects, AccessorySpellbook.SpellbookEffectDetails> SpellbookPossibleEffectsAndDurationsOnHumans = new Dictionary<AccessorySpellbook.PossibleEffects, AccessorySpellbook.SpellbookEffectDetails>
	{
		{
			AccessorySpellbook.PossibleEffects.Invisibility,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 12f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Swiftness,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 12f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Petrified,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 10f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Giant,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 10f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.FlatulencesWithMegaFart,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 15f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Blind,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 12f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Illusion,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 30f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Asleep,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 20f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Bomb,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 20f,
				Ponderation = 1
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Burning,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 12f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Detected,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 15f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Teleportation,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 15f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.ConfusionAndForcedRotation,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 20f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Regeneration,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 40f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Camouflage,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 60f,
				Ponderation = 3
			}
		}
	};

	public static Dictionary<AccessorySpellbook.PossibleEffects, AccessorySpellbook.SpellbookEffectDetails> SpellbookPossibleEffectsAndDurationsOnWolves = new Dictionary<AccessorySpellbook.PossibleEffects, AccessorySpellbook.SpellbookEffectDetails>
	{
		{
			AccessorySpellbook.PossibleEffects.Invisibility,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 12f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Swiftness,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 12f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Petrified,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 5f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Giant,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 5f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.FlatulencesWithMegaFart,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 15f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Blind,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 5f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Asleep,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 4f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Bomb,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 20f,
				Ponderation = 1
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Burning,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 8f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Detected,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 15f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Teleportation,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 10f,
				Ponderation = 2
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Resilience,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 20f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Clairvoyance,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 15f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.Camouflage,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 40f,
				Ponderation = 3
			}
		},
		{
			AccessorySpellbook.PossibleEffects.ConfusionAndForcedRotation,
			new AccessorySpellbook.SpellbookEffectDetails
			{
				Duration = 8f,
				Ponderation = 2
			}
		}
	};

	public const float SabotageObjectRaycastRange = 7f;

	public const float SabotageSubtlyDurationMultiplier = 5f;

	public const float DiseaseEffectHealthDecrease = 0.3f;

	public const float DiseaseMoveSpeedMultiplier = 0.85f;

	public const float EmpoweredWolfHungerMultiplier = 0.75f;

	public const float EmpoweredMovementSpeed = 1.15f;

	public const float PortalSabotageStayActiveTime = 120f;

	public const float PortalSabotageReactivateTime = 10f;

	public const float PortalBonusMovementSpeed = 1.2f;

	public const float PortalMaximumHealthGainOnPass = 0.35f;

	public const float CursedNightFogEndDistanceMultiplier = 0.65f;

	public const float CursedNightDefaultCurseCooldown = 100f;

	public const float LaboratoryLightsFogEndDistance = 17.5f;

	public const float MayorActionStunDuration = 7f;

	public const float MayorActionStunCooldown = 15f;

	public const float MayorActionListenDuration = 8f;

	public const float MayorActionListenCooldown = 25f;

	public const float MayorActionSpeechDuration = 8f;

	public const float MayorActionSpeechCooldown = 25f;

	public const float EventHarvestLootMultiplier = 1.75f;

	public const float EventHarvestItemsMultiplier = 1.75f;

	public const float EventHarvestAccessoriesMultiplier = 1.75f;

	public const float EventHasteHumanMoveSpeedMultiplier = 1.35f;

	public const float EventHasteWolfMoveSpeedMultiplier = 1.25f;

	public const float EventFogCamouflageDuration = 30f;

	public const float EventFogCamouflageMinimumDelay = 50f;

	public const float EventFogCamouflageMaximumDelay = 90f;

	public static Color EventFogFogColor = new Color(0f, 0f, 0.5f, 0.1f);

	public const float EventFogFogMaximumDistance = 40f;

	public const float EventSpellstormChanceEachSecondForEachPlayer = 0.01f;

	public static Color EventSpellstormFogColor = new Color(0.5f, 0f, 0.5f, 0.1f);

	public const float EventSpellstormFogMaximumDistance = 70f;

	public const float EventEclipseWolfMoveSpeedMultiplier = 0.86f;

	public const float EventEclipseTimeSpeedMultiplier = 1.5f;

	public const float EventEclipseLootHealMultiplier = 2f;

	public static Color EventEclipseFogColor = new Color(0f, 0f, 0f, 0.1f);

	public const float EventEclipseFogMaximumDistance = 60f;

	public const float EventPlagueHungerGainPerSecond = 0.4f;

	public const float EventPlagueHungerLossAtZeroDistance = 25f;

	public const float EventPlagueHungerLossAtMaxDistance = 5f;

	public const float EventPlagueHungerRange = 10f;

	public static Color EventPlagueFogColor = new Color(0f, 0.5f, 0f, 1f);

	public const float EventPlagueFogMaximumDistance = 65f;

	public const float EventTournamentWinnerHealthIncreaseWolfForm = 0.6f;

	public const float EventTournamentWinnerHealthIncreaseHumanForm = 0.8f;

	public const float EventTournamentWinnerMovementSpeedIncreaseWolfForm = 1.08f;

	public const float EventTournamentWinnerMovementSpeedIncreaseHumanForm = 1.15f;

	public const float EventFullMoonWolfMoveSpeedMultiplier = 1.2f;

	public const float EventFullMoonWolfHealthIncrease = 0.7f;

	public const float EventFullMoonNightTimeSpeedMultiplier = 1.65f;

	public const float EventRageDelayToAutomaticTransform = 20f;

	public const float PlayerRaycastRange = 10f;

	public const float PlayerRaycastRangeWhenInvisible = 3.5f;

	public const float PlayerRaycastRangeWhenHiddenWithPeasant = 2.25f;

	public const float TrapSetupTime = 1f;

	public const float TrapDisarmTime = 2.5f;

	public const float TrapDisarmTimeInWolfForm = 5f;

	public const int SmokeBoostedRadius = 4000;

	public const float MaxSpeedBase = 1.5f;

	public const int WolfRecuperateMillisecondsToStart = 4000;

	public const float WolfRecuperateHealthIncrease = 2f;

	public const float VillagerRecuperateHealthIncrease = 1.2f;

	public const float WolfHealthPercentageAfterShotByHunter = 0.125f;

	public const float WolfTenacityMaximumRatio = 0.16f;

	public const float WolfTenacityMovementSpeedMultiplier = 1.2f;

	public const float WolfTenacityHungerMultiplier = 0.7f;

	public const float WolfTenacityRecuperateEffectivenessMultiplier = 2f;

	public const float WolfTenacityNegativeEffectsDurationMultiplier = 0.66f;

	public const float WolfHubrisMinimumRatio = 0.31f;

	public const float WolfHubrisMovementSpeedMultiplier = 0.92f;

	public const float WolfHubrisHungerMultiplier = 1.08f;

	public const float WolfHubrisPositiveEffectsDurationMultiplier = 0.75f;

	public const float WolfCamouflageLevel1DetectionMultiplier = 0.9f;

	public const float WolfCamouflageLevel2DetectionMultiplier = 0.8f;

	public const float WolfCamouflageLevel3DetectionMultiplier = 0.7f;

	public const int ItemChanceTrap = 10;

	public const int ItemChanceSmoke = 10;

	public const int ItemChanceSpyglass = 10;

	public const int ItemChanceLock = 10;

	public const int ItemChanceScroll = 14;

	public const int ItemChanceDiamond = 7;

	public const int ItemChanceGrenade = 8;

	public const int ItemChanceGas = 6;

	public const int ItemChanceMolotov = 8;

	public const int ItemChanceRadar = 7;

	public const float AmogusModeKillRange = 5f;

	public const float AmogusModeKillCooldown = 30f;

	public const float AmogusModeReportRange = 10f;

	public const int AmogusBaseLootsSpawn = 5;

	public const int AmogusBaseIntervalBetweenLoots = 60;

	public static Dictionary<int, float> FogEndDistanceDaytimeByFogConfigPercentage = new Dictionary<int, float>
	{
		{ 0, 300f },
		{ 10, 300f },
		{ 20, 300f },
		{ 30, 300f },
		{ 40, 300f },
		{ 50, 300f },
		{ 60, 300f },
		{ 70, 300f },
		{ 80, 300f },
		{ 90, 300f },
		{ 100, 300f }
	};

	public static Dictionary<int, float> FogEndDistanceNightByFogConfigPercentage = new Dictionary<int, float>
	{
		{ 0, 300f },
		{ 10, 150f },
		{ 20, 125f },
		{ 30, 100f },
		{ 40, 80f },
		{ 50, 65f },
		{ 60, 55f },
		{ 70, 45f },
		{ 80, 35f },
		{ 90, 26f },
		{ 100, 18f }
	};

	public const int FogBonusEndDistanceForWolfForm = 25;

	public static int BeastMarkCooldown => Mathf.RoundToInt(63f - (float)PlayerRegistry.Count * 3f);

	public static float CurrentLootProgress => (float)GameManager.Instance.Score / (float)GameManager.Instance.MaxScore;

	public static float CultistSkullCreationCooldown(int currentSkulls)
	{
		return currentSkulls switch
		{
			0 => 20f, 
			1 => 20f, 
			2 => 40f, 
			3 => 80f, 
			4 => 125f, 
			5 => 175f, 
			_ => 175f, 
		};
	}

	public static float CultistChargeGainMultiplierForPlayersAmount(int livingPlayers)
	{
		return livingPlayers switch
		{
			6 => 0.4f, 
			7 => 0.45f, 
			8 => 0.5f, 
			9 => 0.55f, 
			10 => 0.6f, 
			11 => 0.68f, 
			12 => 0.76f, 
			13 => 0.84f, 
			14 => 0.92f, 
			15 => 1f, 
			_ => 0.4f, 
		};
	}

	public static float PossessorMaximumRangeByMap(int mapId)
	{
		return 25f * DistanceMultiplierByMap(mapId);
	}

	public static int GetMerchantOfferTypePonderation(MerchantOffer.MerchantOfferType type)
	{
		return type switch
		{
			MerchantOffer.MerchantOfferType.Scroll => 2, 
			MerchantOffer.MerchantOfferType.OtherGadget => 4, 
			MerchantOffer.MerchantOfferType.Potion => 2, 
			MerchantOffer.MerchantOfferType.ImmediateEffect => 4, 
			MerchantOffer.MerchantOfferType.PriestProtection => 1, 
			MerchantOffer.MerchantOfferType.Heal => 1, 
			_ => 0, 
		};
	}

	public static int? GetMerchantCostForPotionEffect(Effect effect)
	{
		if (effect is AuditionEffect)
		{
			return 10;
		}
		if (effect is InvisibilityEffect)
		{
			return 20;
		}
		if (effect is NightVision)
		{
			return 15;
		}
		if (effect is SatiatedEffect)
		{
			return 20;
		}
		if (effect is SpeedEffect)
		{
			return 20;
		}
		if (effect is MidasEffect)
		{
			return 15;
		}
		if (effect is AssassinEffect)
		{
			return 40;
		}
		if (effect is ClairvoyanceEffect)
		{
			return 20;
		}
		if (effect is CamouflageEffect)
		{
			return 15;
		}
		if (effect is GiantEffect)
		{
			return 15;
		}
		if (effect is TeleportationEffect)
		{
			return 10;
		}
		if (effect is VampireEffect)
		{
			return 10;
		}
		if (effect is TinyEffect)
		{
			return 10;
		}
		if (effect is HauntedEffect)
		{
			return 10;
		}
		if (effect is ChaosEffect)
		{
			return 15;
		}
		if (effect is StinkingEffect)
		{
			return 10;
		}
		if (effect is ImmuneEffect)
		{
			return 10;
		}
		if (effect is FlatulenceEffect)
		{
			return null;
		}
		if (effect is GlowingEffect)
		{
			return null;
		}
		if (effect is ParanoiaEffect)
		{
			return null;
		}
		if (effect is DeafnessEffect)
		{
			return null;
		}
		if (effect is NearsightedEffect)
		{
			return null;
		}
		if (effect is MuteEffect)
		{
			return null;
		}
		if (effect is ConfusedEffect)
		{
			return null;
		}
		return null;
	}

	public static int GetMerchantCostForScrollEffect(Effect effect)
	{
		if (effect is AuditionEffect)
		{
			return 10;
		}
		if (effect is InvisibilityEffect)
		{
			return 20;
		}
		if (effect is NightVision)
		{
			return 15;
		}
		if (effect is SatiatedEffect)
		{
			return 20;
		}
		if (effect is SpeedEffect)
		{
			return 20;
		}
		if (effect is ClairvoyanceEffect)
		{
			return 20;
		}
		if (effect is CamouflageEffect)
		{
			return 15;
		}
		if (effect is GiantEffect)
		{
			return 15;
		}
		if (effect is TeleportationEffect)
		{
			return 10;
		}
		if (effect is VampireEffect)
		{
			return 10;
		}
		if (effect is HauntedEffect)
		{
			return 10;
		}
		if (effect is ChaosEffect)
		{
			return 15;
		}
		if (effect is ImmuneEffect)
		{
			return 15;
		}
		if (effect is FlatulenceEffect)
		{
			return 10;
		}
		if (effect is ParanoiaEffect)
		{
			return 10;
		}
		if (effect is DeafnessEffect)
		{
			return 10;
		}
		if (effect is NearsightedEffect)
		{
			return 10;
		}
		if (effect is MuteEffect)
		{
			return 10;
		}
		if (effect is ConfusedEffect)
		{
			return 10;
		}
		return 10;
	}

	public static int? GetMerchantCostForImmediateEffect(Effect effect)
	{
		if (effect is AuditionEffect)
		{
			return 10;
		}
		if (effect is InvisibilityEffect)
		{
			return 25;
		}
		if (effect is NightVision)
		{
			return 20;
		}
		if (effect is SatiatedEffect)
		{
			return 25;
		}
		if (effect is SpeedEffect)
		{
			return 25;
		}
		if (effect is MidasEffect)
		{
			return 20;
		}
		if (effect is AssassinEffect)
		{
			return 40;
		}
		if (effect is ClairvoyanceEffect)
		{
			return 25;
		}
		if (effect is CamouflageEffect)
		{
			return 15;
		}
		if (effect is GiantEffect)
		{
			return 20;
		}
		if (effect is TeleportationEffect)
		{
			return 15;
		}
		if (effect is VampireEffect)
		{
			return 15;
		}
		if (effect is TinyEffect)
		{
			return 15;
		}
		if (effect is HauntedEffect)
		{
			return 10;
		}
		if (effect is ChaosEffect)
		{
			return 15;
		}
		if (effect is StinkingEffect)
		{
			return 10;
		}
		if (effect is ImmuneEffect)
		{
			return 15;
		}
		if (effect is FlatulenceEffect)
		{
			return null;
		}
		if (effect is GlowingEffect)
		{
			return null;
		}
		if (effect is ParanoiaEffect)
		{
			return null;
		}
		if (effect is DeafnessEffect)
		{
			return null;
		}
		if (effect is NearsightedEffect)
		{
			return null;
		}
		if (effect is MuteEffect)
		{
			return null;
		}
		if (effect is ConfusedEffect)
		{
			return null;
		}
		return null;
	}

	public static int MediumAdditionalPlayersToAdd(int livingPlayersCount)
	{
		if (livingPlayersCount <= 5)
		{
			return 1;
		}
		if (livingPlayersCount <= 9)
		{
			return 2;
		}
		return 3;
	}

	public static int GetVillagerJobChancePonderation(PlayerCustom.PlayerPrimaryRolePower power)
	{
		return power switch
		{
			PlayerCustom.PlayerPrimaryRolePower.Peasant => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Exorcist => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Avenger => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Investigator => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Survivalist => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Priest => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Scout => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Magician => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Mystic => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Shadow => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Hermit => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Runemaster => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Avatar => 0, 
			PlayerCustom.PlayerPrimaryRolePower.Mole => 0, 
			_ => 0, 
		};
	}

	public static int DetectiveOneIsEvilPlayersToAdd(int livingPlayersCount)
	{
		if (livingPlayersCount <= 8)
		{
			return 2;
		}
		return 3;
	}

	public static float ExorcistChargeMultiplierByMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 0.65f, 
			"map_dungeon" => 0.85f, 
			"map_haddoncans" => 1f, 
			"map_apartcan" => 0.9f, 
			"map_laboratory" => 1f, 
			"map_got" => 0.8f, 
			_ => 1f, 
		};
	}

	public static float DetectiveMaximumRangeByMap(int mapId)
	{
		return 15f * DistanceMultiplierByMap(mapId);
	}

	public static int? GetCastTimeForSecondaryRole(PlayerCustom.PlayerSecondaryRole role)
	{
		return role switch
		{
			PlayerCustom.PlayerSecondaryRole.BothTeleporter => 3, 
			PlayerCustom.PlayerSecondaryRole.BothMedium => 3, 
			PlayerCustom.PlayerSecondaryRole.BothScavenger => 2, 
			_ => null, 
		};
	}

	public static int? GetCooldownForPrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower role, int totalCycleTime)
	{
		return role switch
		{
			PlayerCustom.PlayerPrimaryRolePower.Warlock => 5 * totalCycleTime, 
			PlayerCustom.PlayerPrimaryRolePower.Deceiver => 5 * totalCycleTime, 
			PlayerCustom.PlayerPrimaryRolePower.Investigator => 30, 
			PlayerCustom.PlayerPrimaryRolePower.Hermit => 20, 
			PlayerCustom.PlayerPrimaryRolePower.Angel => 6 * totalCycleTime, 
			_ => null, 
		};
	}

	public static PowerMaterialsInfo? GetMaterialsInfoForPrimaryRolePower(PlayerCustom.PlayerPrimaryRolePower power)
	{
		return power switch
		{
			PlayerCustom.PlayerPrimaryRolePower.Necromancer => new PowerMaterialsInfo(10000, 2f, 1.006f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Possessor => new PowerMaterialsInfo(10000, 1f, 0f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Bomber => new PowerMaterialsInfo(70, 2.5f, 0.506f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Ritualist => new PowerMaterialsInfo(10000, 1.5f, 0.256f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Saboteur => new PowerMaterialsInfo(10000, 3f, 0.506f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Tracker => new PowerMaterialsInfo(10000, 2f, 0.506f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Host => new PowerMaterialsInfo(40, 4f, 2.006f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Peasant => new PowerMaterialsInfo(10000, 1f, 0.506f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Avenger => new PowerMaterialsInfo(10000, 1.5f, 0.256f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Exorcist => new PowerMaterialsInfo(80, 1.5f, 1.056f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Priest => new PowerMaterialsInfo(100, 1.5f, 1.056f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Investigator => new PowerMaterialsInfo(100, 2f, 0.506f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Survivalist => new PowerMaterialsInfo(10000, 1.5f, 1.306f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Scout => new PowerMaterialsInfo(120, 1.5f, 1.256f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Magician => new PowerMaterialsInfo(40, 4f, 2.506f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Mystic => new PowerMaterialsInfo(10000, 1.5f, 0.756f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Shadow => new PowerMaterialsInfo(10000, 3f, 1.056f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Hermit => new PowerMaterialsInfo(200, 2f, 0.506f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Runemaster => new PowerMaterialsInfo(80, 2.5f, 1.556f, gainsMaterialsOnCollect: true, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Alchemist => new PowerMaterialsInfo(40, 5f, 2.006f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Spotter => new PowerMaterialsInfo(100, 3f, 1.5f, gainsMaterialsOnCollect: false, 0f), 
			PlayerCustom.PlayerPrimaryRolePower.Purifier => new PowerMaterialsInfo(100, 3f, 1.5f, gainsMaterialsOnCollect: false, 0f), 
			_ => null, 
		};
	}

	public static int? GetCooldownForSecondaryRole(PlayerCustom.PlayerSecondaryRole role, int totalCycleTime)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		return role switch
		{
			PlayerCustom.PlayerSecondaryRole.BothAlcoholic => 60, 
			PlayerCustom.PlayerSecondaryRole.BothEngineer => NetworkBool.op_Implicit(Plugin.CustomConfig.DropItemsAvailable) ? 120 : 80, 
			PlayerCustom.PlayerSecondaryRole.BothGambler => 150, 
			PlayerCustom.PlayerSecondaryRole.BothIllusionist => 90, 
			PlayerCustom.PlayerSecondaryRole.BothInfected => 90, 
			PlayerCustom.PlayerSecondaryRole.BothMetabolic => 8 * totalCycleTime, 
			PlayerCustom.PlayerSecondaryRole.BothSherif => 120, 
			PlayerCustom.PlayerSecondaryRole.BothAstral => 5, 
			PlayerCustom.PlayerSecondaryRole.BothSprinter => 90, 
			PlayerCustom.PlayerSecondaryRole.BothTeleporter => 120, 
			PlayerCustom.PlayerSecondaryRole.BothScavenger => 30, 
			PlayerCustom.PlayerSecondaryRole.BothActor => 30, 
			PlayerCustom.PlayerSecondaryRole.BothScribe => 60, 
			PlayerCustom.PlayerSecondaryRole.BothCarabineer => 60, 
			PlayerCustom.PlayerSecondaryRole.BothForger => 120, 
			_ => null, 
		};
	}

	public static int? GetSecondCooldownForSecondaryRole(PlayerCustom.PlayerSecondaryRole role, int totalCycleTime)
	{
		return role switch
		{
			PlayerCustom.PlayerSecondaryRole.BothScribe => 20, 
			PlayerCustom.PlayerSecondaryRole.BothForger => 40, 
			_ => null, 
		};
	}

	public static ModifiedEffectData GetModifiedEffectData(Effect effect)
	{
		if (effect is AuditionEffect)
		{
			return new ModifiedEffectData(0, 60, null, null);
		}
		if (effect is InvisibilityEffect)
		{
			return new ModifiedEffectData(1, 120, 25f, 25f);
		}
		if (effect is NightVision)
		{
			return new ModifiedEffectData(2, 90, 45f, null);
		}
		if (effect is SatiatedEffect)
		{
			return new ModifiedEffectData(3, 60, 30f, null);
		}
		if (effect is SpeedEffect)
		{
			return new ModifiedEffectData(4, 120, 30f, 30f);
		}
		if (effect is MidasEffect)
		{
			return new ModifiedEffectData(5, 90, null, null);
		}
		if (effect is AssassinEffect)
		{
			return new ModifiedEffectData(6, 120, 0f, 0f);
		}
		if (effect is StinkingEffect)
		{
			return new ModifiedEffectData(7, 60, null, null);
		}
		if (effect is ClairvoyanceEffect)
		{
			return new ModifiedEffectData(8, 75, 30f, 45f);
		}
		if (effect is GiantEffect)
		{
			return new ModifiedEffectData(9, 90, 10f, 20f);
		}
		if (effect is TeleportationEffect)
		{
			return new ModifiedEffectData(10, 75, 15f, 15f);
		}
		if (effect is VampireEffect)
		{
			return new ModifiedEffectData(11, 75, 45f, 45f);
		}
		if (effect is TinyEffect)
		{
			return new ModifiedEffectData(12, 120, 12f, null);
		}
		if (effect is HauntedEffect)
		{
			return new ModifiedEffectData(13, 60, null, null);
		}
		if (effect is ChaosEffect)
		{
			return new ModifiedEffectData(14, 75, null, null);
		}
		if (effect is FlatulenceEffect)
		{
			return new ModifiedEffectData(15, 60, null, null);
		}
		if (effect is GlowingEffect)
		{
			return new ModifiedEffectData(16, 60, null, 0f);
		}
		if (effect is ParanoiaEffect)
		{
			return new ModifiedEffectData(17, 45, null, null);
		}
		if (effect is DeafnessEffect)
		{
			return new ModifiedEffectData(18, 60, null, null);
		}
		if (effect is NearsightedEffect)
		{
			return new ModifiedEffectData(19, 60, null, null);
		}
		if (effect is MuteEffect)
		{
			return new ModifiedEffectData(20, 60, null, null);
		}
		if (effect is EnergizedEffect)
		{
			return new ModifiedEffectData(21, 75, null, null);
		}
		if (effect is CamouflageEffect)
		{
			return new ModifiedEffectData(22, 75, null, null);
		}
		if (effect is ConfusedEffect)
		{
			return new ModifiedEffectData(23, 75, 15f, null);
		}
		if (effect is ImmuneEffect)
		{
			return new ModifiedEffectData(24, 60, null, null);
		}
		return new ModifiedEffectData(99, 60, null, null);
	}

	public static int EnergizedMaterialsGainByPower(PlayerCustom.PlayerPrimaryRolePower power)
	{
		return power switch
		{
			PlayerCustom.PlayerPrimaryRolePower.Alchemist => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Avenger => 50, 
			PlayerCustom.PlayerPrimaryRolePower.Bomber => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Exorcist => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Investigator => 1, 
			PlayerCustom.PlayerPrimaryRolePower.Peasant => 100, 
			PlayerCustom.PlayerPrimaryRolePower.Survivalist => 100, 
			PlayerCustom.PlayerPrimaryRolePower.Priest => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Scout => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Magician => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Mystic => 100, 
			PlayerCustom.PlayerPrimaryRolePower.Shadow => 200, 
			PlayerCustom.PlayerPrimaryRolePower.Hermit => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Runemaster => 2, 
			PlayerCustom.PlayerPrimaryRolePower.Spotter => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Purifier => 3, 
			PlayerCustom.PlayerPrimaryRolePower.Ritualist => 100, 
			PlayerCustom.PlayerPrimaryRolePower.Saboteur => 200, 
			PlayerCustom.PlayerPrimaryRolePower.Tracker => 100, 
			PlayerCustom.PlayerPrimaryRolePower.Host => 2, 
			_ => 0, 
		};
	}

	public static int AgentMaxSurvivorsToWin(int totalPlayers)
	{
		switch (totalPlayers)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
			return 2;
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
			return 3;
		case 13:
		case 14:
		case 15:
			return 4;
		default:
			return 4;
		}
	}

	public static int SpyGoal(int totalPlayers)
	{
		return 10000;
	}

	public static float SpyMaximumRange(int mapId)
	{
		return 25f * DistanceMultiplierByMap(mapId);
	}

	public static int BeastTargetAmount(int survivingPlayers)
	{
		return Mathf.CeilToInt((float)survivingPlayers * 0.6f);
	}

	public static int ScientistGoal(int totalPlayers)
	{
		return Mathf.RoundToInt((float)(PlayerRegistry.Count * 500));
	}

	public static int VoodooReanimationCooldown(int zombiesCount, int totalPlayers)
	{
		float num = Mathf.Lerp(1f, 0.5f, CurrentLootProgress);
		return Mathf.RoundToInt((float)VoodooBaseCooldown(totalPlayers) * num * (1f + (float)zombiesCount * 0.5f));
	}

	public static int VoodooBaseCooldown(int totalPlayers)
	{
		switch (totalPlayers)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
			return 200;
		case 10:
			return 180;
		case 11:
			return 160;
		case 12:
			return 145;
		case 13:
			return 130;
		case 14:
			return 115;
		case 15:
			return 100;
		default:
			return 100;
		}
	}

	public static int MercenaryTotalObjective(int lootMultiplier, float configMultiplier, int totalCycleTime, float soloRoleDifficulty)
	{
		return Mathf.RoundToInt((float)(totalCycleTime * lootMultiplier) * configMultiplier * soloRoleDifficulty * 18f);
	}

	public static int KidnapperFinalCooldown(int abductsCount, int abductsObjective, int remainingLivingPlayers, int totalPlayers)
	{
		float num = KidnapperBaseCooldown(totalPlayers);
		float num2 = (float)abductsCount / (float)abductsObjective;
		float num3 = Mathf.Lerp(0.5f, 2f, num2);
		float num4 = (float)remainingLivingPlayers / (float)totalPlayers;
		float num5 = Mathf.Lerp(0.15f, 1.8f, num4);
		return Mathf.RoundToInt(num * num3 * num5);
	}

	public static int KidnapperTargetAmount(int totalPlayers)
	{
		switch (totalPlayers)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
			return 3;
		case 12:
		case 13:
		case 14:
		case 15:
			return 4;
		default:
			return 4;
		}
	}

	public static int KidnapperBaseCooldown(int totalPlayers)
	{
		switch (totalPlayers)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			return 65;
		case 7:
			return 70;
		case 8:
			return 75;
		case 9:
			return 80;
		case 10:
			return 85;
		case 11:
			return 90;
		case 12:
			return 60;
		case 13:
			return 65;
		case 14:
			return 70;
		case 15:
			return 75;
		default:
			return 70;
		}
	}

	public static int CultistTargetAmount(int survivingPlayers)
	{
		return Mathf.CeilToInt((float)survivingPlayers * 0.75f);
	}

	public static int ScrollsEffectPonderation(Effect effect)
	{
		if (!(effect is AuditionEffect))
		{
			if (!(effect is InvisibilityEffect))
			{
				if (!(effect is NightVision))
				{
					if (!(effect is SatiatedEffect))
					{
						if (!(effect is SpeedEffect))
						{
							if (!(effect is ClairvoyanceEffect))
							{
								if (!(effect is EnergizedEffect))
								{
									if (!(effect is CamouflageEffect))
									{
										if (!(effect is GiantEffect))
										{
											if (!(effect is TeleportationEffect))
											{
												if (!(effect is VampireEffect))
												{
													if (!(effect is HauntedEffect))
													{
														if (!(effect is ChaosEffect))
														{
															if (!(effect is ImmuneEffect))
															{
																if (!(effect is GlowingEffect))
																{
																	if (!(effect is FlatulenceEffect))
																	{
																		if (!(effect is ParanoiaEffect))
																		{
																			if (!(effect is DeafnessEffect))
																			{
																				if (!(effect is NearsightedEffect))
																				{
																					if (!(effect is MuteEffect))
																					{
																						if (effect is ConfusedEffect)
																						{
																							return 3;
																						}
																						return 0;
																					}
																					return 2;
																				}
																				return 1;
																			}
																			return 2;
																		}
																		return 2;
																	}
																	return 2;
																}
																return 1;
															}
															return 3;
														}
														return 2;
													}
													return 2;
												}
												return 1;
											}
											return 2;
										}
										return 2;
									}
									return 2;
								}
								return 2;
							}
							return 2;
						}
						return 2;
					}
					return 2;
				}
				return 1;
			}
			return 2;
		}
		return 1;
	}

	public static int GetScrollCharges(Effect effect)
	{
		if (!(effect is AuditionEffect))
		{
			if (!(effect is InvisibilityEffect))
			{
				if (!(effect is NightVision))
				{
					if (!(effect is SatiatedEffect))
					{
						if (!(effect is SpeedEffect))
						{
							if (!(effect is ClairvoyanceEffect))
							{
								if (!(effect is EnergizedEffect))
								{
									if (!(effect is CamouflageEffect))
									{
										if (!(effect is GiantEffect))
										{
											if (!(effect is TeleportationEffect))
											{
												if (!(effect is VampireEffect))
												{
													if (!(effect is HauntedEffect))
													{
														if (!(effect is ChaosEffect))
														{
															if (!(effect is ImmuneEffect))
															{
																if (!(effect is GlowingEffect))
																{
																	if (!(effect is FlatulenceEffect))
																	{
																		if (!(effect is ParanoiaEffect))
																		{
																			if (!(effect is DeafnessEffect))
																			{
																				if (!(effect is NearsightedEffect))
																				{
																					if (!(effect is MuteEffect))
																					{
																						if (effect is ConfusedEffect)
																						{
																							return 1;
																						}
																						return 1;
																					}
																					return 2;
																				}
																				return 2;
																			}
																			return 2;
																		}
																		return 2;
																	}
																	return 2;
																}
																return 2;
															}
															return 2;
														}
														return 1;
													}
													return 2;
												}
												return 1;
											}
											return 1;
										}
										return 1;
									}
									return 1;
								}
								return 1;
							}
							return 1;
						}
						return 1;
					}
					return 1;
				}
				return 2;
			}
			return 1;
		}
		return 2;
	}

	public static float SleepingGasRadiusByMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 0.5f, 
			"map_dungeon" => 1f, 
			"map_haddoncans" => 0.6f, 
			"map_apartcan" => 0.65f, 
			"map_laboratory" => 0.85f, 
			"map_got" => 0.6f, 
			_ => 1f, 
		};
	}

	public static int AccessoriesAmountToSpawn(int configItemsCount, int livingPlayersWithoutAccessory)
	{
		float num = (float)livingPlayersWithoutAccessory * (float)configItemsCount / 7f;
		int num2 = Mathf.FloorToInt(num);
		if (Random.value < num % 1f)
		{
			num2++;
		}
		return num2;
	}

	public static bool WolvesHaveTenacity()
	{
		int num = 0;
		num = LivingWolves() switch
		{
			1 => 5, 
			2 => 11, 
			_ => 20, 
		};
		if (PlayerCustomRegistry.Any((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor))
		{
			num = Mathf.RoundToInt((float)num * 1.5f);
		}
		return LivingPlayers() >= num;
	}

	public static bool WolvesHaveHubris()
	{
		int num = 0;
		num = LivingWolves() switch
		{
			1 => 0, 
			2 => 6, 
			_ => 10, 
		};
		if (PlayerCustomRegistry.Any((PlayerCustom o) => o.NewPrimaryRole == PlayerCustom.PlayerNewPrimaryRole.Traitor))
		{
			num = Mathf.RoundToInt((float)num * 0.7f);
		}
		return LivingPlayers() <= num;
	}

	public static int LivingWolves()
	{
		return PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && (int)o.PlayerController.Role == 1 && !NetworkBool.op_Implicit(o.Resurrected));
	}

	public static int LivingPlayers()
	{
		return PlayerCustomRegistry.CountWhere((PlayerCustom o) => !NetworkBool.op_Implicit(o.PlayerController.IsDead) && !NetworkBool.op_Implicit(o.Resurrected));
	}

	public static float DistanceMultiplierByMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 0.6f, 
			"map_dungeon" => 1f, 
			"map_haddoncans" => 0.6f, 
			"map_apartcan" => 0.75f, 
			"map_laboratory" => 0.75f, 
			"map_got" => 0.65f, 
			_ => 1f, 
		};
	}

	public static float WolfKillSoundRangeByMap(int mapId)
	{
		return 30f * DistanceMultiplierByMap(mapId);
	}

	public static float NightFogEndDistanceMultiplierByMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 0.6f, 
			"map_dungeon" => 1f, 
			"map_haddoncans" => 0.5f, 
			"map_apartcan" => 0.65f, 
			"map_laboratory" => 0.6f, 
			"map_got" => 0.6f, 
			_ => 1f, 
		};
	}

	public static float ScoutRadarRadiusMultiplierByMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 0.45f, 
			"map_dungeon" => 1.15f, 
			"map_haddoncans" => 0.6f, 
			"map_apartcan" => 0.7f, 
			"map_laboratory" => 0.8f, 
			"map_got" => 0.6f, 
			_ => 1f, 
		};
	}

	public static float ScientistPowerMultiplierByMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 1.5f, 
			"map_dungeon" => 1.5f, 
			"map_haddoncans" => 1f, 
			"map_apartcan" => 1.4f, 
			"map_laboratory" => 1.5f, 
			"map_got" => 1.4f, 
			_ => 1f, 
		};
	}

	public static float BeastHeartbeatMaximumRange(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 12f, 
			"map_2" => 9f, 
			"map_dungeon" => 12f, 
			"map_haddoncans" => 9f, 
			"map_apartcan" => 12f, 
			"map_laboratory" => 12f, 
			"map_got" => 9f, 
			_ => 12f, 
		};
	}

	public static float FallingHeightMultiplierForDamage(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 0.2f, 
			"map_2" => 0.2f, 
			"map_dungeon" => 1f, 
			"map_haddoncans" => 1.75f, 
			"map_apartcan" => 1f, 
			"map_laboratory" => 1f, 
			"map_got" => 1f, 
			_ => 1f, 
		};
	}

	public static float GravityMultiplier(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 1f, 
			"map_dungeon" => 1f, 
			"map_haddoncans" => 1f, 
			"map_apartcan" => 1f, 
			"map_laboratory" => 0.5f, 
			"map_got" => 0.75f, 
			_ => 1f, 
		};
	}

	public static float LootGenerationMultiplier(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 1f, 
			"map_2" => 1f, 
			"map_dungeon" => 1.15f, 
			"map_haddoncans" => 1f, 
			"map_apartcan" => 1f, 
			"map_laboratory" => 1f, 
			"map_got" => 1f, 
			_ => 1f, 
		};
	}

	public static int MerchantTotalCoinsOnMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => 7, 
			"map_2" => 7, 
			"map_dungeon" => 8, 
			"map_haddoncans" => 6, 
			"map_apartcan" => 6, 
			"map_laboratory" => 8, 
			"map_got" => 7, 
			_ => 8, 
		};
	}

	public static bool ShowMinimapArrowsOnMap(int mapId)
	{
		return MapManager.FindMapNameById(mapId) switch
		{
			"map_1" => false, 
			"map_2" => true, 
			"map_dungeon" => false, 
			"map_haddoncans" => false, 
			"map_apartcan" => false, 
			"map_laboratory" => true, 
			"map_got" => true, 
			_ => false, 
		};
	}
}
