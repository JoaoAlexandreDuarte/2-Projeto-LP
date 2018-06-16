using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Player : IHasHP, IHasWeight {
        public readonly double maxHP = 100;
        public readonly double maxWeight = 100;
        public double HP { get; set; }
        public Weapon SelectedWeapon { get; set; }
        public Inventory Inventory { get; }
        public double Weight {
            get {
                double weapongWeight = (SelectedWeapon == null) ? 0 :
                    SelectedWeapon.Weight;

                return Inventory.Weight + weapongWeight;
            }
        }
        public int X { get; set; }
        public int Y { get; set; }

        public Player() {
            HP = maxHP;
            Inventory = new Inventory();
        }

        public double LoseHP(double damage) {
            HP -= damage;

            return HP;
        }

        public bool MoveNorth() {
            bool canMove = false;

            if ((X - 1) >= 0) {
                X -= 1;
                canMove = true;
            }

            return canMove;
        }

        public bool MoveSouth(int worldX) {
            bool canMove = false;

            if ((X + 1) < worldX) {
                X += 1;
                canMove = true;
            }

            return canMove;
        }

        public bool MoveWest() {
            bool canMove = false;

            if ((Y - 1) >= 0) {
                Y -= 1;
                canMove = true;
            }

            return canMove;
        }

        public bool MoveEast(int worldY) {
            bool canMove = false;

            if ((Y + 1) < worldY) {
                Y += 1;
                canMove = true;
            }

            return canMove;
        }
    }
}
