using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    class Tile : List<Object> {

        public Tile(int tileSize) : base(tileSize) {

        }

        public IEnumerable<Object> GetInfo() {
            foreach (Object obj in this) {
                if (obj != null) {
                    yield return obj;
                } else {
                    yield return 0;
                }
                
            }
        }
    }
}
