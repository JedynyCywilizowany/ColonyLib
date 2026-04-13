using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ColonyLib.TileEntities;

/// <summary>
/// A tile entity storing the cooldown for simple mechanisms, mostly traps.<br/>
/// Valid for all tiles in <see cref="TileID.Sets.IsAMechanism"/>.<br/>
/// In multiplayer, should only be accessed from the server.<br/>
/// </summary>
public class MechCooldown : ModTileEntity
{
	/// <summary>
	/// Sets the cooldown for this tile.<br/>
	/// If the <see cref="TileEntity"/> is not present, attempts to place it.
	/// </summary>
	public static void SetCooldown(int x,int y,int cooldown)
	{
		if (!TryGet(x,y,out MechCooldown tileEntity))
		{
			PlaceEntityNet(x,y,ModContent.TileEntityType<MechCooldown>());
			if (!TryGet(x,y,out tileEntity))
			{
				Projectile.NewProjectile(new EntitySource_Misc(""),new Point(x,y).ToWorldCoordinates(),Vector2.Zero,ProjectileID.DaybreakExplosion,1,20);
				return;
			}
		}
		tileEntity.timeLeft=cooldown;
	}
	/// <summary>
	/// If this tile has a cooldown, returns false and does nothing,<br/>
	/// otherwise returns true, places the <see cref="TileEntity"/> if not present and sets the <paramref name="cooldown"/> parameter as cooldown.<br/>
	/// Also returns false if the tile does not have this <see cref="TileEntity"/> and it cannot be placed.
	/// </summary>
	public static bool Apply(int x,int y,int cooldown)
	{
		if (!TryGet(x,y,out MechCooldown tileEntity))
		{
			PlaceEntityNet(x,y,ModContent.TileEntityType<MechCooldown>());
			if (!TryGet(x,y,out tileEntity))
			{
				Projectile.NewProjectile(new EntitySource_Misc(""),new Point(x,y).ToWorldCoordinates(),Vector2.Zero,ProjectileID.DaybreakExplosion,1,20);
				return false;
			}
		}
		if (tileEntity.timeLeft>0) return false;

		tileEntity.timeLeft=cooldown;
		return true;
	}
	/// <summary>
	/// Returns whether the tile has this <see cref="TileEntity"/> with an active cooldown.
	/// </summary>
	public static bool HasCooldown(int x,int y)
	{
		if (TryGet(x,y,out MechCooldown tileEntity)) return tileEntity.timeLeft>0;
		return false;
	}

	public int timeLeft;
	public override bool IsTileValidForEntity(int x, int y)
	{
		var tile=Main.tile[x,y];
		return tile.HasTile&&TileID.Sets.IsAMechanism[tile.TileType];
	}
	public override void Update()
	{
		if (timeLeft>0) timeLeft--;
	}
}