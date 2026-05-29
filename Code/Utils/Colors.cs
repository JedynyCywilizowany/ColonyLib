using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Utilities;

namespace ColonyLib;

partial class ColonyUtils
{
	public static Color RandomColor(UnifiedRandom randomizer)
	{
		Color color=default;
		color.PackedValue=((uint)randomizer.Next(0x00ffffff+1))|0xff000000;
		return color;
	}
	public static Color RandomColor()
	{
		return RandomColor(Main.rand);
	}

	public static Color InvertedColor(Color color)
	{
		Color output=default;
		output.PackedValue=color.PackedValue^0x00ffffff;
		return output;
	}
}