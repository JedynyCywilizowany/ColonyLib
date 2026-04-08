using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public abstract class ColonyGore : ModGore,IColonyContent
{
	string IColonyContent.AssetCategory=>"Gores";
	public override string Texture=>this.DefaultTexturePath();
}