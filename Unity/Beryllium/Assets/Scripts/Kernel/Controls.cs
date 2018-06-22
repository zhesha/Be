using UnityEngine;

public class Controls {

    readonly string upKey, downKey, leftKey, rightKey, fireKey;
    readonly bool auto;

    public Controls (PlayerIndex playerIndex) {
        if (playerIndex == PlayerIndex.first) {
            upKey = "up";
            downKey = "down";
            leftKey = "left";
            rightKey = "right";
            fireKey = "space";
        } else if (playerIndex == PlayerIndex.second) {
            upKey = "w";
            downKey = "s";
            leftKey = "a";
            rightKey = "d";
            fireKey = "f";
        } else {
            auto = true;
        }
    }

    public bool up {
        get {
            if (auto) {
                return false;
            }
            return Input.GetKey(upKey);
        }
    }

    public bool down {
        get {
            if (auto) {
                return false;   
            }
            return Input.GetKey(downKey);
        }
    }

    public bool left {
        get {
            if (auto) {
                return false;
            }
            return Input.GetKey(leftKey);
        }
    }

    public bool right {
        get {
            if (auto) {
                return false;
            }
            return Input.GetKey(rightKey);
        }
    }

    public bool fire {
        get {
            return auto || Input.GetKey(fireKey);
        }
    }
}
