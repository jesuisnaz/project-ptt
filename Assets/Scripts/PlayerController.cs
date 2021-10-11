using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _movePoint;

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

    private void Update()
    {
        float horizontalAxisInput = Input.GetAxisRaw(HorizontalAxisName);
        float verticalAxisInput = Input.GetAxisRaw(VerticalAxisName);
        if (!isCurrentlyMoving(horizontalAxisInput, verticalAxisInput) &&
            Vector3.Distance(transform.position, _movePoint.position) <= Tolerance)
        {
            if (Mathf.Abs(horizontalAxisInput) == 1f)
            {
                OnMoveChange(new DirectionWrapper(HorizontalAxisName, horizontalAxisInput));
            }
            if (Mathf.Abs(verticalAxisInput) == 1f)
            {
                OnMoveChange(new DirectionWrapper(VerticalAxisName, verticalAxisInput));
            }
            OnAnimationChange(new DirectionWrapper(VerticalAxisName, verticalAxisInput));
            OnAnimationChange(new DirectionWrapper(HorizontalAxisName, horizontalAxisInput));
        }
    }
}
