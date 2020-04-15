using Othello.Domain.Model;
using Othello.Infrastructure;
using Othello.Infrastructure.AI;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace Othello.Application
{
    public class OthelloAppService
    {
        public static event EventHandler<BoardDrawEventArgs> BoardDrawEvent;

        private readonly IEventAggregator ea_;
        private IBoard board_;
        private readonly IAI blackAI_;
        private readonly IAI whiteAI_;

        private Teban teban_;

        public OthelloAppService(IEventAggregator ea, string blackAI, string whiteAI)
        {
            ea_ = ea;
            board_ = new BitBoard(34628173824, 68853694464);
            teban_ = Teban.Black;
            switch (blackAI)
            {
                case "RandomMoveAI":
                    blackAI_ = new RandomMoveAI();
                    break;

                default:
                    break;
            }
            switch (whiteAI)
            {
                case "RandomMoveAI":
                    whiteAI_ = new RandomMoveAI();
                    break;

                default:
                    break;
            }
        }

        public async Task Run()
        {
            while (true)
            {
                DrawBoard();
                if (board_.GetLegalPosistionList(teban_).Count == 0)
                {
                    SwitchTeban();
                    if (board_.GetLegalPosistionList(teban_).Count == 0)
                        break;
                }
                IAI ai;
                ai = teban_ == Teban.Black
                    ? blackAI_
                    : whiteAI_;
                board_ = board_.PutTurn(teban_, ai.DecideMove(teban_, board_));
                SwitchTeban();
                await Task.Delay(500);
            }
        }

        public string End()
        {
            var bs = board_.CountBlackStone();
            var ws = board_.CountWhiteStone();
            var resultText = $"黒: {bs}個, 白: {ws}個\n\n";
            return resultText +=
                bs > ws
                ? "黒の勝ちです。"
                : bs < ws
                    ? "白の勝ちです。"
                    : "引き分けです。";
        }

        private void SwitchTeban()
        {
            switch (teban_)
            {
                case Teban.Black:
                    teban_ = Teban.White;
                    break;

                case Teban.White:
                    teban_ = Teban.Black;
                    break;

                default:
                    break;
            }
        }

        private void DrawBoard()
        {
            BoardDrawEvent(this, new BoardDrawEventArgs(board_.Placement));
        }
    }
}