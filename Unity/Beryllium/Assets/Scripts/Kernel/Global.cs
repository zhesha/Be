public static class Global {
    public static NumberOfPlayer numberOfPlayer { get; set; }
    public static GameType gameType { get; set; }

    static Global() {
        gameType = GameType.torpedo;
    }
}
