using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.Physics2D;

namespace SnowBallin
{
	/**
	 * 
	 * GameObject extends SpriteUV and has variables and methods that specifically give it collidor capability
	 * 
	 */
	
	public class GameObject : SpriteUV
	{
		public double rotation = 0.0;	// in degrees
		public float speed = 0.0f;
		public float drag = 0.0f;
		
		public List<EntityCollider.CollisionEntry> CollisionDatas;
		public virtual void CollideTo(GameObject owner, Node collider) { }
		public virtual void CollideFrom(GameObject owner, Node collider) { }

		public GameObject ()
		{
			Sce.PlayStation.HighLevel.GameEngine2D.Scheduler.Instance.Schedule(this, Update, 0.0f, false);
			CollisionDatas = new List<EntityCollider.CollisionEntry>();
		}
		public void setImage(String imageLoc) {
			this.TextureInfo = new TextureInfo(new Texture2D(imageLoc,false));
			this.Scale = this.TextureInfo.TextureSizef;
            this.Pivot = new Sce.PlayStation.Core.Vector2(0.5f,0.5f);
			this.CenterSprite();
			Scheduler.Instance.ScheduleUpdateForTarget(this,0,false);
		}		
		public void ScaleUV(Vector2 scaleFactor) {
			this.Scale = new Vector2(scaleFactor.X*this.Scale.X, scaleFactor.Y*this.Scale.Y);
		}
		public virtual void Translate(Vector2 translateFactor) {
			Position = new Vector2(Position.X+translateFactor.X, Position.Y+translateFactor.Y);	
		}
		public virtual void Update(float dt) {
			Translate(new Vector2((float)(speed*System.Math.Cos(rotation)), (float)(speed*System.Math.Sin(rotation))));
			foreach (EntityCollider.CollisionEntry c in CollisionDatas)
				{
					if (c.owner != null)
						Game.Instance.Collider.Add(c);
				}
		}
		public static Vector2 GetCollisionCenter(Node node)
		{
			Bounds2 bounds = new Bounds2();
			node.GetlContentLocalBounds(ref bounds);
			Vector2 center = node.LocalToWorld(bounds.Center);
			return center;
		}
	}
	
}

