using UnityEngine;
using NUnit.Framework;
using UnityEditor.SceneManagement;

public class MenuControllerTests {

    MenuController menuController;

    [OneTimeSetUp]
    protected void SetUp()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MenuScene.unity");
        GameObject gameObject = GameObject.Find("MenuController");
        menuController = gameObject.GetComponent<MenuController>();
    }

    [Test]
    public void onOnePlayerClick()
    {
        menuController.onOnePlayerClick();
        Assert.AreEqual(NumberOfPlayer.one, Global.numberOfPlayer);
    }

    [Test]
    public void onTwoPlayerClick()
    {
        menuController.onTwoPlayerClick();
        Assert.AreEqual(NumberOfPlayer.two, Global.numberOfPlayer);
    }

    [Test]
    public void onAntiAircraftClick()
    {
        menuController.onAntiAircraftClick();
        Assert.AreEqual(GameType.antiAircraft, Global.gameType);
    }

    [Test]
    public void onBomberClick()
    {
        menuController.onBomberClick();
        Assert.AreEqual(GameType.bomber, Global.gameType);
    }

    [Test]
    public void onTorpedoClick()
    {
        menuController.onTorpedoClick();
        Assert.AreEqual(GameType.torpedo, Global.gameType);
    }

    [Test]
    public void onShootingGallaryClick()
    {
        menuController.onShootingGallaryClick();
        Assert.AreEqual(GameType.shootingGallary, Global.gameType);
    }
}