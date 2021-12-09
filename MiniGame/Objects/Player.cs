using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // добавил using
using System.Drawing.Drawing2D;

namespace MiniGame.Objects
{
    class Player : BaseObject
    {
        public Action<Marker> OnMarkerOverlap;
        public Action<GreenCircle> OnCircleOverlap;
        public Action<RedCircle> OnRedOverlap;
        public Player(float x, float y, float angle) : base(x, y, angle)
        {
            changeColor = defaultColor = Color.DeepSkyBlue;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(changeColor), -15, -15, 30, 30);//Кружочек с синим фоном
            g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);//Рамка кружочка
            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);//Палочка - направление игрока

        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);

            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
            if (obj is GreenCircle)
            {
                OnCircleOverlap(obj as GreenCircle);
            }
            if (obj is RedCircle)
            {
                OnRedOverlap(obj as RedCircle);
            }
        }
    }
}
