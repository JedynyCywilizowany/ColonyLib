using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public abstract class ColonyProjectile : ModProjectile,IColonyContent
{
	string IColonyContent.AssetCategory=>"Projectiles";
	public override string Texture=>this.DefaultTexturePath();
}