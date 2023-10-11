using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShopTests : ScriptableObject
{
    [SerializeField] GameObject ShopManagerObject;
    [SerializeField] GameObject playerPersistencyObject;
    // A Test behaves as an ordinary method
    [Test]
    public void ShopTestsSimplePasses()
    {
        //make an instance of the shop manager
        GameObject shopManager = GameObject.Instantiate(ShopManagerObject) as GameObject;
        ShopManager shopManagerComp = shopManager.GetComponent<ShopManager>();

        //make an instance of the playerpersistency which handles player currency
        GameObject playerPersistency = GameObject.Instantiate(playerPersistencyObject) as GameObject;
        PlayerPersistency playerPeristencyComp = playerPersistency.GetComponent<PlayerPersistency>();

        //ensure the shop manager is using the testing playerpersistency
        shopManagerComp.useTestingPlayerPersistency(playerPeristencyComp);

        //simulate the player having no currency; player currency should not become negative when attempting to purchase an item
        playerPeristencyComp.money.SetCurrency(0);
        shopManagerComp.purchaseKnightHelmetTest();
        int currencyAfterPurchaseAttempt = playerPeristencyComp.money.getCurrency();

        int correctCurrencyAmount = 0;
        Assert.AreEqual(correctCurrencyAmount, currencyAfterPurchaseAttempt);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShopTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
