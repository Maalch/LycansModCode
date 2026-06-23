using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Fusion;
using UnityEngine;
using UnityEngine.Scripting;

namespace LycansNewRoles.NewMaps.Components;

[NetworkBehaviourWeaved(2)]
public class MechanismButton : NetworkBehaviour
{
	public string MechanismId;

	private List<MechanismObject> _associatedObjects = new List<MechanismObject>();

	public int MapID;

	private void Start()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if ((Object)(object)GameManager.Instance == (Object)null)
			{
				return;
			}
			float? num = null;
			IEnumerable<GameObject> enumerable = from o in Object.FindObjectsOfType<GameObject>()
				where o.gameObject.activeSelf && ((Object)o).name == "MechanismButtons"
				select o;
			Vector3 val = default(Vector3);
			((Vector3)(ref val))._002Ector(((Component)this).transform.position.x, ((Component)this).transform.position.y, ((Component)this).transform.position.z);
			Transform parent = null;
			foreach (GameObject item in enumerable)
			{
				for (int num2 = 0; num2 < item.transform.childCount; num2++)
				{
					Transform child = item.transform.GetChild(num2);
					float num3 = Vector3.Distance(val, ((Component)child).transform.position);
					if (!num.HasValue || num3 < num.Value)
					{
						num = num3;
						parent = child;
					}
				}
			}
			((Component)this).gameObject.transform.parent = parent;
			MapManager.RescaleSpawnedObject(((Component)this).gameObject, ((Component)((Component)this).transform.parent).gameObject, MapManager.NewMapsByIdInfo[GameManager.Instance.MapID]);
			_associatedObjects = (from o in Object.FindObjectsOfType<MechanismObject>(false)
				where ((Component)o).gameObject.activeSelf && o.MechanismId == MechanismId
				select o).ToList();
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("MechanismButton start error: " + ex));
		}
	}

	[Rpc(/*Could not decode attribute arguments.*/)]
	public unsafe void Rpc_Activate(PlayerRef actor)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			if (!base.InvokeRpc)
			{
				NetworkBehaviourUtils.ThrowIfBehaviourNotInitialized((NetworkBehaviour)(object)this);
				if ((int)((SimulationBehaviour)this).Runner.Stage == 4)
				{
					return;
				}
				int localAuthorityMask = ((SimulationBehaviour)this).Object.GetLocalAuthorityMask();
				if ((localAuthorityMask & 7) == 0)
				{
					NetworkBehaviourUtils.NotifyLocalSimulationNotAllowedToSendRpc("System.Void Door::Rpc_Activate(Fusion.PlayerRef)", ((SimulationBehaviour)this).Object, 7);
					return;
				}
				if ((localAuthorityMask & 1) != 1)
				{
					if (((SimulationBehaviour)this).Runner.HasAnyActiveConnections())
					{
						int num = 8;
						num += 4;
						SimulationMessage* ptr = SimulationMessage.Allocate(((SimulationBehaviour)this).Runner.Simulation, num);
						byte* data = SimulationMessage.GetData(ptr);
						int num2 = RpcHeader.Write(RpcHeader.Create(((SimulationBehaviour)this).Object.Id, base.ObjectIndex, 1), data);
						Unsafe.Write(data + num2, actor);
						num2 += 4;
						((SimulationMessage)ptr).Offset = num2 * 8;
						((SimulationBehaviour)this).Runner.SendRpc(ptr);
					}
					if ((localAuthorityMask & 1) == 0)
					{
						return;
					}
				}
			}
			else
			{
				base.InvokeRpc = false;
			}
			foreach (MechanismObject associatedObject in _associatedObjects)
			{
				try
				{
					associatedObject.Activate();
				}
				catch (Exception ex)
				{
					Plugin.Logger.LogError((object)("Failed to activate mechanism object from button! " + ex));
				}
			}
		}
		catch (Exception ex2)
		{
			Plugin.Logger.LogError((object)("Rpc_Activate error: " + ex2));
		}
	}

	[NetworkRpcWeavedInvoker(1, 7, 1)]
	[Preserve]
	protected unsafe static void Rpc_Activate_0040Invoker(NetworkBehaviour behaviour, SimulationMessage* message)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		byte* data = SimulationMessage.GetData(message);
		int num = (RpcHeader.ReadSize(data) + 3) & -4;
		PlayerRef val = (PlayerRef)data[num];
		num += 4;
		PlayerRef actor = val;
		behaviour.InvokeRpc = true;
		((MechanismButton)(object)behaviour).Rpc_Activate(actor);
	}
}
