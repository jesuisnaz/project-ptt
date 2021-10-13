using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManager : MonoBehaviour
{
    [SerializeField] private GameObject _PumpkinPrefab;
    [SerializeField] private GameObject _Parent;
    [SerializeField] private int _MaxPumpkinCount = 5;
    private List<GameObject> _InstantiatedPumpkins;

    private void Awake()
    {
        _InstantiatedPumpkins = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (_InstantiatedPumpkins.Count < _MaxPumpkinCount) {
            InstantiatePumpkin();
        }
    }

    private void InstantiatePumpkin() {
        int x = (int)Random.Range(0, 31), y = (int)Random.Range(0, 17), z = 0;
        Vector3 position = new Vector3(x, y, z);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        GameObject newPumpkin = Instantiate(_PumpkinPrefab, position, rotation, _Parent.transform);
        _InstantiatedPumpkins.Add(newPumpkin);
    }
}
