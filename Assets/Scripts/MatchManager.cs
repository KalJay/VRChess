using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public BoardManager board;
    public bool canWCastleK = true;
    public bool canWCastleQ = true;
    public bool canBCastleK = true;
    public bool canBCastleQ = true;

    private List<Piece> enpassantPawns;


    void Awake()
    {
        board = GetComponent<BoardManager>();
        enpassantPawns = new List<Piece>();
    }

    public List<Vector2> GetValidMoves(Piece piece)
    {
        List<Vector2> validMoves = new List<Vector2>();

        switch(piece.type)
        {
            case Piece.PieceType.Pawn:
                if(piece.colour == Piece.PieceColour.White)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + 1);

                    if (!board.isSquareOccupied(checkpos) && isPosInBounds(checkpos))
                    {
                        validMoves.Add(checkpos);
                        if(!board.isSquareOccupied(new Vector2(checkpos.x, checkpos.y + 1)) && piece.BoardPosition.y == 2)
                        {
                            validMoves.Add(new Vector2(checkpos.x, checkpos.y + 1));
                        }
                    }

                    checkpos = new Vector2(piece.BoardPosition.x + 1, piece.BoardPosition.y + 1);

                    if (board.isSquareOccupied(checkpos)) {
                        if(board.GetPieceOnSquare(checkpos).colour == Piece.PieceColour.Black && board.GetPieceOnSquare(checkpos).type != Piece.PieceType.King)
                        {
                            validMoves.Add(checkpos);
                        }
                    }

                    checkpos = new Vector2(piece.BoardPosition.x - 1, piece.BoardPosition.y + 1);

                    if (board.isSquareOccupied(checkpos))
                    {
                        if (board.GetPieceOnSquare(checkpos).colour == Piece.PieceColour.Black && board.GetPieceOnSquare(checkpos).type != Piece.PieceType.King)
                        {
                            validMoves.Add(checkpos);
                        }
                    }

                    for(int i = -1; i < 2; i+=2)
                    {
                        checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                        foreach(Piece pawn in enpassantPawns)
                        {
                            if(pawn.BoardPosition == checkpos)
                            {
                                validMoves.Add(new Vector2(checkpos.x, checkpos.y + 1));
                            }
                        }
                    }
                }
                if(piece.colour == Piece.PieceColour.Black)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y - 1);

                    if (!board.isSquareOccupied(checkpos))
                    {
                        validMoves.Add(checkpos);
                        if (!board.isSquareOccupied(new Vector2(checkpos.x, checkpos.y - 1)) && piece.BoardPosition.y == 7)
                        {
                            validMoves.Add(new Vector2(piece.BoardPosition.x, piece.BoardPosition.y - 2));
                        }
                    }

                    checkpos = new Vector2(piece.BoardPosition.x + 1, piece.BoardPosition.y - 1);

                    if (board.isSquareOccupied(checkpos))
                    {
                        if (board.GetPieceOnSquare(checkpos).colour == Piece.PieceColour.White && board.GetPieceOnSquare(checkpos).type != Piece.PieceType.King)
                        {
                            validMoves.Add(checkpos);
                        }
                    }

                    checkpos = new Vector2(piece.BoardPosition.x - 1, piece.BoardPosition.y - 1);

                    if (board.isSquareOccupied(checkpos))
                    {
                        if (board.GetPieceOnSquare(checkpos).colour == Piece.PieceColour.White && board.GetPieceOnSquare(checkpos).type != Piece.PieceType.King)
                        {
                            validMoves.Add(new Vector2(piece.BoardPosition.x - 1, piece.BoardPosition.y - 1));
                        }
                    }

                    for (int i = -1; i < 2; i += 2)
                    {
                        checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                        foreach (Piece pawn in enpassantPawns)
                        {
                            if (pawn.BoardPosition == checkpos)
                            {
                                validMoves.Add(new Vector2(checkpos.x, checkpos.y - 1));
                            }
                        }
                    }
                }
                break;
            case Piece.PieceType.Knight:
                for(int i = -1; i < 2; i+=2)
                {
                    for(int j = -1; j < 2; j+=2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + 2 * i, piece.BoardPosition.y + 1 * j);
                        if(!board.isSquareOccupied(checkpos))
                        {
                            if (isPosInBounds(checkpos))
                            {
                                validMoves.Add(checkpos);
                            }
                        } else
                        {
                            if(board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                    }
                }
                for (int i = -1; i < 2; i += 2)
                {
                    for (int j = -1; j < 2; j += 2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + 1 * i, piece.BoardPosition.y + 2 * j);
                        if (!board.isSquareOccupied(checkpos))
                        {
                            if (isPosInBounds(checkpos))
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                        else
                        {
                            if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                    }
                }
                break;
            case Piece.PieceType.Bishop:
                for (int i = -1; i < 2; i += 2)
                {
                    for (int j = -1; j < 2; j += 2)
                    {
                        bool inbounds = true;
                        bool foundPiece = false;
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y + j);
                        inbounds = isPosInBounds(checkpos);
                        while (inbounds && !foundPiece)
                        {
                            
                            if(board.isSquareOccupied(checkpos))
                            {
                                foundPiece = true;
                                if(board.GetPieceOnSquare(checkpos).colour != piece.colour)
                                {
                                    validMoves.Add(checkpos);
                                }
                            } else
                            {
                                validMoves.Add(checkpos);
                            }

                            checkpos = new Vector2(checkpos.x + i, checkpos.y + j);
                            inbounds = isPosInBounds(checkpos);
                        }
                    }
                }
                break;
            case Piece.PieceType.Rook:
                for (int i = -1; i < 2; i += 2)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                    bool inbounds = true;
                    bool foundPiece = false;
                    inbounds = isPosInBounds(checkpos);
                    while (inbounds && !foundPiece)
                    {
                        if (board.isSquareOccupied(checkpos))
                        {
                            foundPiece = true;
                            if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                        else
                        {
                            validMoves.Add(checkpos);
                        }

                        checkpos = new Vector2(checkpos.x + i, checkpos.y);
                        inbounds = isPosInBounds(checkpos);
                    }
                }
                for (int i = -1; i < 2; i += 2)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + i);
                    bool inbounds = true;
                    bool foundPiece = false;
                    inbounds = isPosInBounds(checkpos);
                    while (inbounds && !foundPiece)
                    {
                        if (board.isSquareOccupied(checkpos))
                        {
                            foundPiece = true;
                            if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                        else
                        {
                            validMoves.Add(checkpos);
                        }

                        checkpos = new Vector2(checkpos.x, checkpos.y + i);
                        inbounds = isPosInBounds(checkpos);
                    }
                }
                break;
            case Piece.PieceType.Queen:
                for (int i = -1; i < 2; i += 2)
                {
                    for (int j = -1; j < 2; j += 2)
                    {
                        bool inbounds = true;
                        bool foundPiece = false;
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y + j);
                        inbounds = isPosInBounds(checkpos);
                        while (inbounds && !foundPiece)
                        {

                            if (board.isSquareOccupied(checkpos))
                            {
                                foundPiece = true;
                                if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                                {
                                    validMoves.Add(checkpos);
                                }
                            }
                            else
                            {
                                validMoves.Add(checkpos);
                            }

                            checkpos = new Vector2(checkpos.x + i, checkpos.y + j);
                            inbounds = isPosInBounds(checkpos);
                        }
                    }
                }
                for (int i = -1; i < 2; i += 2)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                    bool inbounds = true;
                    bool foundPiece = false;
                    inbounds = isPosInBounds(checkpos);
                    while (inbounds && !foundPiece)
                    {
                        if (board.isSquareOccupied(checkpos))
                        {
                            foundPiece = true;
                            if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                        else
                        {
                            validMoves.Add(checkpos);
                        }

                        checkpos = new Vector2(checkpos.x + i, checkpos.y);
                        inbounds = isPosInBounds(checkpos);
                    }
                }
                for (int i = -1; i < 2; i += 2)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + i);
                    bool inbounds = true;
                    bool foundPiece = false;
                    inbounds = isPosInBounds(checkpos);
                    while (inbounds && !foundPiece)
                    {
                        if (board.isSquareOccupied(checkpos))
                        {
                            foundPiece = true;
                            if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                        else
                        {
                            validMoves.Add(checkpos);
                        }

                        checkpos = new Vector2(checkpos.x, checkpos.y + i);
                        inbounds = isPosInBounds(checkpos);
                    }
                }
                break;
            case Piece.PieceType.King:
                for (int i = -1; i < 2; i += 2)
                {
                    for (int j = -1; j < 2; j += 2)
                    {
                        bool inbounds = true;
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y + j);
                        inbounds = isPosInBounds(checkpos);
                        if (inbounds && !isSquareAttackedByOtherSide(checkpos, piece.colour))
                        {
                            if (board.isSquareOccupied(checkpos))
                            {
                                if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                                {
                                    validMoves.Add(checkpos);
                                }
                            }
                            else
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                    }
                }
                for (int i = -1; i < 2; i += 2)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                    bool inbounds = true;
                    inbounds = isPosInBounds(checkpos);
                    if (inbounds && !isSquareAttackedByOtherSide(checkpos, piece.colour))
                    {
                        if (board.isSquareOccupied(checkpos))
                        {
                            if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                        else
                        {
                            validMoves.Add(checkpos);
                        }
                    }
                }
                for (int i = -1; i < 2; i += 2)
                {
                    Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + i);
                    bool inbounds = true;
                    inbounds = isPosInBounds(checkpos);
                    if (inbounds && !isSquareAttackedByOtherSide(checkpos, piece.colour))
                    {
                        if (board.isSquareOccupied(checkpos))
                        {
                            if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                            {
                                validMoves.Add(checkpos);
                            }
                        }
                        else
                        {
                            validMoves.Add(checkpos);
                        }
                    }
                }

                if(piece.colour == Piece.PieceColour.White && canWCastleK)
                {
                    bool good = true;
                    for (int i = 6; i < 8; i++)
                    {
                        Vector2 checkpos = new Vector2(i, 1);
                        if(board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour)) {
                            good = false;
                        }
                    }

                    if(good)
                    {
                        validMoves.Add(new Vector2(7, 1));
                        validMoves.Add(new Vector2(8, 1));
                    }
                }
                if (piece.colour == Piece.PieceColour.White && canWCastleQ)
                {
                    bool good = true;
                    for (int i = 2; i < 5; i++)
                    {
                        Vector2 checkpos = new Vector2(i, 1);
                        if (board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour))
                        {
                            good = false;
                        }
                    }

                    if (good)
                    {
                        validMoves.Add(new Vector2(1, 1));
                        validMoves.Add(new Vector2(3, 1));
                    }
                }
                if (piece.colour == Piece.PieceColour.Black && canBCastleK)
                {
                    bool good = true;
                    for (int i = 6; i < 8; i++)
                    {
                        Vector2 checkpos = new Vector2(i, 8);
                        if (board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour))
                        {
                            good = false;
                        }
                    }

                    if (good)
                    {
                        validMoves.Add(new Vector2(7, 8));
                        validMoves.Add(new Vector2(8, 8));
                    }
                }
                if (piece.colour == Piece.PieceColour.Black && canBCastleQ)
                {
                    bool good = true;
                    for (int i = 2; i < 5; i++)
                    {
                        Vector2 checkpos = new Vector2(i, 8);
                        if (board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour))
                        {
                            good = false;
                        }
                    }

                    if (good)
                    {
                        validMoves.Add(new Vector2(1, 8));
                        validMoves.Add(new Vector2(3, 8));
                    }
                }
                break;
        }

        return validMoves;
    }

    private bool isPosInBounds(Vector2 checkpos)
    {
        return (checkpos.x <= 8 && checkpos.x >= 1 && checkpos.y <= 8 && checkpos.y >= 1);
    }

    private bool isSquareAttackedByOtherSide(Vector2 pos, Piece.PieceColour colour)
    {
        if(colour == Piece.PieceColour.Black)
        {
            colour = Piece.PieceColour.White;
        } else
        {
            colour = Piece.PieceColour.Black;
        }

        List<Vector2> attacks = new List<Vector2>();
        foreach (Piece piece in board.GetPiecesOfColour(colour))
        {
            switch (piece.type)
            {
                case Piece.PieceType.Pawn:
                    if (piece.colour == Piece.PieceColour.White)
                    {
                        attacks.Add(new Vector2(piece.BoardPosition.x + 1, piece.BoardPosition.y + 1));
                        attacks.Add(new Vector2(piece.BoardPosition.x - 1, piece.BoardPosition.y + 1));
                    }
                    if (piece.colour == Piece.PieceColour.Black)
                    {
                        attacks.Add(new Vector2(piece.BoardPosition.x + 1, piece.BoardPosition.y - 1));
                        attacks.Add(new Vector2(piece.BoardPosition.x - 1, piece.BoardPosition.y - 1));
                    }
                    break;
                case Piece.PieceType.Knight:
                    for (int i = -1; i < 2; i += 2)
                    {
                        for (int j = -1; j < 2; j += 2)
                        {
                            Vector2 checkpos = new Vector2(piece.BoardPosition.x + 2 * i, piece.BoardPosition.y + 1 * j);
                            attacks.Add(checkpos);
                        }
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        for (int j = -1; j < 2; j += 2)
                        {
                            Vector2 checkpos = new Vector2(piece.BoardPosition.x + 1 * i, piece.BoardPosition.y + 2 * j);
                            if (!board.isSquareOccupied(checkpos))
                            {
                                attacks.Add(checkpos);
                            }
                            else
                            {
                                if (board.GetPieceOnSquare(checkpos).colour != piece.colour)
                                {
                                    attacks.Add(checkpos);
                                }
                            }
                        }
                    }
                    break;
                case Piece.PieceType.Bishop:
                    for (int i = -1; i < 2; i += 2)
                    {
                        for (int j = -1; j < 2; j += 2)
                        {
                            bool inbounds = true;
                            bool foundPiece = false;
                            Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y + j);
                            inbounds = isPosInBounds(checkpos);
                            while (inbounds && !foundPiece)
                            {

                                if (board.isSquareOccupied(checkpos))
                                {
                                    foundPiece = true;
                                }
                                attacks.Add(checkpos);

                                checkpos = new Vector2(checkpos.x + i, checkpos.y + j);
                                inbounds = isPosInBounds(checkpos);
                            }
                        }
                    }
                    break;
                case Piece.PieceType.Rook:
                    for (int i = -1; i < 2; i += 2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                        bool inbounds = true;
                        bool foundPiece = false;
                        inbounds = isPosInBounds(checkpos);
                        while (inbounds && !foundPiece)
                        {
                            if (board.isSquareOccupied(checkpos))
                            {
                                foundPiece = true;
                                
                            }
                            attacks.Add(checkpos);

                            checkpos = new Vector2(checkpos.x + i, checkpos.y);
                            inbounds = isPosInBounds(checkpos);
                        }
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + i);
                        bool inbounds = true;
                        bool foundPiece = false;
                        inbounds = isPosInBounds(checkpos);
                        while (inbounds && !foundPiece)
                        {
                            if (board.isSquareOccupied(checkpos))
                            {
                                foundPiece = true;
                            }
                            attacks.Add(checkpos);

                            checkpos = new Vector2(checkpos.x, checkpos.y + i);
                            inbounds = isPosInBounds(checkpos);
                        }
                    }
                    break;
                case Piece.PieceType.Queen:
                    for (int i = -1; i < 2; i += 2)
                    {
                        for (int j = -1; j < 2; j += 2)
                        {
                            bool inbounds = true;
                            bool foundPiece = false;
                            Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y + j);
                            inbounds = isPosInBounds(checkpos);
                            while (inbounds && !foundPiece)
                            {

                                if (board.isSquareOccupied(checkpos))
                                {
                                    foundPiece = true;
                                }
                                attacks.Add(checkpos);

                                checkpos = new Vector2(checkpos.x + i, checkpos.y + j);
                                inbounds = isPosInBounds(checkpos);
                            }
                        }
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                        bool inbounds = true;
                        bool foundPiece = false;
                        inbounds = isPosInBounds(checkpos);
                        while (inbounds && !foundPiece)
                        {
                            if (board.isSquareOccupied(checkpos))
                            {
                                foundPiece = true;
                            }
                            attacks.Add(checkpos);

                            checkpos = new Vector2(checkpos.x + i, checkpos.y);
                            inbounds = isPosInBounds(checkpos);
                        }
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + i);
                        bool inbounds = true;
                        bool foundPiece = false;
                        inbounds = isPosInBounds(checkpos);
                        while (inbounds && !foundPiece)
                        {
                            if (board.isSquareOccupied(checkpos))
                            {
                                foundPiece = true;
                            }
                                attacks.Add(checkpos);

                            checkpos = new Vector2(checkpos.x, checkpos.y + i);
                            inbounds = isPosInBounds(checkpos);
                        }
                    }
                    break;
                case Piece.PieceType.King:
                    for (int i = -1; i < 2; i += 2)
                    {
                        for (int j = -1; j < 2; j += 2)
                        {
                            bool inbounds = true;
                            Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y + j);
                            inbounds = isPosInBounds(checkpos);
                            if (inbounds)
                            {
                                attacks.Add(checkpos);
                            }
                        }
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                        bool inbounds = true;
                        inbounds = isPosInBounds(checkpos);
                        if (inbounds)
                        {
                            attacks.Add(checkpos);
                        }
                    }
                    for (int i = -1; i < 2; i += 2)
                    {
                        Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + i);
                        bool inbounds = true;
                        inbounds = isPosInBounds(checkpos);
                        if (inbounds)
                        {
                            attacks.Add(checkpos);
                        }
                    }
                    break;
            }
        }

        return attacks.Contains(pos);
    }

    public bool Move(Piece piece, Vector2 pos)
    {
        if(GetValidMoves(piece).Contains(pos))
        {
            
            if(piece.type == Piece.PieceType.King)
            {
                if(isKingMoveCastling(piece, pos))
                {
                    int kingY;
                    int rookCurrentY;
                    int rookNewY;
                    int x;

                    if (pos.x > 5)
                    {
                        kingY = 7;
                        rookCurrentY = 8;
                        rookNewY = 6;
                    } else
                    {
                        kingY = 3;
                        rookCurrentY = 1;
                        rookNewY = 4;
                    }

                    if (piece.colour == Piece.PieceColour.White)
                    {
                        x = 1;
                        canWCastleK = false;
                        canWCastleQ = false;
                    }
                    else
                    {
                        x = 8;
                        canBCastleK = false;
                        canBCastleQ = false;
                    }

                    piece.UpdateCoordinates(new Vector2(kingY, x));
                    Piece rook = board.GetPieceOnSquare(new Vector2(rookCurrentY, x));
                    rook.UpdateCoordinates(new Vector2(rookNewY, x));
                    return true;
                } else
                {
                    if(piece.colour == Piece.PieceColour.White)
                    {
                        canWCastleK = false;
                        canWCastleQ = false;
                    } else
                    {
                        canBCastleK = false;
                        canBCastleQ = false;
                    }
                }
            }
            if(piece.type == Piece.PieceType.Rook)
            {
                Debug.Log("Rook move!");
                if(piece.BoardPosition.x == 8)
                {
                    if(piece.colour == Piece.PieceColour.White)
                    {
                        canWCastleK = false;
                    } else
                    {
                        canBCastleK = false;
                    }
                } else
                {
                    if (piece.BoardPosition.x == 1)
                    {
                        if (piece.colour == Piece.PieceColour.White)
                        {
                            canWCastleQ = false;
                        }
                        else
                        {
                            canBCastleQ = false;
                        }
                    }
                }
            }
            if(piece.type == Piece.PieceType.Pawn)
            {
                if(Mathf.Abs(piece.BoardPosition.y - pos.y) == 2)
                {
                    enpassantPawns.Add(piece);
                } else
                {
                    if(enpassantPawns.Contains(piece))
                    {
                        enpassantPawns.Remove(piece);
                    }
                }
                if(isPawnEnPassant(piece, pos))
                {
                    if(piece.colour == Piece.PieceColour.White)
                    {
                        board.GetPieceOnSquare(new Vector2(pos.x, pos.y - 1)).DestroyPiece();
                    } else
                    {
                        board.GetPieceOnSquare(new Vector2(pos.x, pos.y + 1)).DestroyPiece();
                    }
                }
            }
            if (board.isSquareOccupied(pos))
            {
                board.GetPieceOnSquare(pos).DestroyPiece();
            }
            piece.UpdateCoordinates(pos);
            return true;
        }
        piece.UpdatePosition();
        return false;
    }

    private bool isKingMoveCastling(Piece piece, Vector2 move)
    {
        List<Vector2> validMoves = new List<Vector2>();

        if (piece.colour == Piece.PieceColour.White && canWCastleK)
        {
            bool good = true;
            for (int i = 6; i < 8; i++)
            {
                Vector2 checkpos = new Vector2(i, 1);
                if (board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour))
                {
                    good = false;
                }
            }

            if (good)
            {
                validMoves.Add(new Vector2(7, 1));
                validMoves.Add(new Vector2(8, 1));
            }
        }
        if (piece.colour == Piece.PieceColour.White && canWCastleQ)
        {
            bool good = true;
            for (int i = 2; i < 5; i++)
            {
                Vector2 checkpos = new Vector2(i, 1);
                if (board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour))
                {
                    good = false;
                }
            }

            if (good)
            {
                validMoves.Add(new Vector2(1, 1));
                validMoves.Add(new Vector2(3, 1));
            }
        }
        if (piece.colour == Piece.PieceColour.Black && canBCastleK)
        {
            bool good = true;
            for (int i = 6; i < 8; i++)
            {
                Vector2 checkpos = new Vector2(i, 8);
                if (board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour))
                {
                    good = false;
                }
            }

            if (good)
            {
                validMoves.Add(new Vector2(7, 8));
                validMoves.Add(new Vector2(8, 8));
            }
        }
        if (piece.colour == Piece.PieceColour.Black && canBCastleQ)
        {
            bool good = true;
            for (int i = 2; i < 5; i++)
            {
                Vector2 checkpos = new Vector2(i, 8);
                if (board.isSquareOccupied(checkpos) || isSquareAttackedByOtherSide(checkpos, piece.colour))
                {
                    good = false;
                }
            }

            if (good)
            {
                validMoves.Add(new Vector2(1, 8));
                validMoves.Add(new Vector2(3, 8));
            }
        }

        return validMoves.Contains(move);
    }

    private bool isPawnEnPassant(Piece piece, Vector2 move)
    {
        List<Vector2> validMoves = new List<Vector2>();

        if (piece.colour == Piece.PieceColour.White)
        {
            Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y + 1);

            for (int i = -1; i < 2; i += 2)
            {
                checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                foreach (Piece pawn in enpassantPawns)
                {
                    if (pawn.BoardPosition == checkpos)
                    {
                        validMoves.Add(new Vector2(checkpos.x, checkpos.y + 1));
                    }
                }
            }
        }
        if (piece.colour == Piece.PieceColour.Black)
        {
            Vector2 checkpos = new Vector2(piece.BoardPosition.x, piece.BoardPosition.y - 1);

            for (int i = -1; i < 2; i += 2)
            {
                checkpos = new Vector2(piece.BoardPosition.x + i, piece.BoardPosition.y);
                foreach (Piece pawn in enpassantPawns)
                {
                    if (pawn.BoardPosition == checkpos)
                    {
                        validMoves.Add(new Vector2(checkpos.x, checkpos.y - 1));
                    }
                }
            }
        }

        return validMoves.Contains(move);
    }
}
