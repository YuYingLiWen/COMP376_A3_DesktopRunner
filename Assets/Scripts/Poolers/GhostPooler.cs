using UnityEngine;
using UnityEngine.Pool;

public class GhostPooler : GameObjectPooler<Ghost>
{

    private void Awake()
    {
        if (!instance) instance = this;
        else Debug.LogWarning("Multiple " + this.GetType().Name, this);
    }

    protected override void Start()
    {
        pool = new ObjectPool<Ghost>(OnCreate, OnGetFromPool, OnRelease, OnDestruction, collectionCheck, initialAmount);
    }

    protected override Ghost OnCreate()
    {
        var newObject = Instantiate(toPool);
        newObject.transform.parent = transform;
        return newObject.GetComponent<Ghost>();
    }

    protected override void OnGetFromPool(Ghost obj)
    {
        obj.gameObject.SetActive(true);
    }

    protected override void OnRelease(Ghost obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected override void OnDestruction(Ghost obj)
    {
        Destroy(obj.gameObject);
    }

    private ObjectPool<Ghost> pool;
    public ObjectPool<Ghost> Pool => pool;

    private static GhostPooler instance;
    public static GhostPooler Instance => instance;
}
