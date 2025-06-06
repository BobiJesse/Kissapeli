using UnityEngine;
using System.Collections.Generic;

public struct PrefabInstancePool<T> where T : MonoBehaviour
{
    Stack<T> pool;

    public T GetInstance (T prefab)
    {
        // Check if the pool is null
        if (pool == null)
        {
            pool = new();
        }

#if UNITY_EDITOR
        else if (pool.TryPeek(out T i) && !i)
        {
            // Instances destroyed, assuming due to exiting play mode.
            pool.Clear();
        }
#endif

        // Check if the prefab is already in the pool
        if (pool.TryPop(out T instance))
        {
            instance.gameObject.SetActive(true);
        }
        else
        {
            instance = Object.Instantiate(prefab);
            instance.transform.SetParent(GameCanvas.instance.transform, false);
        }
        return instance;
    }

    public void Recycle (T instance)
    {

#if UNITY_EDITOR
        if (pool == null)
        {
            // Pool lost, assuming due to hot reload.
            Object.Destroy(instance.gameObject);
            return;
        }
#endif

        pool.Push(instance);
        instance.gameObject.SetActive(false);
    }
}
