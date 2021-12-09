using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace MiniGame.Objects
{
    class MyRectangle : BaseObject // наследуем BaseObject
    {
        public Action<BaseObject> OnRectangleOverlap;

        // создаем конструктор с тем же набором параметров что и в BaseObject
        // base(x, y, angle) -- вызывает конструктор родительского класса
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        // переопределяем Render
        public override void Render(Graphics g)
        {
            // и запихиваем туда код из формы
            g.FillRectangle(new SolidBrush(Color.Black), -25, -15, 300, 450);
            g.DrawRectangle(new Pen(Color.Black, 2), -25, -15, 300, 450);

        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-25, -15, 300, 450));

            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            OnRectangleOverlap(obj);
        }

    }
}

