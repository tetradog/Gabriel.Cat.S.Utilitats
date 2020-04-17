using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionRectangle
    {
        public static Point RelativeToAbsolute(this Rectangle rect, Point point)
        {
            return new Point(point.X+rect.X, point.Y+rect.Y);
        }
    }
}
