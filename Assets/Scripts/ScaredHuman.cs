
using UnityEngine;

public class ScaredHuman : MonoBehaviour,  IUnit
{
    public Vector3 CurrentPosition { get; private set; }

    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private ScaredHumanController _scaredHumanController;
    [SerializeField] private SoundManager _soundManager;
    private DirectionWrapper _currentDirection;


    private void Awake()
    {
        _scaredHumanController.OnMoveChange += Move;
        _scaredHumanController.OnAnimationChange += SetAnimator;
        _scaredHumanController.OnTeleport += Teleport;
    }

    private void OnDestroy()
    {
        _scaredHumanController.OnMoveChange -= Move;
        _scaredHumanController.OnAnimationChange -= SetAnimator;
        _scaredHumanController.OnTeleport -= Teleport;
    }

    private void Start()
    {
        _movePoint.parent = null;
        CurrentPosition = _movePoint.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Pumpkin")) {
        //     _pumpkinManager.PickUpPumpkin(other.gameObject);
        //     _soundManager.playSound("PumpkinPickedUp");      
        // }
        Debug.Log("collision with object: " + other.tag);
    }

    public void Move(DirectionWrapper directionWrapper)
    {
        _movePoint.position += directionWrapper.Vector3Value;
        CurrentPosition = _movePoint.position;
    }

    public void SetAnimator(DirectionWrapper horizontalDirectionWrapper, DirectionWrapper verticalDirectionWrapper)
    {
        _animator.SetFloat(horizontalDirectionWrapper.AxisName, horizontalDirectionWrapper.AxisValue);
        _animator.SetFloat(verticalDirectionWrapper.AxisName, verticalDirectionWrapper.AxisValue);
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
        CurrentPosition = position;
    }
}
