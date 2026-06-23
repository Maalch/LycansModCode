using Fusion;
using LycansNewRoles.NewItems;
using UnityEngine;

namespace LycansNewRoles;

public class PlayerHeldItemComponent : MonoBehaviour
{
	public static GameObject HeldItemPrefab;

	private PlayerController _playerController;

	private GameObject _heldItemParent;

	private GameObject _heldItem;

	private NetworkBehaviourId? _showedObjectItemId;

	private void Awake()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		_heldItemParent = Object.Instantiate<GameObject>(HeldItemPrefab, ((Component)((Component)this).GetComponent<PlayerController>()).transform);
		_heldItemParent.SetActive(true);
		_heldItemParent.transform.position = new Vector3(_heldItemParent.transform.position.x, _heldItemParent.transform.position.y + 1.75f, _heldItemParent.transform.position.z);
		_heldItemParent.SetActive(false);
	}

	private void Start()
	{
		_playerController = ((Component)this).GetComponent<PlayerController>();
	}

	public void SetVisible(bool visible)
	{
		if (!visible && (Object)(object)_heldItem != (Object)null)
		{
			Object.Destroy((Object)(object)_heldItem);
			_showedObjectItemId = null;
		}
		_heldItemParent.SetActive(visible);
	}

	private void Update()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		if (!_heldItemParent.activeSelf)
		{
			return;
		}
		PlayerCustom player = PlayerCustomRegistry.GetPlayer(PlayerController.Local.LocalCameraHandler.PovPlayer.Ref);
		NetworkBehaviourId id;
		NetworkBehaviourId? showedObjectItemId;
		if ((Object)(object)_heldItem != (Object)null)
		{
			if (!((Object)(object)_playerController.Item == (Object)null))
			{
				id = ((NetworkBehaviour)_playerController.Item).Id;
				showedObjectItemId = _showedObjectItemId;
				if (showedObjectItemId.HasValue && !(id != showedObjectItemId.GetValueOrDefault()) && CanSeeItem(player, _playerController.Item))
				{
					goto IL_00c6;
				}
			}
			Object.Destroy((Object)(object)_heldItem);
			_heldItem = null;
			_showedObjectItemId = null;
			return;
		}
		goto IL_00c6;
		IL_00c6:
		if (!((Object)(object)_heldItem == (Object)null) || !((Object)(object)_playerController.Item != (Object)null))
		{
			return;
		}
		id = ((NetworkBehaviour)_playerController.Item).Id;
		showedObjectItemId = _showedObjectItemId;
		if ((!showedObjectItemId.HasValue || id != showedObjectItemId.GetValueOrDefault()) && CanSeeItem(player, _playerController.Item))
		{
			_heldItem = Object.Instantiate<GameObject>(((Component)_playerController.Item).gameObject, _heldItemParent.transform);
			((Renderer)_heldItem.GetComponentInChildren<MeshRenderer>()).enabled = true;
			Object.DestroyImmediate((Object)(object)_heldItem.GetComponent<Item>());
			Object.DestroyImmediate((Object)(object)_heldItem.GetComponent<MeshCollider>());
			Object.DestroyImmediate((Object)(object)_heldItem.GetComponent<NetworkTransform>());
			Object.DestroyImmediate((Object)(object)_heldItem.GetComponent<NetworkObject>());
			Object.DestroyImmediate((Object)(object)((Component)_heldItem.GetComponentInChildren<ItemCustom>()).gameObject);
			_heldItem.SetActive(true);
			float num = 0f;
			float num2 = 1f;
			float num3 = 0f;
			if (_playerController.Item is TrapItem)
			{
				num = 0.5f;
				num2 = 0.75f;
			}
			else if (_playerController.Item is SmokeItem)
			{
				num2 = 0.75f;
			}
			else if (_playerController.Item is SpyglassItem)
			{
				num2 = 0.75f;
			}
			else if (_playerController.Item is LockItem)
			{
				num2 = 0.75f;
				num3 = 90f;
			}
			else if (_playerController.Item is BulletItem)
			{
				num2 = 0.75f;
			}
			else if (_playerController.Item is Potion)
			{
				num2 = 0.75f;
			}
			else if (_playerController.Item is MagicScrollItem)
			{
				num2 = 1f;
			}
			else if (_playerController.Item is PhasingDiamondItem)
			{
				num2 = 1f;
			}
			else if (_playerController.Item is GrenadeItem)
			{
				num2 = 1f;
			}
			else if (_playerController.Item is SleepingGasItem)
			{
				num2 = 1f;
			}
			else if (_playerController.Item is MolotovItem)
			{
				num2 = 1f;
			}
			else if (_playerController.Item is RadarItem)
			{
				num2 = 1f;
			}
			_heldItem.transform.localPosition = new Vector3(0f, num, 0f);
			_heldItem.transform.localScale = _heldItem.transform.localScale * num2;
			if (num3 != 0f)
			{
				_heldItem.transform.Rotate(num3, 0f, 0f);
			}
			_showedObjectItemId = ((NetworkBehaviour)_playerController.Item).Id;
		}
	}

	public static bool CanSeeItem(PlayerCustom playerCustom, Item item)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Invalid comparison between Unknown and I4
		if (item is BulletItem && (int)playerCustom.PlayerController.Role != 1 && playerCustom.NewPrimaryRole != PlayerCustom.PlayerNewPrimaryRole.Traitor)
		{
			return false;
		}
		return true;
	}
}
