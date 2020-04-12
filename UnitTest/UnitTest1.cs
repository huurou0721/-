using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Othello.Domain.Model;
using Othello.Infrastructure;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InitialBoardPlacementTest()
        {
            var expectedPlaceMent = new Placement(
                new bool[8, 8] { { false, false, false, false, false, false, false, false },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, true, false, false, false, },
                                 { false, false, false, true, false, false, false, false, },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,}
                },
                new bool[8, 8] { { false, false, false, false, false, false, false, false },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, true, false, false, false, false, },
                                 { false, false, false, false, true, false, false, false, },
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,},
                                 { false, false, false, false, false, false, false, false,}
                });
            var board = new BitBoard(34628173824, 68853694464);
            CollectionAssert.AreEqual(expectedPlaceMent.BlackBoard, board.Placement.BlackBoard);
            CollectionAssert.AreEqual(expectedPlaceMent.WhiteBoard, board.Placement.WhiteBoard);
        }
    }
}
