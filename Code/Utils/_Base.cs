using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ColonyLib;

public static partial class ColonyUtils
{
	/// <summary>
	/// Alphabethically compares the display names of two item types in the current language.
	/// </summary>
	public static readonly Comparer<int> itemSortByNameComparer=Comparer<int>.Create((a,b)=>string.Compare(DummyItems[a].Name,DummyItems[b].Name));

	/// <summary>
	/// If <paramref name="value"/> is not empty, ensures the tooltip exists and sets its value,<br/>
	/// otherwise, removes the tooltip.
	/// </summary>
	public static void UpdateTooltip(this List<TooltipLine> tooltips,Mod mod,string name,string value,Color? color=null)
	{
		bool MatchLine(TooltipLine l)
		{
			return l.Name==name&&l.Mod==mod.Name;
		}

		if (value=="")
		{
			var line=tooltips.FindIndex(MatchLine);
			if (line>=0) tooltips.RemoveAt(line);
		}
		else
		{
			var line=tooltips.FindIndex(MatchLine);
			if (line<0) tooltips.Add(new TooltipLine(mod,name,value){OverrideColor=color});
			else tooltips[line].Text=value;
		}
	}
	/// <inheritdoc cref="UpdateTooltip(List{TooltipLine},Mod,string,string,Color?)"/>
	public static void UpdateTooltip(this List<TooltipLine> tooltips,Mod mod,string name,LocalizedText value,Color? color=null)
	{
		UpdateTooltip(tooltips,mod,name,value.Value,color);
	}

	/// <summary>
	/// Creates a string that displays the specified amount of coins using item chat tags.
	/// </summary>
	public static string ValueToCoinsCompact(long value)
	{
		StringBuilder builder=new((2*4+2)+((4+2)*4+2));

		if (value>=Item.platinum)
		{
			builder.Append(value/Item.platinum);
			builder.Append("[i:"+ItemID.PlatinumCoin+"]");
		}
		if (value>=Item.gold)
		{
			builder.Append(value/Item.gold%100);
			builder.Append("[i:"+ItemID.GoldCoin+"]");
		}
		if (value>=Item.silver)
		{
			builder.Append(value/Item.silver%100);
			builder.Append("[i:"+ItemID.SilverCoin+"]");
		}
		builder.Append(value%100);
		builder.Append("[i:"+ItemID.CopperCoin+"]");

		return builder.ToString();
	}

	/// <summary>
	/// Contains item IDs of coins, from copper to platinum.
	/// </summary>
	public static readonly ImmutableArray<int> CoinTypes=[ItemID.CopperCoin,ItemID.SilverCoin,ItemID.GoldCoin,ItemID.PlatinumCoin];
	/// <summary>
	/// Spawns the specified amount of coins on the player.<br/>
	/// Can be called on both server and client.
	/// </summary>
	public static void GiveMoney(this Player player,int amount)
	{
		foreach (var coinType in CoinTypes)
		{
			if (amount<=0) break;

			player.QuickSpawnItem(player.GetSource_DropAsItem(),coinType,amount%100);
			amount/=100;
		}
	}

	/// <summary>
	/// Whether this player's <see cref="Entity.whoAmI"/> matches <see cref="Main.myPlayer"/>.
	/// </summary>
	public static bool IsLocal(this Player player)
	{
		return player.whoAmI==Main.myPlayer;
	}
	/// <summary>
	/// Whether this item's <see cref="Item.playerIndexTheItemIsReservedFor"/> matches <see cref="Main.myPlayer"/>.
	/// </summary>
	public static bool IsReservedHere(this Item item)
	{
		return item.playerIndexTheItemIsReservedFor==Main.myPlayer;
	}
	
	/// <summary>
	/// Emits <see cref="OpCodes.Call"/> to the method this delegate points to.<br/>
	/// Only works for delegates pointing to a single, static, non-lambda function.
	/// </summary>
	public static void EmitCallFromDelegate(this ILCursor c,Delegate func)
	{
		c.EmitCall(func.GetMethodInfo());
	}
}