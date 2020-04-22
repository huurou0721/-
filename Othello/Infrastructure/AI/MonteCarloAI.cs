using Othello.Domain.Model;
using System;
using System.Linq;

namespace Othello.Infrastructure.AI
{
    internal class MonteCarloAI : IAI
    {
        private readonly int repeatCount_;

        public MonteCarloAI(int repeatCount)
        {
            repeatCount_ = repeatCount;
        }

        public Position DecideMove(Teban teban, IBoard board)
        {
            var tebanTemp = teban;

            var legalList = board.GetLegalPosistionList(teban);
            var countArray = new int[legalList.Count];
            for (var i = 0; i < repeatCount_; i++)
            {
                teban = tebanTemp;
                var rem = i % legalList.Count;
                board = board.PutTurn(teban, legalList[rem]);
                teban = SwitchTeban(teban);
                (var bs, var ws) = Play(teban, board);
                countArray[rem] += bs >= ws ? 1 : 0;
            }
            return tebanTemp == Teban.Black
                ? legalList[Array.IndexOf(countArray, countArray.Max())]
                : legalList[Array.IndexOf(countArray, countArray.Min())];
        }

        private (int blackStone, int whiteStone) Play(Teban teban, IBoard board)
        {
            while (true)
            {
                if (board.GetLegalPosistionList(teban).Count == 0)
                {
                    teban = SwitchTeban(teban);
                    if (board.GetLegalPosistionList(teban).Count == 0)
                        break;
                }
                board = board.PutTurn(teban, AIUtility.DecidePositionRandom(teban, board));
                teban = SwitchTeban(teban);
            }
            return (board.CountBlackStone(), board.CountWhiteStone());
        }

        private Teban SwitchTeban(Teban teban)
        {
            return teban == Teban.Black
                ? Teban.White : Teban.Black;
        }
    }
}