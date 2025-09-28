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

    [Header("延迟设置")]
    [Range(0.05f, 0.5f)]
    public float delayTime = 0.1f;

    [Header("射线设置")]
    public LayerMask collisionMask = -1; // 碰撞层掩码
    public float maxLineDistance = 100f; // 最大射线距离

    protected Vector2 _currentTargetPos;
    protected Vector2 _smoothedPos;
    protected Vector2 _lineEndPoint; // 实际的线终点（考虑碰撞）

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
        Debug.Assert(_camera != null, "主相机未找到！");
    }

    protected void DrawAimingLine()
    {
        if (!isShow || _lineRenderer == null || _character == null) return;

        // 1. 平滑跟随鼠标位置
        _smoothedPos = Vector2.Lerp(_smoothedPos, _currentTargetPos, Time.deltaTime / delayTime);

        // 2. 计算射线方向（从角色到平滑后的鼠标位置）
        Vector2 shootDirection = (_smoothedPos - (Vector2)_character.transform.position).normalized;

        // 3. 射线检测，找到碰撞点
        _lineEndPoint = CalculateLineEndPoint(_character.transform.position, shootDirection);

        // 4. 绘制瞄准线
        _lineRenderer.SetPosition(0, _character.transform.position);
        _lineRenderer.SetPosition(1, _lineEndPoint);
    }

    /// <summary>
    /// 计算瞄准线的终点（考虑碰撞体）
    /// </summary>
    protected Vector2 CalculateLineEndPoint(Vector2 startPoint, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, maxLineDistance, collisionMask);

        if (hit.collider != null)
        {
            // 碰到碰撞体，线在碰撞点截止
            return hit.point;
        }
        else
        {
            // 没有碰撞，线延伸到最大距离
            return startPoint + direction * maxLineDistance;
        }
    }

    public Vector2 GetMouseWorldPosition()
    {
        if (_camera == null) return Vector2.zero;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -_camera.transform.position.z; // 重要：设置正确的Z值
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
            // 隐藏时清空线条
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

    // 调试绘制（只在Scene视图中显示）
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || !isShow || _character == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_character.transform.position, _lineEndPoint);
        Gizmos.DrawWireSphere(_lineEndPoint, 0.1f);
    }
}