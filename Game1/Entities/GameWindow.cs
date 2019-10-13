using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.Controls;
using MonoGame.Forms.Services;
using MapGenerator.Input;
using MapGenerator.Util;

namespace MapGenerator.Entities
{
    class GameWindow : UpdateWindow
    {

        InputState InState;
        Map map;
        Drawer drawer;
        CellularGenerator Gen; 

        protected override void Initialize()
        {
            base.Initialize();
            Editor.Content.RootDirectory = "Content";
            Editor.BackgroundColor = Color.Black;
            GlobalVariables.LOADER.LoadTileSheet(Editor,null);
            InState = new InputState();
            GlobalVariables.CAMERA.SetAndCenterViewport(GlobalVariables.SCREEN_WIDTH,GlobalVariables.SCREEN_HEIGHT);
            drawer = new Drawer(Editor, GlobalVariables.LOADER.Tiles, GlobalVariables.LOADER.TileSheet);
            map = new Map();
            Gen = new CellularGenerator(map);
            map.Layers[0].FillLayer(GlobalConstants.DEFUALT_LAVA);
            map.Layers[1] = Gen.RandomlyGenerateNewLayer("Timmy", GlobalConstants.DEFAULT_WATER);
            map.Layers[2] = Gen.RandomlyGenerateNewLayer("Bilbo", GlobalConstants.DEFAULT_SAND);
        }

        protected override void Draw()
        {
            base.Draw();
            drawer.DrawMap(map);
            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            InState.Update();
            GlobalVariables.CAMERA.DoInput(InState);
            
        }



    }
}
