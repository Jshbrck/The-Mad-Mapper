using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapGenerator.Util;

namespace MapGenerator.Entities
{
    class TileSelectionBox : ListBox
    {
        public TileSelectionBox()
        {
            GlobalVariables.LOADER.LoadEvent += new ContentLoader.LoadEventHandler(LoadTileNames);
        }
        public void LoadTileNames(Dictionary<String, Rectangle> Tiles)
        {
            BeginUpdate();
            foreach (KeyValuePair<String,Rectangle> Tile in Tiles)
            {
                if (!Tile.Key.ToString().Contains("NOTUSED")) { Items.Add(Tile.Key); }
            }
            EndUpdate();
        }
                
    }
}
