using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ColonyLib;

partial class ColonyUtils
{
	/// <summary>
	/// Creates a span over the entirety of the specified multi-dimensional array of unmanaged structs.<br/>
	/// Make sure <typeparamref name="T"/> matches the type of the array's elements, as there are no warnings if not, and that might lead to serious issues.
	/// </summary>
	public static unsafe Span<T> MultiDimArraySpan<T>(this Array array) where T : unmanaged
	{
		return new(Unsafe.AsPointer(ref MemoryMarshal.GetArrayDataReference(array)),array.Length);
	}
	
	/// <summary>
	/// Whether the length of each dimension of the array matches the respective entry of the <paramref name="dimensions"/> parameter.<br/>
	/// Returns false if the length of the <paramref name="dimensions"/> parameter does not match the array's rank.
	/// </summary>
	public static bool IsOfSize(this Array array,params int[] dimensions)
	{
		if (array.Rank!=dimensions.Length) return false;
		for (int i=0;i<dimensions.Length;i++)
		{
			if (array.GetLength(i)!=dimensions[i]) return false;
		}

		return true;
	}
}