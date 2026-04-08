using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public abstract class ColonyWall : ModWall,IColonyContent
{
	string IColonyContent.AssetCategory=>"Walls";
	public override string Texture=>this.DefaultTexturePath();
}