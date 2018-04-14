using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    public interface IBoard
    {
        IPiece Occupant(int position);
        IEnumerable<int> Pieces(Color color);
        void Move(IPiece piece, int destination);
        void RemoveCapturedPieces();
        void Promote(IPiece piece);
    }

    public enum Color { Black, White }
    public enum Status { Captured, Active }

    public interface IPiece
    {
        IEnumerable<int> NormalMoves(IBoard board);
        IEnumerable<int> CapturingMoves(IBoard board);
        Status Status { get; }
        Color Color { get; }
        int Position { get; }
        void Move(int destination);
    }
    public interface IPlayer
    {
        string Name { get; }
        (int, int) GetMove();
    }
    public interface IReferee
    {
        IPlayer Winner();
        bool IsDraw();
        void Play();
    }
    public class IllegalMoveException : ApplicationException { }

}
