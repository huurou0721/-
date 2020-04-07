using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        List<Position> LegalList(Teban teban, Position position);
        (int black, int white, int total) CountStone();

    }
}
