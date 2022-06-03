using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;

public class Localization : MonoBehaviour
{
    public LocalizationWord[] localizationWords;
    public string[] languages;
    private int currentIndexLang;
    public UnityEvent changeLoc;

    public static Localization Instance { get ; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentIndexLang = PlayerPrefs.GetInt("Localization", 0);
        ChangeLolization(false);
        changeLoc.Invoke();
    }
    public void ChangeLolization(bool addIndex)
    {
        if (addIndex)
        {
            currentIndexLang++;
            if (currentIndexLang >= languages.Length)
            {
                currentIndexLang = 0;
            }
            PlayerPrefs.SetInt("Localization", currentIndexLang);
        }
        TextAsset textAsset = Resources.Load<TextAsset>($"Localization/{languages[currentIndexLang]}");
        localizationWords = JsonConvert.DeserializeObject<LocalizationWord[]>(textAsset.text);
        changeLoc.Invoke();
    }

    public string GetWord(string code)
    {
        return localizationWords.ToList().Find(x => x.code == code).word;
    }

}

[System.Serializable]
public class LocalizationWord
{
    public string code;
    public string word;
}
