using System;
using System.Collections.Generic;

namespace Patterns
{
    public class FlightTowerDemo
    {
        public static void MainDemo(string[] args)
        {
            var tower = new FlightTower();
            tower.list = new List<Component>() {
                new Plane(tower){ Number = 23},
                new Plane(tower){ Number = 45, NotifeMe = false},
                new AirCraft(tower){ Name = "Wow", NotifeMe = true},
                new AirCraft(tower){ Name = "Wow2"}
            };
            tower.list[0].SendMessage(String.Format("Hi, guys, I am plane {0}", ((Plane)tower.list[0]).Number));
            
        }
    }
}