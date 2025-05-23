using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance; // Singleton instance
    public bool playerHasControl = true; // Boolean to check if the player has control

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactionAction;
    private InputAction pauseAction;

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
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactionAction = InputSystem.actions.FindAction("Interact");
        pauseAction = InputSystem.actions.FindAction("Menu");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
