using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemSetup : MonoBehaviour
{
    public TotemPiece headTop;
    public TotemPiece headMiddle;
    public TotemPiece headBottom;

    void Start()
    {
        headTop.pieceBelow = headMiddle;

        headMiddle.pieceAbove = headTop;
        headMiddle.pieceBelow = headBottom;

        headBottom.pieceAbove = headMiddle;
    }
}
