using UnityEngine;
using UnityEngine.UI;

public class QuickTimeEvent : MonoBehaviour
{
    public Slider slider; // Reference to the UI slider

    public float targetMin;
    public float targetMax;

    public float sliderSpeed = 1f; // Speed of the slider
    public float timeLimit = 5f; // Time limit for the QTE
    private float timeRemaining; // Time remaining for the QTE
    private bool isQTEActive = false; // Flag to check if the QTE is active
    private bool isQTECompleted = false; // Flag to check if the QTE is completed
    public bool QTEUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>(); // Get the Slider component attached to this GameObject
        isQTEActive = true; // Set the QTE to active
        isQTECompleted = false; // Set the QTE to not completed
        QTEUp = true; // Set the QTE direction to up
    }

    // Update is called once per frame
    void Update()
    {
        if (isQTEActive) 
        {
            if (slider.value == 1)
            {
                QTEUp = false; // Set QTEUp to false when the slider is at maximum
            }
            if (slider.value == 0)
            {
                QTEUp = true; // Set QTEUp to true when the slider is at minimum
            }
            if (slider.value < 1f && QTEUp)
            {
                slider.value += Time.deltaTime * sliderSpeed; // Increment the slider value over time
            }
            if (slider.value > 0f && !QTEUp)
            {
                slider.value -= Time.deltaTime * sliderSpeed; // Decrement the slider value over time
            }
        }
    }

    public void CheckValue()
    {
        if (isQTEActive && !isQTECompleted)
        {
            if (targetMin < slider.value && slider.value < targetMax)
            {
                isQTEActive = false; // Deactivate the QTE
                isQTECompleted = true; // Mark the QTE as completed
                Debug.Log("QTE Completed!"); // Log QTE completion
            }
            else
            {
                slider.value = 0; // Reset the slider value to 0
                isQTEActive = true; // Deactivate the QTE
                Debug.Log("QTE Failed!"); // Log QTE failure
            }
        }
    }
}
