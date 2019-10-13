using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using MapGenerator.Util;
using MapGenerator.Input;

namespace MapGenerator.Entities
{
     
    class SidePanel : DrawWindow
    {
        Texture2D DrawFont;
        private Map Grid;
        private Drawer drawer;

        public bool GetAutoInvalidation
        {
            get { return AutomaticInvalidation; }
            set { AutomaticInvalidation = value; }
        }

        public void ChangeDisplayedTile(String NewTile)
        {
            Grid.Layers[0].ChangeTile(NewTile,1,1);
            Invalidate();
        }
        protected override void Initialize()
        {
            GetAutoInvalidation = false;
            base.Initialize();
            Editor.Content.RootDirectory = "Content";
            Editor.BackgroundColor = Color.Black;
            DrawFont = Editor.Content.Load<Texture2D>("terrain");
            GlobalVariables.LOADER.LoadTileSheet(null, Editor);
            System.Diagnostics.Debug.WriteLine("Inside SidePanel");
            drawer = new Drawer(Editor, GlobalVariables.LOADER.Tiles, GlobalVariables.LOADER.TileSheet);
            Grid = new Map(2,2,1);
            Grid.Layers[0].ChangeTile(GlobalConstants.DEFAULT_STONE,1,1);
        }

        protected override void Draw()
        {
            base.Draw();
            drawer.DrawMap(Grid,4);
        }
    }
}
