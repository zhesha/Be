using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public LineRenderer ground;

	void Start () {
        var gameType = Global.gameType;
        if (gameType == GameType.antiAircraft) {
            setUp();
        } else if (gameType == GameType.torpedo) {
            setUp();
        } else if (gameType == GameType.shootingGallary) {
            setUpShootingGallary();
        } else if (gameType == GameType.bomber) {
            setUpBomber();
        }
	}
	
	void Update () {
        GameObject target = GameObject.FindWithTag("Target");
        if (target == null) {
            wave();
        }
	}

    void setUp () {
        placeGround();

        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        GameObject playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player firstPlayer = playerObject.GetComponent<Player>();
        firstPlayer.setUp(PlayerIndex.first);

        playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player secondPlayer = playerObject.GetComponent<Player>();
        if (Global.numberOfPlayer == NumberOfPlayer.two) {
            secondPlayer.setUp(PlayerIndex.second);
        } else {
            secondPlayer.setUp(PlayerIndex.auto);
        }

        wave();
    }

    void setUpShootingGallary () {
        Debug.Log("Not realized");
    }

    void setUpBomber () {
        Debug.Log("Not realized");
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
        bool[] path = randomizePath();
        TargetType targetType = randomizeTargetType();
        MovementDirection movementDirection = randomizeDirection();

        for (int i = 0; i < path.Length; i++) {
            var exist = path[i];
            if (exist) {
                spawnAir(i, targetType, movementDirection);
            }
        }
    }

    bool[] randomizePath () {
        const int pathCount = 6;
        const float boundaryProbability = 0.3f;

        var result = new bool[pathCount];

        for (int i = 0; i < pathCount; i++) {
            float probability = Random.Range(0, 1f);
            result[i] = probability > boundaryProbability;
        }
        return result;
    }

    MovementDirection randomizeDirection () {
        if (Random.Range(0, 2) > 0) {
            return MovementDirection.left;
        } else {
            return MovementDirection.right;
        }
    }

    TargetType randomizeTargetType () {
        int randomType = 0;
        if (Global.gameType == GameType.antiAircraft) {
            randomType = Random.Range(0, 3);
        } else if (Global.gameType == GameType.torpedo) {
            randomType = Random.Range(3, 6);
        }
        return (TargetType)randomType;
    }

    void spawnAir (int path, TargetType targetType, MovementDirection direction) {
        TargetOptions options = new TargetOptions(targetType);

        Object targetPrefab = Resources.Load("Prefabs/Target");
        GameObject targetObj = (GameObject)Instantiate(
            targetPrefab,
            Vector3.zero,
            Quaternion.identity
        );

        Target target = targetObj.GetComponent<Target>();
        target.init(options, direction, path);
    }
}