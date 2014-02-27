using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace SnowBallin
{
	public class Player : GameObject
	{
		public enum PlayerType { PLAYER1, PLAYER2 };
		
		private PlayerType type;
		
		public static Player Player1Instance;
		public static Player Player2Instance;
		
		public Sce.PlayStation.HighLevel.GameEngine2D.SpriteTile BodySprite { get; set; }
		
		public Player (PlayerType type)
		{
			this.type = type;

			if(type== PlayerType.PLAYER1)
			{
				this.TextureInfo = new TextureInfo(new Texture2D("Application/assets/art/player1.png",false));
				this.Scale = this.TextureInfo.TextureSizef;
				this.Position = new Sce.PlayStation.Core.Vector2(
					Director.Instance.GL.Context.GetViewport().Width - this.Scale.X/2-10,
					Director.Instance.GL.Context.GetViewport().Height/2 - this.Scale.Y/2);	
				Player.Player1Instance = this;
			}
			else
			{
				this.TextureInfo = new TextureInfo(new Texture2D("Application/assets/art/player2.png",false));
				this.Scale = this.TextureInfo.TextureSizef;
				this.Position = new Sce.PlayStation.Core.Vector2(
					10 + this.Scale.X/2,
					Director.Instance.GL.Context.GetViewport().Height - this.Scale.Y/2 - 10);
				Player.Player2Instance = this;
			}
			
			CollisionDatas.Add(new EntityCollider.CollisionEntry() {
	            type = EntityCollider.CollisionEntityType.Player,
				bounds = EntityCollider.CollisionBoundsType.Circle,
				owner = this,
				collider = this,
				center = () => new Vector2(Scale.X/2, Scale.Y/2),
				radius = () => 14.0f,
			});
			
			this.Pivot = new Sce.PlayStation.Core.Vector2(0.5f,0.5f);
		}

		public override void Update (float dt)
		{
			/*	look at the other player	*/
			float difX = 0.0f;
			float difY = 0.0f;
			
			if(type == PlayerType.PLAYER2)
			{
				difX = Player2Instance.Position.X - Player1Instance.Position.X;
				difY = Player2Instance.Position.Y - Player1Instance.Position.Y;
				
//				if(Input2.GamePad0.Left.Down)
//				{
//					Translate(new Vector2(-3, 0));
//				}
//				if(Input2.GamePad0.Right.Down)
//				{
//					Translate(new Vector2(3, 0));
//				}
//				if(Input2.GamePad0.Up.Down)
//				{
//					Translate(new Vector2(0, 3));
//				}
//				if(Input2.GamePad0.Down.Down)
//				{
//					Translate(new Vector2(0, -3));
//				}
				Translate(Input2.GamePad0.AnalogRight*new Vector2(1,-1)*5);
				
				if(Input2.GamePad0.Cross.Press || Input2.GamePad0.Square.Press ||
				   Input2.GamePad0.Circle.Press || Input2.GamePad0.Triangle.Press) 
				{
					Bullet.Spawn(new Vector2(Position.X-Scale.X*FMath.Cos((float)rotation),
					                         Position.Y+Scale.X*FMath.Sin((float)rotation)), rotation+FMath.PI);
				}
			}
			else if(type == PlayerType.PLAYER1)
			{
				difX = Player1Instance.Position.X - Player2Instance.Position.X;
				difY = Player1Instance.Position.Y - Player2Instance.Position.Y;
				Translate(Input2.GamePad0.AnalogLeft*new Vector2(1,-1)*5);
				Translate(new Vector2(0.1f,0));
				if(Input2.GamePad0.Left.Press || Input2.GamePad0.Up.Press ||
				   Input2.GamePad0.Right.Press || Input2.GamePad0.Down.Press) 
				{
					Bullet.Spawn(new Vector2(Position.X-Scale.X*FMath.Cos((float)rotation),
					                         Position.Y+Scale.X*FMath.Sin((float)rotation)), rotation+FMath.PI);
				}
			}
			float whatToRotate = ((float)(System.Math.Atan2(difY, difX)))-(float)rotation;
			
			this.Rotate(whatToRotate);
			rotation = (float)(System.Math.Atan2(difY, difX));
			base.Update(dt);
			
			
		}
		
		public override void CollideFrom(GameObject owner, Node collider)
		{
			base.CollideTo(owner, collider);
			Type type = owner.GetType();
			if (type == typeof(Bullet))
			{
				Game.Instance.RemoveQueue.Add(owner);		
			}
		}
		
		~Player()
		{
			this.TextureInfo.Texture.Dispose ();
		}
	}
}