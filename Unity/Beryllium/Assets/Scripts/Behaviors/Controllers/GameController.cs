using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public LineRenderer ground;

	void Start () {
        placeGround();

        var playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        var playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        var firstPlayer = playerObject.GetComponent<Player>();
        firstPlayer.setUp(PlayerIndex.first);

        playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        var secondPlayer = playerObject.GetComponent<Player>();
        if (Global.numberOfPlayer == NumberOfPlayer.two) {
            secondPlayer.setUp(PlayerIndex.second);
        } else {
            secondPlayer.setUp(PlayerIndex.auto);
        }

        wave();
	}
	
	void Update () {
        var target = GameObject.FindWithTag("Target");
        if (target == null) {
            wave();
        }
        if (Input.GetKey("escape")) {
            SceneManager.LoadScene("MenuScene");
        }
	}

    void placeGround () {
        float groundShift = ground.startWidth / 2;
        SceneUtils scene = SceneUtils.instance;
        var groundY = scene.minY - groundShift;
        var start = new Vector3(scene.maxX, groundY, 0);
        var end = new Vector3(scene.minX, groundY, 0);
        ground.SetPosition(0, start);
        ground.SetPosition(1, end);
    }

    void wave () {
        if (Global.gameType == GameType.shootingGallary) {
            sootingGallaryWave();
        } else {
            antiAirAndTorpedoWave();
        }
    }

    void antiAirAndTorpedoWave () {
        var path = randomizePath();
        var targetType = randomizeTargetType();
        var movementDirection = randomizeDirection();

        for (int i = 0; i < path.Length; i++) {
            var exist = path[i];
            if (exist) {
                spawnAir(i, targetType, movementDirection);
            }
        }
    }

    void sootingGallaryWave () {
        var path = randomizePath();

        for (int i = 0; i < path.Length; i++) {
            var exist = path[i];
            if (exist) {

                var targetType = randomizeTargetType();
                var movementDirection = randomizeDirection();
                spawnAir(i, targetType, movementDirection);
            }
        }
    }

    bool[] randomizePath () {
        const int pathCount = 6;
        const float boundaryProbability = 0.3f;

        var result = new bool[pathCount];

        for (int i = 0; i < pathCount; i++) {
            var probability = Random.Range(0, 1f);
            result[i] = probability > boundaryProbability;
        }
        return result;
    }

    MovementDirection randomizeDirection () {
        if (Random.Range(0, 2) > 0) {
            return MovementDirection.left;
        }
        return MovementDirection.right;
    }

    TargetType randomizeTargetType () {
        int randomType = 0;
        switch (Global.gameType) {
            case GameType.antiAircraft:
                randomType = Random.Range(0, 3);
                break;
            case GameType.torpedo:
                randomType = Random.Range(3, 6);
                break;
            case GameType.shootingGallary:
                randomType = Random.Range(6, 8);
                break;
        }

        return (TargetType)randomType;
    }

    void spawnAir (int path, TargetType targetType, MovementDirection direction) {
        var options = new TargetOptions(targetType);

        var targetPrefab = Resources.Load("Prefabs/Target");
        var targetObj = (GameObject)Instantiate(
            targetPrefab,
            Vector3.zero,
            Quaternion.identity
        );

        var target = targetObj.GetComponent<Target>();
        target.init(options, direction, path);
    }
}