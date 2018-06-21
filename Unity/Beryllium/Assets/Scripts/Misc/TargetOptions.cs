using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOptions {

    const string pathPrefix = "Sprites/";
    readonly public string name;
    readonly public int spriteNumber;
    readonly public float speed;

    public TargetOptions (TargetType type) {
        switch (type) {
            case TargetType.bigAircraft:
                name = "Air";
                spriteNumber = 0;
                speed = 2;
                break;
            case TargetType.mediumAircraft:
                name = "Air";
                spriteNumber = 1;
                speed = 2.5f;
                break;
            case TargetType.smallAircraft:
                name = "Air";
                spriteNumber = 2;
                speed = 3;
                break;
            case TargetType.firstShip:
                name = "Sea";
                spriteNumber = 0;
                speed = 2;
                break;
            case TargetType.secondShip:
                name = "Sea";
                spriteNumber = 1;
                speed = 2.5f;
                break;
            case TargetType.thirdShip:
                name = "Sea";
                spriteNumber = 2;
                speed = 3;
                break;
            case TargetType.duck:
                name = "ShoocctingGallary";
                spriteNumber = 0;
                speed = 3;
                break;
            case TargetType.rabbit:
                name = "ShoocctingGallary";
                spriteNumber = 1;
                speed = 3;
                break;
        }
    }

    public string path {
        get {
            return pathPrefix + name;
        }
    }
}
