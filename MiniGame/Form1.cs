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

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);//� ������ ������

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] ����� ��������� � {obj}\n" + txtLog.Text;
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
            var g = e.Graphics; // �������� ������ ������� �� �������

            g.Clear(Color.White);

            // ����� ��� objects �� objects.ToList()
            // ��� ����� ��������� ����� ������
            // � �������� �������������� ������������ objects ����� �� ����� foreach
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj); // �� ���� ����� ��������� � ��������
                    obj.Overlap(player); // � ������ ��������� � �������

                    //txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] ����� ��������� � {obj}\n" + txtLog.Text;

                    // ��� �������� ��� ������ �������
                    if (obj == marker)
                    {
                        // ���� ������, �� ������ ������ �� ������������� objects
                        objects.Remove(marker);
                        marker = null; // � ������� ������
                    }
                }

                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // ��� ��������� �������� �� marker �� �������
            if (marker != null)
            {
                //������ ����� ������� � ��������
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                //��� �����
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                //������������� ���������� ������
                player.X += dx * 2;
                player.Y += dy * 2;
            }

            //����������� ���������� pbMain
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // ��� ������� �������� ������� �� ����� ���� �� ��� �� ������
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // � ������� �� ������ ��������� � objects
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}