namespace Neuro
{
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Floor : Entity
    {
        private List<Texture2D> textures;

        public Floor()
        {
            Globals.Scrolling = true;
            LoadTextures(); 

            for (int row = 7; row >= -7; row--)
                AddTileRow(row, null);
        }

        private void LoadTextures()
        {
            textures = new List<Texture2D>();
            string filename;

            for (int h = 0; h < Constants.TileTextureCount; h ++)
            {
                filename = String.Format("Tile_{0}", h);
                textures.Add(ContentManager.Load<Texture2D>(filename));
            }
        }

        private void AddTileRow(int row, Texture2D texture)
        {            
            for (int h = 0; h < 3; h++)
            {
                Tile aux = new Tile();
                
                AlignType align = h == 0 ? AlignType.LEFT : 
                    (h == 1 ? AlignType.CENTER : AlignType.RIGHT);
                
                aux.Reset(
                    texture != null ? texture : 
                    textures[Globals.Random.Next(textures.Count)], 
                    TileType.FLOOR, 
                    align,
                    row);
                
                EntityManager.Add(aux);
            }
        }
    }
};