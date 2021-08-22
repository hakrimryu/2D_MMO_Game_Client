using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine.Tilemaps;
#endif

public class MapEditor
{
#if UNITY_EDITOR
    
    [MenuItem("Tools/GenerateMap %#g")]
    private static void GenerateMap()
    {
        // Map取得
        GameObject go = GameObject.Find("Map");
        if (go == null)
            return;
        
        // Collision Tilemap 取得
        Tilemap tm = Util.FindChild<Tilemap>(go, "Tilemap_Collision", true);
        if (tm == null)
            return;
        
        // マップデータ出力
        using (var writer = File.CreateText("Assets/Ryu/Resources/Map/output.txt"))
        {
            writer.WriteLine(tm.cellBounds.xMin);
            writer.WriteLine(tm.cellBounds.xMax);
            writer.WriteLine(tm.cellBounds.yMin);
            writer.WriteLine(tm.cellBounds.yMax);

            // 左上から始めて右下に出力
            for (int y = tm.cellBounds.yMax; y >= tm.cellBounds.yMin; y--)
            {
                for (int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++)
                {
                    TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                    if (tile != null)
                        writer.Write("1");
                    else
                        writer.Write("0");
                }
                writer.WriteLine();
            }
        }

    }


#endif
}
