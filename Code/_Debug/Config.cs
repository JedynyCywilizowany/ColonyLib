using Newtonsoft.Json;
using Terraria.ModLoader.Config;

namespace ColonyLib.Debug;

public class ColonyDebugConfig : ModConfig
{
	public override ConfigScope Mode=>ConfigScope.ServerSide;

	public bool ReportPackets{get;set;}

	
	[JsonIgnore]
	[ShowDespiteJsonIgnore]
	public int WorldGenMarkersCount=>(ColonyDebugSystem.worldGenMarkers?.Count??0);
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