using Newtonsoft.Json;
using Terraria;
using Terraria.ModLoader.Config;

namespace ColonyLib.Debug;

public class ColonyDebugConfig : ModConfig
{
	public override ConfigScope Mode=>ConfigScope.ServerSide;

	public bool ReportPackets{get;set;}

	[Header("WorldGenMarkers")]
	[JsonIgnore]
	[ShowDespiteJsonIgnore]
	public string WorldGenMarkersCount=>(Main.gameMenu ? "n/a" : (ColonyDebugSystem.worldGenMarkers?.Count??0).ToString());
	public bool ClearWorldGenMarkers
	{
		get=>false;
		set
		{
			if (value) ColonyDebugSystem.worldGenMarkers?.Clear();
		}
	}

	public override bool Autoload(ref string name)
	{
		return ColonyLib.IsDebugMode;
	}
}