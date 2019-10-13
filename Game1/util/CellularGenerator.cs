using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator.Util
{
    class CellularGenerator
    {
        Map WorkingMap;
        Map.Layer PreviousLayer;
        Map.Layer CurrentLayer;


        public CellularGenerator(Map m)
        {
            WorkingMap = m;
        }

        public Map.Layer RandomlyGenerateNewLayer(string seed, string tex)
        {
            CurrentLayer = new Map.Layer(WorkingMap);
            SeedLayer(seed, tex, 70);
            CellularAutomataWrapper(2, tex);
            return CurrentLayer;
        }

        void SeedLayer(string seed, string tex, int SeedThreshold)
        {
            PreviousLayer = CurrentLayer;
            Random Seeder = new Random(seed.GetHashCode());
            for (int w = 0; w < WorkingMap.Width; w++)
            {
                for (int h = 0; h < WorkingMap.Height; h++)
                {
                    if (Seeder.Next(100) > (SeedThreshold % 100)) // We mod by 100 so there is always a chance for seed
                    {
                        CurrentLayer.ChangeTile(tex, w, h);
                    }
                }
            }
        }

        void CellularAutomataWrapper(int Steps, string tex)
        {
            for (int i = 0; i < Steps; i++)
            {
                CellularAutomataStep(tex);
            }
        }

        void CellularAutomataStep(string tex)
        {
            PreviousLayer = CurrentLayer;
            for (int w = 0; w < WorkingMap.Width; w++)
            {
                for (int h = 0; h < WorkingMap.Height; h++)
                {
                    if (NumberSameNeighbors(w,h,tex) >= 3)
                    {
                        CurrentLayer.ChangeTile(tex, w, h);
                    }
                }
            }

        }

        int NumberSameNeighbors(int x, int y, string tex)
        {
            int Counter = 0;
            if ((x > 0) && (PreviousLayer.Tiles[x - 1, y].Texture == tex)) Counter++;
            if ((x < WorkingMap.Width - 1) && (PreviousLayer.Tiles[x + 1, y].Texture == tex)) Counter++;
            if ((y > 0) && (PreviousLayer.Tiles[x, y-1].Texture == tex)) Counter++;
            if ((y < WorkingMap.Height - 1) && (PreviousLayer.Tiles[x, y+1].Texture == tex)) Counter++;
            return Counter;
        }
            
    }
}
