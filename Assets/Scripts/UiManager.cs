using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Factory _factory;
    [SerializeField] private GameObject _factoryPanel;
    [SerializeField] private TextMeshProUGUI _textFactory;

    private void Start()
    {
        _factory.ShowError.AddListener(ShowErrorFactory);
        _factory.HideError.AddListener(HideErrorFactory);
    }

    public void ShowErrorFactory(ResourceType type, string str1, string str2)
    {
        _factoryPanel.SetActive(true);
        _textFactory.text = type.ToString() + " " + str1 + "  " + str2;
    }

    public void HideErrorFactory()
    {
        _factoryPanel.SetActive(false);
    }

}
