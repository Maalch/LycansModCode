using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LycansNewRoles;

public static class ComponentExtensions
{
	public static T GetCopyOf<T>(this T comp, T other) where T : Component
	{
		Type type = ((object)comp).GetType();
		Type type2 = ((object)other).GetType();
		if (type != type2)
		{
			Plugin.Logger.LogError((object)$"The type \"{type.AssemblyQualifiedName}\" of \"{comp}\" does not match the type \"{type2.AssemblyQualifiedName}\" of \"{other}\"!");
			return default(T);
		}
		BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		PropertyInfo[] properties = type.GetProperties(bindingAttr);
		PropertyInfo[] array = properties;
		foreach (PropertyInfo propertyInfo in array)
		{
			if (propertyInfo.CanWrite)
			{
				propertyInfo.SetValue(comp, propertyInfo.GetValue(other, null), null);
			}
		}
		FieldInfo[] fields = type.GetFields(bindingAttr);
		FieldInfo[] array2 = fields;
		foreach (FieldInfo fieldInfo in array2)
		{
			fieldInfo.SetValue(comp, fieldInfo.GetValue(other));
		}
		return comp;
	}

	public static void ListComponentsAndChildren(this GameObject gameObject)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		Plugin.Logger.LogInfo((object)(((Object)gameObject).name + ", position: " + ((object)gameObject.transform.position/*cast due to constrained. prefix*/).ToString() + ", layer: " + gameObject.layer));
		Component[] components = gameObject.GetComponents<Component>();
		foreach (Component val in components)
		{
			Plugin.Logger.LogInfo((object)(((Object)gameObject).name + " Component: " + (object)val));
		}
		for (int j = 0; j < gameObject.transform.childCount; j++)
		{
			Transform child = gameObject.transform.GetChild(j);
			Plugin.Logger.LogInfo((object)(((Object)gameObject).name + " Child: " + ((object)child)?.ToString() + ", position: " + ((object)child.position/*cast due to constrained. prefix*/).ToString()));
			Component[] components2 = ((Component)child).gameObject.GetComponents<Component>();
			foreach (Component val2 in components2)
			{
				Plugin.Logger.LogInfo((object)(((Object)gameObject).name + " Child component: " + ((object)val2).ToString()));
			}
			for (int l = 0; l < child.childCount; l++)
			{
				Transform child2 = child.GetChild(l);
				Plugin.Logger.LogInfo((object)(((object)child)?.ToString() + " Child: " + ((object)child2)?.ToString() + " / " + ((Object)((Component)child2).gameObject).name));
				Component[] components3 = ((Component)child2).gameObject.GetComponents<Component>();
				foreach (Component val3 in components3)
				{
					Plugin.Logger.LogInfo((object)("- Child2 component: " + ((object)val3).ToString()));
				}
				for (int n = 0; n < child2.childCount; n++)
				{
					Transform child3 = child2.GetChild(n);
					Plugin.Logger.LogInfo((object)(((object)child2)?.ToString() + " Child: " + ((object)child3)?.ToString() + " / " + ((Object)((Component)child3).gameObject).name));
					Component[] components4 = ((Component)child3).gameObject.GetComponents<Component>();
					foreach (Component val4 in components4)
					{
						Plugin.Logger.LogInfo((object)("-- Child3 component: " + ((object)val4).ToString()));
					}
					for (int num2 = 0; num2 < child3.childCount; num2++)
					{
						Transform child4 = child3.GetChild(num2);
						Plugin.Logger.LogInfo((object)(((object)child3)?.ToString() + " Child: " + ((object)child4)?.ToString() + " / " + ((Object)((Component)child4).gameObject).name));
						Component[] components5 = ((Component)child4).gameObject.GetComponents<Component>();
						foreach (Component val5 in components5)
						{
							Plugin.Logger.LogInfo((object)("--- Child4 component: " + ((object)val5).ToString()));
						}
					}
				}
			}
		}
	}

	public static void ListChildren(this GameObject gameObject, int recursion = 0)
	{
		string text = "";
		for (int i = 0; i < recursion; i++)
		{
			text += "-";
		}
		text += ((Object)gameObject).name;
		Plugin.Logger.LogInfo((object)text);
		for (int j = 0; j < gameObject.transform.childCount; j++)
		{
			Transform child = gameObject.transform.GetChild(j);
			((Component)child).gameObject.ListChildren(recursion + 1);
		}
	}

	public static void RemoveAllChildren(this GameObject gameObject)
	{
		int childCount = gameObject.transform.childCount;
		for (int num = childCount - 1; num >= 0; num--)
		{
			Object.Destroy((Object)(object)((Component)gameObject.transform.GetChild(num)).gameObject);
		}
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		int num = list.Count;
		while (num > 1)
		{
			num--;
			int index = Random.Range(0, num + 1);
			T value = list[index];
			list[index] = list[num];
			list[num] = value;
		}
	}

	public static Transform FindVillagerHandLeft(this PlayerController playerController)
	{
		return playerController.FindVillagerBaseSpine().Find("shoulder.L").Find("upper_arm.L")
			.Find("forearm.L")
			.Find("hand.L");
	}

	public static Transform FindVillagerHandRight(this PlayerController playerController)
	{
		return playerController.FindVillagerBaseSpine().Find("shoulder.R").Find("upper_arm.R")
			.Find("forearm.R")
			.Find("hand.R");
	}

	private static Transform FindVillagerBaseSpine(this PlayerController playerController)
	{
		return ((Component)playerController).transform.Find("Body").Find("Villager").Find("metarig")
			.Find("spine")
			.Find("spine.001")
			.Find("spine.002")
			.Find("spine.003");
	}

	public static Transform FindVillagerItem(this PlayerController playerController)
	{
		return playerController.FindVillagerHandRight().Find("Item");
	}
}
