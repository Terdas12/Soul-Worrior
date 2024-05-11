using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [field:SerializeField] public List<EntryPoint> EntryPoints { get; private set; }
    [field: SerializeField] public Transform SpawnPoint { get; private set; }
    [field: SerializeField] public int X { get; private set; }
    [field: SerializeField] public int Y { get; private set; }
    public void Init(int x, int y)
    {
        X = x;
        Y = y;
    }
}