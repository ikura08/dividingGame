using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public interface IMovable
{
    IEnumerator Move(Vector2 direction);
}