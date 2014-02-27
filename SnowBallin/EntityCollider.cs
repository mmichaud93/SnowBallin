using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;

using Sce.PlayStation.HighLevel.GameEngine2D;

namespace SnowBallin
{
	public class EntityCollider
	{
		public enum CollisionEntityType
		{
			Player,
			Bullet,
			Wall
		}
		public enum CollisionBoundsType
		{
			Circle,
			Rectangle
		}
		
		public delegate Vector2 GetCenterDelegate();
		public delegate float GetRadiusDelegate();
		public delegate Vector2 GetTopLeftDelegate();
		public delegate Vector2 GetBottomRightDelegate();
		// GetForceVector()?
		
		public struct CollisionEntry
		{
			public CollisionEntityType type;
			public CollisionBoundsType bounds;
			public GameObject owner;
			public Node collider;
			public GetCenterDelegate center;
			public GetRadiusDelegate radius;
			public GetTopLeftDelegate topLeft;
			public GetBottomRightDelegate bottomRight;
		}
		
		List<List<CollisionEntry>> typed_entries;
		
		public EntityCollider()
		{
			typed_entries = new List<List<CollisionEntry>>();
			typed_entries.Add(new List<CollisionEntry>()); // Player
			typed_entries.Add(new List<CollisionEntry>()); // Enemy
			typed_entries.Add(new List<CollisionEntry>()); // Walls
		}
		
		public void AddCircle(CollisionEntityType type, GameObject owner, Node collider, GetCenterDelegate center, GetRadiusDelegate radius)
		{	
			CollisionEntry entry = new CollisionEntry() { type = type, bounds = CollisionBoundsType.Circle, owner = owner, collider = collider, center = center, radius = radius };
			List<CollisionEntry> entries = typed_entries[(int)type];
			entries.Add(entry);
		}
		
		public void AddRectangle(CollisionEntityType type, GameObject owner, Node collider, GetTopLeftDelegate topLeft, GetBottomRightDelegate bottomRight)
		{	
			CollisionEntry entry = new CollisionEntry() { type = type, bounds = CollisionBoundsType.Rectangle, owner = owner, collider = collider, topLeft = topLeft, bottomRight = bottomRight };
			List<CollisionEntry> entries = typed_entries[(int)type];
			entries.Add(entry);
		}
		
		public void Add(CollisionEntry entry)
		{
			List<CollisionEntry> entries = typed_entries[(int)entry.type];
			entries.Add(entry);
		}
		
		public void Collide()
		{
			// for each list
			//   check for each other list
			
			foreach (List<CollisionEntry> entries in typed_entries)
			{
				
				foreach (List<CollisionEntry> other_entries in typed_entries)
				{
					if (other_entries == entries)
						continue;
					
					for (int i = 0; i < entries.Count; ++i)
					{
						GameObject collider_owner = entries[i].owner;
						Node collider_collider = entries[i].collider;
						Vector2 collider_center = new Vector2(0,0);
						if(entries[i].center!=null)
							collider_center = entries[i].center()+collider_owner.Position;
						float collider_radius = 0;
						if(entries[i].radius!=null)
							collider_radius = entries[i].radius();
						
						Vector2 collider_topLeft = new Vector2(0,0);
						if(entries[i].topLeft!=null)
							collider_topLeft = entries[i].topLeft()+collider_owner.Position;
						Vector2 collider_bottomRight = new Vector2(0,0);
						if(entries[i].bottomRight!=null)
							collider_bottomRight = entries[i].bottomRight()+collider_owner.Position;
						
						
						for (int j = 0; j < other_entries.Count; ++j)
						{
							GameObject collidee_owner = other_entries[j].owner;
							Node collidee_collider = other_entries[j].collider;
							
							if (collider_owner == collidee_owner)
								continue;
							
							Vector2 collidee_center = new Vector2(0,0);
							if(other_entries[j].center!=null)
								collidee_center = other_entries[j].center()+collidee_owner.Position;
							float collidee_radius = 0;
							if(other_entries[j].radius!=null)
								collidee_radius = other_entries[j].radius();
							Vector2 collidee_topLeft = new Vector2(0,0);
							if(other_entries[j].topLeft!=null)
								collidee_topLeft = other_entries[j].topLeft()+collidee_owner.Position;
							Vector2 collidee_bottomRight = new Vector2(0,0);
							if(other_entries[j].bottomRight!=null)
								collidee_bottomRight = other_entries[j].bottomRight()+collidee_owner.Position;
							
							if(entries[i].bounds==CollisionBoundsType.Circle && other_entries[j].bounds==CollisionBoundsType.Circle) {
								// circle and circle	
								float r = collider_radius + collidee_radius;
							
								Vector2 offset = collidee_center - collider_center;
								float lensqr = offset.LengthSquared();	
							
								if (lensqr < r * r)
								{
									collider_owner.CollideTo(collidee_owner, collidee_collider);
									collidee_owner.CollideFrom(collider_owner, collider_collider);
								}
								
							} else if(entries[i].bounds==CollisionBoundsType.Circle && other_entries[j].bounds==CollisionBoundsType.Rectangle) {
								// circle and rect	
								Console.WriteLine("circle and rect");
								Vector2 offset = collidee_center - collider_center;
								float r = collider_radius + (FMath.Sqrt(
									(collidee_topLeft.X-collidee_bottomRight.X)*(collidee_topLeft.X-collidee_bottomRight.X)+
									(collidee_topLeft.Y-collidee_bottomRight.Y)*(collidee_topLeft.Y-collidee_bottomRight.Y)));
								
								float lensqr = offset.LengthSquared();	
							Console.WriteLine("offset = "+offset.Length()+", r = "+(r));
								if (lensqr < r * r)
								{
									collider_owner.CollideTo(collidee_owner, collidee_collider);
									collidee_owner.CollideFrom(collider_owner, collider_collider);
								}
							} else if(entries[i].bounds==CollisionBoundsType.Rectangle && other_entries[j].bounds==CollisionBoundsType.Rectangle) {
								// rect and rect	
							} else if(entries[i].bounds==CollisionBoundsType.Rectangle && other_entries[j].bounds==CollisionBoundsType.Circle) {
								// rect and circle	
							}
							
							
							
							
						}
					}
				}
			}
			
			Clear();
		}
		
		public void Clear()
		{
			foreach (List<CollisionEntry> entries in typed_entries)
				entries.Clear();
		}
	}
}

