﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Web.Mvc;

namespace VotingSystem.Web.Helpers
{
	public class CaptchaHelper
	{
		public static ActionResult GetCaptcha(bool noisy)
		{
			Random rand = new Random((int)DateTime.Now.Ticks);
			string captcha = CreateCaptchaText(rand);
			return CreateCaptcha(rand, captcha, noisy);
		}

		private static string CreateCaptchaText(Random rand)
		{
			int a = rand.Next(10, 99);
			int b = rand.Next(0, 9);
			GlobalVariables.Captcha = a + b;
			return String.Format("{0} + {1} = ?", a, b);
		}

		private static ActionResult CreateCaptcha(Random rand, string captcha, bool noisy)
		{
			//REVIEW: Pls, check guidelines how to define multiple usings
			using (MemoryStream mem = new MemoryStream())
			using (Bitmap bmp = new Bitmap(130, 30))
			using (Graphics gfx = Graphics.FromImage(bmp))
			{
				return DrawCaptcha(gfx, bmp, mem, rand, captcha, noisy);
			}
		}

		private static ActionResult DrawCaptcha(Graphics gfx, Bitmap bmp, MemoryStream mem, Random rand, string captcha, bool noisy)
		{
			gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			gfx.SmoothingMode = SmoothingMode.AntiAlias;
			gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
			if (noisy)
			{
				DrawEllipses(gfx, rand);
			}
			gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);
			bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
			return new FileContentResult(mem.GetBuffer(), "image/Jpeg");
		}

		private static void DrawEllipses(Graphics gfx, Random rand)
		{
			//REVIEW: Pls, check guidelines how to define multiple variables
			int i, r, x, y;
			Pen pen = new Pen(Color.Yellow);
			for (i = 1; i < 10; i++)
			{
				pen.Color = Color.FromArgb(
				(rand.Next(0, 255)),
				(rand.Next(0, 255)),
				(rand.Next(0, 255)));

				r = rand.Next(0, (130 / 3));
				x = rand.Next(0, 130);
				y = rand.Next(0, 30);

				gfx.DrawEllipse(pen, x - r, y - r, r, r);
			}
		}
	}
}