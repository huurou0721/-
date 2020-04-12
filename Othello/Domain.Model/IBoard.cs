using System.Collections.Generic;

namespace Othello.Domain.Model
{
    public interface IBoard
    {
        /// <summary>
        /// 盤面描写用　黒石白石がそれぞれ盤面1マスごとに存在しているかどうかbool[X,Y]
        /// </summary>
        Placement Placement { get; }

        IBoard PutTurn(Teban teban, Position position);

        bool IsLegal(Teban teban, Position position);

        List<Position> GetLegalPosistionList(Teban teban);

        int CountBlackStone();

        int CountWhiteStone();

        int CountTotalStone();
    }
}