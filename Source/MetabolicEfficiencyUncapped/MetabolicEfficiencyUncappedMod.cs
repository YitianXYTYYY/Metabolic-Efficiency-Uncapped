using HarmonyLib;
using UnityEngine;
using Verse;

namespace MetabolicEfficiencyUncapped;

public class MetabolicEfficiencyUncappedMod : Mod
{
	public static Settings settings;

	public MetabolicEfficiencyUncappedMod(ModContentPack content)
		: base(content)
	{
		// initialize settings
		settings = GetSettings<Settings>();
#if DEBUG
		Harmony.DEBUG = true;
#endif
		Harmony harmony = new Harmony("keyz182.rimworld.MetabolicEfficiencyUncapped.main");
		harmony.PatchAll();
	}

	public override void DoSettingsWindowContents(Rect inRect)
	{
		base.DoSettingsWindowContents(inRect);
		settings.DoWindowContents(inRect);
	}

	public override string SettingsCategory()
	{
		return "MetabolicEfficiencyUncapped_SettingsCategory".Translate();
	}
}
