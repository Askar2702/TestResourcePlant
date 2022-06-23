using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private int _lenght;
    [SerializeField] private int _width;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _offset;
    public Dictionary<Vector3, bool> PositionsFree { get; private set; } = new Dictionary<Vector3, bool>();

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
                var MeshSize = _mesh.bounds.size + new Vector3(_offset, 0, _offset);
                var pos = new Vector3(_spawnPoint.position.x + x * MeshSize.x, _spawnPoint.position.y, _spawnPoint.position.z + y * MeshSize.z);
                PositionsFree.Add(pos, true);
            }
        }
    }


}
