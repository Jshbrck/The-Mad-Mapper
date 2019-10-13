using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MapGenerator
{
    class Map
    {
        int _Height;
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        int _Width;
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        public List<Layer> Layers = new List<Layer>();
        
        public Map()
        {
            
            Width = 30;
            Height = 25;
            Layer init_Layer = new Layer(this);
            for (int i = 0; i < 3; i++)
            {
                Layers.Add(init_Layer);
            }
            
        }

        public Map(int W, int H)
        {
            Width = W;
            Height = H;
            Layer init_Layer = new Layer(this);
            for (int i = 0; i < 3; i++)
            {
                Layers.Add(init_Layer);
            }
        }

        public Map(int W, int H, int NumLayers)
        {
            Width = W;
            Height = H;
            Layer init_Layer = new Layer(this);
            for (int i = 0; i < NumLayers; i++)
            {
                Layers.Add(init_Layer);
            }
        }

        public class Layer
        {
            Map Container;
            int LayerWidth;
            int LayerHeight;
            public int Index;

            Tile[,] _Tiles;
            public Tile[,] Tiles
            {
                get { return _Tiles; }
                set { _Tiles = value; }
            }

            public Layer(Map Container)
            {
                this.Container = Container;
                LayerWidth = Container.Width;
                LayerHeight = Container.Height;
                Tiles = new Tile[LayerWidth, LayerHeight];
                FillLayer(GlobalConstants.NULL);
                Index = 0;
            }

            public Layer(Map Container, int index)
            {
                this.Container = Container;
                LayerWidth = Container.Width;
                LayerHeight = Container.Height;
                Tiles = new Tile[LayerWidth, LayerHeight];
                FillLayer(GlobalConstants.NULL);
                Index = index;
            }

            public void FillLayer(String tex)
            {
                for (int w = 0; w < LayerWidth; w++)
                {       

                    for (int h = 0; h < LayerHeight; h++)
                    {
                        ChangeTile(tex,w,h);
                    }
                }

            }

            Tuple<int, int> VectorToLayerIndex(Vector2 vec)
            {
                Tuple<int, int> Index = new Tuple<int, int>(Convert.ToInt32(vec.X / GlobalVariables.TILE_WIDTH),
                                                            Convert.ToInt32(vec.Y / GlobalVariables.TILE_HEIGHT));
                return Index;
            }

            Vector2 LayerIndexToVector(int X, int Y)
            {
                Vector2 vec = new Vector2(X * GlobalVariables.TILE_WIDTH,
                                          Y * GlobalVariables.TILE_HEIGHT);
                return vec;
            }

            public void ChangeTile(string tex, int X, int Y)
            {
                Vector2 vec = LayerIndexToVector(X,Y);
                if (Tiles[X,Y] == null) Tiles[X, Y] = new Tile(tex, vec);
                else
                {
                    Tiles[X, Y].Texture = tex;
                    Tiles[X, Y].Position = vec;
                }
            }

            public void ChangeTile(string tex, Vector2 vec)
            {
                Tuple<int, int> Index = VectorToLayerIndex(vec);
                if (Tiles[Index.Item1, Index.Item2] == null) Tiles[Index.Item1, Index.Item2] = new Tile(tex, vec);
                else
                {
                    Tiles[Index.Item1, Index.Item2].Texture = tex;
                    Tiles[Index.Item1, Index.Item2].Position = vec;
                }
            }

            public class Tile
            {
                Vector2 _Position;
                public Vector2 Position
                {
                    get { return _Position; }
                    set { _Position = value; }
                }

                string _Texture;
                public string Texture
                {
                    get { return _Texture; }
                    set { _Texture = value; }
                }

                public Tile(string tex, Vector2 pos)
                {
                    _Position = pos;
                    _Texture = tex;
                }

            }

        }

    }
}
