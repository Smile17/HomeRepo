using System;

namespace BagProblem
{
    public class Point
    {
        public int X {get; set;}
        public int Y {get; set;}
        public override string ToString(){
            return String.Format("X: {0}; Y: {1}", X, Y);
        }  
    }
}