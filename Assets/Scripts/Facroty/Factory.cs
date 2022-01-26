using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Factory : MonoBehaviour
{
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
    private List<int> _id = new List<int>();
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
            if (_receivings.Length > 1)
            {
                for (int i = 0; i < _receivings.Length; i++)
                {
                    if (_receivings[i].Tiles.Count <= 0)
                    {
                        isCan = false;
                        _fill.color = Color.red;
                        break;
                    }
                    else
                    {
                        _fill.color = Color.green;
                        isCan = true;
                    }
                }
            }
            else
            {
                if (_receivings[0].Tiles.Count <= 0)
                {
                    isCan = false;
                    _fill.color = Color.red;
                }
                else
                { 
                    isCan = true;
                    _fill.color = Color.green;
                }
            }
        }
        return isCan;
    }

    private bool ResourceUsage()
    {
        bool isCan = false;
        if (_resourceType == ResourceType.Time) isCan = true;
        else
        {

            if (_receivings.Length > 1)
            {
                for (int i = 0; i < _receivings.Length; i++)
                {
                    if (_receivings[i].Tiles.Count <= 0) isCan = false;
                    else
                    {
                        _receivings[i].RemoveItem();
                        isCan = true;
                    }
                }
            }
            else
            {
                if (_receivings[0].Tiles.Count <= 0) isCan = false;
                else
                {
                    _receivings[0].RemoveItem();
                    isCan = true;
                }
            }
            if (!isCan) isStoped = isCan;
        }
        return isCan;
    }
    private void SpawnResource()
    {
        var res = Instantiate(_tile, transform.position, Quaternion.identity);
        if (_storage.PositionsFree.Count <= _currentTile || !ResourceUsage())
        {
            _fill.color = Color.red;
            isStoped = true;
            return; 
        }
        if (_id.Count > 0)
        { 
            _currentTile = _id[0];
            _id.RemoveAt(0);
        }
        res.Init(_currentTile , _storage.PositionsFree[_currentTile]);
        res.ChangeStatus.AddListener(() => DeleteItem(res));
        _currentTile++;
    }
    private void DeleteItem(Tile tile)
    {
        if (!_id.Contains(tile.Id))
        {
            _id.Add(tile.Id);
            isStoped = false;
            _currentTile--;
            _fill.color = Color.green;
        }
    }
}

public enum ResourceType { Note , Time , blue , red }
