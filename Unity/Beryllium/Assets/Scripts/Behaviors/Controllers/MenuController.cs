using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController: MonoBehaviour {
    
    public Button OnePlayerButton;
    public Button TwoPlayerButton;

    public Button AntiAircraftButton;
    public Button TorpedoButton;
    public Button ShootingGallaryButton;

    public Button PlayButton;
    public Button ExitButton;

    // Use this for initialization
    void Start () {
        selectNumberOfPlayer(NumberOfPlayer.one);
        selectGameType(GameType.antiAircraft);
    }

    public void onPlayClick () {
        SceneManager.LoadScene("GameScene");
    }

    public void onExitClick () {
        Application.Quit();
    }

    public void onOnePlayerClick () {
        selectNumberOfPlayer(NumberOfPlayer.one);
    }

    public void onTwoPlayerClick () {
        selectNumberOfPlayer(NumberOfPlayer.two);
    }

    public void onAntiAircraftClick () {
        selectGameType(GameType.antiAircraft);
    }

    public void onTorpedoClick () {
        selectGameType(GameType.torpedo);
    }

    public void onShootingGallaryClick () {
        selectGameType(GameType.shootingGallary);
    }

    void selectNumberOfPlayer (NumberOfPlayer numberOfPlayer) {
        unselectNumberOfPlayerButtons();
        Global.numberOfPlayer = numberOfPlayer;
        var button = buttonForNumberOfPlayer(numberOfPlayer);
        button.colors = selectedColors();
    }

    void selectGameType (GameType gameType) {
        unselectGameTypeButtons();
        Global.gameType = gameType;
        var button = buttonForGameType(gameType);
        button.colors = selectedColors();
    }

    void unselectNumberOfPlayerButtons () {
        OnePlayerButton.colors = unselectedColors();
        TwoPlayerButton.colors = unselectedColors();
    }

    void unselectGameTypeButtons () {
        AntiAircraftButton.colors = unselectedColors();
        TorpedoButton.colors = unselectedColors();
        ShootingGallaryButton.colors = unselectedColors();
    }

    ColorBlock unselectedColors () {
        ColorBlock colors = ColorBlock.defaultColorBlock;
        float shadeOfGray = 0.7843137f;//c8 for same color as camera background
        colors.normalColor = new Color(shadeOfGray, shadeOfGray, shadeOfGray);
        colors.highlightedColor = Color.white;
        colors.pressedColor = Color.gray;

        return colors;
    }

    ColorBlock selectedColors () {
        ColorBlock colors = ColorBlock.defaultColorBlock;
        colors.normalColor = Color.cyan;
        colors.highlightedColor = Color.cyan;
        colors.pressedColor = Color.cyan;

        return colors;
    }

    Button buttonForNumberOfPlayer (NumberOfPlayer numberOfPlayer) {
        switch (numberOfPlayer) {
            case NumberOfPlayer.one:
                return OnePlayerButton;
            case NumberOfPlayer.two:
                return TwoPlayerButton;
            default:
                return null;
        }
    }

    Button buttonForGameType (GameType gameType) {
        switch (gameType) {
            case GameType.antiAircraft:
                return AntiAircraftButton;
            case GameType.torpedo:
                return TorpedoButton;
            case GameType.shootingGallary:
                return ShootingGallaryButton;
            default:
                return null;
        }
    }
}