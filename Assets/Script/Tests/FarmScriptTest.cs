using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class FarmScriptTest
{
    private FarmScript farmScript;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject for the farm script
        GameObject farmObject = new GameObject();
        farmScript = farmObject.AddComponent<FarmScript>();

        // Create a new GameObject for the messageText
        GameObject messageTextObject = new GameObject("MessageText");
        farmScript.messageText = messageTextObject.AddComponent<Text>();

        // Assign the messageText reference to the Text component
        farmScript.messageText.text = "";

        // Create a new GameObject for the popupObject
        farmScript.popupObject = new GameObject();

        // Add a Text component to the popupObject
        farmScript.popUpText = farmScript.popupObject.AddComponent<Text>();

        // Initialize the messages list
        farmScript.messages = new List<string>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(farmScript.gameObject);
    }

    [Test]
    public void OnClickFarmButton_AddsExperienceMessage()
    {
        // Call the OnClickFarmButton method
        farmScript.OnClickFarmButton();

        // Assert that a message has been added to the messages list
        Assert.AreEqual(1, farmScript.messages.Count);
        // Assert that the added message contains "Experiences:"
        Assert.IsTrue(farmScript.messages[0].Contains("Experiences:"));

        // Assert that the added message contains "GloomGold:"
        // Assert.AreEqual(2, farmScript.messages.Count);
        // Assert.IsTrue(farmScript.messages[1].Contains("GloomGold:"));

        // Assert that the added message contains "Object:"
        // Assert.AreEqual(3, farmScript.messages.Count);
        // Assert.IsTrue(farmScript.messages[2].Contains("Object:"));
    }

    [Test]
    public void OnClickFarmButton_ResetsClickCount_AfterCooldown()
    {
        // Set clickCount to 3
        farmScript.OnClickFarmButton();
        farmScript.OnClickFarmButton();
        farmScript.OnClickFarmButton();

        // Trigger cooldown
        farmScript.OnClickFarmButton();

        // Assert that clickCount has been reset to 0
        Assert.AreEqual(0, farmScript.clickCount);
    }

    [UnityTest]
    public IEnumerator OnClickFarmButton_SetsCooldownPopupText()
    {
        // Activate the farmScript GameObject
        farmScript.gameObject.SetActive(true);

        // Set cooldown state
        farmScript.isCooldown = true;

        // Call the OnClickFarmButton method
        farmScript.OnClickFarmButton();

        yield return null; // Wait for the end of the frame

        // Assert that the popupObject is active
        Assert.IsTrue(farmScript.popupObject.activeSelf);
        // Assert that the popUpText contains "Remaining time:"
        Assert.IsTrue(farmScript.popUpText.text.Contains("Remaining time:"));
    }

    [UnityTest]
    public IEnumerator OnClickFarmButton_ClearsMessagesAfterDelay()
    {
        // Activate the farmScript GameObject
        farmScript.gameObject.SetActive(true);

        // Add messages
        farmScript.OnClickFarmButton();
        farmScript.OnClickFarmButton();
        farmScript.OnClickFarmButton();

        // Trigger cooldown
        farmScript.OnClickFarmButton();

        yield return new WaitForSeconds(2f);

        // Assert that the messages list is empty
        Assert.AreEqual(0, farmScript.messages.Count);
        // Assert that the messageText is empty
        Assert.AreEqual("", farmScript.messageText.text);
    }
}
