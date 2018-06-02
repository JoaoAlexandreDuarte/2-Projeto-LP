using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Tile : List<Object> {

        public bool IsVisible { get; set; }
        private int TileSize { get; set; }

        public Tile(int tileSize) : base(new Object[tileSize]) {
            IsVisible = false;
            TileSize = tileSize;
        }

        public IEnumerable<Object> GetInfo() {

            foreach (Object obj in this) {
                yield return obj;
            }
        }

        public void SetVisible(bool isVisible) {
            IsVisible = isVisible;
        }

        //public bool Add(Object obj) {
        //    LastSpaceFilled();
        //}

        private int LastSpaceFilled() {
            int space = -1;

            for (int i = 0; i < this.Count; i++) {
                if (this[i] != null) {
                    space = i;
                    break;
                }
            }

            return space;
        }
    }
}
