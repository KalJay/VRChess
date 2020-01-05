using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PieceType { Pawn, King, Queen, Bishop, Knight, Rook };
    public enum PieceColour { White, Black };

    public PieceType type;
    public PieceColour colour;
    public Vector2 BoardPosition;
    public BoardManager board;

    void Start()
    {
        switch(gameObject.name)
        {
            case ("pawn(Clone)"):
                type = PieceType.Pawn;
                colour = PieceColour.White;
                break;
            case ("pawn1(Clone)"):
                type = PieceType.Pawn;
                colour = PieceColour.Black;
                break;
            case ("king(Clone)"):
                type = PieceType.King;
                colour = PieceColour.White;
                break;
            case ("king1(Clone)"):
                type = PieceType.King;
                colour = PieceColour.Black;
                break;
            case ("queen(Clone)"):
                type = PieceType.Queen;
                colour = PieceColour.White;
                break;
            case ("queen1(Clone)"):
                type = PieceType.Queen;
                colour = PieceColour.Black;
                break;
            case ("elephant(Clone)"):
                type = PieceType.Bishop;
                colour = PieceColour.White;
                break;
            case ("elephant1(Clone)"):
                type = PieceType.Bishop;
                colour = PieceColour.Black;
                break;
            case ("knight(Clone)"):
                type = PieceType.Knight;
                colour = PieceColour.White;
                break;
            case ("knight1(Clone)"):
                type = PieceType.Knight;
                colour = PieceColour.Black;
                break;
            case ("rook(Clone)"):
                type = PieceType.Rook;
                colour = PieceColour.White;
                break;
            case ("rook1(Clone)"):
                type = PieceType.Rook;
                colour = PieceColour.Black;
                break;
        }
        UpdateCoordinates();
    }

    public void UpdateCoordinates()
    {
        Vector2 pos = board.GetPieceCoordinates(this);

        if (pos != new Vector2(-1, -1))
        {
            BoardPosition = pos;
        }
        UpdatePosition();
    }

    public void UpdateCoordinates(Vector2 pos)
    {
        BoardPosition = pos;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        transform.position = board.BoardPositions[board.ConvertCoordinatestoString(BoardPosition)];
        transform.rotation = new Quaternion();
    }

    public void PlacePiece()
    {
        Vector2 pos = board.GetPieceCoordinates(this);

        if (pos != new Vector2(-1, -1) && pos != BoardPosition)
        {
            board.match.Move(this, pos);
        } else
        {
            UpdatePosition();
        }
    }

    public void DestroyPiece()
    {
        Debug.Log("Destroying");
        Destroy(gameObject);
    }
}
