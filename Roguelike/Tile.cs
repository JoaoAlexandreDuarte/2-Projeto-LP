using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Tile : List<Object> {

        public bool IsVisible { get; set; }
        public bool IsExit { get; set; }
        private int TileSize { get; }

        public Tile(int tileSize) : base(new Object[tileSize]) {
            IsVisible = false;
            TileSize = tileSize;
        }

        public IEnumerable<Object> GetInfo() {

            foreach (Object obj in this) {
                yield return obj;
            }
        }

        public bool AddTo(Object obj) {
            bool returnValue = false;
            int spaceToAdd;

            if (IsExit) {
                spaceToAdd = 1;
            } else {
                spaceToAdd = LastSpaceFilled();
            }

            if (spaceToAdd >= 0) {
                if ((spaceToAdd == TileSize - 1) && !(obj is Player)) {

                } else {
                    this.Insert(spaceToAdd, obj);
                    this.RemoveAt(this.Count - 1);
                    returnValue = true;
                }
            }

            return returnValue;
        }

        public void FillEmpty() {
            if (this.Count < TileSize) {
                this.Add(null);
            }
        }

        private int LastSpaceFilled() {
            int space = -1;

            for (int i = 0; i < this.Count; i++) {
                if (this[i] == null) {
                    space = i;
                    break;
                }
            }

            return space;
        }
    }
}
