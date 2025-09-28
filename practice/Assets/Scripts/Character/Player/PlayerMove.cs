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
        // ��ȡ����
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // ����Ŀ���ٶ�
        targetVelocity = new Vector2(moveHorizontal, moveVertical) * _character.characterProperty.MovementSpeed;

        // ʹ�� Lerp ʵ�ּ��ٶ�Ч��
        if (targetVelocity.magnitude > 0.1f)
        {
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }

        // ֱ���ƶ�Transform
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);
    }
}
