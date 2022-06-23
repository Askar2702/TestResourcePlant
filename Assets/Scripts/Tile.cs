using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;

public class Tile : MonoBehaviour
{
    [field: SerializeField] public ResourceType ResourceType { get; private set; }

    private bool isRaise = true;
    public Vector3 Pos { get; private set; }
    private Factory _factory;

    private StickMan _stickMan;
    public void Init(Vector3 pos = new Vector3(), Factory factory = null)
    {
        if (pos != Vector3.zero)
        {
            Pos = pos;
            transform.DOMove(Pos, 0.3f);
        }
        if (factory)
            _factory = factory;
    }

    public async void RaiseItem(StickMan s, Vector3 pos)
    {
        if (!isRaise) return;
        _stickMan = s;
        _factory.DeleteItem(this);
        transform.parent = s.Baggage;
        await Move(pos);
        transform.rotation = s.Baggage.rotation;
    }


    public async Task Move(Vector3 pos, float speed = 0.3f)
    {
        Pos = pos;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(Pos, speed));
        await seq.AsyncWaitForCompletion();
    }
    public async Task MoveStorage(Vector3 pos, Transform parent)
    {
        isRaise = false;
        transform.parent = null;
        transform.rotation = parent.rotation;
        await Move(pos);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
