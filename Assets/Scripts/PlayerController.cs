using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Vector2 _minMapCordinatesPoint;
    [SerializeField] private Vector2 _maxMapCordinatesPoint;

    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
   
    public Action<DirectionWrapper> OnMoveChange = delegate {};
    public Action<DirectionWrapper, DirectionWrapper> OnAnimationChange = delegate {};
    
    private bool IsCurrentlyMoving(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper)
    {
        return Mathf.Abs(horizontalDirectionWrapper.AxisValue) != 0f && Mathf.Abs(verticalDirectionWrapper.AxisValue) != 0f;
    }

    private bool IsWithinBounds(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper) {
        float afterMoveHorizontal = transform.position.x + horizontalDirectionWrapper.AxisValue;
        float afterMoveVertical = transform.position.y + verticalDirectionWrapper.AxisValue;
        return _minMapCordinatesPoint.x <= afterMoveHorizontal && afterMoveHorizontal <= _maxMapCordinatesPoint.x &&
               _minMapCordinatesPoint.y <= afterMoveVertical && afterMoveVertical <= _maxMapCordinatesPoint.y;
    }

    private void Update()
    {
        ControlCommonMovement();
    }

    private void ControlCommonMovement()
    {
        DirectionWrapper horizontalDirectionWrapper = new DirectionWrapper(HorizontalAxisName);
        DirectionWrapper verticalDirectionWrapper = new DirectionWrapper(VerticalAxisName);
        if (!IsCurrentlyMoving(horizontalDirectionWrapper, verticalDirectionWrapper) &&
            Vector3.Distance(transform.position, _movePoint.position) <= Tolerance)
        {
            if (IsWithinBounds(horizontalDirectionWrapper, verticalDirectionWrapper))
            {
                if (Mathf.Abs(horizontalDirectionWrapper.AxisValue) == 1f)
                {
                    OnMoveChange(horizontalDirectionWrapper);
                }
                if (Mathf.Abs(verticalDirectionWrapper.AxisValue) == 1f)
                {
                    OnMoveChange(verticalDirectionWrapper);
                }
            }
            OnAnimationChange(horizontalDirectionWrapper, verticalDirectionWrapper);
        }
    }
}
