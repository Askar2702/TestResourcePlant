using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;


public class Factory : MonoBehaviour
{
    public UnityEvent<ResourceType, string, string> ShowError;
    public UnityEvent HideError;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private ReceivingWarehouse[] _receivings;
    [SerializeField] private Storage _storage;
    [SerializeField] private Tile _tile;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private float _time;
    private float _currentTime;
    private int _currentTile;
    private bool isStoped;

    private string _noResources;
    private string _noplace;

    private void Start()
    {
        _slider.maxValue = _time;
    }
    void Update()
    {
        if (isStoped || !CheckResource()) return;


        if (_currentTime < _time)
        {
            _currentTime += Time.deltaTime;
            _slider.value = _currentTime;
        }
        else
        {
            _currentTime = 0;
            SpawnResource();
        }
    }

    private bool CheckResource()
    {
        bool isCan = true;
        if (_resourceType == ResourceType.Time) isCan = true;
        else
        {
            if (_receivings.Length >= 1)
            {
                for (int i = 0; i < _receivings.Length; i++)
                {
                    if (_receivings[i].Count <= 0)
                    {
                        isCan = false;
                        _fill.color = Color.red;
                        _noResources = "Нет ресурсов";
                        ShowError?.Invoke(_resourceType, _noResources, _noplace);
                        break;
                    }
                    else
                    {
                        _fill.color = Color.green;
                        isCan = true;
                        HideError?.Invoke();
                    }
                }
            }

        }
        return isCan;
    }
    private void SpawnResource()
    {
        if (_storage.PositionsFree.Count <= _currentTile || !ResourceUsage())
        {
            _fill.color = Color.red;
            isStoped = true;
            _noplace = "Нет места на складе";
            ShowError?.Invoke(_resourceType, _noResources, _noplace);
            return;
        }
        var res = Instantiate(_tile, transform.position, Quaternion.identity);
        HideError?.Invoke();
        foreach (var item in _storage.PositionsFree)
        {
            if (item.Value == true)
            {
                res.Init(item.Key, this);
                _currentTile++;
                _storage.PositionsFree[item.Key] = false;
                break;
            }
        }
    }
    private bool ResourceUsage()
    {
        bool isCan = false;
        if (_resourceType == ResourceType.Time) isCan = true;
        else
        {

            if (_receivings.Length >= 1)
            {
                for (int i = 0; i < _receivings.Length; i++)
                {
                    if (_receivings[i].Count <= 0)
                    {
                        isCan = false;
                    }
                    else
                    {
                        _receivings[i].RemoveItem();
                        isCan = true;
                    }
                }
            }


            if (!isCan) isStoped = isCan;
        }
        return isCan;
    }

    public void DeleteItem(Tile tile)
    {
        if (_storage.PositionsFree.ContainsKey(tile.Pos))
        {
            _storage.PositionsFree[tile.Pos] = true;
            isStoped = false;
            _currentTile--;
            _fill.color = Color.green;
        }

    }
}

public enum ResourceType { Note, Time, blue, red }
