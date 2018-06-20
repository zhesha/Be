using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUtils {

	public static SceneUtils instance {
        get {
            return new SceneUtils(
                (float)Screen.width,
                (float)Screen.height,
                Camera.main.orthographicSize,
                GameObject.Find("Ground").GetComponent<LineRenderer>().startWidth
            );
        }
    }

    private float screenWidth;
    private float screenHeight;
    private float cameraSize;

    public float bottomShift { private set; get; }

    public SceneUtils (float screenWidth, float screenHeight, float cameraSize, float groundWidth) {
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.cameraSize = cameraSize;
        bottomShift = groundWidth;
    }

    public float maxX {
        get {
            return screenWidth / screenHeight * cameraSize;
        }
    }

    public float minX {
        get {
            return -maxX;
        }
    }

    public float maxY {
        get {
            return cameraSize;
        }
    }

    public float minY {
        get {
            return -maxY + bottomShift;
        }
    }

    public bool inBound (Vector3 position) {
        if (
            position.x > maxX ||
            position.x < minX ||
            position.y > maxY ||
            position.y < minY
        ) {
            return false;
        } else {
            return true;
        }
    }
}
