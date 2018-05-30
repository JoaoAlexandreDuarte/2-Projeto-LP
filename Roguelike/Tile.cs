using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class Tile : List<Object>  {

        public Tile(int tileSize) : base(tileSize) {

        }

        public IEnumerable<T> GetInfo<T>() {
            foreach (T obj in this) {
                yield return obj;
            }
        }
    }
}
