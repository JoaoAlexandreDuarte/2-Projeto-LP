using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class Tile : List<Object> {

        public bool IsVisible { get; set; }

        public Tile(int tileSize) : base(tileSize) {
            IsVisible = false;
        }

        public IEnumerable<T> GetContent<T>() {
            foreach (T obj in this) {
                yield return obj;
            }
        }
    }
}
