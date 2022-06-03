using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Localization))]
public class LocalizationEditor : Editor
{
    private string nameLang;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        nameLang = EditorGUILayout.TextField("NameLang",nameLang);

        if (GUILayout.Button("Save"))
        {
            var loc = FindObjectOfType<Localization>();
            string json = JsonConvert.SerializeObject(loc.localizationWords);
            File.WriteAllText(Application.dataPath + $"/Resources/Localization/{nameLang}.json", json);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("Load"))
        {
            var loc = FindObjectOfType<Localization>();
            TextAsset textAsset = Resources.Load<TextAsset>($"Localization/{nameLang}");
            loc.localizationWords = JsonConvert.DeserializeObject<LocalizationWord[]>(textAsset.text);
        }
    }
}
