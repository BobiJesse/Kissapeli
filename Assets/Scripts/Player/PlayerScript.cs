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
    public Rigidbody2D rb;
    public Interact interactionScript;

    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundLayer;
    public bool grounded;
    public bool closeToCat;

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
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        if (jumpAction.triggered && grounded && playerHasControl)
        {
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, jumpForce);
        }

        if (interactionAction.triggered && grounded && playerHasControl && closeToCat)
        {
            Interact.closestCat.StartDialogue();
            playerHasControl = false;
        }

        if (pauseAction.triggered)
        {
            Debug.Log("Pausing game");
        }
    }
    private void FixedUpdate()
    {
        if(playerHasControl)
        {
            Vector2 input = moveAction.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
        }
        
    }

    private void OnEnable()
    {
        GameEvents.OnMinigameExit += EnablePlayerControl;
    }

    private void OnDisable()
    {
        GameEvents.OnMinigameExit -= EnablePlayerControl;
    }

    void EnablePlayerControl()
    {
        playerHasControl = true;
        interactionScript.mainCam.SetActive(true);
    }
}
