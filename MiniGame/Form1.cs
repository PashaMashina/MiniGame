using MiniGame.Objects;

namespace MiniGame
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        GreenCircle circle;
        GreenCircle circle2;
        Random rnd = new Random();
        int point = 0;
        

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);//В центре экрана

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            circle = new GreenCircle(rnd.Next() % (pbMain.Width - 35), rnd.Next() % (pbMain.Height - 35), 0);
            circle2 = new GreenCircle(rnd.Next() % (pbMain.Width - 35), rnd.Next() % (pbMain.Height - 35), 0);

            player.OnCircleOverlap += (c) =>
            {
                point++;
                txtPoint.Text = $"Очки: {point}";
                objects.Remove(c);
                if (circle == c)
                {
                    circle = new GreenCircle(rnd.Next() % (pbMain.Width - 35), rnd.Next() % (pbMain.Height - 35), 0);
                    objects.Add(circle); // и главное не забыть пололжить в objects
                }
                if (circle2 == c)
                {
                    circle2 = new GreenCircle(rnd.Next() % (pbMain.Width - 35), rnd.Next() % (pbMain.Height - 35), 0);
                    objects.Add(circle2); // и главное не забыть пололжить в objects
                }
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            objects.Add(player);


            //objects.Add(new MyRectangle(50, 50, 0));
            //objects.Add(new MyRectangle(100, 100, 45));
            //objects.Add(new MyRectangle(150, 150, 30));
            objects.Add(circle);
            objects.Add(circle2);

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            
            var g = e.Graphics; // вытащили объект графики из события

            g.Clear(Color.White);

            updatePlayer();

            // меняю тут objects на objects.ToList()
            // это будет создавать копию списка
            // и позволит модифицировать оригинальный objects прямо из цикла foreach
            // пересчитываем пересечения
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void updateCircle()
        {
            //timer = circle.Timer; 
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                // по сути мы теперь используем вектор dx, dy
                // как вектор ускорения, точнее даже вектор притяжения
                // который притягивает игрока к маркеру
                // 0.5 просто коэффициент который подобрал на глаз
                // и который дает естественное ощущение движения
                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                // расчитываем угол поворота игрока 
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            // тормозящий момент,
            // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиция игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // тут добавил создание маркера по клику если он еще не создан
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            circle.timer--;
            circle2.timer--;
            if (circle.timer <0)
            {
                objects.Remove(circle);
                circle = new GreenCircle(rnd.Next() % (pbMain.Width - 35), rnd.Next() % (pbMain.Height - 35), 0);
                objects.Add(circle);
            }
            if (circle2.timer < 0)
            {
                objects.Remove(circle2);
                circle2 = new GreenCircle(rnd.Next() % (pbMain.Width - 35), rnd.Next() % (pbMain.Height - 35), 0);
                objects.Add(circle2);
            }
        }
    }
}