namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Timer : Entity
    {
        int seconds;

        float time = 0;
        int mode = 0;

        public Timer(int value)
        {
            seconds = value;
            Globals.Scrolling = false;
        }

        public override void Update(float delta)
        {
            if (this.Done)
                return;

            time += delta * Constants.FrameTime;

            if (mode == 0 && time > 1.0f)
            {
                mode++;
                time = 0.0f;
            }

            if (mode == 1)
            {
                if (Helpers.IsKeyUp(Keys.Space))
                {
                    mode++;                    
                    
                    // BUG - not more brains in the gamejam
                    Globals.Accuracy = (int)(time / (float)seconds * 100 / 25);
                    if (Globals.Accuracy > 4)
                        Globals.Accuracy = 0;

                    Globals.Accuracy = 5 - Globals.Accuracy;                    

                    time = 0.0f;
                } 
            }

            if (mode == 2 && time > 2.0f)
            {
                EntityManager.Add(new Burst());
                this.Done = true;
                mode++;
                Globals.Scrolling = true;                
            }
        }

        public override void Draw()
        {            
            if (mode == 0)
                SpriteBatch.DrawString(Font, "T i m e   M e a s u r e   a b o u t   t o   s t a r t !", new Vector2(100, 100), 
                    Color.Pink, 0, Vector2.Zero, 1, SpriteEffects.None, 1.0f);
            else if (mode == 1)
                SpriteBatch.DrawString(Font, 
                    String.Format("M e a s u r e  {0}  s e c o n d s \n   P r e s s   s p a c e   t o   s t o p.", seconds),
                    new Vector2(100, 100), 
                    Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);        
            else 
                SpriteBatch.DrawString(Font,
                    String.Format("Y o u   g o t {0} / 4  a c c u r a c y!  I m m u n e   f o r  {1}  s e c o n d s.", Globals.Accuracy, Globals.Accuracy),
                    new Vector2(100, 100),
                    Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}
