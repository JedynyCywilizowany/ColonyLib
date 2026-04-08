using System;
using ColonyLib.ContentBases;
using MonoMod.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace ColonyLib;

partial class ColonyLib
{
	private static void LoadILEditsAndDetours()
	{
		try
		{
			IL_PlayerDrawLayers.DrawPlayer_16_ArmorLongCoat+=(il)=>
			{
				ILCursor c=new(il);

				c.GotoNext(MoveType.After,
					(ins)=>ins.MatchLdfld(typeof(Player).GetField(nameof(Player.body))!),
					(ins)=>ins.MatchStloc(out _)
				);
				c.Prev!.MatchStloc(out var bodyInputVar);
				
				c.GotoNext(MoveType.Before,
					(ins)=>ins.MatchLdloc(out _),
					(ins)=>ins.MatchLdcI4(-1),
					(ins)=>ins.MatchBeq(out _)
				);
				c.Next!.MatchLdloc(out var legsOutputVar);
				c.GotoNext(MoveType.Before,(ins)=>ins.MatchBeq(out _));
				c.Next!.MatchBeq(out var endLabel);

				var normalLabel=c.DefineLabel();

				c.Remove();
				c.EmitCeq();
				c.EmitBrfalse(normalLabel);

				c.EmitLdloc(bodyInputVar);
				c.EmitLdcI4(ArmorIDs.Body.Count);
				c.EmitBlt(normalLabel);

				c.EmitLdarg0();
				c.EmitLdfld(typeof(PlayerDrawSet).GetField(nameof(PlayerDrawSet.drawPlayer))!);

				static int Insertion(Player drawPlayer)
				{
					var item=ColonyUtils.DummyItems[Item.bodyType[drawPlayer.body]];
					if (item.ModItem is ColonyItem cywilsItem)
					{
						return cywilsItem.BodyArmorLegsOverlay(drawPlayer.Male);
					}
					return -1;
				}
				c.EmitCallFromDelegate(Insertion);
				
				c.EmitStloc(legsOutputVar);
				c.MarkLabel(normalLabel);
				c.EmitLdloc(legsOutputVar);
				c.EmitLdcI4(0);
				c.EmitBlt(endLabel!);
			};
		}
		catch (Exception e)
		{
			Instance.Logger.Error(e.Message);
		}
		/*
		for (int i=0;i<il.Instrs.Count;i++) il.Instrs[i].Offset=i;
		MonoModHooks.DumpIL(Instance,c.Context);
		*/
	}
}