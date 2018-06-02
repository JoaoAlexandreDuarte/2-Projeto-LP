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

        public bool Add() {

        }
    }
}
