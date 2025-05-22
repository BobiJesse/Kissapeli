using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance; // Singleton instance
    public bool playerHasControl = true; // Boolean to check if the player has control


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
