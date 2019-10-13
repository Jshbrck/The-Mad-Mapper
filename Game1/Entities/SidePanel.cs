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
        private ContentLoader loader;
        private Map Grid;
        private Drawer drawer;

        public bool GetAutoInvalidation
        {
            get { return AutomaticInvalidation; }
            set { AutomaticInvalidation = value; }
        }


        public void ChangeDisplayedTile(String NewTile)
        {
            Grid.Layers[0].FillLayer(NewTile);
            GetAutoInvalidation = true;
        }

        protected override void Initialize()
        {
            GetAutoInvalidation = false;
            base.Initialize();
            Editor.Content.RootDirectory = "Content";
            Editor.BackgroundColor = Color.Black;
            DrawFont = Editor.Content.Load<Texture2D>("terrain");
            System.Diagnostics.Debug.WriteLine("Inside SidePanel");
            loader = new ContentLoader(Editor);
            loader.LoadTileSet("Terrain.tsx");
            drawer = new Drawer(Editor, loader.Tiles, loader.TileSheet);
            Grid = new Map(7,6,1);
            Grid.Layers[0].FillLayer(GlobalConstants.DEFAULT_STONE);
        }

        protected override void Draw()
        {
            if (GetAutoInvalidation) GetAutoInvalidation = false;
            base.Draw();
            drawer.DrawMap(Grid);
        }
    }
}
