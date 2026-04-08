using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public abstract class ColonyNPC : ModNPC,IColonyContent
{
	string IColonyContent.AssetCategory=>"NPCs";
	public override string Texture=>this.DefaultTexturePath();
}