using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmScript : MonoBehaviour
{
    [SerializeField]
    private Button farmButton;
    public Text messageText;

    [SerializeField]
    private GameObject popupObject;
    public Text popUpText;

    public List<string> messages = new List<string>();
    public float messageDuration = 5f;
    private float timer = 0f;
    private int clickCount = 0;
    private bool isCooldown = false;
    private float cooldownTimer = 120f; // 2 minutes

    // Messages possibles
    private string[] possibleMessages = { "Experiences", "GloomGold", "Object" };

    // Start is called before the first frame update
    void Start()
    {
        farmButton.onClick.AddListener(OnClickFarmButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;

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
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                messages.RemoveAt(0);
                timer = messageDuration;
            }

            messageText.text = string.Join("\n", messages.ToArray());
        }
    }

    void OnClickFarmButton()
    {
        if (isCooldown)
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
                int experienceValue = Random.Range(10, 51);
                messages.Add("Experiences: " + experienceValue);
            }
            else if (clickCount == 2)
            {
                int gloomGoldValue = Random.Range(10, 51);
                messages.Add("GloomGold: " + gloomGoldValue);
            }
            else if (clickCount == 3)
            {
                int randomIndex = Random.Range(0, 2);
                string potionName = (randomIndex == 0) ? "potion 1" : "potion 2";
                messages.Add("Object: " + potionName);
            }

            timer = messageDuration;
        }
        else
        {
            isCooldown = true;
            popupObject.SetActive(true);
            UpdateCooldownPopupText();
            StartCoroutine(ClearMessagesAfterDelay(2f));
        }
    }

    IEnumerator ClearMessagesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearMessages();
    }

    void ClearMessages()
    {
        messages.Clear();
        messageText.text = "";
    }

    void UpdateCooldownPopupText()
    {
        if (popupObject.activeSelf)
        {
            popUpText.text = "Vous avez déjà tout récupéré. Temps restant : " + Mathf.Ceil(cooldownTimer) + " secondes";
        }
    }
}
