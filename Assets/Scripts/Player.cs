using UnityEngine;

public class Player : MonoBehaviour, IUnit
{
    private const float Tolerance = 0.1f;
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    
    public Vector3 CurrentPosition { get; private set; }

    private void Start()
    {
        movePoint.parent = null;
        CurrentPosition = movePoint.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);


        if (!isCurrentlyMoving(Input.GetAxisRaw(HorizontalAxisName), Input.GetAxisRaw(VerticalAxisName)) &&
            Vector3.Distance(transform.position, movePoint.position) <= Tolerance)
        {
            if (Mathf.Abs(Input.GetAxisRaw(HorizontalAxisName)) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0f, 0f);
                CurrentPosition = movePoint.position;
            }
            if (Mathf.Abs(Input.GetAxisRaw(VerticalAxisName)) == 1f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw(VerticalAxisName), 0f);
                CurrentPosition = movePoint.position;
            }
        }
    }

    private bool isCurrentlyMoving(float horizontalAxis, float verticalAxis)
    {
        return Mathf.Abs(horizontalAxis) != 0f &&
                Mathf.Abs(verticalAxis) != 0f;
    }
}
