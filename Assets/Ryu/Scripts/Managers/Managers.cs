using UnityEngine;

/// <summary>
/// すべてのマネージャー管理
/// </summary>
public class Managers : MonoBehaviour
{
    /// <summary>
    /// 唯一性の保障
    /// </summary>
    static Managers s_instance;
    /// <summary>
    /// 唯一のマネージャーを取得
    /// </summary>
    static Managers Instance { get { Init(); return s_instance; } }

    //コンテンツエリア(Core+追加)
    #region Contents
    private MapManager _map = new MapManager();

    public static MapManager Map { get { return Instance._map; } }

    #endregion

    #region Core
    /// <summary>
    /// 各マネージャー
    /// </summary>
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    void Start()
    {
        Init();
    }

    /// <summary>
    /// マネージャー初期化
    /// </summary>
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
        }		
    }

    /// <summary>
    /// 削除
    /// </summary>
    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}