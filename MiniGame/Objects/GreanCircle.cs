using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace MiniGame.Objects
{
    class GreenCircle : BaseObject
    {
        static Random rnd = new Random();
        public int radius = rnd.Next(40,100);
        public GreenCircle(float x, float y, float angle) : base(x, y, angle) {
            changeColor = defaultColor = Color.GreenYellow;
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
