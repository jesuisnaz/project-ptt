using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScaredHumanController : MonoBehaviour
{
    private const int MaxXPosition = 31;
    private const int MaxYPosition = 17;
    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    
    [SerializeField] private ScaredHuman _scaredHuman;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Vector2 _minMapCordinatesPoint;
    [SerializeField] private Vector2 _maxMapCordinatesPoint;
    [SerializeField] private Stone[] _stones;
    private Vector3 _goalPoint;
    
    public Action<DirectionWrapper> OnMoveChange = delegate {};
    public Action<DirectionWrapper, DirectionWrapper> OnAnimationChange = delegate {};
    public Action<Vector3> OnTeleport = delegate {};
    
    private void Update()
    {
        ControlCommonMovement();
    }

    private void Start()
    {
        GenerateRandomPoint();
    }

    private bool IsCurrentlyMoving(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper)
    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          return Mathf.Abs(horizontalDirectionWrapper.AxisValue) != 0f && Mathf.Abs(verticalDirectionWrapper.AxisValue) != 0f;
    }

    private bool IsLegalMove(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper) {
        float afterMoveHorizontal = _scaredHuman.CurrentPosition.x + horizontalDirectionWrapper.AxisValue; 
        float afterMoveVertical = _scaredHuman.CurrentPosition.y + verticalDirectionWrapper.AxisValue;
        Vector3 afterMovePosition = new Vector3(afterMoveHorizontal, afterMoveVertical);
        bool isWithinBounds = _minMapCordinatesPoint.x <= afterMoveHorizontal &&
                              afterMoveHorizontal <= _maxMapCordinatesPoint.x &&
                              _minMapCordinatesPoint.y <= afterMoveVertical &&
                              afterMoveVertical <= _maxMapCordinatesPoint.y;
        bool isNotStuckIntoStone = true;
        foreach (var stone in _stones)
        {
            if (stone.CurrentPosition == afterMovePosition)
            {
                isNotStuckIntoStone = false;
            }
        }
        
        return isWithinBounds && isNotStuckIntoStone;
    }

    private void ControlCommonMovement()
    {
        int distanceToXCord = (int)_goalPoint.x - (int)_scaredHuman.CurrentPosition.x;
        int distanceToYCord = (int)_goalPoint.y - (int)_scaredHuman.CurrentPosition.y;
        if (distanceToXCord == 0 && distanceToYCord == 0)
        {
            GenerateRandomPoint();
            return;
        }

        DirectionWrapper horizontalDirectionWrapper = new DirectionWrapper(HorizontalAxisName, Mathf.Sign(distanceToXCord));
        DirectionWrapper verticalDirectionWrapper = new DirectionWrapper(VerticalAxisName, Mathf.Sign(distanceToYCord));

        bool isMovingHorizontally = Convert.ToBoolean(Random.Range(0, 1));
        
        if (horizontalDirectionWrapper.AxisValue == 0)
        {
            isMovingHorizontally = false;
        }
        
        if (verticalDirectionWrapper.AxisValue == 0)
        {
            isMovingHorizontally = true;
        }
        
        if (!IsCurrentlyMoving(horizontalDirectionWrapper, verticalDirectionWrapper) &&
            Vector3.Distance(transform.position, _movePoint.position) <= Tolerance)
        {
            if (IsLegalMove(horizontalDirectionWrapper, verticalDirectionWrapper))
            {
                if (isMovingHorizontally)
                {
                    OnMoveChange(horizontalDirectionWrapper);
                }
                else
                {
                    OnMoveChange(verticalDirectionWrapper);
                }
            }
            OnAnimationChange(horizontalDirectionWrapper, verticalDirectionWrapper);
        }
    }
    
    private void GenerateRandomPoint()
    {
        int x = Random.Range(0, MaxXPosition), y = Random.Range(0, MaxYPosition), z = 0;
        _goalPoint= new Vector3(x, y, z);
        if (Random.Range(0, 10) == 1)
        {
            OnTeleport(_goalPoint);
            GenerateRandomPoint();
        }
    }
}
