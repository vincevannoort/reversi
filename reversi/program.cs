using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
	public class Program
	{
		public static void Main()
		{
            Reversi r = new Reversi(14,8);
            r.DebugReversi();
			Application.Run(r);
		}
	}
}