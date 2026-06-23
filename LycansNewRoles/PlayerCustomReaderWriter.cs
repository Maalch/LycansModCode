using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Fusion;

namespace LycansNewRoles;

[StructLayout(LayoutKind.Sequential, Size = 1)]
internal struct PlayerCustomReaderWriter : IElementReaderWriter<PlayerRef>
{
	public static IElementReaderWriter<PlayerRef> Instance;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe PlayerRef Read(byte* data, int index)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return (PlayerRef)data[index * 4];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe ref PlayerRef ReadRef(byte* data, int index)
	{
		return ref *(PlayerRef*)(data + index * 4);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe void Write(byte* data, int index, PlayerRef val)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Unsafe.Write(data + index * 4, val);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int GetElementWordCount()
	{
		return 1;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IElementReaderWriter<PlayerRef> GetInstance()
	{
		if (Instance == null)
		{
			Instance = default(PlayerCustomReaderWriter);
		}
		return Instance;
	}
}
