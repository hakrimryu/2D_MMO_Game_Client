using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

/// <summary>
/// プレーヤーコントローラ
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// TODO. マネージャーで管理しなければならない。
    /// </summary>
    [SerializeField]
    private Grid _grid;
    
    /// <summary>
    /// 移動速度
    /// </summary>
    private float _speed = 5.0f;
    /// <summary>
    /// 現在のセル座標
    /// </summary>
    private Vector3Int _cellPos = Vector3Int.zero;
    /// <summary>
    /// 動き方
    /// </summary>
    private MoveDir _dir = MoveDir.None;
    /// <summary>
    /// 動きチェック
    /// </summary>
    private bool _isMoving = false;
    
    void Start()
    {
        // ワールド座標取得 (TODO. キャラクターのサイズため、臨時で +0.5)
        Vector3 pos = this._grid.CellToWorld(this._cellPos) + new Vector3(0.5f, 0.65f);
        this.transform.position = pos;
    }

    void Update()
    {
        // 動き入力
        this.GetDirInput();
        // 自然な移動を表示するための移動演出
        this.UpdatePosition();
        // 動きのチェック及びアップデート
        this.UpdateIsMoving();
    }

    /// <summary>
    /// 動き入力
    /// </summary>
    private void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this._dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this._dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this._dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this._dir = MoveDir.Right;
        }
        else
        {
            this._dir = MoveDir.None;
        }
    }

    /// <summary>
    /// 動きのチェック及びアップデート
    /// </summary>
    private void UpdateIsMoving()
    {
        if (this._isMoving == false)
        {
            switch (this._dir)
            {
                case MoveDir.Up:
                    this._cellPos += Vector3Int.up;
                    this._isMoving = true;
                    break;
                case MoveDir.Down:
                    this._cellPos += Vector3Int.down;
                    this._isMoving = true;
                    break;
                case MoveDir.Left:
                    this._cellPos += Vector3Int.left;
                    this._isMoving = true;
                    break;
                case MoveDir.Right:
                    this._cellPos += Vector3Int.right;
                    this._isMoving = true;
                    break;
            }
        }
    }

    /// <summary>
    /// 自然な移動を表示するための移動演出
    /// </summary>
    private void UpdatePosition()
    {
        if (this._isMoving == false)
            return;
        
        // 目的地ワールド座標取得 (TODO. キャラクターのサイズため、臨時で +0.5)
        Vector3 destPos = this._grid.CellToWorld(this._cellPos) + new Vector3(0.5f, 0.65f);
        // 目的地 - 現在位置 = 方向Vector
        Vector3 moveDir = destPos - this.transform.position;
        
        // 残距離取得
        float dist = moveDir.magnitude;
        // 到着チェック(到着する)
        if (dist < this._speed * Time.deltaTime)
        {
            this.transform.position = destPos;
            this._isMoving = false;
        }
        else
        {
            // TODO. スピードが速い場合、バグが発生する可能性が高いため、修正が必要
            this.transform.position += moveDir.normalized * (this._speed * Time.deltaTime);
            this._isMoving = true;
        }
    }

}
