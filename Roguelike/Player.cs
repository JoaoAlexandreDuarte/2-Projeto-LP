using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Player : IHasHP {
        private readonly float maxHP = 100;
        public float MaxWeight { get; } = 100;
        public double HP { get; set; }
        public Object SelectedWeapon { get; set; }
        public Object Inventory { get; set; }
        public float Weight { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Player() {
            HP = maxHP;
        }

        public double LoseHP(double damage) {
            HP -= damage;

            return HP;
        }

        public bool MoveNorth() {
            bool canMove = false;

            if ((X - 1) >= 0 ) {
                X -= 1;
                canMove = true;
            }

            return canMove;
        }

        public bool MoveSouth(int worldX) {
            bool canMove = false;

            if ((X + 1) < worldX ) {
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

        public override string ToString() {
            return "Player";
        }
    }
}
