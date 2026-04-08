using Terraria.ModLoader.Config;

namespace ColonyLib.Config;

public class ColonyConfig : ModConfig
{
	public override ConfigScope Mode=>ConfigScope.ServerSide;

	[ReloadRequired]
	public bool DebugMode{get;set;}
}