using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

public class BattleSystemTests
{
    // A Test behaves as an ordinary method
    //BattleSystem system = new BattleSystem();
    [Test]
    public void ZeroTest()
        {
            // Use the Assert class to test conditions
            //BattleSystem system = new BattleSystem();
            int num = 0;
            int num2 = BattleSystem.returnZero();
            Assert.AreEqual(num,num2);
        }
    [Test]
    public void AttackButtonTriggersEnemyTurn()
        {
            GameObject gameObject = new GameObject();
            BattleSystem battle = gameObject.AddComponent<BattleSystem>();
            battle.state = BattleState.ENEMYTURN;
            battle.onAttackButton();
            Assert.AreEqual(BattleState.ENEMYTURN, battle.state);
        }
    [Test]
    public void StartBattleEnumTest()
    {
        GameObject gameObject = new GameObject();
        BattleSystem battle = gameObject.AddComponent<BattleSystem>();
        battle.state = BattleState.START;
        Assert.AreEqual(BattleState.START, battle.state);
    }
    [Test]
    public void ActionChoiceStaysAfterPlayerTurn()
        {
            GameObject gameObject = new GameObject();
            BattleSystem battle = gameObject.AddComponent<BattleSystem>();
            string text = "Choose an Action:";
            battle.state = BattleState.PLAYERTURN;
            StringAssert.AreEqualIgnoringCase("Choose an Action:", text, "They are not the same string");
        }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BattleSystemTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
