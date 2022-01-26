using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour 
{
    [field:SerializeField] public ResourceType ResourceType { get; private set; }
    public UnityEvent ChangeStatus;
    private bool isRaise = true;
    public int Id { get; private set; }
    public void Init(int id , Vector3 pos = new Vector3())
    {
        if (pos != Vector3.zero)
            transform.DOMove(pos, 0.3f);
        Id = id;
    }

    public void RaiseItem(StickMan s)
    {
        if (!isRaise) return;
        RemoveTile();
        LiftTile(s.Baggage, s);
    }

    private void RemoveTile()
    {
        ChangeStatus?.Invoke();
    }
   
    private void LiftTile(Transform baggage , StickMan stickMan)
    {
        var tilePosBaggage = stickMan;
        var Mesh = transform.GetComponent<MeshRenderer>();
        var MeshSize = Mesh.bounds.size + new Vector3(0f, 0.02f, 0f); // это нужно для границы сетки
        var position = new Vector3(baggage.localPosition.x, baggage.localPosition.y + tilePosBaggage.Count * MeshSize.y, baggage.localPosition.z);
        transform.parent = baggage;
        transform.DOLocalMove(position, 0.3f);
        transform.rotation = baggage.rotation;
        tilePosBaggage.Add(transform);
        stickMan.AddBaggage(this);
    }

    public void MoveStorage(Vector3 pos , Transform parent)
    {
        RemoveTile();
        isRaise = false;
        transform.parent = null;
        transform.DOMove(pos, 0.3f);
        transform.rotation = parent.rotation;
    }

    public void DestroySelf()
    {
        ChangeStatus?.Invoke();
        Destroy(gameObject);
    }
}
