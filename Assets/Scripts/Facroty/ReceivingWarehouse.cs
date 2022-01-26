using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivingWarehouse : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private int _lenght;
    [SerializeField] private int _width;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _offset;
    private List<Vector3> PositionsFree = new List<Vector3>();
    private List<int> _id = new List<int>();
    private List<Tile> _tiles = new List<Tile>();
    public List<Tile> Tiles { get; private set; } = new List<Tile>();
    private int _notFreeSpace;

    private void Start()
    {
        GetFreePosition();
    }
    private void GetFreePosition()
    {
        for (int x = 0; x < _lenght; x++)
        {
            for (int y = 0; y < _width; y++)
            {
                var MeshSize = _mesh.bounds.size + new Vector3(_offset, 0, _offset); // это нужно для границы сетки
                var pos = new Vector3(_spawnPoint.position.x + x * MeshSize.x, _spawnPoint.position.y, _spawnPoint.position.z + y * MeshSize.z);
                PositionsFree.Add(pos);
            }
        }
    }

  

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out StickMan stickMan))
        {
            if (_notFreeSpace==PositionsFree.Count  || stickMan.Tiles.Count == 0) return;

           

            _tiles.Clear();
            foreach (var t in stickMan.Tiles)
                _tiles.Add(t);

            var count = 0;
            if (_tiles.Count <= PositionsFree.Count) count = _tiles.Count;
            else count = PositionsFree.Count;


            for (int i = 0; i < count; i++)
            {
                if (_tiles[i].ResourceType == _resourceType)
                {
                    var t = _tiles[i];
                    var id = _notFreeSpace;
                    if (_id.Count > 0)
                    {
                        id = _id[0];
                        _id.RemoveAt(0);
                    }
                    t.Init(id);
                    t.MoveStorage(PositionsFree[id], transform);
                    Tiles.Add(t);
                    t.ChangeStatus.AddListener(() => RemoveItem(t));
                    _notFreeSpace++;
                }
            }
        }
    }

    public void RemoveItem()
    {
        Tiles[0].DestroySelf();
        _notFreeSpace--;
    }
    private void RemoveItem(Tile tile)
    {
        if (!_id.Contains(tile.Id))
        {
            _id.Add(tile.Id);
            Tiles.Remove(tile);
        }
    }
}
