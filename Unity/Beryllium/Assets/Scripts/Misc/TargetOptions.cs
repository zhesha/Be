using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOptions {

    const string pathPrefix = "Sprites/";
    readonly public float widthShift;
    readonly public string name;
    readonly public int spriteNumber;

    public TargetOptions (TargetType type) {
        switch (type) {
            case TargetType.bigAircraft:
                widthShift = 0.5f;
                name = "Air";
                spriteNumber = 0;
                break;
            case TargetType.mediumAircraft:
                widthShift = 0.5f;
                name = "Air";
                spriteNumber = 1;
                break;
            case TargetType.smallAircraft:
                widthShift = 0.5f;
                name = "Air";
                spriteNumber = 2;
                break;
            case TargetType.firstShip:
                widthShift = 0.5f;
                name = "Sea";
                spriteNumber = 0;
                break;
            case TargetType.secondShip:
                widthShift = 0.5f;
                name = "Sea";
                spriteNumber = 1;
                break;
            case TargetType.thirdShip:
                widthShift = 0.5f;
                name = "Sea";
                spriteNumber = 2;
                break;
        }
    }

    public string path {
        get {
            return pathPrefix + name;
        }
    }
}
