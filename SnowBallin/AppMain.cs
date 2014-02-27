using System;
using Sce.PlayStation.HighLevel.UI; 
using Sce.PlayStation.HighLevel.GameEngine2D;

namespace SnowBallin
{
	public class AppMain
	{		
		public static void Main (string[] args)
		{
			Director.Initialize();
			UISystem.Initialize(Director.Instance.GL.Context);
			Director.Instance.RunWithScene(new MenuScene());
//			Sce.PlayStation.HighLevel.GameEngine2D.Director.Initialize( 1024*4 );
//			
//			Game.Instance = new Game();
//            var game = Game.Instance;
//			
//			while (true) {
//				Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.GL.SetBlendMode(BlendMode.Normal);
//                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Update();
//				
//				game.FrameUpdate();
//				
//                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.Render();
//                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.GL.Context.SwapBuffers();
//                Sce.PlayStation.HighLevel.GameEngine2D.Director.Instance.PostSwap();
//			}
		}
	}
}
