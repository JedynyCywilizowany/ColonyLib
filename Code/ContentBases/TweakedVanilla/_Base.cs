using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public interface IColonyContent
{
	public string AssetCategory{get;}
}
public static class ColonyContentUtils
{
	private static string AssetPath<T>(T type,string assetType,string name) where T : ModType,IColonyContent
	{
		return $"{type.Mod.Name}/Assets/{assetType}/{type.AssetCategory}/{name}";
	}

	public static string TexturePath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return AssetPath(type,"Textures",name);
	}
	public static string OwnTexturePath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return TexturePath(type,$"{type.Name}/{name}");
	}
	public static string DefaultTexturePath<T>(this T type) where T : ModType,IColonyContent
	{
		return TexturePath(type,type.Name);
	}

	public static string SoundPath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return AssetPath(type,"Sounds",name);
	}
	public static string OwnSoundPath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return SoundPath(type,$"{type.Name}/{name}");
	}

	public static readonly string NoTexture=nameof(ColonyLib)+"/Assets/Textures/None";
}