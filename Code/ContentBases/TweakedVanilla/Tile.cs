using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public abstract class ColonyTile : ModTile,IColonyContent
{
	string IColonyContent.AssetCategory=>"Tiles";
	public override string Texture=>this.DefaultTexturePath();
}