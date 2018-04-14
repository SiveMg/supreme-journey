using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;
using Checkers;
 


namespace Checkers.Test
{
    [TestFixture]
    public class Tests
    {
       
        [Test]
        public void ABoardHas12BlackAnd12WhitePieces()
        {
            Checkers.Board b = new Board();
            int blackCount = b.Pieces(Color.Black).Count();
            int whiteCount = b.Pieces(Color.White).Count();
            Assert.That(blackCount == 12);
            Assert.That(whiteCount == 14);

        }
        
    }
}
