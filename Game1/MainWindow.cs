using System.Drawing;
using System.Windows.Forms;

namespace MapGenerator
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            GlobalVariables.LOADER = new Util.ContentLoader();
            InitializeComponent();
            GlobalVariables.LOADER.LoadTileSet("Terrain.tsx");
        }

        private void TileSelector_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string SelectedTile;
            if (TileSelector.SelectedIndex > 0)
            {
                SelectedTile = TileSelector.SelectedItem.ToString();
                TilePreview.ChangeDisplayedTile(SelectedTile);
            }
        }
    }
}
