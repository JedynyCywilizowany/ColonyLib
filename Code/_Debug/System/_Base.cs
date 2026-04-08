using Terraria.ModLoader;

namespace ColonyLib.Debug;

public partial class ColonyDebugSystem : ModSystem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ColonyLib.IsDebugMode;
	}
}