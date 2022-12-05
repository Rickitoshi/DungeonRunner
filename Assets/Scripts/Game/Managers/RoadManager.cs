using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private RoadPart[] roadPartsPool;
    [SerializeField] private int playParts = 2;
    [SerializeField] private float roadPartLenght = 37f;

    private Queue<RoadPart> _currentRoad;
    private List<RoadPart> _instantiatedRoadParts;
    private RoadPart _partObject;
    private Vector3 _partPosition;


    private void Awake()
    {
        _instantiatedRoadParts = new List<RoadPart>(roadPartsPool.Length);
        _currentRoad = new Queue<RoadPart>();
        SetDefaultPosition();
    }

    private void Start()
    {
        Initialize();
    }
    
    private void OnDestroy()
    {
       Unsubscribe();
    }

    private void Initialize()
    {
        InstantiateRoadPool();
        Subscribe();
        InitializeRoad();
    }

    private void InitializeRoad()
    {
        for (int i = 0; i < playParts; i++)
        {
            GetPart();
        }
    }

    private void SetDefaultPosition()
    {
        _partPosition = new Vector3(0, 0, roadPartLenght);
    }
    
    private void GetPart()
    {
        if (_instantiatedRoadParts.Count == 0) 
            return;
        
        int index = Random.Range(0, _instantiatedRoadParts.Count );
        _partObject = _instantiatedRoadParts[index];
        _instantiatedRoadParts.Remove(_partObject);
        _partObject.transform.localPosition = _partPosition;
        _partObject.gameObject.SetActive(true);
        _partObject.ResetItemsAndObstacles();
        _partPosition.z += roadPartLenght;
        _currentRoad.Enqueue(_partObject);
    }

    private void RemovePart()
    {
        if (_currentRoad.Count == 0)
            return;
        
        _partObject = _currentRoad.Dequeue();
        _partObject.gameObject.SetActive(false);
        _instantiatedRoadParts.Add(_partObject);
    }

    private void RebuildRoad()
    {
        RemovePart();
        GetPart();
    }
    
    private void InstantiateRoadPool()
    {
        foreach (var part in roadPartsPool)
        {
            _partObject = Instantiate(part, transform);
            _partObject.gameObject.SetActive(false);
            _instantiatedRoadParts.Add(_partObject);
        }
    }

    private void Subscribe()
    {
        foreach (var part in _instantiatedRoadParts)
        {
            part.OnPlayerExit += RebuildRoad;
        }
    }
    
    private void Unsubscribe()
    {
        foreach (var part in _instantiatedRoadParts)
        {
            part.OnPlayerExit -= RebuildRoad;
        }
    }

    public void Restart()
    {
        int size = _currentRoad.Count;
        for (int i = 0; i < size; i++)
        {
            RemovePart();
        }

        SetDefaultPosition();
        InitializeRoad();
    }
}
