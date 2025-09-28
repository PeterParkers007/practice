using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMove : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] protected float acceleration = 1f;
    [Range(0f, 10f)]
    [SerializeField] protected float deceleration = 1f;

    [Tooltip("����Ŀ��λ�õ��ж�����")]
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
        // ȷ����ɫ�������
        if (_character == null) return;

        // ���㵽Ŀ��ķ���2Dƽ�棩
        Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPos);

        // ���ݾ����ж��Ƿ���Ҫ�ƶ�
        targetVelocity = distance > stopDistance
            ? direction * _character.characterProperty.MovementSpeed
            : Vector2.zero;

        // ƽ���ƶ��߼�������ұ���һ�£�
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

        // �ڹ�����Χ�ھͲ��ƶ���������Ŀ���ɫ�ƶ�
        if (Vector2.Distance(transform.position, targetCharacter.transform.position) > _character.characterProperty.AttackRange)
        {
            MoveToTargetPos(targetCharacter.transform.position);
        }
        else
        {
            // ���빥����Χ��ֹͣ�ƶ�
            targetVelocity = Vector2.zero;
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, deceleration * Time.deltaTime);
            transform.Translate(currentVelocity * Time.deltaTime, Space.World);
        }
    }
}
