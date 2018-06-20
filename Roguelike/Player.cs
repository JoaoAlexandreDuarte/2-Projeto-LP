using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Player : IHasHP, IHasWeight {
        private Random Rnd = new Random(Guid.NewGuid().GetHashCode());
        public readonly double maxHP = 100;
        public readonly double maxWeight = 100;
        public double HP { get; set; }
        public Weapon SelectedWeapon { get; set; }
        public Inventory Inventory { get; }
        public double Weight {
            get {
                double weaponWeight = (SelectedWeapon == null) ? 0 :
                    SelectedWeapon.Weight;

                return Inventory.Weight + weaponWeight;
            }
        }
        public int X { get; set; }
        public int Y { get; set; }

        public Player() {
            HP = maxHP;
            Inventory = new Inventory();
        }

        public void LoseHP(double damage) {
            HP -= damage;

            if (HP < 0) {
                HP = 0;
            }
        }

        public void GainHP(double life) {
            HP += life;

            if (HP > 100) {
                HP = 100;
            }
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

        public void AttackNPC(GameManager gm, NPC npc) {
            double dmg;

            dmg = Rnd.NextDouble() * SelectedWeapon.AttackPower;
            npc.TakeDamage(gm, dmg);

            if (Rnd.NextDouble() < 1 - SelectedWeapon.Durability) {
                SelectedWeapon = null;
                gm.messages.Add("The weapon you were using " +
                    SelectedWeapon.ToString() + " just broke");
            }
        }
    }
}
