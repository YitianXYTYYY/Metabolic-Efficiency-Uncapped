using HarmonyLib;
using Verse;

namespace MetabolicEfficiencyUncapped.BS;

public class MetabolicEfficiencyUncappedBSMod : Mod
{
	public MetabolicEfficiencyUncappedBSMod(ModContentPack content)
		: base(content)
	{
#if DEBUG
		Harmony.DEBUG = true;
#endif
		Harmony harmony = new Harmony("MetabolicEfficiencyUncapped.BS.main");
		harmony.PatchAll();
	}
}
