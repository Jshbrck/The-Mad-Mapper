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

        UpdateService Editor;
        DrawService Draw;

        public ContentLoader(UpdateService e)
        {
            Editor = e;
            Draw = null;
        }

        public ContentLoader(DrawService d)
        {
            Editor = null;
            Draw = d;
        }

        public void LoadTileSet(string TSX)
        {
            string Pth, SourceImage;
            List<string> TileNames = new List<string>();
            int ImageHeight, ImageWidth;
            IEnumerable<XElement> tileset;

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


            LoadTileSet(SourceImage, ImageHeight, ImageWidth, TileNames);

        }

        private void LoadTileSet(string TileFile, int ImageH, int ImageW, List<string> TileNames)
        {
            int k = 0;
            Rectangle rect = new Rectangle(0, 0, GlobalVariables.TILE_WIDTH, GlobalVariables.TILE_HEIGHT);
            for (int y = 0; y < ImageH; y += GlobalVariables.TILE_HEIGHT)
            {
                for (int x = 0; x < ImageW; x += GlobalVariables.TILE_WIDTH)
                {
                    if (TileNames[k] != "")
                    {
                        rect.X = x;
                        rect.Y = y;
                        if (TileNames[k] == "NOTUSED") TileNames[k] = ("NOTUSED" + k);
                        _Tiles.Add(TileNames[k], rect);
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
            string s = Path.GetFileNameWithoutExtension(TileFile);
            if (Draw == null) _TileSheet = Editor.Content.Load<Texture2D>(s);
            else if (Editor == null) _TileSheet = Draw.Content.Load<Texture2D>(s);
        }
    }
}
