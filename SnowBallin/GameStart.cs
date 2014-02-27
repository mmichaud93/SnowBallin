using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace SnowBallin
{
	public class GameStart
	{
		public Game game;
		
		public GameStart ()
		{
			Game.Instance = new Game();
			var game = Game.Instance;
			
			Director.Instance.ReplaceScene(game.scene);
						
			System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
			
			while(Game.Running)
			{
				timer.Start();
                SystemEvents.CheckEvents();
				
				Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.GL.SetBlendMode(BlendMode.Normal);
                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Update();
                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Render();
			
				game.FrameUpdate();
				
				timer.Stop();
                long ms = timer.ElapsedMilliseconds;
                //Console.WriteLine("ms: {0}", (int)ms);
            	timer.Reset();

                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.GL.Context.SwapBuffers();
                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.PostSwap();
			}
		}
	}
}

