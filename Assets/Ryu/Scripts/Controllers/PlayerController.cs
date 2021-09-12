using UnityEngine;
using static Define;

/// <summary>
/// プレーヤーコントローラ
/// </summary>
public class PlayerController : CreatureController
{
    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
        // 動き入力
        this.GetDirInput();
        base.UpdateController();
    }
    
    /// <summary>
    /// TODO. 臨時、カメラ移動
    /// </summary>
    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    /// <summary>
    /// 動き入力
    /// </summary>
    private void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.Dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.Dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.Dir = MoveDir.Right;
        }
        else
        {
            this.Dir = MoveDir.None;
        }
    }
}
