using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace ColonyLib.Debug;

public class WorldGenMarkersLayer : ModMapLayer
{
	public override void Draw(ref MapOverlayDrawContext context,ref string text)
	{
		foreach (var marker in ColonyDebugSystem.worldGenMarkers)
		{
			if (ModContent.RequestIfExists<Texture2D>(marker.Value,out var texture))
			{
				context.Draw(texture.Value,marker.Key,Color.White,new SpriteFrame(1,1,0,0),1.5f,1.5f,Alignment.Center);
			}
			else context.Draw(TextureAssets.Confuse.Value,marker.Key,Alignment.Center);
		}
	}
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ColonyLib.IsDebugMode;
	}
}
partial class ColonyDebugSystem
{
	internal static Dictionary<Vector2,string> worldGenMarkers=null!;

	/// <summary>
	/// Makes this world's map permanently display an icon with the given texture at the specified position.<br/>
	/// Designed to help with locating points of interest when debugging WorldGen.<br/>
	/// Currently does not work in multiplayer.
	/// </summary>
	public static void AddWorldGenMarker(Point position,string texture)
	{
		worldGenMarkers[position.ToVector2()+new Vector2(0.5f)]=texture;
	}

	private const string SavingTag_WorldGenMarkers_Positions=nameof(worldGenMarkers)+"_Positions";
	private const string SavingTag_WorldGenMarkers_Textures=nameof(worldGenMarkers)+"_Textures";
	public override void SaveWorldData(TagCompound tag)
	{
		tag.AddIfNotEmpty(SavingTag_WorldGenMarkers_Positions,worldGenMarkers.Keys.Select((p)=>p.ToPoint()).ToArray());
		tag.AddIfNotEmpty(SavingTag_WorldGenMarkers_Textures,worldGenMarkers.Values.ToArray());
	}
	public override void LoadWorldData(TagCompound tag)
	{
		var poss=tag.Get<Point[]>(SavingTag_WorldGenMarkers_Positions);
		var texs=tag.Get<string[]>(SavingTag_WorldGenMarkers_Textures);

		worldGenMarkers=new(poss.Zip(texs).Select((pt)=>new KeyValuePair<Vector2,string>(pt.First.ToVector2()+new Vector2(0.5f),pt.Second)));
	}
	public override void ClearWorld()
	{
		worldGenMarkers=new();
	}
}