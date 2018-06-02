using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public class Tile : List<Object> {

        public bool IsVisible { get; set; }

        public Tile(int tileSize) : base(new Object[tileSize]) {
            IsVisible = false;
        }

        public IEnumerable<Object> GetInfo() {
            foreach (Object obj in this) {
                //if (obj != null) {
                //    yield return obj;
                //} else {
                //    yield return 0;
                //}
                yield return obj;
            }
        }

        public void Add() {

        }
    }
}
