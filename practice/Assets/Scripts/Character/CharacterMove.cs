using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMove : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] protected float acceleration = 1f;
    [Range(0f, 10f)]
    [SerializeField] protected float deceleration = 1f;

    [Tooltip("到达目标位置的判定距离")]
    public float stopDistance = 0.1f;

    protected Character _character;

    protected Vector2 currentVelocity;
    protected Vector2 targetVelocity;
    private void Start()
    {
        Initialize();
    }
    protected abstract void Initialize();
    public abstract void Move();
    public void MoveToTargetPos(Vector3 targetPos)
    {
        // 确保角色组件存在
        if (_character == null) return;

        // 计算到目标的方向（2D平面）
        Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPos);

        // 根据距离判断是否需要移动
        targetVelocity = distance > stopDistance
            ? direction * _character.characterProperty.MovementSpeed
            : Vector2.zero;

        // 平滑移动逻辑（与玩家保持一致）
        if (targetVelocity.magnitude > 0.1f)
        {
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.deltaTime);
        }

        transform.Translate(currentVelocity * Time.deltaTime, Space.World);
    }

    public void MoveToCharacter(Character targetCharacter)
    {
        if (targetCharacter == null) return;

        // 在攻击范围内就不移动，否则向目标角色移动
        if (Vector2.Distance(transform.position, targetCharacter.transform.position) > _character.characterProperty.AttackRange)
        {
            MoveToTargetPos(targetCharacter.transform.position);
        }
        else
        {
            // 进入攻击范围后停止移动
            targetVelocity = Vector2.zero;
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.deltaTime);
            transform.Translate(currentVelocity * Time.deltaTime, Space.World);
        }
    }
}
