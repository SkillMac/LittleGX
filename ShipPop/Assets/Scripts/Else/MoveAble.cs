using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAble : MonoBehaviour {

    private GamePiece piece;

    void Awake()
    {
        piece = GetComponent<GamePiece>();
    }

    public void Move(int newX, int newY)
    {
        piece.X = newX;
        piece.Y = newY;

        piece.transform.localPosition = piece.GridRef.GetWorldPos(newX, newY);
    }
}
