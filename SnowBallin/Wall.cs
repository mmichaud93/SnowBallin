using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;
using Sce.PlayStation.HighLevel.Physics2D;
using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace SnowBallin
{
	public class Wall : GameObject
	{
		public Wall (Vector2 Position, Vector2 Scale)
		{
			this.Position=Position;
			CollisionDatas.Add(new EntityCollider.CollisionEntry() {
	            type = EntityCollider.CollisionEntityType.Wall,
				bounds = EntityCollider.CollisionBoundsType.Rectangle,
				owner = this,
				collider = this,
				topLeft = () => new Vector2(0, 0),
				bottomRight = () => new Vector2(Scale.X, Scale.Y)
			});
			setImage("Application/assets/art/ice_block.png");
			this.Scale = Scale;
			StopAllActions();
			UnscheduleAll();
			Sce.PlayStation.HighLevel.GameEngine2D.Scheduler.Instance.Schedule(this, this.Update, 0.0f, false);
		}
		public override void Update(float dt) 
		{
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
	}
}

