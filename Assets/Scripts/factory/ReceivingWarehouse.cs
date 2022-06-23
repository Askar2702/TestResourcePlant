using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
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
    private Tile[] _tiles;



    public int Count { get; private set; }


    private void Start()
    {
        GetFreePosition();
        _tiles = new Tile[PositionsFree.Count];
    }
    private void GetFreePosition()
    {
        for (int x = 0; x < _lenght; x++)
        {
            for (int y = 0; y < _width; y++)
            {
                var MeshSize = _mesh.bounds.size + new Vector3(_offset, 0, _offset);
                var pos = new Vector3(_spawnPoint.position.x + x * MeshSize.x, _spawnPoint.position.y, _spawnPoint.position.z + y * MeshSize.z);
                PositionsFree.Add(pos);
            }
        }
    }



    private async void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StickMan stickMan))
        {
            if (Count == PositionsFree.Count || stickMan.Tiles.Count == 0) return;


            for (var i = 0; i < stickMan.Tiles.Count; i++)
            {
                for (var j = 0; j < _tiles.Length; j++)
                {
                    if (!_tiles[j] && stickMan.Tiles[i].ResourceType == _resourceType)
                    {
                        _tiles[j] = stickMan.Tiles[i];
                        _tiles[j].gameObject.layer = 6;
                        Count++;

                        break;
                    }
                }

            }
            // тут второй цикл чтоб удалить из хранилища стикмена его ресурсы , если делать в одном цикле то выходила ошибка так как коллекция которая хранит ресурсы сокращалась

            for (var i = 0; i < _tiles.Length; i++)
            {
                if (_tiles[i])
                {
                    var id = i;
                    await _tiles[i].MoveStorage(PositionsFree[id], transform);
                    stickMan.RemoveItem(_tiles[i]);
                }
            }
            stickMan.CheckFreePosition();

        }
    }


    public void RemoveItem()
    {
        for (var i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i])
            {
                _tiles[i].DestroySelf();
                _tiles[i] = null;
                Count--;
                break;
            }
        }
    }
}
