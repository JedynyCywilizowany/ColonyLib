using System.Runtime.CompilerServices;
using ColonyLib.ContentBases;

namespace ColonyLib.Debug;

partial class ColonyDebug
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static bool ShouldReportPackets()
	{
		return false;
	}
	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static void ReportPacket(ColonyPacket packet,ColonyPacketType type,int size,int toClient,int ignoreClient)
	{
	}
}