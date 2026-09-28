using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace ColonyLib;

partial class ColonyUtils
{
	/// <summary>
	/// Divides an int.<br/>
	/// The remainder is then used to randomly add 1 to the result, so it averages to the real quotient.
	/// </summary>
	public static int AveragedDivide(this int x,int divideBy)
	{
		int r=x/divideBy;
		if (Main.rand.NextBool(x%divideBy,divideBy)) r++;
		return r;
	}

	/// <summary>
	/// Casts the given float-point number to int.<br/>
	/// Randomly rounds up or down depending on the fractional part, averages to the original value.
	/// </summary>
	public static int AveragedInt(this float x)
	{
		int r=(int)MathF.Floor(x);
		if (x%1>Main.rand.NextFloat()) r++;
		return r;
	}
	/// <inheritdoc cref="AveragedInt(float)"/>
	public static int AveragedInt(this double x)
	{
		int r=(int)Math.Floor(x);
		if (x%1>Main.rand.NextDouble()) r++;
		return r;
	}

	/// <summary>
	/// Multiplies the value by 100, then rounds it to the specified amount of fractional digits.<br/>
	/// The value is rounded towards 50%, so that a value larger than 0 is never 0%, and a value lesser than 1 is never 100%.
	/// </summary>
	public static float ToPercentage(this float value,int fractionalDigits=0)
	{
		return MathF.Round(value*100,fractionalDigits,(value<0.5f ? MidpointRounding.ToPositiveInfinity : MidpointRounding.ToNegativeInfinity));
	}
	/// <inheritdoc cref="ToPercentage(float,int)"/>
	public static double ToPercentage(this double value,int fractionalDigits=0)
	{
		return Math.Round(value*100,fractionalDigits,(value<0.5 ? MidpointRounding.ToPositiveInfinity : MidpointRounding.ToNegativeInfinity));
	}

	public static Vector2 LerpVector2(Vector2 value1,Vector2 value2,float amount)
	{
		return new(MathHelper.Lerp(value1.X,value2.X,amount),MathHelper.Lerp(value1.Y,value2.Y,amount));
	}
	public static Vector2 LerpVector2Clamped(Vector2 value1,Vector2 value2,float amount)
	{
		return LerpVector2(value1,value2,Math.Clamp(amount,0,1));
	}
}