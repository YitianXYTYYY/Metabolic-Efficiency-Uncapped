using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace MetabolicEfficiencyUncapped;

public class MEU_WorldComp(World world) : WorldComponent(world)
{
	public static readonly SimpleCurve MetabolismToFoodConsumptionFactorCurveNew = new()
	{
		{ new CurvePoint(-100f, 26f), true },
		{ new CurvePoint(-50f, 13.5f), true },
		{ new CurvePoint(-40f, 11f), true },
		{ new CurvePoint(-30f, 8.5f), true },
		{ new CurvePoint(-20f, 6f), true },
		{ new CurvePoint(-10f, 3.5f), true },
		{ new CurvePoint(-5f, 2.25f), true },
		{ new CurvePoint(0.0f, 1f), true },
		{ new CurvePoint(5f, 0.5f), true },
	};

	public static readonly SimpleCurve MetabolismToFoodConsumptionFactorCurveOrig = new()
	{
		{ new CurvePoint(-5f, 2.25f), true },
		{ new CurvePoint(0.0f, 1f), true },
		{ new CurvePoint(5f, 0.5f), true },
	};

	public static readonly SimpleCurve ComplexityToCreationHoursCurveNew = new()
    {
        { new CurvePoint(0.0f, 3f), true },
        { new CurvePoint(4f, 4f), true },
        { new CurvePoint(8f, 6f), true },
        { new CurvePoint(12f, 9f), true },
        { new CurvePoint(16f, 13f), true },
        { new CurvePoint(20f, 18f), true },
        { new CurvePoint(24f, 24f), true },
        { new CurvePoint(28f, 31f), true },
		{ new CurvePoint(32f, 39f), true },
		{ new CurvePoint(36f, 48f), true },
        { new CurvePoint(40f, 52f), true },
        { new CurvePoint(44f, 56f), true },
        { new CurvePoint(48f, 60f), true },
        { new CurvePoint(52f, 64f), true },
        { new CurvePoint(56f, 68f), true },
        { new CurvePoint(60f, 72f), true },
    };

	public static readonly SimpleCurve ComplexityToCreationHoursCurveOrig = new()
	{
		{ new CurvePoint(0.0f, 3f), true },
		{ new CurvePoint(4f, 5f), true },
		{ new CurvePoint(8f, 8f), true },
		{ new CurvePoint(12f, 12f), true },
		{ new CurvePoint(16f, 17f), true },
		{ new CurvePoint(20f, 23f), true },
	};

	public static readonly IntRange BioStatRangeNew = new(-100, 100);
	public static readonly IntRange BioStatRangeOrig = new(-5, 5);

	public static Lazy<FieldInfo> BiostatRange = new(() => AccessTools.Field(typeof(GeneTuning), nameof(GeneTuning.BiostatRange)));

	public static void OverrideSettings(Settings settings = null)
	{
		if (settings == null)
		{
			if (MetabolicEfficiencyUncappedMod.settings == null)
			{
				return;
			}

			settings = MetabolicEfficiencyUncappedMod.settings;
		}

		// Clean up curves
		MetabolismToFoodConsumptionFactorCurveNew?.SortPoints();
		ComplexityToCreationHoursCurveNew?.SortPoints();

		if (settings.EnableExtendedMetabolismMultipliers && MetabolismToFoodConsumptionFactorCurveNew != null)
		{
			GeneTuning.MetabolismToFoodConsumptionFactorCurve.SetPoints(MetabolismToFoodConsumptionFactorCurveNew.Points);
		}
		else
		{
			GeneTuning.MetabolismToFoodConsumptionFactorCurve.SetPoints(MetabolismToFoodConsumptionFactorCurveOrig.Points);
		}

		if (settings.EnableExtendedComplexityCurve && ComplexityToCreationHoursCurveNew != null)
		{
			GeneTuning.ComplexityToCreationHoursCurve.SetPoints(ComplexityToCreationHoursCurveNew.Points);
		}
		else
		{
			GeneTuning.ComplexityToCreationHoursCurve.SetPoints(ComplexityToCreationHoursCurveOrig.Points);
		}

		BiostatRange.Value.SetValue(null, settings.EnableExtendedBioStatRange ? BioStatRangeNew : BioStatRangeOrig);
	}

	public override void FinalizeInit(bool _)
	{
		OverrideSettings();
	}
}
