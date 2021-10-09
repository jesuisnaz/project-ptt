using UnityEngine;

public class Player : MonoBehaviour, IUnit
{
    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private Animator animator;

    public Vector3 CurrentPosition { get; private set; }

    private void Start()
    {
        movePoint.parent = null;
        CurrentPosition = movePoint.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        float horizontalAxisInput = Input.GetAxisRaw(HorizontalAxisName);
        float verticalAxisInput = Input.GetAxisRaw(VerticalAxisName);

        if (!isCurrentlyMoving(horizontalAxisInput, verticalAxisInput) &&
            Vector3.Distance(transform.position, movePoint.position) <= Tolerance)
        {
            if (Mathf.Abs(horizontalAxisInput) == 1f)
            {
                movePoint.position += new Vector3(horizontalAxisInput, 0f, 0f);
                CurrentPosition = movePoint.position;
            }
            if (Mathf.Abs(verticalAxisInput) == 1f)
            {
                movePoint.position += new Vector3(0f, verticalAxisInput, 0f);
                CurrentPosition = movePoint.position;
            }
            animator.SetFloat(HorizontalAxisName, horizontalAxisInput);
            animator.SetFloat(VerticalAxisName, verticalAxisInput);
        }
    }

    private bool isCurrentlyMoving(float horizontalAxis, float verticalAxis)
    {
        return Mathf.Abs(horizontalAxis) != 0f &&
                Mathf.Abs(verticalAxis) != 0f;
    }
}
