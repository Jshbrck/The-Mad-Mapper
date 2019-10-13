using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapGenerator.Input;
using MapGenerator.Util;

namespace MapGenerator
{
    static class GlobalVariables
    {
        public static int   TILE_HEIGHT;
        public static int   TILE_WIDTH;
        public static float TILE_SCALE = 1f;
        public static int   SCREEN_WIDTH = 968;
        public static int   SCREEN_HEIGHT = 800;
        public static readonly Camera CAMERA = new Camera();
        /// <summary>
        /// Initialized in GameWindow's Initialize function.
        /// </summary>
        public static ContentLoader LOADER;
    }

    static class GlobalConstants
    {
        public const string NULL = "NULL";
        public const string DEFAULT_WATER = "water_middle";
        public const string DEFAULT_GRASS = "grass_1_middle";
        public const string DEFAULT_DIRT = "dirt_middle";
        public const string DEFAULT_STONE = "stone_middle";
        public const string DEFUALT_LAVA = "lava_middle";
        public const string DEFAULT_SAND = "sand_middle";
        public const string DEFAULT_SNOW = "snow_middle";
    }
}
