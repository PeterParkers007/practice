using System.Collections;
using System.Collections.Generic;
using Game.Tools;
using UnityEngine;

public class PlayerMove : CharacterMove
{
    protected override void Initialize()
    {
        _character = ComponentTools.GetOrLogComponent<Character>(gameObject);
    }
    public override void Move()
    {
        // 获取输入
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // 计算目标速度
        targetVelocity = new Vector2(moveHorizontal, moveVertical) * _character.characterProperty.MovementSpeed;

        // 使用 Lerp 实现加速度效果
        if (targetVelocity.magnitude > 0.1f)
        {
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }

        // 直接移动Transform
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);
    }
}
