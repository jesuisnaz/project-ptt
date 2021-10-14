using UnityEngine;

public class Stone : MonoBehaviour, IUnit
{
    [SerializeField] private Vector3 _currentPosition;

    public Vector3 CurrentPosition => _currentPosition;

    private void Start()
    {
        transform.position = _currentPosition;
    }
}
