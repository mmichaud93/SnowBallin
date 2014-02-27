using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Test
{
	public class Shape : GameObject
	{
		Image img;
		int width = 0;
		int height = 0;
		
		public static int SHAPE_CIRCLE = 0;
		public static int SHAPE_SQUARE = 1;
		public static int SHAPE_TRIANGLE = 2;
		
		public Shape() {}
		public Shape (int width, int height)
		{
			this.width = width;
			this.height = height;
			 img = new Image(ImageMode.Rgba, new ImageSize(width,height),
                         new ImageColor(255,0,0,0));
  
   Texture2D texture = new Texture2D(width,height,false,
                                     PixelFormat.Rgba);
   texture.SetPixels(0,img.ToBuffer());
   img.Dispose();                                  
   
   TextureInfo ti = new TextureInfo();
   ti.Texture = texture;
   
   SpriteUV sprite = new SpriteUV();
   sprite.TextureInfo = ti;
   
   sprite.Quad.S = ti.TextureSizef;
   sprite.CenterSprite();
   sprite.Position = scene.Camera.CalcBounds().Center;
		}
		public void setShape(int shape) {
			switch(shape) {
			case SHAPE_CIRCLE:
				break;
			case SHAPE_SQUARE:
				break;
			case SHAPE_TRIANGLE:
				break;
			}
		}
	}
}

