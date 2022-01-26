using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
 //   [SerializeField] private TileScriptableObject _tile;
    [SerializeField] private int _width; //ширина
    [SerializeField] private int _length;//длина
    [SerializeField] private GameObject _MapObject;
   
    void Start()
    {
        Spawn();
    }



    private void Spawn()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                var tile = GetTile();
                var Mesh = tile.GetComponent<MeshRenderer>();
                var MeshSize = Mesh.bounds.size + new Vector3(1f, 0, 1f); // это нужно для границы сетки
                var position = new Vector3(transform.position.x + i * MeshSize.x, transform.position.y, transform.position.z + j * MeshSize.z);
                tile.transform.position = position;
                tile.transform.parent = transform;
            }
        }
    }

    private GameObject GetTile()
    {
        var tile = Instantiate(_MapObject);
      /*  tile.transform.localScale = _tile.Size;
        tile.AddComponent<MeshFilter>().mesh = _tile.Mesh;
        tile.AddComponent<MeshRenderer>().material = _tile.Material[Random.Range(0, _tile.Material.Length)];
        tile.AddComponent<BoxCollider>().isTrigger = true;*/
        return tile;
    }

   
}
