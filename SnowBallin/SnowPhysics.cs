using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace SnowBallin
{
	public class SnowPhysics : PhysicsScene
		{
			// PixelsToMeters
			public const float PtoM = 50.0f;
			private const float BULLETRADIUS = 9.0f/2f; 
			private const float PLAYERWIDTH = 27.0f;
			private const float PLAYERHEIGHT = 27.0f;
			private float _screenWidth;
			private float _screenHeight;
			
			
			public enum BODIES { Player1, Player2, Bullet, Wall};
			
			public SnowPhysics ()
			{
				_screenWidth = Director.Instance.GL.Context.GetViewport().Width;
				_screenHeight = Director.Instance.GL.Context.GetViewport().Height;
				
				// turn gravity off
				this.InitScene();
				this.Gravity = new Vector2(0.0f,0.0f);
				
				// Set the screen boundaries + 2m or 100pixel
				this.SceneMin = new Vector2(-100f,-100f) / PtoM;
				this.SceneMax = new Vector2(_screenWidth + 100.0f,_screenHeight + 100.0f) / PtoM;
				
				// And turn the bouncy bouncy on
				this.RestitutionCoeff = 0.0f;
			
			// Player shape
			this.SceneShapes[numShape] = new PhysicsShape(PLAYERWIDTH/2f/PtoM);
			numShape++;
			
			// create the player1 physics object
			this.SceneBodies[numBody] = new PhysicsBody(SceneShapes[0],10.0f);
			this.SceneBodies[numBody].Position = new Vector2(10+PLAYERWIDTH/2,_screenHeight/2-PLAYERHEIGHT/2) / PtoM;
			this.SceneBodies[numBody].Rotation = 0;
			this.SceneBodies[numBody].ShapeIndex = 0;
			numBody++;
				
			// Player2
			this.SceneBodies[numBody] = new PhysicsBody(SceneShapes[0],10.0f);
			this.SceneBodies[numBody].Position = new Vector2(_screenWidth-10-PLAYERWIDTH/2,_screenHeight/2-PLAYERHEIGHT/2) / PtoM;
			this.SceneBodies[numBody].Rotation = 0;
			this.SceneBodies[numBody].ShapeIndex = 0;
			numBody++;
			
			// vertical wall shape
			Vector2 verticalWallBox = new Vector2(10.0f,_screenHeight)/PtoM;
			this.SceneShapes[numShape] = new PhysicsShape(verticalWallBox);
			numShape++;
				
			// Left wall
			this.SceneBodies[numBody] = new PhysicsBody(SceneShapes[1],PhysicsUtility.FltMax);
            this.SceneBodies[numBody].Position = new Vector2(-10,_screenHeight/2f) / PtoM;
            this.sceneBodies[numBody].ShapeIndex = 1;
            this.sceneBodies[numBody].Rotation = 0;
            this.SceneBodies[numBody].SetBodyStatic();
			numBody++;
			
			// right wall
			this.SceneBodies[numBody] = new PhysicsBody(SceneShapes[1],PhysicsUtility.FltMax);
            this.SceneBodies[numBody].Position = new Vector2(_screenWidth+10,_screenHeight/2f) / PtoM;
            this.sceneBodies[numBody].ShapeIndex = 1;
            this.sceneBodies[numBody].Rotation = 0;
            this.SceneBodies[numBody].SetBodyStatic();
			numBody++;
			
			// horizontal wall shape
			Vector2 horizontalWallBox = new Vector2(_screenWidth,10.0f)/PtoM;
			this.SceneShapes[numShape] = new PhysicsShape(horizontalWallBox);
			numShape++;
			
			// up wall
			this.SceneBodies[numBody] = new PhysicsBody(SceneShapes[2],PhysicsUtility.FltMax);
            this.SceneBodies[numBody].Position = new Vector2(_screenWidth/2,-10) / PtoM;
            this.sceneBodies[numBody].ShapeIndex = 2;
            this.sceneBodies[numBody].Rotation = 0;
            this.SceneBodies[numBody].SetBodyStatic();
			numBody++;
			
			// down wall
			this.SceneBodies[numBody] = new PhysicsBody(SceneShapes[2],PhysicsUtility.FltMax);
            this.SceneBodies[numBody].Position = new Vector2(_screenWidth/2,_screenHeight+10) / PtoM;
            this.sceneBodies[numBody].ShapeIndex = 2;
            this.sceneBodies[numBody].Rotation = 0;
            this.SceneBodies[numBody].SetBodyStatic();
			numBody++;
			
			// bullet shape
			this.SceneShapes[numShape] = new PhysicsShape(BULLETRADIUS/PtoM);

		}
		public void SpawnBullet(Bullet bullet) {
			
		}
		public void SpawnBullet() {		
			
		}
	}
}

