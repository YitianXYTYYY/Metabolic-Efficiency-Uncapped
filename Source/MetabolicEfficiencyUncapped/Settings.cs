using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace MetabolicEfficiencyUncapped;

public class Settings : ModSettings
{
	public bool EnableExtendedMetabolismMultipliers = true;
	public bool EnableExtendedComplexityCurve = true;
	public bool EnableExtendedBioStatRange = true;

	public void DoWindowContents(Rect wrect)
	{
		Listing_Standard options = new();
		options.Begin(wrect);

		options.CheckboxLabeled("MetabolicEfficiencyUncapped_EnableExtendedMetabolismMultipliers".Translate(), ref EnableExtendedMetabolismMultipliers);
		options.Gap();
		options.CheckboxLabeled("MetabolicEfficiencyUncapped_EnableExtendedComplexityCurve".Translate(), ref EnableExtendedComplexityCurve);
		options.Gap();
		options.CheckboxLabeled("MetabolicEfficiencyUncapped_EnableExtendedBioStatRange".Translate(), ref EnableExtendedBioStatRange);
		options.Gap();

		options.End();
	}

	public override void ExposeData()
	{
		Scribe_Values.Look(ref EnableExtendedMetabolismMultipliers, "EnableExtendedMetabolismMultipliers", true);
		Scribe_Values.Look(ref EnableExtendedComplexityCurve, "EnableExtendedComplexityCurve", true);
		Scribe_Values.Look(ref EnableExtendedBioStatRange, "EnableExtendedBioStatRange", true);

		MEU_WorldComp.OverrideSettings(this);
	}
}
