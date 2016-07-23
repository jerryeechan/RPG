using UnityEngine;
using System.Collections;

public class PieceGenerator : MonoBehaviour {

	Piece[] PieceGenre;
	Piece GetPiece()
	{
		return PieceGenre[0];
	}
}
