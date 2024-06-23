using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public void moveRight() => transform.Translate(Vector3.right);

    public void moveLeft() => transform.Translate(Vector3.left);

    public void moveUp() => transform.Translate(Vector3.up);

    public void moveDown() => transform.Translate(Vector3.down);
}
