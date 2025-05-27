using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas instance;

    private void Awake()
    {
        // Ensure that there is only one instance of GameCanvas
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
