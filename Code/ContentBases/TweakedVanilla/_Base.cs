using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

public interface IColonyContent
{
	public string AssetCategory{get;}
}
public static class ColonyContentUtils
{
	private const string AssetType_Textures="Textures";
	private const string AssetType_Sounds="Sounds";
	private const string AssetType_Shaders="Shaders";

	public static string CustomAssetPath(this Mod mod,string assetType,string name)
	{
		return $"{mod.Name}/Assets/{assetType}/{name}";
	}
	public static string ContentCustomAssetPath<T>(this T type,string assetType) where T : ModType,IColonyContent
	{
		return CustomAssetPath(type.Mod,assetType,$"{type.AssetCategory}/{type.Name}");
	}
	public static string ContentExtraCustomAssetPath<T>(this T type,string assetType,string name) where T : ModType,IColonyContent
	{
		return CustomAssetPath(type.Mod,assetType,$"{type.AssetCategory}/{type.Name}/{name}");
	}

	public static string TexturePath(this Mod mod,string name)
	{
		return CustomAssetPath(mod,AssetType_Textures,name);
	}
	public static string ContentTexturePath<T>(this T type) where T : ModType,IColonyContent
	{
		return ContentCustomAssetPath(type,AssetType_Textures);
	}
	public static string ContentExtraTexturePath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return ContentExtraCustomAssetPath(type,AssetType_Textures,name);
	}

	public static string SoundPath(this Mod mod,string name)
	{
		return CustomAssetPath(mod,AssetType_Sounds,name);
	}
	public static string ContentSoundPath<T>(this T type) where T : ModType,IColonyContent
	{
		return ContentCustomAssetPath(type,AssetType_Sounds);
	}
	public static string ContentExtraSoundPath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return ContentExtraCustomAssetPath(type,AssetType_Sounds,name);
	}

	public static string ShaderPath(this Mod mod,string name)
	{
		return CustomAssetPath(mod,AssetType_Shaders,name);
	}
	public static string ContentShaderPath<T>(this T type) where T : ModType,IColonyContent
	{
		return ContentCustomAssetPath(type,AssetType_Shaders);
	}
	public static string ContentExtraShaderPath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return ContentExtraCustomAssetPath(type,AssetType_Shaders,name);
	}
	public static string DyeShaderPath(this ColonyItem item)
	{
		return ShaderPath(item.Mod,$"Dyes/{item.Name}");
	}

	public static readonly string EmptyTexturePath=TexturePath(ColonyLib.Instance,"Empty");
	public static Asset<Texture2D> EmptyTexture=>ModContent.Request<Texture2D>(EmptyTexturePath);
	public static readonly string VanillaNoisePath="Images/Misc/noise";
}