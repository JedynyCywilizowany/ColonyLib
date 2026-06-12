using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace ColonyLib;

partial class ColonyUtils
{
	private static readonly List<ImmutableArray<Point>?> circleCache=[[new(0,0)]];
	private static void ChacheCircle(int radius)
	{
		if ((uint)radius>byte.MaxValue) throw new ArgumentOutOfRangeException(nameof(radius));

		if (circleCache.Count<=radius) CollectionsMarshal.SetCount(circleCache,radius+1);
		if (!circleCache[radius].HasValue)
		{
			List<Point> points=new();
			var radiusSQ=radius*radius;
			for (int y=-radius;y<=radius;y++) for (int x=-radius;x<=radius;x++)
			{
				if ((x*x)+(y*y)<=radiusSQ) points.Add(new(x,y));
			}
			circleCache[radius]=points.OrderBy((point)=>point.ManhattanDistance(Point.Zero)).ToImmutableArray();
		}
	}
	/// <summary>
	/// Used to get a perfect circle out of tiles in high-performance scenarios.<br/>
	/// Radius must be within the range of <see cref="byte"/>.<br/>
	/// Radius 0 is a single tile.<br/>
	/// <br/>
	/// The returned array is to be enumerated, it contains the offset from center for every tile in the circle.<br/>
	/// Increases performance by reusing generated circle-arrays for every call with the same <paramref name="radius"/> parameter.
	/// </summary>
	public static ImmutableArray<Point> GetCachedCircle(int radius)
	{
		ChacheCircle(radius);
		return circleCache[radius]!.Value;
	}
}