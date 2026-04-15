using Terraria.ModLoader;

namespace ColonyLib.Debug;

/// <summary>
/// Helper functions to interact with Colony Debugger, if it's present.<br/>
/// If not, they simply do nothing, so they can be safely used without a check.<br/>
/// If you still need to make sure, use <see cref="IsDebugMode"/>.
/// </summary>
public static partial class ColonyDebug
{
	internal static Mod? debuggerMod=null;
	/// <summary>
	/// Whether Colony Debugger is present and loaded.<br/>
	/// If false, none of the other functions in this class do anything.
	/// </summary>
	public static bool IsDebugMode=>(debuggerMod is not null);
}