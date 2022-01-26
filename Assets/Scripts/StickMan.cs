using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    [SerializeField] private Transform _baggage;
    public List<Tile> Tiles { get; private set; } = new List<Tile>();
    public Transform Baggage => _baggage;
    public Material Material { get; private set; }
    private List<Transform> _tilePosBaggage = new List<Transform>();
    public int Count => _tilePosBaggage.Count;
    public event Action<string> ChangeCountItem;
    public Transform this [int index]
    {
        get
        {
            return _tilePosBaggage[index];
        }
    }
   
  
    public void AddBaggage(Tile tile)
    {
        Tiles.Add(tile);
        tile.ChangeStatus.AddListener(() => RemoveBaggage(tile));
        tile.ChangeStatus.AddListener(() => RemoveAt(tile.transform));
    }

    private void RemoveBaggage(Tile tile)
    {
        if (Tiles.Contains(tile))
            Tiles.Remove(tile);
    }

    public void Add(Transform item)
    {
        _tilePosBaggage.Add(item);
    }
    public void RemoveAt(Transform tile)
    {
        _tilePosBaggage.Remove(tile);
    }


    private void OnTriggerEnter(Collider collision)
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Tile item))
            item.RaiseItem(this);
    }
}
