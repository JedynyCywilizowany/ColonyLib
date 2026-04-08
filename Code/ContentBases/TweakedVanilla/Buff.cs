using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public abstract class ColonyBuff : ModBuff,IColonyContent
{
	string IColonyContent.AssetCategory=>"Buffs";
	public override string Texture=>this.DefaultTexturePath();
}