using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChangeLocText : MonoBehaviour
{
    [SerializeField]
    private string code;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Localization.Instance.changeLoc.AddListener(UpdateText);
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = Localization.Instance.GetWord(code);
    }
}
