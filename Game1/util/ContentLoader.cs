using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Services;

namespace MapGenerator.Util
{

    

    class ContentLoader
    {
        public delegate void LoadEventHandler(Dictionary<String, Rectangle> D);
        public event LoadEventHandler LoadEvent;
        Dictionary<String, Rectangle> _Tiles = new Dictionary<string, Rectangle>();
        public Dictionary<String,Rectangle> Tiles
        {
            get { return _Tiles; }
        }

        Texture2D _TileSheet;
        public Texture2D TileSheet
        {
            get { return _TileSheet; }
        }

        string SourceImage;
        List<string> TileNames ;

        public void LoadTileSet(string TSX)
        {
            string Pth;
            TileNames = new List<string>();
            IEnumerable<XElement> tileset;
            int ImageHeight, ImageWidth;
            Pth = AppDomain.CurrentDomain.BaseDirectory;
            XDocument XML = XDocument.Load(Path.Combine(Pth, "Content", TSX));
            tileset = XML.Descendants();

            foreach (var tile in tileset.Descendants())
            {
                if (tile.Name == "tile")
                {
                    if (tile.HasElements)
                    {
                        TileNames.Add(Convert.ToString(tile.Element("properties").Element("property").Attribute("value").Value));
                    }
                }

            }

            GlobalVariables.TILE_WIDTH = Convert.ToInt32(XML.Element("tileset").Attribute("tilewidth").Value);
            GlobalVariables.TILE_HEIGHT = Convert.ToInt32(XML.Element("tileset").Attribute("tileheight").Value);
            SourceImage = Convert.ToString(XML.Element("tileset").Element("image").Attribute("source").Value);
            ImageHeight = Convert.ToInt32(XML.Element("tileset").Element("image").Attribute("height").Value);
            ImageWidth = Convert.ToInt32(XML.Element("tileset").Element("image").Attribute("width").Value);
            LoadTileNames(ImageHeight,ImageWidth);
        }

        void LoadTileNames(int ImageHeight,int ImageWidth)
        {
            int k = 0;
            Rectangle rect = new Rectangle(0, 0, GlobalVariables.TILE_WIDTH, GlobalVariables.TILE_HEIGHT);
            for (int y = 0; y < ImageHeight; y += GlobalVariables.TILE_HEIGHT)
            {
                for (int x = 0; x < ImageWidth; x += GlobalVariables.TILE_WIDTH)
                {
                    if (TileNames[k] != "")
                    {
                        rect.X = x;
                        rect.Y = y;
                        if (TileNames[k] == "NOTUSED") TileNames[k] = ("NOTUSED" + k);
                        { _Tiles.Add(TileNames[k], rect); }
                        k++;
                        if (k >= TileNames.Count)
                        {
                            break;
                        }
                    }
                }
                if (k >= TileNames.Count)
                {
                    break;
                }
            }
            LoadEvent(Tiles);
        }

        // For use within the editor window and the preview Window
        // If within the editor window pass in the editor object
        // If within the preview window pass in the draw object
        public void LoadTileSheet(UpdateService Editor, DrawService Draw)
        {
            if (Editor != null || Draw != null)
            {
                string s = Path.GetFileNameWithoutExtension(SourceImage);
                if (Draw == null) _TileSheet = Editor.Content.Load<Texture2D>(s);
                else if (Editor == null) _TileSheet = Draw.Content.Load<Texture2D>(s);
            }
        }
    }
}
