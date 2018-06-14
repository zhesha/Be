using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NumberOfPlayer {one, two}
public enum GameType {antiAircraft, torpedo, shootingGallary, bomber}

public static class Global {
    public static NumberOfPlayer numberOfPlayer {get; set;}
    public static GameType gameType {get; set;}
}
