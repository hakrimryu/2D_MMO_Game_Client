using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// タイルマップ機能テスト用
/// </summary>
public class TestTilemapCollision : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tilemap;
    [SerializeField]
    private TileBase _tilemapBase;

    void Start()
    {
        this._tilemap.SetTile(new Vector3Int(0,0,0), _tilemapBase);
    }

    void Update()
    {
        List<Vector3Int> noEntry = new List<Vector3Int>();

        // タイルマップ全領域スキャン
        // 接近禁止で、使用するタイルを取得
        foreach (Vector3Int pos in this._tilemap.cellBounds.allPositionsWithin)
        {
            var title = this._tilemap.GetTile(pos);
            
            if (title != null)
                noEntry.Add(pos);
        }
    }
}
