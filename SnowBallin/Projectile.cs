using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace SnowBallin
{
	public class Projectile : GameObject
	{
		float direction;
		
		public Projectile (String imageloc) 
		{
			
		}
		public void setSpeed(float speed) {
			this.speed = speed;
		}
		public void setDirection(float direction) {
			this.direction = direction;	
		}
		public float getSpeed() {
			return speed;
		}
		public float getDirection() {
			return direction;
		}
		
		public override void Update(float dt) {
				Translate(new Vector2((float)(speed*Math.Cos(direction)), (float)(speed*Math.Sin(direction))));
				Collide ();
		}
		public virtual void Collide() {
			
		}
	}
}

