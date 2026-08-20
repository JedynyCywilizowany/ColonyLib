using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

using SmallPoint=(sbyte x,sbyte y);

namespace ColonyLib;

partial class ColonyUtils
{
	private static readonly List<ImmutableArray<SmallPoint>?> circleCache=[[new(0,0)]];
	private static void ChacheCircle(int radius)
	{
		if ((uint)radius>(uint)sbyte.MaxValue) throw new ArgumentOutOfRangeException(nameof(radius));

		if (circleCache.Count<=radius) CollectionsMarshal.SetCount(circleCache,radius+1);
		if (!circleCache[radius].HasValue)
		{
			List<SmallPoint> points=new();
			var radiusSQ=radius*radius;
			for (int y=-radius;y<=radius;y++) for (int x=-radius;x<=radius;x++)
			{
				if ((x*x)+(y*y)<=radiusSQ) points.Add(((sbyte)x,(sbyte)y));
			}
			circleCache[radius]=points.OrderBy((point)=>Math.Abs((int)point.x)+Math.Abs((int)point.y)).ToImmutableArray();
		}
	}
	/// <summary>
	/// Used to get a perfect circle out of tiles in high-performance scenarios.<br/>
	/// For technical reasons, radius must be within the range 0 - 127.<br/>
	/// Radius 0 is a single tile.<br/>
	/// <br/>
	/// The returned collection is to be enumerated, it contains the offset from center for every tile in the circle.<br/>
	/// Increases performance by reusing generated circle-arrays for every call with the same <paramref name="radius"/> parameter.
	/// </summary>
	public static IEnumerable<Point> GetCachedCircle(int radius)
	{
		ChacheCircle(radius);
		foreach (var (x,y) in circleCache[radius]!.Value)
		{
			yield return new Point(x,y);
		}
	}
}