using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // добавил using
using System.Drawing.Drawing2D;

namespace MiniGame.Objects
{
    class BaseObject
    {
        public Action<Marker> OnMarkerOverlap;
        public Action<GreenCircle> OnRectangleOverlap;
        public float vX, vY;

        public float X;
        public float Y;
        public float Angle;
        public Color changeColor;
        public Color defaultColor;

        // добавил поле делегат, к которому можно будет привязать реакцию на собыития
        public Action<BaseObject, BaseObject> OnOverlap;

        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);

            return matrix;
        }

        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            // берем информацию о форме
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            // применяем к объектам матрицы трансформации
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            // используем класс Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            // пока возвращаем пустую форму
            return new GraphicsPath();
        }

        // добавил виртуальный метод для отрисовки
        public virtual void Render(Graphics g)
        {
            // тут пусто
        }

        public virtual void Overlap(BaseObject obj)
        {
            if (this.OnOverlap != null) //Если к полю есть привязанные функции 
            {
                this.OnOverlap(this, obj);//Вызываем их
            }
        }
    }
}
