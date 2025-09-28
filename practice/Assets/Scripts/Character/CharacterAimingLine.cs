using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using Game.Tools;
using UnityEngine;

public class CharacterAimingLine : MonoBehaviour, IObserverUpdate
{
    public Character target;

    protected Character _character;
    protected Camera _camera;
    protected LineRenderer _lineRenderer;

    [Range(0.01f, 1.0f)]
    public float lineWidth = 0.1f;
    public Color lineColor = Color.white;
    public bool isShow = true;

    [Header("�ӳ�����")]
    [Range(0.05f, 0.5f)]
    public float delayTime = 0.1f;

    [Header("��������")]
    public LayerMask collisionMask = -1; // ��ײ������
    public float maxLineDistance = 100f; // ������߾���

    protected Vector2 _currentTargetPos;
    protected Vector2 _smoothedPos;
    protected Vector2 _lineEndPoint; // ʵ�ʵ����յ㣨������ײ��

    private void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {

        _lineRenderer = ComponentTools.GetOrLogComponent<LineRenderer>(gameObject);
        _lineRenderer.positionCount = 2;
        _lineRenderer.widthMultiplier = lineWidth;
        _lineRenderer.useWorldSpace = true;

        if (_lineRenderer.material == null)
        {
            _lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        }
        _lineRenderer.material.color = lineColor;

        _camera = Camera.main;
        Debug.Assert(_camera != null, "�����δ�ҵ���");
    }

    protected void DrawAimingLine()
    {
        if (!isShow || _lineRenderer == null || _character == null) return;

        // 1. ƽ���������λ��
        _smoothedPos = Vector2.Lerp(_smoothedPos, _currentTargetPos, Time.deltaTime / delayTime);

        // 2. �������߷��򣨴ӽ�ɫ��ƽ��������λ�ã�
        Vector2 shootDirection = (_smoothedPos - (Vector2)_character.transform.position).normalized;

        // 3. ���߼�⣬�ҵ���ײ��
        _lineEndPoint = CalculateLineEndPoint(_character.transform.position, shootDirection);

        // 4. ������׼��
        _lineRenderer.SetPosition(0, _character.transform.position);
        _lineRenderer.SetPosition(1, _lineEndPoint);
    }

    /// <summary>
    /// ������׼�ߵ��յ㣨������ײ�壩
    /// </summary>
    protected Vector2 CalculateLineEndPoint(Vector2 startPoint, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, maxLineDistance, collisionMask);

        if (hit.collider != null)
        {
            // ������ײ�壬������ײ���ֹ
            return hit.point;
        }
        else
        {
            // û����ײ�������쵽������
            return startPoint + direction * maxLineDistance;
        }
    }

    public Vector2 GetMouseWorldPosition()
    {
        if (_camera == null) return Vector2.zero;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -_camera.transform.position.z; // ��Ҫ��������ȷ��Zֵ
        return _camera.ScreenToWorldPoint(mousePos);
    }

    public virtual void ObserverUpdate()
    {
        if (isShow)
        {
            DrawAimingLine();
        }
        else
        {
            // ����ʱ�������
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }
    }

    private void OnEnable()
    {
        UpdateManager.RegisterObserverUpdate(this);
        if (_lineRenderer != null)
            _lineRenderer.enabled = isShow;
    }

    private void OnDisable()
    {
        UpdateManager.UnRegisterObserverUpdate(this);
        if (_lineRenderer != null)
            _lineRenderer.enabled = false;
    }

    // ���Ի��ƣ�ֻ��Scene��ͼ����ʾ��
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || !isShow || _character == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_character.transform.position, _lineEndPoint);
        Gizmos.DrawWireSphere(_lineEndPoint, 0.1f);
    }
}