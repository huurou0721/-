using Othello.Domain.Model;
using System;
using System.Collections.Generic;

namespace Othello.Infrastructure
{
    public class BitBoard : IBoard
    {
        public Placement Placement
        {
            get
            {
                var bb = new bool[8, 8];
                var wb = new bool[8, 8];
                ulong mask;
                for (var x = 0; x < 8; x++)
                {
                    for (var y = 0; y < 8; y++)
                    {
                        mask = (ulong)Math.Pow(2, 63 - (y * 8 + x));
                        bb[x, y] = (mask & blackBoard_) != 0;
                        wb[x, y] = (mask & whiteBoard_) != 0;
                    }
                }
                return new Placement(bb, wb);
            }
        }

        private readonly ulong blackBoard_;
        private readonly ulong whiteBoard_;

        public BitBoard(ulong blackBoard, ulong whiteBoard)
        {
            blackBoard_ = blackBoard;
            whiteBoard_ = whiteBoard;
        }

        public IBoard PutTurn(Teban teban, Position position)
        {
            ulong playerBoard;
            ulong opponentBoard;
            if (teban == Teban.Black)
            {
                playerBoard = blackBoard_;
                opponentBoard = whiteBoard_;
            }
            else
            {
                playerBoard = whiteBoard_;
                opponentBoard = blackBoard_;
            }

            ulong rev = 0;
            for (var dir = 0; dir < 8; dir++)
            {
                ulong rev_ = 0;
                var mask = Transfer(position, dir);
                while (mask != 0 && ((mask & opponentBoard) != 0))
                {
                    rev_ |= mask;
                    mask = Transfer(ToPosition(mask), dir);
                }
                if ((mask & playerBoard) != 0)
                {
                    rev |= rev_;
                }
            }
            playerBoard ^= ToBitPosition(position) | rev;
            opponentBoard ^= rev;
            return teban == Teban.Black ?
                new BitBoard(playerBoard, opponentBoard) :
                new BitBoard(opponentBoard, playerBoard);
        }

        public bool IsLegal(Teban teban, Position position)
        {
            var legalList = GetLegalPosistionList(teban);
            return legalList.Contains(position);
        }

        public List<Position> GetLegalPosistionList(Teban teban)
        {
            var legalBoard = LegalBoard(teban);
            var ll = new List<Position>();
            while (legalBoard != 0)
            {
                var bitPosition = legalBoard & (~legalBoard + 1);
                ll.Add(ToPosition(bitPosition));
                legalBoard &= ~bitPosition;
            }
            return ll;
        }

        public int CountBlackStone()
        {
            return BitCount(blackBoard_);
        }

        public int CountWhiteStone()
        {
            return BitCount(whiteBoard_);
        }

        public int CountTotalStone()
        {
            return CountBlackStone() + CountWhiteStone();
        }

        /// <summary>
        /// 合法手のbitのみが立ったビットボードを返す
        /// </summary>
        /// <param name="teban"></param>
        /// <returns></returns>
        private ulong LegalBoard(Teban teban)
        {
            ulong playerBoard;
            ulong opponentBoard;
            if (teban == Teban.Black)
            {
                playerBoard = blackBoard_;
                opponentBoard = whiteBoard_;
            }
            else
            {
                playerBoard = whiteBoard_;
                opponentBoard = blackBoard_;
            }
            //左右方向の番兵
            var hb = opponentBoard & 0x7e7e7e7e7e7e7e7e;
            //上下方向の番兵
            var vb = opponentBoard & 0x00ffffffffffff00;
            //全方向の番兵
            var ab = opponentBoard & 0x007e7e7e7e7e7e00;
            //石がない場所にbitを立てたビットボード
            var blank = ~(playerBoard | opponentBoard);

            //各8方向にそれぞれ6回シフトし隣に相手の石がある石についてbitを立てる

            //左方向
            var t = hb & (playerBoard << 1);
            for (var i = 0; i < 5; i++)
            {
                t |= hb & (t << 1);
            }
            var legalBoard = blank & (t << 1);
            //右方向
            t = hb & (playerBoard >> 1);
            for (var i = 0; i < 5; i++)
            {
                t |= hb & (t >> 1);
            }
            legalBoard |= blank & (t >> 1);
            //上方向
            t = vb & (playerBoard << 8);
            for (var i = 0; i < 5; i++)
            {
                t |= vb & (t << 8);
            }
            legalBoard |= blank & (t << 8);
            //下方向
            t = vb & (playerBoard >> 8);
            for (var i = 0; i < 5; i++)
            {
                t |= vb & (t >> 8);
            }
            legalBoard |= blank & (t >> 8);
            //左下方向
            t = ab & (playerBoard << 9);
            for (var i = 0; i < 5; i++)
            {
                t |= ab & (t << 9);
            }
            legalBoard |= blank & (t << 9);
            //右上方向
            t = ab & (playerBoard << 7);
            for (var i = 0; i < 5; i++)
            {
                t |= ab & (t << 7);
            }
            legalBoard |= blank & (t << 7);
            //左下方向
            t = ab & (playerBoard >> 7);
            for (var i = 0; i < 5; i++)
            {
                t |= ab & (t >> 7);
            }
            legalBoard |= blank & (t >> 7);
            //右下方向
            t = ab & (playerBoard >> 9);
            for (var i = 0; i < 5; i++)
            {
                t |= ab & (t >> 9);
            }
            legalBoard |= blank & (t >> 9);

            return legalBoard;
        }

        private Position ToPosition(ulong bitPosition)
        {
            var n = 63 - (int)Math.Log(bitPosition, 2);
            return new Position(n % 8, n / 8);
        }

        private ulong ToBitPosition(Position position)
        {
            var n = 63 - (position.Y * 8 + position.X);
            return (ulong)Math.Pow(2, n);
        }

        private ulong Transfer(Position position, int dir)
        {
            var bitPosition = ToBitPosition(position);
            var dd = new Dictionary<int, ulong>
            {
                {0, (bitPosition << 8) & 0xffffffffffffff00},
                {1, (bitPosition << 7) & 0x7f7f7f7f7f7f7f00},
                {2, (bitPosition >> 1) & 0x7f7f7f7f7f7f7f7f},
                {3, (bitPosition >> 9) & 0x007f7f7f7f7f7f7f},
                {4, (bitPosition >> 8) & 0x00ffffffffffffff},
                {5, (bitPosition >> 7) & 0x00fefefefefefefe},
                {6, (bitPosition << 1) & 0xfefefefefefefefe},
                {7, (bitPosition << 9) & 0xfefefefefefefe00}
            };
            return dd[dir];
        }

        private int BitCount(ulong x)
        {
            x -= (x >> 1) & 0x5555555555555555;
            x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
            x = (x + (x >> 4)) & 0x0f0f0f0f0f0f0f0f;
            x += x >> 8;
            x += x >> 16;
            x += x >> 32;
            return (int)x & 0x0000007f;
        }
    }
}