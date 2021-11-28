using MiniGame.Objects;

namespace MiniGame
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);//¬ центре экрана

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] »грок пересекс€ с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            objects.Add(player);

            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));
            objects.Add(new MyRectangle(150, 150, 30));

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics; // вытащили объект графики из событи€

            g.Clear(Color.White);

            // мен€ю тут objects на objects.ToList()
            // это будет создавать копию списка
            // и позволит модифицировать оригинальный objects пр€мо из цикла foreach
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); // то есть игрок пересекс€ с объектом
                    obj.Overlap(player); // и объект пересекс€ с игроком

                    //txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] »грок пересекс€ с {obj}\n" + txtLog.Text;

                    // тут провер€ю что достиг маркера
                    if (obj == marker)
                    {
                        // если достиг, то удал€ю маркер из оригинального objects
                        objects.Remove(marker);
                        marker = null; // и обнул€ю маркер
                    }
                }

                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // тут добавл€ем проверку на marker не нулевой
            if (marker != null)
            {
                //¬ектор между игроком и маркером
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                //≈го длина
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                //пересчитываем координаты игрока
                player.X += dx * 2;
                player.Y += dy * 2;
            }

            //запрашиваем обновление pbMain
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
    }
}