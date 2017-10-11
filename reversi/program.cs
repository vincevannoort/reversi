using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
	public class Program
	{
		public static void Main()
		{
			Mandelbrot m = new Mandelbrot();
			Application.Run(m);
		}
	}
}