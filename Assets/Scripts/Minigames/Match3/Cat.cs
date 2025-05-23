using UnityEngine;

public class Cat : MonoBehaviour
{
    PrefabInstancePool<Cat> pool;

    public Cat Spawn (Vector3 position)
    {
        Cat instance = pool.GetInstance(this);
        instance.pool = pool;
        instance.transform.localPosition = position;
        return instance;
    }
    
    public void Despawn() => pool.Recycle(this);

}
