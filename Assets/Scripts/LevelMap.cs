using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunks;
    [SerializeField] private Chunk _fistChunk;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private int _chunksAmount = 6;
    [SerializeField] private Bridge _bridgePrefab;
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private GameObject _portalPrefab;

    public void Start()
    {
        _mapGenerator.StartGenereting(_fistChunk, _chunks, _startPoint, _chunksAmount = 6, _bridgePrefab, _portalPrefab);
    }
}
