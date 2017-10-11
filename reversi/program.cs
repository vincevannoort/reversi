using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
	public class Program
	{
		public static void Main()
		{
            Reversi r = new Reversi();
            r.DebugReversi();
			Application.Run(r);
		}
	}
}