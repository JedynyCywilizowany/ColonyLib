using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

[Autoload(Side=ModSide.Client)]
public abstract class ColonyScene : ModSceneEffect,IColonyContent
{
	string IColonyContent.AssetCategory=>"Scenes";
}