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
        public int timer = 99;
        public int radius = rnd.Next(30,90);
        public GreenCircle(float x, float y, float angle) : base(x, y, angle) { }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.GreenYellow), 0, 0, radius, radius);
            g.DrawString(
                $"{radius}",
                new Font("Verdana", 8), // шрифт и размер
                new SolidBrush(Color.Red), // цвет шрифта
                radius-radius/2, radius - radius / 2 // точка в которой нарисовать текст
);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0, 0, radius, radius);

            return path;
        }
    }
}
