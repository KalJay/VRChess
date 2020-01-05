using System.Collections.Generic;
using UnityEngine;

//Responsible for spawning and moving pieces on the board when told to by other code. Does not care about chess rules. Acts as the interface between the Unity coordinate system and the chess coordinate system.

public class BoardManager : MonoBehaviour
{
    public GameObject A1, A8, H1, H8;

    public GameObject WhitePawn;
    public GameObject WhiteKing;
    public GameObject WhiteQueen;
    public GameObject WhiteBishop;
    public GameObject WhiteKnight;
    public GameObject WhiteRook;

    public GameObject BlackPawn;
    public GameObject BlackKing;
    public GameObject BlackQueen;
    public GameObject BlackBishop;
    public GameObject BlackKnight;
    public GameObject BlackRook;

    public Dictionary<string, Vector3> BoardPositions = new Dictionary<string, Vector3>();
    private Dictionary<Vector3, string> InverseBoardPositions = new Dictionary<Vector3, string>();
    public List<Piece> pieces;

    public MatchManager match;

    public List<GameObject> particlePrefabs;
    private List<GameObject> Indicators;
    public bool indicatorsEnabled = false;

    void Awake()
    {
        match = GetComponent<MatchManager>();
        Indicators = new List<GameObject>();
    }

    void Start()
    {
        GenerateBoardPositions();

        pieces = new List<Piece>();
        SpawnStartingBoard();
    }

    private void SpawnPiece(GameObject obj, Vector3 pos)
    {
        GameObject temp = Instantiate(obj, transform);
        temp.transform.position = pos;
        Piece piece = temp.AddComponent<Piece>();
        piece.board = this;
        pieces.Add(piece);
        temp.AddComponent<Valve.VR.InteractionSystem.Interactable>();
        PieceController pc = temp.AddComponent<PieceController>();
        pc.piece = piece;
        pc.board = this;
        MeshCollider col = temp.AddComponent<MeshCollider>();
        col.convex = true;
        col.isTrigger = true;
    }

    private void SpawnStartingBoard()
    {
        pieces.Clear();
        SpawnPiece(WhitePawn, BoardPositions["A2"]);
        SpawnPiece(WhitePawn, BoardPositions["B2"]);
        SpawnPiece(WhitePawn, BoardPositions["C2"]);
        SpawnPiece(WhitePawn, BoardPositions["D2"]);
        SpawnPiece(WhitePawn, BoardPositions["E2"]);
        SpawnPiece(WhitePawn, BoardPositions["F2"]);
        SpawnPiece(WhitePawn, BoardPositions["G2"]);
        SpawnPiece(WhitePawn, BoardPositions["H2"]);
        SpawnPiece(WhiteKing, BoardPositions["E1"]);
        SpawnPiece(WhiteQueen, BoardPositions["D1"]);
        SpawnPiece(WhiteBishop, BoardPositions["C1"]);
        SpawnPiece(WhiteBishop, BoardPositions["F1"]);
        SpawnPiece(WhiteKnight, BoardPositions["B1"]);
        SpawnPiece(WhiteKnight, BoardPositions["G1"]);
        SpawnPiece(WhiteRook, BoardPositions["A1"]);
        SpawnPiece(WhiteRook, BoardPositions["H1"]);

        SpawnPiece(BlackPawn, BoardPositions["A7"]);
        SpawnPiece(BlackPawn, BoardPositions["B7"]);
        SpawnPiece(BlackPawn, BoardPositions["C7"]);
        SpawnPiece(BlackPawn, BoardPositions["D7"]);
        SpawnPiece(BlackPawn, BoardPositions["E7"]);
        SpawnPiece(BlackPawn, BoardPositions["F7"]);
        SpawnPiece(BlackPawn, BoardPositions["G7"]);
        SpawnPiece(BlackPawn, BoardPositions["H7"]);
        SpawnPiece(BlackKing, BoardPositions["E8"]);
        SpawnPiece(BlackQueen, BoardPositions["D8"]);
        SpawnPiece(BlackBishop, BoardPositions["C8"]);
        SpawnPiece(BlackBishop, BoardPositions["F8"]);
        SpawnPiece(BlackKnight, BoardPositions["B8"]);
        SpawnPiece(BlackKnight, BoardPositions["G8"]);
        SpawnPiece(BlackRook, BoardPositions["A8"]);
        SpawnPiece(BlackRook, BoardPositions["H8"]);
    }

    private void GenerateBoardPositions()
    {
        float unitWidth = (H1.transform.position.x - A1.transform.position.x) / 7.0f;
        float unitHeight = (A8.transform.position.z - A1.transform.position.z) / 7.0f;
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                //Debug.Log(ConvertNumericalCoordinate("" + (j + 1) + (i + 1)));
                BoardPositions.Add(ConvertNumericalCoordinate("" + (j+1) + (i+1)), new Vector3(A1.transform.position.x + unitWidth * j, A1.transform.position.y, A1.transform.position.z + unitHeight * i));
                InverseBoardPositions.Add(new Vector3(A1.transform.position.x + unitWidth * j, A1.transform.position.y, A1.transform.position.z + unitHeight * i), ConvertNumericalCoordinate("" + (j + 1) + (i + 1)));
            }
        }
    }

    public string ConvertNumericalCoordinate(string ori)
    {
        char letter;
        switch(ori[0])
        {
            case '1':
                letter = 'A';
                break;
            case '2':
                letter = 'B';
                break;
            case '3':
                letter = 'C';
                break;
            case '4':
                letter = 'D';
                break;
            case '5':
                letter = 'E';
                break;
            case '6':
                letter = 'F';
                break;
            case '7':
                letter = 'G';
                break;
            case '8':
                letter = 'H';
                break;
            default:
                letter = 'Z';
                break;
        }
        return "" + letter + ori[1];
    }

    public string ConvertCoordinatestoString(Vector2 pos)
    {
        char letter;
        switch(pos.x)
        {
            case 1:
                letter = 'A';
                break;
            case 2:
                letter = 'B';
                break;
            case 3:
                letter = 'C';
                break;
            case 4:
                letter = 'D';
                break;
            case 5:
                letter = 'E';
                break;
            case 6:
                letter = 'F';
                break;
            case 7:
                letter = 'G';
                break;
            case 8:
                letter = 'H';
                break;
            default:
                letter = 'Z';
                break;
        }
        return "" + letter + pos.y;
    }

    public Vector2 ConvertStringtoCoordinates(string input)
    {
        Vector2 result = new Vector2();
        switch (input[0])
        {
            case 'A':
                result.x = 1;
                break;
            case 'B':
                result.x = 2;
                break;
            case 'C':
                result.x = 3;
                break;
            case 'D':
                result.x = 4;
                break;
            case 'E':
                result.x = 5;
                break;
            case 'F':
                result.x = 6;
                break;
            case 'G':
                result.x = 7;
                break;
            case 'H':
                result.x = 8;
                break;
            default:
                result.x = -1;
                break;
        }
        if(result.x == -1)
        {
            result.y = -1;
        } else
        {
            result.y = int.Parse(input[1].ToString());
        }
        return result;
    }

    public Vector2 GetPieceCoordinates(Piece piece)
    {
        if(!pieces.Contains(piece))
        {
            return new Vector2(-1,-1);
        }
        Vector3 closest = new Vector3();
        foreach(Vector3 pos in BoardPositions.Values)
        {
            if(closest == new Vector3())
            {
                closest = pos;
            } else
            {
                if(Vector3.Distance(closest, piece.transform.position) > Vector3.Distance(pos, piece.transform.position))
                {
                    closest = pos;
                }
            }
        }

        if(Vector3.Distance(closest, piece.transform.position) > 0.1f)
        {
            return new Vector2(-1, -1);
        } else
        {
            return ConvertStringtoCoordinates(InverseBoardPositions[closest]);
        }
    }

    public bool isSquareOccupied(Vector2 pos)
    {
        foreach (Piece piece in pieces)
        {
            if(piece.BoardPosition == pos)
            {
                return true;
            }
        }
        return false;
    }
    
    public Piece GetPieceOnSquare(Vector2 pos)
    {
        foreach (Piece piece in pieces)
        {
            if (piece.BoardPosition == pos)
            {
                return piece;
            }
        }
        return null;
    }

    public List<Piece> GetPiecesOfColour(Piece.PieceColour colour)
    {
        List<Piece> pieces= new List<Piece>();
        foreach(Piece piece in this.pieces)
        {
            if(piece.colour == colour)
            {
                pieces.Add(piece);
            }
        }
        return pieces;
    }

    public void EnableIndicators(Piece piece)
    {
        GameObject temp = Instantiate(particlePrefabs[1], transform);
        temp.transform.position = BoardPositions[ConvertCoordinatestoString(piece.BoardPosition)];
        Indicators.Add(temp);

        if(!indicatorsEnabled)
        {
            indicatorsEnabled = true;
            foreach(Vector2 move in match.GetValidMoves(piece))
            {
                GameObject temp1;
                if (isSquareOccupied(move))
                {
                    if (GetPieceOnSquare(move).colour == piece.colour)
                    {
                        temp1 = Instantiate(particlePrefabs[0], transform);
                    }
                    else
                    {
                        temp1 = Instantiate(particlePrefabs[2], transform);
                    }
                    
                } else
                {
                    temp1 = Instantiate(particlePrefabs[0], transform);
                }
                temp1.transform.position = BoardPositions[ConvertCoordinatestoString(move)];
                Indicators.Add(temp1);
            }
        }
    }

    public void DisableIndicators()
    {
        if(indicatorsEnabled)
        {
            indicatorsEnabled = false;
            foreach(GameObject obj in Indicators)
            {
                Destroy(obj);
            }
        }
    }
}
