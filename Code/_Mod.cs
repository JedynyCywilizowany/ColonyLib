using System.IO;
using ColonyLib.Config;
using ColonyLib.ContentBases;
using Terraria.ModLoader;

namespace ColonyLib;

public partial class ColonyLib : Mod
{
	public override string Name=>nameof(ColonyLib);
	public static ColonyLib Instance=>ModContent.GetInstance<ColonyLib>();
	public override void Load()
	{
		LoadILEditsAndDetours();
	}
	public override void Unload()
	{
		ColonyUtils.RevertArrayModifications();
		this.AutoUnload();
	}
	public override void PostSetupContent()
	{
		ColonyUtils.SetupDummyEntities();
	}

	public override void HandlePacket(BinaryReader reader,int whoAmI)
	{
		ColonyPacketType.Receive(reader,whoAmI);
	}
	
	public static bool IsDebugMode=>ModContent.GetInstance<ColonyConfig>().DebugMode;
}