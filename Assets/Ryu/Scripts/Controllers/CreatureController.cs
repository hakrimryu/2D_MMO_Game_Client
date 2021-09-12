using UnityEngine;
using static Define;

public class CreatureController : MonoBehaviour
{
    /// <summary>
    /// 移動速度
    /// </summary>
    protected float _speed = 5.0f;
    /// <summary>
    /// 現在のセル座標
    /// </summary>
    protected Vector3Int _cellPos = Vector3Int.zero;
    /// <summary>
    /// 動きチェック
    /// </summary>
    protected bool _isMoving = false;
    /// <summary>
    /// キャラクター·アニメーター
    /// </summary>
    protected Animator _animator;
    /// <summary>
    /// 動き方
    /// </summary>
    private MoveDir _dir = MoveDir.Down;
    /// <summary>
    /// キャラクターの方向処理に伴うアニメーション再生
    /// </summary>
    public MoveDir Dir
    {
        get { return this._dir; }
        set
        {
            if (this._dir == value)
                return;

            switch (value)
            {
                case MoveDir.Up:
                    this._animator.Play("WALK_BACK");
                    // キャラクター Flip (初期化用)
                    this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Down:
                    this._animator.Play("WALK_FRONT");
                    // キャラクター Flip (初期化用)
                    this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Left:
                    this._animator.Play("WALK_RIGHT");
                    // キャラクター Flip
                    this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Right:
                    this._animator.Play("WALK_RIGHT");
                    // キャラクター Flip
                    this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.None:
                    if (this._dir == MoveDir.Up)
                    {
                        this._animator.Play("IDLE_BACK");
                        // キャラクター Flip (初期化用)
                        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (this._dir == MoveDir.Down)
                    {
                        this._animator.Play("IDLE_FRONT");
                        // キャラクター Flip (初期化用)
                        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (this._dir == MoveDir.Left)
                    {
                        this._animator.Play("IDLE_RIGHT");
                        // キャラクター Flip
                        this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        this._animator.Play("IDLE_RIGHT");
                        // キャラクター Flip
                        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    break;
            }

            this._dir = value;
        }
    }

    void Start()
    {
        this.Init();
    }

    void Update()
    {
        this.UpdateController();
    }

    /// <summary>
    /// Startで、初期化(再定義用)
    /// </summary>
    protected virtual void Init()
    {
        // アニメーター取得
        this._animator = GetComponent<Animator>();
        // ワールド座標取得 (TODO. キャラクターのサイズため、臨時で +0.5)
        Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(this._cellPos) + new Vector3(0.5f, 0.5f);
        this.transform.position = pos;
    }

    /// <summary>
    /// Update (再定義用)
    /// </summary>
    protected virtual void UpdateController()
    {
        // 自然な移動を表示するための移動演出
        this.UpdatePosition();
        // 動きのチェック及びアップデート
        this.UpdateIsMoving();
    }
    
    /// <summary>
    /// 動きのチェック及びアップデート
    /// </summary>
    private void UpdateIsMoving()
    {
        if (this._isMoving == false && _dir != MoveDir.None)
        {
            Vector3Int destPos = this._cellPos;
            
            switch (this._dir)
            {
                case MoveDir.Up:
                    destPos += Vector3Int.up;
                    break;
                case MoveDir.Down:
                    destPos += Vector3Int.down;
                    break;
                case MoveDir.Left:
                    destPos += Vector3Int.left;
                    break;
                case MoveDir.Right:
                    destPos += Vector3Int.right;
                    break;
            }

            Debug.Log($"{Managers.Map.CanGo(destPos)}");
            if (Managers.Map.CanGo(destPos))
            {
                this._cellPos = destPos;
                this._isMoving = true ;
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
        Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(this._cellPos) + new Vector3(0.5f, 0.5f);
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
