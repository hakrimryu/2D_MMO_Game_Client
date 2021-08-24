using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リアルタイムで、マップのロードや削除
/// ロードされたマップから、Collisionを抽出後、移動可能・不可能の領域管理
/// </summary>
public class MapManager
{
    /// <summary>
    /// 現在Grid(照会用)
    /// </summary>
    public Grid CurrentGrid { get; private set; }

    /// <summary>
    /// マップ·ロード
    /// </summary>
    public void LoadMap(int mapId)
    {
        // ロード前削除
        this.DestroyMap();

        // マップ取得
        string mapName = "Map_" + mapId.ToString("000");
        GameObject go = Managers.Resource.Instantiate($"Map/{mapName}");
        go.name = "Map";
        
        // ロード後、Collisionを非表示
        GameObject collision = Util.FindChild(go, "Tilemap_Collision", true);
        if (collision != null)
            collision.SetActive(false);

        // 現在Grid取得
        this.CurrentGrid = go.GetComponent<Grid>();
    }

    /// <summary>
    /// マップ削除
    /// </summary>
    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
            this.CurrentGrid = null;
        }

    }
}
