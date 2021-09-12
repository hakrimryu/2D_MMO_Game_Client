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
    /// キャラクター·アニメーター
    /// </summary>
    protected Animator _animator;
    /// <summary>
    /// Flip用スプライト
    /// </summary>
    protected SpriteRenderer _sprite;
    
    /// <summary>
    /// クリーチャー状態
    /// </summary>
    private CreatureState _state = CreatureState.Idle;
    /// <summary>
    /// クリーチャー状態 Get Set
    /// </summary>
    public CreatureState State
    {
        get { return this._state; }
        set
        {
            if (this._state == value)
                return;

            this._state = value;
            this.UpdateAnimation();
        }
    }
    /// <summary>
    /// 動き方
    /// </summary>
    private MoveDir _dir = MoveDir.Down;
    /// <summary>
    /// 最後に、眺めた方向
    /// </summary>
    private MoveDir _lastDir = MoveDir.Down;
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

            this._dir = value;
            if (value != MoveDir.None)
                this._lastDir = value;
            
            this.UpdateAnimation();
        }
    }
    
    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        this.Init();
    }

    /// <summary>
    /// Update
    /// </summary>
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
        // Flip用スプライト取得
        this._sprite = GetComponent<SpriteRenderer>();
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
    /// ステータスに、応じたアニメー、アップデート
    /// </summary>
    protected virtual void UpdateAnimation()
    {
        if (this._state == CreatureState.Idle)
        {
            switch (this._lastDir)
            {
                case MoveDir.Up:
                    this._animator.Play("IDLE_BACK");
                    this._sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    this._animator.Play("IDLE_FRONT");
                    this._sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    this._animator.Play("IDLE_RIGHT");
                    // キャラクター Flip
                    this._sprite.flipX = true;
                    break;
                case MoveDir.Right:
                    this._animator.Play("IDLE_RIGHT");
                    this._sprite.flipX = false;
                    break;
            }
        }
        else if ( this._state == CreatureState.Moving)
        {
            switch (this._dir)
            {
                case MoveDir.Up:
                    this._animator.Play("WALK_BACK");
                    this._sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    this._animator.Play("WALK_FRONT");
                    this._sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    this._animator.Play("WALK_RIGHT");
                    // キャラクター Flip
                    this._sprite.flipX = true;
                    break;
                case MoveDir.Right:
                    this._animator.Play("WALK_RIGHT");
                    this._sprite.flipX = false;
                    break;
                case MoveDir.None:
                    
                    break;
            }
            
        }
        else if (this._state == CreatureState.Dead)
        {
            // TODO
        }
        else
        {
            // TODO
        }
    }
    
    /// <summary>
    /// 動きのチェック及びアップデート
    /// </summary>
    private void UpdateIsMoving()
    {
        if (this.State == CreatureState.Idle && _dir != MoveDir.None)
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

            if (Managers.Map.CanGo(destPos))
            {
                this._cellPos = destPos;
                this.State = CreatureState.Moving;
            }
        }
    }

    /// <summary>
    /// 自然な移動を表示するための移動演出
    /// </summary>
    private void UpdatePosition()
    {
        if ( this.State != CreatureState.Moving)
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
            
            // (例外)アニメの直接コントロール
            this._state = CreatureState.Idle;
            if ( this._dir == MoveDir.None)
                this.UpdateAnimation();
        }
        else
        {
            // TODO. スピードが速い場合、バグが発生する可能性が高いため、修正が必要
            this.transform.position += moveDir.normalized * (this._speed * Time.deltaTime);
            this.State = CreatureState.Moving;
        }
    }
}
