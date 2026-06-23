using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Fusion;
using Newtonsoft.Json;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace LycansNewRoles;

public class TranslationManager
{
	private Dictionary<string, string> _translationsForCurrentLanguage = new Dictionary<string, string>();

	private Dictionary<string, Dictionary<string, string>> _allTranslationsByLanguage = null;

	private static TranslationManager _instance;

	public static TranslationManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new TranslationManager();
			}
			return _instance;
		}
	}

	private string GetLanguageCode
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			LocaleIdentifier identifier = LocalizationSettings.SelectedLocale.Identifier;
			return (((LocaleIdentifier)(ref identifier)).Code == "fr") ? "fr" : "en";
		}
	}

	public TranslationManager()
	{
		try
		{
			LoadTranslationsFromFile();
			LoadTranslationsForLanguage(GetLanguageCode);
			LocalizationSettings.SelectedLocaleChanged += SelectedLocaleChanged;
		}
		catch (Exception ex)
		{
			Plugin.Logger.LogError((object)("TranslationManager constructor error: " + ex));
		}
	}

	public void Initialize()
	{
	}

	private void SelectedLocaleChanged(Locale locale)
	{
		LoadTranslationsForLanguage(GetLanguageCode);
	}

	private void LoadTranslationsFromFile()
	{
		string name = "LycansNewRoles.resources.translations.json";
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		try
		{
			using Stream stream = executingAssembly.GetManifestResourceStream(name);
			if (stream != null)
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					_allTranslationsByLanguage = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(streamReader.ReadToEnd());
					return;
				}
			}
			Plugin.Logger.LogError((object)"Translations resource not found");
		}
		catch (Exception ex)
		{
			Log.Info((object)ex.Message);
		}
	}

	public string GetTranslation(string key)
	{
		if (_translationsForCurrentLanguage.ContainsKey(key))
		{
			return _translationsForCurrentLanguage[key];
		}
		return key;
	}

	public string GetTranslationForStats(string key)
	{
		if (_allTranslationsByLanguage.ContainsKey("fr") && _allTranslationsByLanguage["fr"].ContainsKey(key))
		{
			return _allTranslationsByLanguage["fr"][key];
		}
		return key;
	}

	private void LoadTranslationsForLanguage(string languageCode)
	{
		_translationsForCurrentLanguage = _allTranslationsByLanguage[languageCode];
		PopulateStringDatabase();
	}

	private void PopulateStringDatabase()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		StringTable table = ((LocalizedDatabase<StringTable, StringTableEntry>)(object)LocalizationSettings.StringDatabase).GetTable(TableReference.op_Implicit("UI Text"), (Locale)null);
		foreach (KeyValuePair<string, string> item in _translationsForCurrentLanguage)
		{
			((DetailedLocalizationTable<StringTableEntry>)(object)table).AddEntry(item.Key, item.Value);
		}
	}
}
