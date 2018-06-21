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

    private void selectNumberOfPlayer (NumberOfPlayer numberOfPlayer) {
        unselectNumberOfPlayerButtons();
        Global.numberOfPlayer = numberOfPlayer;
        Button button = buttonForNumberOfPlayer(numberOfPlayer);
        button.colors = selectedColors();
    }

    private void selectGameType (GameType gameType) {
        unselectGameTypeButtons();
        Global.gameType = gameType;
        Button button = buttonForGameType(gameType);
        button.colors = selectedColors();
    }

    private void unselectNumberOfPlayerButtons () {
        OnePlayerButton.colors = unselectedColors();
        TwoPlayerButton.colors = unselectedColors();
    }

    private void unselectGameTypeButtons () {
        AntiAircraftButton.colors = unselectedColors();
        TorpedoButton.colors = unselectedColors();
        ShootingGallaryButton.colors = unselectedColors();
    }

    private ColorBlock unselectedColors () {
        ColorBlock colors = ColorBlock.defaultColorBlock;
        float shadeOfGray = 0.7843137f;//c8 for same color as camera background
        colors.normalColor = new Color(shadeOfGray, shadeOfGray, shadeOfGray);
        colors.highlightedColor = Color.white;
        colors.pressedColor = Color.gray;

        return colors;
    }

    private ColorBlock selectedColors () {
        ColorBlock colors = ColorBlock.defaultColorBlock;
        colors.normalColor = Color.cyan;
        colors.highlightedColor = Color.cyan;
        colors.pressedColor = Color.cyan;

        return colors;
    }

    private Button buttonForNumberOfPlayer (NumberOfPlayer numberOfPlayer) {
        if (numberOfPlayer == NumberOfPlayer.one) {
            return OnePlayerButton;
        } else if (numberOfPlayer == NumberOfPlayer.two) {
            return TwoPlayerButton;
        }

        return null;
    }

    private Button buttonForGameType (GameType gameType) {
        if (gameType == GameType.antiAircraft) {
            return AntiAircraftButton;
        } else if (gameType == GameType.torpedo) {
            return TorpedoButton;
        } else if (gameType == GameType.shootingGallary) {
            return ShootingGallaryButton;
        }
        return null;
    }
}