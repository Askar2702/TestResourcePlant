using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class StickMan : MonoBehaviour
{
    [SerializeField] private Transform _baggage;
    public List<Tile> Tiles { get; private set; } = new List<Tile>();
    public Transform Baggage => _baggage;
    public Material Material { get; private set; }

    private int _limit = 24;
    [SerializeField] private MeshRenderer _meshTile;



    private Vector3 CreatePosition(int i)
    {
        var mesh = _meshTile;
        var MeshSize = mesh.bounds.size + new Vector3(0f, 0.02f, 0f); // это нужно для границы сетки
        return new Vector3(Baggage.localPosition.x, Baggage.localPosition.y + i * MeshSize.y, Baggage.localPosition.z);
    }




    public void RemoveItem(Tile tile)
    {
        if (Tiles.Contains(tile))
        {
            Tiles.Remove(tile);
        }
    }



    public async void CheckFreePosition()
    {
        for (var i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i])
                await Tiles[i].Move(CreatePosition(i), 0.1f);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Tile tile))
        {
            if (Tiles.Count < _limit && !Tiles.Contains(tile))
            {
                Tiles.Add(tile);
                tile.RaiseItem(this, CreatePosition(Tiles.IndexOf(tile)));
            }
        }

    }



}


