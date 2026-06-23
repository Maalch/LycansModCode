using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LycansNewRoles;

public class InputManagerExtra : MonoBehaviour
{
	public const string UseSecondaryRolePowerName = "SECONDARYROLEPOWER";

	public const string ShowMinimap = "SHOWMINIMAP";

	public const string ItemSecondary = "ITEMSECONDARY";

	public const string MayorAction = "MAYORACTION";

	public const string AccessoryAction = "ACCESSORYACTION";

	public const string Emote1 = "EMOTE1";

	public const string Emote2 = "EMOTE2";

	public const string Emote3 = "EMOTE3";

	public const string Emote4 = "EMOTE4";

	public const string Emote5 = "EMOTE5";

	public const string Emote6 = "EMOTE6";

	public const string Emote7 = "EMOTE7";

	public const string Emote8 = "EMOTE8";

	private static InputManagerExtra _instance;

	private Dictionary<string, InputAction> _actions = new Dictionary<string, InputAction>();

	public static InputManagerExtra Instance
	{
		get
		{
			if ((Object)(object)_instance == (Object)null)
			{
				_instance = ((Component)InputManager.Instance).gameObject.AddComponent<InputManagerExtra>();
			}
			return _instance;
		}
	}

	public Dictionary<string, InputAction> Actions => _actions;

	public bool SecondaryRoleActionJustPressed { get; private set; }

	public bool ShowMinimapJustPressed { get; private set; }

	public bool ItemSecondaryJustPressed { get; private set; }

	public bool MayorActionJustPressed { get; private set; }

	public bool AccessoryActionJustPressed { get; private set; }

	public bool Emote1JustPressed { get; private set; }

	public bool Emote2JustPressed { get; private set; }

	public bool Emote3JustPressed { get; private set; }

	public bool Emote4JustPressed { get; private set; }

	public bool Emote5JustPressed { get; private set; }

	public bool Emote6JustPressed { get; private set; }

	public bool Emote7JustPressed { get; private set; }

	public bool Emote8JustPressed { get; private set; }

	public void AddAction(InputAction action)
	{
		_actions[action.name] = action;
	}

	private void Update()
	{
		SecondaryRoleActionJustPressed = _actions["SECONDARYROLEPOWER"].WasPerformedThisFrame();
		ShowMinimapJustPressed = _actions["SHOWMINIMAP"].WasPerformedThisFrame();
		ItemSecondaryJustPressed = _actions["ITEMSECONDARY"].WasPerformedThisFrame();
		MayorActionJustPressed = _actions["MAYORACTION"].WasPerformedThisFrame();
		AccessoryActionJustPressed = _actions["ACCESSORYACTION"].WasPerformedThisFrame();
		Emote1JustPressed = _actions["EMOTE1"].WasPerformedThisFrame();
		Emote2JustPressed = _actions["EMOTE2"].WasPerformedThisFrame();
		Emote3JustPressed = _actions["EMOTE3"].WasPerformedThisFrame();
		Emote4JustPressed = _actions["EMOTE4"].WasPerformedThisFrame();
		Emote5JustPressed = _actions["EMOTE5"].WasPerformedThisFrame();
		Emote6JustPressed = _actions["EMOTE6"].WasPerformedThisFrame();
		Emote7JustPressed = _actions["EMOTE7"].WasPerformedThisFrame();
		Emote8JustPressed = _actions["EMOTE8"].WasPerformedThisFrame();
	}

	public InputAction GetAction(string actionName)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (_actions.ContainsKey(actionName))
		{
			return _actions[actionName];
		}
		return InputManager.Instance.GetInputAction(Enum.Parse<InputActionName>(actionName));
	}

	public static void CreateAction(InputManager inputManager, string actionName, string keyboardBinding, string gamepadBinding, string keyboardBindingGuid, string gamepadBindingGuid)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		InputActionMap currentActionMap = ((Component)inputManager).GetComponent<PlayerInput>().currentActionMap;
		if (((IEnumerable<InputAction>)(object)currentActionMap.actions).Any((InputAction o) => o.name == actionName))
		{
			Instance.AddAction(((IEnumerable<InputAction>)(object)currentActionMap.actions).First((InputAction o) => o.name == actionName));
			return;
		}
		currentActionMap.Disable();
		InputAction val = InputActionSetupExtensions.AddAction(currentActionMap, actionName, (InputActionType)1, (string)null, (string)null, (string)null, (string)null, (string)null);
		InputBinding val2 = default(InputBinding);
		((InputBinding)(ref val2))._002Ector(keyboardBinding, (string)null, "Keyboard", (string)null, (string)null, (string)null);
		((InputBinding)(ref val2)).id = new Guid(keyboardBindingGuid);
		InputActionSetupExtensions.AddBinding(val, val2);
		InputBinding val3 = default(InputBinding);
		((InputBinding)(ref val3))._002Ector(gamepadBinding, (string)null, "Gamepad", (string)null, (string)null, (string)null);
		((InputBinding)(ref val3)).id = new Guid(gamepadBindingGuid);
		InputActionSetupExtensions.AddBinding(val, val3);
		currentActionMap.Enable();
		Instance.AddAction(val);
	}

	public static void AdaptUI(GameUI gameUI)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		RebindActionUI[] source = Object.FindObjectsOfType<RebindActionUI>(true);
		GameObject gameObject = ((Component)((Component)source.First((RebindActionUI o) => ((Object)((Component)o).transform.parent).name == "KeyboardControls")).transform.parent).gameObject;
		GridLayoutGroup component = gameObject.GetComponent<GridLayoutGroup>();
		component.cellSize = new Vector2(component.cellSize.x, 60f);
		GameObject gameObject2 = ((Component)((Component)source.First((RebindActionUI o) => ((Object)((Component)o).transform.parent).name == "GamepadControls")).transform.parent).gameObject;
		GridLayoutGroup component2 = gameObject2.GetComponent<GridLayoutGroup>();
		component2.cellSize = new Vector2(component2.cellSize.x, 75f);
	}

	public static void AddActionToUI(GameUI gameUI, string actionName)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		RebindActionUI val = Object.FindObjectOfType<RebindActionUI>(true);
		RebindActionUI val2 = Object.Instantiate<RebindActionUI>(val, Traverse.Create((object)gameUI).Field<GameObject>("settingsMenuKeyboard").Value.transform);
		InputAction val3 = Instance.Actions[actionName];
		InputActionReference val4 = ScriptableObject.CreateInstance<InputActionReference>();
		val4.Set(val3);
		val2.actionReference = val4;
		InputBinding val5 = val3.bindings[0];
		val2.bindingId = ((InputBinding)(ref val5)).id.ToString();
		CustomKeyComponent customKeyComponent = ((Component)val2).gameObject.AddComponent<CustomKeyComponent>();
		customKeyComponent.TranslateKey = actionName;
		customKeyComponent.UpdateActionText();
		RebindActionUI val6 = Object.Instantiate<RebindActionUI>(val, Traverse.Create((object)gameUI).Field<GameObject>("settingsMenuGamepad").Value.transform);
		val6.actionReference = val4;
		if (val3.bindings.Count > 1)
		{
			val5 = val3.bindings[1];
			val6.bindingId = ((InputBinding)(ref val5)).id.ToString();
		}
		CustomKeyComponent customKeyComponent2 = ((Component)val6).gameObject.AddComponent<CustomKeyComponent>();
		customKeyComponent2.TranslateKey = actionName;
		customKeyComponent2.UpdateActionText();
	}
}
