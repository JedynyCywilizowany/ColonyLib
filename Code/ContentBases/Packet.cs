using System;
using System.Buffers;
using System.IO;
using ColonyLib.Debug;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace ColonyLib.ContentBases;

/// <summary>
/// Designed to help with managing mod packets
/// </summary>
public abstract class ColonyPacketType : ModType
{
	private static ColonyPacketType[] all=[];
	internal static ColonyPacketType ByID(int id)
	{
		return all[id];
	}
	public int ID{get;private set;}
	protected sealed override void Register()
	{
		ID=all.Length;
		Array.Resize(ref all,ID+1);
		all[ID]=this;
		ModTypeLookup<ColonyPacketType>.Register(this);
	}
	protected sealed override void ValidateType()
	{
		if (!(LoaderUtils.HasOverride(this,(t)=>t.HandleServer)||LoaderUtils.HasOverride(this,(t)=>t.HandleClient)||LoaderUtils.HasOverride(this,(t)=>t.Handle))) throw new Exception($"{FullName} must override at least one of: {nameof(HandleServer)}, {nameof(HandleClient)}, {nameof(Handle)}");
	}

	public sealed override void SetupContent()
	{
		SetStaticDefaults();
	}
	
	/// <summary>
	/// Used to get a packet of this type.<br/>
	/// </summary>
	public ColonyPacket Get()
	{
		var p=new ColonyPacket(this);
		OnCreated(p);
		return p;
	}
	/// <summary>
	/// Used to get a packet of the given type.<br/>
	/// </summary>
	public static ColonyPacket Get<T>() where T : ColonyPacketType
	{
		return ModContent.GetInstance<T>().Get();
	}

	private void Redistribute(BinaryReader reader,int sender,int length)
	{
		var startIndex=reader.BaseStream.Position;
		var p=new ColonyPacket(this)
		{
			origSender=(byte)sender
		};

		var buffer=ArrayPool<byte>.Shared.Rent(length);
		try
		{
			reader.Read(buffer,0,length);
			p.Write(buffer,0,length);
		}
		finally
		{
			ArrayPool<byte>.Shared.Return(buffer);
		}

		p.Send(ignoreClient:sender);
		reader.BaseStream.Position=startIndex;
	}
	/// <summary>
	/// Calls <see cref="Get"/> and immediately sends its result.<br/>
	/// Meant to be used with packets that need no further data (ex. informing of an event having taken place) or already set all thier data in <see cref="OnCreated"/>.
	/// </summary>
	public static void SendNew<T>(int toClient=-1,int ignoreClient=-1) where T : ColonyPacketType
	{
		Get<T>().Send(toClient,ignoreClient);
	}
	
	internal static void Receive(BinaryReader reader,int whoAmI)
	{
		var packetType=ByID(reader.Read7BitEncodedInt());
		if (Main.dedServ)
		{
			if (packetType.AutoRedistributed)
			{
				int length=reader.Read7BitEncodedInt();

				packetType.Redistribute(reader,whoAmI,length);
			}
			packetType.HandleServer(reader,whoAmI);
		}
		else
		{
			int origSender=(packetType.AutoRedistributed ? reader.ReadByte() : byte.MaxValue);
			packetType.HandleClient(reader,origSender);
		}
	}

	/// <summary>
	/// Called whenever a packet of this typeID is created, before it's returned.<br/>
	/// Could be used to instantly fill its data for use with <see cref="SendNew"/>.
	/// </summary>
	public virtual void OnCreated(BinaryWriter writer)
	{
	}
	/// <summary>
	/// If true, whenever the server receives a packet of this type it will automatically send copies to other clients.
	/// </summary>
	public virtual bool AutoRedistributed=>false;
	/// <summary>
	/// Called when the server receives this packet.<br/>
	/// See <see cref="Mod.HandlePacket"/> for more information.<br/>
	/// <br/>
	/// <b>NOTE:</b> a few leading bytes representing the packet's internal data have already been read.
	/// </summary>
	/// <param name="whoAmI">Index of the client that sent this packet.</param>
	public virtual void HandleServer(BinaryReader reader,int whoAmI)
	{
		Handle(reader,whoAmI);
	}
	/// <summary>
	/// Called when the client receives this packet.<br/>
	/// See <see cref="Mod.HandlePacket"/> for more information.<br/>
	/// <br/>
	/// <b>NOTE:</b> a few leading bytes representing the packet's internal data have already been read.
	/// </summary>
	/// <param name="whoAmI">If <see cref="AutoRedistributed"/> is true and the packet is redistributed, this is the index of the client that originally sent the packet.<br/>Otherwise it 255 (the server's index)</param>
	public virtual void HandleClient(BinaryReader reader,int whoAmI)
	{
		Handle(reader,whoAmI);
	}
	/// <summary>
	/// Called by default by <see cref="HandleServer"/> and <see cref="HandleClient"/>, making it useful if both do the same thing.
	/// </summary>
	public virtual void Handle(BinaryReader reader,int whoAmI)
	{
		if (Main.dedServ&&AutoRedistributed) return;
		throw new NotImplementedException($"{FullName} does not support handling on the {(Main.dedServ ? "server" : "client")}.");
	}
}
public class ColonyPacket : BinaryWriter
{
	private readonly int typeID;
	internal byte origSender=byte.MaxValue;
	private ModPacket? underlyingPacket;
	internal ColonyPacket(ColonyPacketType packetType) : base(new MemoryStream(16))
	{
		typeID=packetType.ID;
	}
	/// <summary>
	/// Similar to <see cref="ModPacket.Send"/>
	/// </summary>
	public void Send(int toClient=-1,int ignoreClient=-1)
	{
		if (underlyingPacket is null)
		{
			underlyingPacket=ColonyLib.Instance.GetPacket();
			underlyingPacket.Write7BitEncodedInt(typeID);

			var type=ColonyPacketType.ByID(typeID);

			if (type.AutoRedistributed)
			{
				if (Main.dedServ) underlyingPacket.Write(origSender);
				else underlyingPacket.Write7BitEncodedInt((int)OutStream.Length);
			}

			if (!Main.gameMenu&&ColonyDebug.ShouldReportPackets())
			{
				ColonyDebug.ReportPacket(this,type,(int)OutStream.Length,toClient,ignoreClient);
			}
			
			OutStream.Position=0;
			OutStream.CopyTo(underlyingPacket.BaseStream);

			Close();
		}
		underlyingPacket.Send(toClient,ignoreClient);
	}
}