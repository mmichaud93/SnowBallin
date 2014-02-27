using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.Physics2D;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace SnowBallin
{
	public class Bullet : GameObject
	{
		private Player.PlayerType playerExclude;
		public static LinkedList<Bullet> Cache { get; set; }
		public static int CacheLength = 100;
		
		public static float BulletSpeed = 8;
		public static float SpeedRange = 0.1f;
		public static float RotationRange = FMath.PI/120.0f;
		
		public Bullet (Player.PlayerType playerExclude)
		{
			this.playerExclude = playerExclude;
		}
		public Bullet ()
		{
			CollisionDatas.Add(new EntityCollider.CollisionEntry() {
	            type = EntityCollider.CollisionEntityType.Bullet,
				bounds = EntityCollider.CollisionBoundsType.Circle,
				owner = this,
				collider = this,
				center = () => new Vector2(Scale.X/2, Scale.Y/2),
				radius = () => 4.5f,
			});
		}
		public static void Spawn(Vector2 position, double rotation)
		{

				
			Bullet bullet = new Bullet();
				bullet.setImage("Application/assets/art/bullet.png");
				bullet.Scale = new Vector2(10.0f);
				bullet.StopAllActions();
				bullet.UnscheduleAll();
			
			bullet.Position = new Vector2(position.X+bullet.Scale.X/2,
			                              position.Y+bullet.Scale.Y/2);
			bullet.speed = BulletSpeed+(Game.Instance.Random.Next(-(int)(SpeedRange*100), (int)(SpeedRange*100))/100.0f);
			bullet.rotation = rotation+(Game.Instance.Random.Next(-(int)(RotationRange*100), (int)(RotationRange*100))/100.0f);
			
			Sce.PlayStation.HighLevel.GameEngine2D.Scheduler.Instance.UnscheduleAll(bullet);
			Sce.PlayStation.HighLevel.GameEngine2D.Scheduler.Instance.Schedule(bullet, bullet.Update, 0.0f, false);
			Game.Instance.World.AddChild(bullet);
		}
		public override void Update (float dt)
		{
			base.Update(dt);
			
			if(Position.X<0-Scale.X || Position.X>Game.screenWidth+Scale.X ||
			   Position.Y<0-Scale.Y || Position.Y>Game.screenHeight+Scale.Y) {
				Game.Instance.RemoveQueue.Add(this);
			}
		}
	}
}

