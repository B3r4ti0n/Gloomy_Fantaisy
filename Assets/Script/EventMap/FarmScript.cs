using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmScript : MonoBehaviour
{
    [SerializeField]
    public Button farmButton; // Reference to the farm button
    public Text messageText; // Reference to the text component for displaying messages

    [SerializeField]
    public GameObject popupObject; // Reference to the cooldown popup object 
    public Text popUpText; // Reference to the text component in the cooldown popup

    public List<string> messages = new List<string>(); // List to store messages
    public float messageDuration = 5f; // Duration for displaying each message
    public float timer = 0f; // Timer for message display
    public int clickCount = 0; // Number of button clicks
    public bool isCooldown = false; // Flag to indicate cooldown state
    public float cooldownTimer = 120f; // Cooldown duration in seconds (2 minutes)

    // Possible messages
    public string[] possibleMessages = { "Exp", "GloomGold", "Object" };

    // Start is called before the first frame update
    void Start()
    {
        farmButton.onClick.AddListener(OnClickFarmButton); // Register the OnClickFarmButton method as the button click listener
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime; // Decrease the cooldown timer

            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
                clickCount = 0;
                cooldownTimer = 120f;
            }
            else
            {
                UpdateCooldownPopupText();
            }
        }
        else if (messages.Count > 0)
        {
            timer -= Time.deltaTime; // Decrease the message display timer

            if (timer <= 0)
            {
                messages.RemoveAt(0); // Remove the oldest message
                timer = messageDuration;
            }

            messageText.text = string.Join("\n", messages.ToArray()); // Update the message text
        }
    }

    public void OnClickFarmButton()
    {
        if (isCooldown) //If Coodown open cooldown popup
        {
            popupObject.SetActive(true);
            UpdateCooldownPopupText();
            return;
        }

        clickCount++;

        if (clickCount <= 3)
        {
            if (clickCount == 1)
            {
                int experienceValue = Random.Range(10, 51); // Generate a random value for experiences
                messages.Add("Experiences: " + experienceValue);
            }
            else if (clickCount == 2)
            {
                int gloomGoldValue = Random.Range(10, 51); // Generate a random value for GloomGold
                messages.Add("GloomGold: " + gloomGoldValue);
            }
            else if (clickCount == 3)
            {
                int randomIndex = Random.Range(0, 2); // Generate a random index to select a potion name
                string potionName = (randomIndex == 0) ? "potion 1" : "potion 2";
                messages.Add("Object: " + potionName);
            }

            timer = messageDuration; // Reset the timer for displaying messages
        }
        else
        {
            isCooldown = true;
            popupObject.SetActive(true);
            UpdateCooldownPopupText();
            StartCoroutine(ClearMessagesAfterDelay(2f)); // Start a coroutine to clear the messages after a delay
            clickCount = 0;
        }
    }

    IEnumerator ClearMessagesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearMessages(); // Clear all messages
    }

    void ClearMessages()
    {
        messages.Clear(); // Clear the list of messages
        messageText.text = ""; // Clear the message text
    }

    void UpdateCooldownPopupText()
    {
        if (popupObject.activeSelf)
        {
            popUpText.text = "You have already recovered everything. Remaining time :" + Mathf.Ceil(cooldownTimer) + " seconds"; // Update the cooldown popup text
        }
    }
}
