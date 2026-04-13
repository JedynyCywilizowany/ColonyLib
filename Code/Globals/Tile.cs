using ColonyLib.TileEntities;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ColonyLib.Globals;

public class ColonyGlobalTile : GlobalTile
{
	public override void KillTile(int x,int y,int type,ref bool fail,ref bool effectOnly,ref bool noItem)
	{
		if (!fail&&!effectOnly&&TileID.Sets.IsAMechanism[type]&&TileEntity.TryGet(x,y,out MechCooldown tileEntity))
		{
			tileEntity.Kill(x,y);
		}
	}
}