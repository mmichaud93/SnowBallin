using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace SnowBallin
{
	public class Game
	{
		public static Game Instance;
		public Scene scene { get; set; }
		public Vector2 TitleCameraCenter { get; set; }
		public Vector2 CameraTarget { get; set; }

		Camera2D camera;
		
		public static float screenWidth = 960f;
		public static float screenHeight = 544f;
    	
        public Node Background { get; set; }
        public Node World { get; set; }

		public Random Random { get; set; }
        public EntityCollider Collider { get; set; }
        
		public List<GameObject> AddQueue { get; set; }
		public List<GameObject> RemoveQueue { get; set; }
		
        public float WorldScale = 1.0f;

		public static Player player1;
		public static Player player2;
		
		public static bool Running = true;
		
		public Game () {
			Initialize();
		}
		public void Initialize() {
			/* 	the game is not over yet	*/
			Game.Running = true;
			
			/*	Create the scene and the various layers of the game		*/
			scene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			Background = new Node();
            World = new Node();
            Random = new Random();
            Collider = new EntityCollider();
			
			/* 	create the queues for m odify elements in the game	*/
			AddQueue = new List<GameObject>();
			RemoveQueue = new List<GameObject>();
			
			/*	add the aformentioned layers into the game	*/
            scene.AddChild(Background);
            scene.AddChild(World);
			
			/* 	get the camera object and make it look at the screen	*/
			Camera2D camera = scene.Camera as Camera2D;
			camera.SetViewFromViewport();
			Vector2 ideal_screen_size = new Vector2(screenWidth, screenHeight);
			camera.SetViewFromHeightAndCenter(ideal_screen_size.Y, ideal_screen_size / 2.0f);
			TitleCameraCenter = camera.Center;
			CameraTarget = TitleCameraCenter;
			
			// build the default world
			var bg_snow = Support.SpriteFromFile("/Application/assets/art/background.png");
			bg_snow.Position = new Vector2(0.0f, 0.0f);
			bg_snow.Pivot = new Vector2(bg_snow.TextureInfo.TextureSizef.X * 0.5f, 0.0f);
			Background.AddChild(bg_snow);
			
			
			World.AddChild(new Wall(
				new Vector2(200f,screenHeight/2),
				new Vector2(40.0f,200.0f)));
			
			
			player1 = new Player(Player.PlayerType.PLAYER1);
			player1.Position = new Vector2(player1.Scale.X+10,screenHeight/2);
			
			player2 = new Player(Player.PlayerType.PLAYER2);
			player2.Position = new Vector2(screenWidth-player2.Scale.X-10,screenHeight/2);
				
			World.AddChild(player1);
			World.AddChild(player2);
			
			Sce.PlayStation.HighLevel.GameEngine2D.Scheduler.Instance.Schedule(scene, this.Update, 0.0f, false);
		}
		
		public void Input() {
//			player2.Translate(new Vector2(Input2.GamePad0.AnalogLeft.X*(4), Input2.GamePad0.AnalogLeft.Y*(-4)));
//			if(player2.Position.X<26) {
//				player2.Position = new Vector2(26, player2.Position.Y);	
//			} else if(player2.Position.X>width) {
//				player2.Position = new Vector2(width, player2.Position.Y);	
//			}
//			if(player2.Position.Y<26) {
//				player2.Position = new Vector2(player2.Position.X, 26);	
//			} else if(player2.Position.Y>height) {
//				player2.Position = new Vector2(player2.Position.X, height);	
//			}
//			
//			player1.Translate(new Vector2(Input2.GamePad0.AnalogRight.X*(4), Input2.GamePad0.AnalogRight.Y*(-4)));
//			if(player1.Position.X<26) {
//				player1.Position = new Vector2(26, player1.Position.Y);	
//			} else if(player1.Position.X>width) {
//				player1.Position = new Vector2(width, player1.Position.Y);	
//			}
//			if(player1.Position.Y<26) {
//				player1.Position = new Vector2(player1.Position.X, 26);	
//			} else if(player1.Position.Y>height) {
//				player1.Position = new Vector2(player1.Position.X, height);	
//			}
//			
//			if(Input2.GamePad0.Circle.Press || Input2.GamePad0.Square.Press
//			   || Input2.GamePad0.Triangle.Press || Input2.GamePad0.Cross.Press) {
//				// player 2 shoot
//				player1.Shoot();
//			}
//			if(Input2.GamePad0.Left.Press || Input2.GamePad0.Right.Press
//			   || Input2.GamePad0.Up.Press || Input2.GamePad0.Down.Press) {
//				// player 1 shoot
//				player2.Shoot();
//			}
		}

		public void Update(float dt) {
			if(!Game.Running) {
				
			} else {
				if(Input2.GamePad0.Start.Press) 
					Initialize();
				
				if(Input2.GamePad0.Select.Press) 
					Director.Instance.ReplaceScene(new MenuScene());
			}
		}
		
		// to be called after everything else has had a chance to go, i think
		public void FrameUpdate() {
			Collider.Collide();
			
			foreach (GameObject e in RemoveQueue)
				World.RemoveChild(e,true);
			foreach (GameObject e in AddQueue)
				World.AddChild(e);
			
			RemoveQueue.Clear();
			AddQueue.Clear();
			
		}
	}
}

