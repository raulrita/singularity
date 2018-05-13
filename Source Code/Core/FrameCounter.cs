namespace Neuro
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class FrameCounter : Entity
    {
        private long TotalFrames;
        private float TotalSeconds;
        private float AverageFramesPerSecond;
        private float CurrentFramesPerSecond;

        private Vector2 position = new Vector2(1500, 1000);
        private const int MAXIMUM_SAMPLES = 100;

        private Queue<float> sampleBuffer = new Queue<float>();

        public override void Update(float delta)
        {
            CurrentFramesPerSecond = 1.0f / delta * Constants.FPS;

            sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                sampleBuffer.Dequeue();
                AverageFramesPerSecond = sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += delta * Constants.FrameTime;
        }

        public override void Draw() 
        {
            SpriteBatch.DrawString(Font, string.Format("{0} fps", Math.Round(CurrentFramesPerSecond)), position, Color.White);
        }
    }
}