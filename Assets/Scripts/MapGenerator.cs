using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private List<Chunk> _chunks;
    [SerializeField] private Chunk _fistChunk;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private int _chunksAmount = 6;
    [SerializeField] private Bridge _bridgePrefab;
    [SerializeField] private GameObject _portalPrefab;

    private Queue<EntryPoint> _queue = new();
    private LinkedList<Chunk> _placedChunks = new();
    private Chunk lastChunk;

    public void StartGenereting(Chunk fistChunk, List<Chunk> chunks, Transform startPoint, int chunksAmount, Bridge bridgePrefab, GameObject portalPrefab)
    {
        _fistChunk = fistChunk;
        _chunks = chunks;
        _startPoint = startPoint;
        _chunksAmount = chunksAmount;
        _bridgePrefab = bridgePrefab;
        _portalPrefab = portalPrefab;
        Chunk chunk = Instantiate(_fistChunk, _startPoint.position, Quaternion.identity);
        chunk.Init(0, 0);
        _placedChunks.AddLast(chunk);
        AddEntryPoints(chunk);
        GenerateChunks();
    }

    private void AddEntryPoints(Chunk chunk, EntryPoint ignore = null)
    {
        foreach (var point in chunk.EntryPoints)
        {
            if (ignore != null && ignore.Enter == point.Enter)
            {
                continue;
            }
            _queue.Enqueue(point);
        }
    }

    private void GenerateChunks()
    {
        while (_queue.Count > 0 && _chunksAmount > 0)
        {
            _chunksAmount--;
            EntryPoint point = _queue.Dequeue();

            int index = Random.Range(0, _chunks.Count);
            int direction = (Random.Range(0, 2) * 2) - 1;
            int count = 0;
            EntryPoint newPoint;
            while (!HasNeededEntryPoint(point, _chunks[index], out newPoint) && count <= _chunks.Count)
            {
                index = (int)Mathf.Repeat(index + direction, _chunks.Count);
                count++;
            }
            if (count >= _chunks.Count)
            {
                continue;
            }

            if (CanPlace(point.Chunk, point.Exit))
            {
                var newChunk = Instantiate(_chunks[index], Vector3.zero, Quaternion.identity);
                HasNeededEntryPoint(point, newChunk, out newPoint);
                int x, y;
                Queld(point.Exit, out x, out y);

                x = point.Chunk.X + x;
                y = point.Chunk.Y + y;
                newChunk.Init(x, y);
                _placedChunks.AddLast(newChunk);
                var bridge = Instantiate(_bridgePrefab);
                bridge.transform.rotation = Quaternion.Euler(0, 0, 90 * (int)point.Exit - 90);
                point.Fence.SetActive(false);
                newPoint.Fence.SetActive(false);
                Vector3 distance = point.transform.position - bridge.StartPoint.transform.position;
                bridge.transform.position = bridge.transform.position + distance;
                Vector3 distance_2 = bridge.EndPoint.position - newPoint.transform.position;
                newChunk.transform.position = newChunk.transform.position + distance_2;
                AddEntryPoints(newChunk, newPoint);
                // Создаем портал

                lastChunk = newChunk;
            }

        }
        if (lastChunk != null)
        {
            var portal = Instantiate(_portalPrefab, lastChunk.SpawnPoint.transform.position, Quaternion.identity);
            // Дополнительные действия с порталом, если необходимо
        }
    }

    private bool HasNeededEntryPoint(EntryPoint exit, Chunk entry, out EntryPoint newPoint)
    {
        foreach (var point in entry.EntryPoints)
        {
            if (exit.Enter == point.Exit)
            {
                newPoint = point;
                return true;
            }
        }

        newPoint = null;
        return false;
    }
    public bool CanPlace(Chunk chunk, Direction exit)
    {
        int x, y;
        Queld(exit, out x, out y);

        x = chunk.X + x;
        y = chunk.Y + y;

        foreach (var placedChunk in _placedChunks)
        {
            if (x == placedChunk.X && y == placedChunk.Y)
            {
                return false;
            }
        }

        return true;
    }

    private static void Queld(Direction exit, out int x, out int y)
    {
        x = 0;
        y = 0;
        switch (exit)
        {
            case Direction.Top:
                y++;
                break;
            case Direction.Bottom:
                y--;
                break;
            case Direction.Left:
                x--;
                break;
            case Direction.Right:
                x++;
                break;
        }
    }
}
