using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject lobby;
    [SerializeField] private RoadPart firstPart;
    [SerializeField] private RoadPart[] roadParts;
    [SerializeField] private int numberOfActiveRoad = 4;
    [SerializeField] private float roadPartLenght;

    private Queue<RoadPart> _currentRoad;
    private List<RoadPart> _instantiatedRoadParts;
    private RoadPart _partObject;
    private Vector3 _partPosition;


    private void Awake()
    {
        _instantiatedRoadParts = new List<RoadPart>(roadParts.Length);
        _currentRoad = new Queue<RoadPart>(numberOfActiveRoad);
        _partPosition = new Vector3(0, 0, roadPartLenght);
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
        GetPart();
        GetPart();
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
        _partObject.ResetItems();
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
        foreach (var part in roadParts)
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
    
    public void RemoveLobby()
    {
        lobby.SetActive(false);
    }
    
    public void SetLobby()
    {
        for (int i = 0; i < _currentRoad.Count; i++)
        {
            RemovePart();
        }

        lobby.SetActive(true);
    }
}
