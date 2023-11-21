namespace LionKing.GameStateConfig;

public class GameState
{
    public bool IsMenuOpen = true;
    public bool IsGameStart = false;
    public bool IsGameEnd = false;
    public bool IsCaught = false;


    public int SimbaStartingPosX { get; set; }
    public int SimbaStartingPosY { get; set; }

    public int HunterStartingPosX { get; set; }
    public int HunterStartingPosY { get; set; }

    public BoundaryConfig playgroundBoundaryX { get; set; }
    public BoundaryConfig playgroundBoundaryY { get; set; }

    public double RecordedStartTime = 0;
    public double PauseTime = 0;
    public double TimerSeconds = 0;

}