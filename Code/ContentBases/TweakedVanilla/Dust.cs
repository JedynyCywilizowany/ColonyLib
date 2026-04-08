using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public abstract class ColonyDust : ModDust,IColonyContent
{
	string IColonyContent.AssetCategory=>"Dusts";
	public override string Texture=>this.DefaultTexturePath();
}