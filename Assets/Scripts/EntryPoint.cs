using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum Direction
    {
        None,
        Right,
        Top,
        Left,
        Bottom
    }
public class EntryPoint : MonoBehaviour
{
    [field: SerializeField] public Direction Enter {  get; private set; }
    [field: SerializeField] public Direction Exit {  get; private set; }
    [field: SerializeField] public Chunk Chunk { get; private set; }
    [field: SerializeField] public GameObject Fence { get; private set; }

    private void Awake()
    {
        Fence.SetActive(true);
    }
}
