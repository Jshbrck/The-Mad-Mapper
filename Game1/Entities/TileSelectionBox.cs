using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator.Entities
{
    class TileSelectionBox : ListBox
    {

        public void LoadTileNames(Dictionary<String, Rectangle> Tiles)
        {
            BeginUpdate();
            foreach (KeyValuePair<String,Rectangle> Tile in Tiles)
            {
                Items.Add(Tile.Key);
            }
            EndUpdate();
        }

        private void SelectedValueChange(object sender, EventArgs e)
        {
            if (SelectedIndex != -1)
            {
               
            }
        }
        
    }
}
