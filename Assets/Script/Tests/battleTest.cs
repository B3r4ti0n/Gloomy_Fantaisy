using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Player;
using PlayerControler;

public class battleTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestPlayerTakeDamagePasses()
    {
        // Player player = new Player();
        // player.SetMaxHealth(100);
        // player.SetArmor(0);
        // player.SetMana(100);
        // player.SetSpeed(10);
        // PlayerControler playerControler = new PlayerControler(); 
        // playerControler.SetPlayer(player);      
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator battleTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
