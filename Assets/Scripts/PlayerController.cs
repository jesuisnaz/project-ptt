using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Vector2 _minPoint;
    [SerializeField] private Vector2 _maxPoint;

    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
   
    public Action<DirectionWrapper> OnMoveChange = delegate {};
    public Action<DirectionWrapper> OnAnimationChange = delegate {};
    private bool isCurrentlyMoving(float horizontalAxis, float verticalAxis)
    {
        return Mathf.Abs(horizontalAxis) != 0f &&
               Mathf.Abs(verticalAxis) != 0f;
    }

    private bool isWithinBounds(float deltaHorizontal, float deltaVertical) {
        float afterMoveHorizontal = gameObject.transform.position.x + deltaHorizontal;
        float afterMoveVertical = gameObject.transform.position.y + deltaVertical;
        return _minPoint.x <= afterMoveHorizontal && afterMoveHorizontal <= _maxPoint.x &&
            _minPoint.y <= afterMoveVertical && afterMoveVertical <= _maxPoint.y;
    }

    private void Update()
    {
        float horizontalAxisInput = Input.GetAxisRaw(HorizontalAxisName);
        float verticalAxisInput = Input.GetAxisRaw(VerticalAxisName);
        if (!isCurrentlyMoving(horizontalAxisInput, verticalAxisInput) &&
            Vector3.Distance(transform.position, _movePoint.position) <= Tolerance)
        {
            if (Mathf.Abs(horizontalAxisInput) == 1f && isWithinBounds(horizontalAxisInput, verticalAxisInput))
            {
                OnMoveChange(new DirectionWrapper(HorizontalAxisName, horizontalAxisInput));
            }
            if (Mathf.Abs(verticalAxisInput) == 1f && isWithinBounds(horizontalAxisInput, verticalAxisInput))
            {
                OnMoveChange(new DirectionWrapper(VerticalAxisName, verticalAxisInput));
            }
            OnAnimationChange(new DirectionWrapper(VerticalAxisName, verticalAxisInput));
            OnAnimationChange(new DirectionWrapper(HorizontalAxisName, horizontalAxisInput));
        }
    }
}
