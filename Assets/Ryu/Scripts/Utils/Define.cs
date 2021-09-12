/// <summary>
/// Define
/// </summary>
public class Define
{
    /// <summary>
    /// クリーチャーステータス
    /// </summary>
    public enum CreatureState
    {
        Idle = 0,
        Moving = 1,
        Skill = 2,
        Dead = 3
    }
    
    /// <summary>
    /// Direction
    /// 動き方
    /// </summary>
    public enum MoveDir
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
    
    /// <summary>
    /// シーンタイプ
    /// </summary>
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    /// <summary>
    /// サウンドタイプ
    /// </summary>
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    /// <summary>
    /// UIイベントタイプ
    /// </summary>
    public enum UIEvent
    {
        Click,
        Drag,
    }
}