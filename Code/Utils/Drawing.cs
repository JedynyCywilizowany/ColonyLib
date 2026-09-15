using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace ColonyLib;

partial class ColonyUtils
{
	/// <summary>
	/// Doesn't support gradients, but is otherwise a superior alternative to <see cref="Utils.DrawLine(SpriteBatch,Vector2,Vector2,Color,Color,float)"/>
	/// </summary>
	public static void ColonyDrawLine(this SpriteBatch spriteBatch,Vector2 start,Vector2 end,float width,Color color)
	{
		float halfWidth=width/2;
		float rotation=start.AngleTo(end);
		var start2=start-Main.screenPosition+((rotation-(MathF.PI/2)).ToRotationVector2()*halfWidth);
		Rectangle rect=new((int)start2.X,(int)start2.Y,(int)MathF.Ceiling(start.Distance(end)),(int)width);
		spriteBatch.Draw(TextureAssets.BlackTile.Value,rect,null,color,rotation,default,SpriteEffects.None,0);
	}
}