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
        public GreenCircle(float x, float y, float angle) : base(x, y, angle) { }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.GreenYellow), 0, 0, 30, 30);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0, 0, 30, 30);

            return path;
        }
    }
}
