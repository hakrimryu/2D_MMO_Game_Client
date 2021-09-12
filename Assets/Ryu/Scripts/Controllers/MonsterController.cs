using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
        // 動き入力
        //this.GetDirInput();
        base.UpdateController();
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
