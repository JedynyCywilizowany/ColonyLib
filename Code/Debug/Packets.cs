using System.IO;
using ColonyLib.ContentBases;
using Terraria.ModLoader;

namespace ColonyLib.Debug;

partial class ColonyDebug
{
	internal static bool ShouldReportPackets()
	{
		return false;
	}

	internal static void ReportPacket(ColonyPacket packet,ColonyPacketType type,int toClient,int ignoreClient)
	{
		/*
		static string NameId(int id)
		{
			if (id<0||id>byte.MaxValue) return "(Error)";
			return $"({id}: {(id==byte.MaxValue ? "Server" : Main.player[id].name)})";
		}
		var message=$"{NameId(Main.myPlayer)} sent {type.FullName} of size: {packet.OutStream.Length}{(Main.dedServ ? (toClient>=0 ? ", to: "+NameId(toClient) : (ignoreClient>=0 ? ", ignoring: "+NameId(ignoreClient) : "")) : "")}";
		if (Main.dedServ) ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message),Color.Cyan);
		else ChatHelper.SendChatMessageFromClient(new ChatMessage($"[c/{Color.Cyan.Hex3()}:{message}]"));
		*/
	}
}