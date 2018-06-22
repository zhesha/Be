using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SceneUtilsTests {

    SceneUtils sceneUtils;

    [OneTimeSetUp]
    protected void SetUp () {
        sceneUtils = new SceneUtils(800, 600, 5, 2);
    }

    [Test]
    public void maxX() {
        Assert.AreEqual(6.666, sceneUtils.maxX, 0.01);
    }

    [Test]
    public void minX () {
        Assert.AreEqual(-6.666, sceneUtils.minX, 0.01);
    }

    [Test]
    public void maxY () {
        Assert.AreEqual(5, sceneUtils.maxY, 0.01);
    }

    [Test]
    public void minY () {
        Assert.AreEqual(-3, sceneUtils.minY, 0.01);
    }

    [Test]
    public void inBound_success () {
        var point = new Vector3(1, 1, 0);
        Assert.AreEqual(true, sceneUtils.inBound(point));
    }

    [Test]
    public void inBound_fail () {
        var point = new Vector3(-10, 1, 0);
        Assert.AreEqual(false, sceneUtils.inBound(point));
    }
}
