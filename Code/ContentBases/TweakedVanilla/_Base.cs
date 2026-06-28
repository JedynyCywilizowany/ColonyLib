using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace ColonyLib.ContentBases;

/// <summary>
/// So far, the only purpose of implementing this is making content compatibile with <see cref="ColonyContentUtils"/>.
/// </summary>
public interface IColonyContent
{
	/// <summary>
	/// The implemented types' category, used by <see cref="ColonyContentUtils"/>'s asset path functions.
	/// </summary>
	public string AssetCategory{get;}
}
/// <summary>
/// Mainly contains functions that reduce the need to hard-code asset paths, some of them are only compatibile with types implementing <see cref="IColonyContent"/>.<br/>
/// Note that they work according to my convention, which differs from the usual approach.
/// </summary>
public static class ColonyContentUtils
{
	private const string AssetType_Textures="Textures";
	private const string AssetType_Sounds="Sounds";
	private const string AssetType_Shaders="Shaders";

	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='CustomAssetPath']"/>
	/// <br/>
	/// {mod.Name}/Assets/{assetType}/{name}
	/// </summary>
	public static string CustomAssetPath(this Mod mod,string assetType,string name)
	{
		return $"{mod.Name}/Assets/{assetType}/{name}";
	}
	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='CustomAssetPath']"/>
	/// This one is for a single asset associated with specific content, such as the main texture of an entity.<br/>
	/// <br/>
	/// {type.Mod.Name}/Assets/{assetType}/{type.AssetCategory}/{type.Name}
	/// </summary>
	public static string ContentCustomAssetPath<T>(this T type,string assetType) where T : ModType,IColonyContent
	{
		return CustomAssetPath(type.Mod,assetType,$"{type.AssetCategory}/{type.Name}");
	}
	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='CustomAssetPath']"/>
	/// This one is for assets associated with specific content, but unlike <see cref="ContentCustomAssetPath"/>, allows for multiple. Thier usage is not mutually exclusive, Ex. a main texture and extra textures (like overlays).<br/>
	/// <br/>
	/// {type.Mod.Name}/Assets/{assetType}/{type.AssetCategory}/{type.Name}/{name}
	/// </summary>
	public static string ContentExtraCustomAssetPath<T>(this T type,string assetType,string name) where T : ModType,IColonyContent
	{
		return CustomAssetPath(type.Mod,assetType,$"{type.AssetCategory}/{type.Name}/{name}");
	}

	/// <summary>
	/// Same as its CustomAsset counterpart, but with assetType already set.
	/// </summary>
	public static string TexturePath(this Mod mod,string name)
	{
		return CustomAssetPath(mod,AssetType_Textures,name);
	}
	/// <inheritdoc cref="TexturePath"/>
	public static string ContentTexturePath<T>(this T type) where T : ModType,IColonyContent
	{
		return ContentCustomAssetPath(type,AssetType_Textures);
	}
	/// <inheritdoc cref="TexturePath"/>
	public static string ContentExtraTexturePath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return ContentExtraCustomAssetPath(type,AssetType_Textures,name);
	}

	/// <inheritdoc cref="TexturePath"/>
	public static string SoundPath(this Mod mod,string name)
	{
		return CustomAssetPath(mod,AssetType_Sounds,name);
	}
	/// <inheritdoc cref="SoundPath"/>
	public static string ContentSoundPath<T>(this T type) where T : ModType,IColonyContent
	{
		return ContentCustomAssetPath(type,AssetType_Sounds);
	}
	/// <inheritdoc cref="SoundPath"/>
	public static string ContentExtraSoundPath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return ContentExtraCustomAssetPath(type,AssetType_Sounds,name);
	}

	/// <inheritdoc cref="TexturePath"/>
	public static string ShaderPath(this Mod mod,string name)
	{
		return CustomAssetPath(mod,AssetType_Shaders,name);
	}
	/// <inheritdoc cref="ShaderPath"/>
	public static string ContentShaderPath<T>(this T type) where T : ModType,IColonyContent
	{
		return ContentCustomAssetPath(type,AssetType_Shaders);
	}
	/// <inheritdoc cref="ShaderPath"/>
	public static string ContentExtraShaderPath<T>(this T type,string name) where T : ModType,IColonyContent
	{
		return ContentExtraCustomAssetPath(type,AssetType_Shaders,name);
	}

	/// <summary>
	/// The path to <see cref="EmptyTexture"/>:<br/>
	/// <br/>
	/// <inheritdoc cref="EmptyTexture"/>
	/// </summary>
	public static readonly string EmptyTexturePath=TexturePath(ColonyLib.Instance,"Empty");
	/// <summary>
	/// A transparent 1x1 pixel texture, suitable for when a texture is required, but a proper one is not needed.<br/>
	/// Ex. entities need a texture to load, even if it's never used.
	/// </summary>
	public static Asset<Texture2D> EmptyTexture=>ModContent.Request<Texture2D>(EmptyTexturePath);
	/// <summary>
	/// The path to vanilla noise texture.
	/// </summary>
	public static readonly string VanillaNoisePath="Images/Misc/noise";
}