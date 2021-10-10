using UnityEngine;

public class Player : MonoBehaviour, IUnit
{
    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Animator _animator;

    public Vector3 CurrentPosition { get; private set; }

    private void Start()
    {
        _movePoint.parent = null;
        CurrentPosition = _movePoint.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
        float horizontalAxisInput = Input.GetAxisRaw(HorizontalAxisName);
        float verticalAxisInput = Input.GetAxisRaw(VerticalAxisName);

        if (!isCurrentlyMoving(horizontalAxisInput, verticalAxisInput) &&
            Vector3.Distance(transform.position, _movePoint.position) <= Tolerance)
        {
            if (Mathf.Abs(horizontalAxisInput) == 1f)
            {
                _movePoint.position += new Vector3(horizontalAxisInput, 0f, 0f);
            }
            if (Mathf.Abs(verticalAxisInput) == 1f)
            {
                _movePoint.position += new Vector3(0f, verticalAxisInput, 0f);
            }
            CurrentPosition = _movePoint.position;
            _animator.SetFloat(HorizontalAxisName, horizontalAxisInput);
            _animator.SetFloat(VerticalAxisName, verticalAxisInput);
        }
    }

    private bool isCurrentlyMoving(float horizontalAxis, float verticalAxis)
    {
        return Mathf.Abs(horizontalAxis) != 0f &&
                Mathf.Abs(verticalAxis) != 0f;
    }
}
