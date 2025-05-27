using UnityEngine;
using UnityEngine.UIElements;

public class Cat : MonoBehaviour
{
    PrefabInstancePool<Cat> pool;

    [SerializeField, Range(0f, 1f)]
    float disappearDur = 0.25f;

    float disappearTimer = 0f;

    [System.Serializable]
    struct FallingState
    {
        public float fromY, toY, duration, progress;
    }

    FallingState falling;

    public Cat Spawn (Vector3 position)
    {
        Cat instance = pool.GetInstance(this);
        instance.pool = pool;
        instance.transform.localPosition = position;
        instance.transform.localScale = Vector3.one;
        instance.disappearTimer = -1f;
        instance.falling.progress = -1f;
        instance.enabled = false;
        return instance;
    }
    
    public void Despawn() => pool.Recycle(this);

    public float Disappear()
    {
        disappearTimer = 0f;
        enabled = true;
        return disappearDur;
    }

    public float Fall(float toY, float speed)
    {
        falling.fromY = transform.localPosition.y;
        falling.toY = toY;
        falling.duration = (falling.fromY - toY) / speed;
        falling.progress = 0f;
        enabled = true;
        return falling.duration;
    }

    void Update()
    {
        if (disappearTimer >= 0f)
        {
            disappearTimer += Time.deltaTime;
            if (disappearTimer >= disappearDur)
            {
                Despawn();
                return;
            }
            transform.localScale = Vector3.one * (1f - disappearTimer / disappearDur);
        }

        if (falling.progress >= 0f)
        {
            Vector3 position = transform.localPosition;
            falling.progress += Time.deltaTime;
            if (falling.progress >= falling.duration)
            {
                falling.progress = -1f;
                position.y = falling.toY;
                enabled = disappearTimer >= 0f;
            }
            else
            {
                position.y = Mathf.Lerp(
                    falling.fromY, falling.toY, falling.progress / falling.duration
                );
            }
            transform.localPosition = position;
        }
    }
}
