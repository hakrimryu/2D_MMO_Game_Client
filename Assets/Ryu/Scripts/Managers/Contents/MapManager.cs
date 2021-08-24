using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// マップサイズ情報
    /// </summary>
    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }

    /// <summary>
    /// Collision
    /// </summary>
    private bool[,] _collision;
    
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
        
        // Collision 関連
        TextAsset mapTxt = Managers.Resource.Load<TextAsset>($"Map/{mapName}");
        
        // パーシング
        StringReader reader = new StringReader(mapTxt.text);
        this.MinX = int.Parse(reader.ReadLine());
        this.MaxX = int.Parse(reader.ReadLine());
        this.MinY = int.Parse(reader.ReadLine());
        this.MaxY = int.Parse(reader.ReadLine());

        // Collisionカウント取得
        int xCount = this.MaxX - this.MinX + 1;
        int yCount = this.MaxY - this.MinY + 1;
        
        this._collision = new bool[yCount, xCount];
        // Collision取得
        for (int y = 0; y < yCount; y++)
        {
            string line = reader.ReadLine();
            for (int x = 0; x < xCount; x++)
            {
                this._collision[y, x] = (line[x] == '1' ? true : false);
            }
        }

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
