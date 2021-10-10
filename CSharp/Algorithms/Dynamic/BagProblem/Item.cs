using System;

namespace BagProblem
{
    public class Item
    {
        public string ID {get; set;}
        public int Price {get; set;}
        public int Weight {get; set;}
        public override string ToString(){
            return String.Format("ID: {0}; Price: {1}; Weight: {2}", ID, Price, Weight);
        }  
    }
}