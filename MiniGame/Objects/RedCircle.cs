using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace MiniGame.Objects
{
    internal class RedCircle : BaseObject
    {
        public int radius = 10;
        public RedCircle(float x, float y, float angle) : base(x, y, angle) 
        {
            changeColor = defaultColor = Color.FromArgb(120, 255, 0, 0);
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(changeColor), 0, 0, radius, radius);            
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0, 0, radius, radius);

            return path;
        }
    }
}
