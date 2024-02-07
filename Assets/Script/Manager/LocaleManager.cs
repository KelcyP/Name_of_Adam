using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class LocaleManager : MonoBehaviour
{
    Locale currentLocale;
    bool isChangingLanguage;

    public void Init()
    {
        isChangingLanguage = false;

        LanguageChanged(GameManager.OutGameData.GetLanguage());
    }

    public string GetLocalizedStigmaName(string stigmaName) => GetLocalizedString("StigmaNameTable", stigmaName);

    public string GetLocalizedStigmaInfo(string stigmaInfo) => GetLocalizedString("StigmaInfoTable", stigmaInfo);

    public string GetLocalizedUnitName(string unitName) => GetLocalizedString("UnitTable", unitName);

    private string GetLocalizedString(string tableName, string key)
    {
        string str = LocalizationSettings.StringDatabase.GetLocalizedString(tableName, key, currentLocale);
        if (str.Contains("No translation"))
        {
            Debug.Log($"'{key}' Localization is faied.");
            return key;
        }

        return str;
    }

    public void LanguageChanged(int localeIndex)
    {
        if (isChangingLanguage)
            return;

        StartCoroutine(ChangeLanuage(localeIndex));
    }

    IEnumerator ChangeLanuage(int localeIndex)
    {
        isChangingLanguage = true;

        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        currentLocale = LocalizationSettings.SelectedLocale;
        GameManager.OutGameData.SetLanguage(localeIndex);

        Debug.Log(LocalizationSettings.SelectedLocale.LocaleName);

        isChangingLanguage = false;
    }
}
