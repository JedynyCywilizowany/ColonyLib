using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace ColonyLib;

partial class ColonyUtils
{
	private static void SetShader(this SpriteBatch spriteBatch,Effect effect,Matrix matrix)
	{
		spriteBatch.End();
		spriteBatch.Begin(
			SpriteSortMode.Deferred,
			BlendState.AlphaBlend,
			SamplerState.LinearClamp,
			DepthStencilState.None,
			RasterizerState.CullCounterClockwise,
			effect,
			matrix
		);
	}

	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='SetShader']"/>
	/// This variant's scale is the zoom, used to draw tiles, NPCs, projectiles and such.
	/// </summary>
	public static void SetShaderWorld(this SpriteBatch spriteBatch,Effect effect)
	{
		SetShader(spriteBatch,effect,Main.GameViewMatrix.ZoomMatrix);
	}
	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='ResetShader']"/>
	/// </summary>
	public static void ResetShaderWorld(this SpriteBatch spriteBatch)
	{
		SetShaderWorld(spriteBatch,null!);
	}

	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='SetShader']"/>
	/// This variant uses the UI scale, used for drawing UI elements, obviously.
	/// </summary>
	public static void SetShaderUI(this SpriteBatch spriteBatch,Effect effect)
	{
		SetShader(spriteBatch,effect,Main.UIScaleMatrix);
	}
	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='ResetShader']"/>
	/// </summary>
	public static void ResetShaderUI(this SpriteBatch spriteBatch)
	{
		SetShaderUI(spriteBatch,null!);
	}

	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='SetShader']"/>
	/// This variant uses the default scale, used mainly for drawing backgrounds.
	/// </summary>
	public static void SetShaderUnscaled(this SpriteBatch spriteBatch,Effect effect)
	{
		SetShader(spriteBatch,effect,Matrix.Identity);
	}
	/// <summary>
	/// <include file="Docs.xml" path="doc/member[@name='ResetShader']"/>
	/// </summary>
	public static void ResetShaderUnscaled(this SpriteBatch spriteBatch)
	{
		SetShaderUnscaled(spriteBatch,null!);
	}

	/// <summary>
	/// Calls UseColor with the provided <paramref name="color"/>, and UseOpacity with the <paramref name="color"/>'s alpha converted into a float in the range 0-1
	/// </summary>
	public static MiscShaderData UseColorAndOpacity(this MiscShaderData shader,Color color)
	{
		return shader.UseColor(color).UseOpacity(color.A/(float)byte.MaxValue);
	}
	/// <inheritdoc cref="UseColorAndOpacity(MiscShaderData,Color)"/>
	public static ArmorShaderData UseColorAndOpacity(this ArmorShaderData shader,Color color)
	{
		return shader.UseColor(color).UseOpacity(color.A/(float)byte.MaxValue);
	}
	/// <inheritdoc cref="UseColorAndOpacity(MiscShaderData,Color)"/>
	public static ScreenShaderData UseColorAndOpacity(this ScreenShaderData shader,Color color)
	{
		return shader.UseColor(color).UseOpacity(color.A/(float)byte.MaxValue);
	}
}