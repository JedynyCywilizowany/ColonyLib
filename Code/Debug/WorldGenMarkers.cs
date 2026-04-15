using System.IO;
using Terraria.ModLoader;

namespace ColonyLib.Debug;

partial class ColonyDebug
{
	/// <summary>
	/// Makes this world's map permanently display an icon with the given texture at the specified position.<br/>
	/// Designed to help with locating points of interest when debugging WorldGen.<br/>
	/// If one was already present on the same position, it will be replaced.<br/>
	/// Can technically be used in-game, but not from multiplayer clients, and if from the server, <see cref="MessageID.WorldData"/> will need to be manually sent.
	/// </summary>
	public static void AddWorldGenMarker(Point position,string texture)
	{
	}
	/// <inheritdoc cref="AddWorldGenMarker(Point,string)"/>
	public static void AddWorldGenMarker(int x,int y,string texture)
	{
		AddWorldGenMarker(new(x,y),texture);
	}
	
	/// <summary>
	/// If a WorldGen Marker is present at this position, removes it.
	/// </summary>
	public static void RemoveWorldGenMarker(Point position)
	{
	}
	/// <inheritdoc cref="RemoveWorldGenMarker(Point)"/>
	public static void RemoveWorldGenMarker(int x,int y)
	{
		RemoveWorldGenMarker(new(x,y));
	}
	
	/// <summary>
	/// Removes all WorldGen Markers.
	/// </summary>
	public static void ClearWorldGenMarkers()
	{
	}
}