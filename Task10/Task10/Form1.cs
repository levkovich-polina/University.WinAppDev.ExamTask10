using System.ComponentModel;
using Timer = System.Threading.Timer;

namespace Task10
{
    public partial class Form1 : Form
    {
        private class Person
        {
            public string Name { get; set; }
            public int Height { get; set; }
            public int Age { get; set; }
            public BloodType BloodType { get; set; }
        }
        private enum BloodType
        {
            A,
            B,
            AB,
        }

        Timer _timer;
        Random _random = new Random();
        private BindingList<Person> _persons = new BindingList<Person>();

        public Form1()
        {
            InitializeComponent();
            DataGridView.AutoGenerateColumns = true;
            DataGridView.DataSource = _persons;

        }
        private void StartFillingButton_Click(object sender, EventArgs e)
        {
            _persons.Clear();
            TimerCallback tm = new TimerCallback(OnTimerTicked);
            _timer = new Timer(tm, 0, 0, 500);
            DownloadButton.Enabled = false;
        }
        private void OnTimerTicked(object obj)
        {
            string name = "";
            int length = _random.Next(3, 11);
            Char[] letters = new Char[12] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l' };
            for (int i = 0; i < length; i++)
            {
                name += letters[_random.Next(0, 12)];
            }
            int height = _random.Next(120, 190);

            int age = _random.Next(5, 80);
            BloodType type;
            int bloodType = _random.Next(0, 3);
            if (bloodType == 0)
            {
                type = BloodType.A;
            }
            else if (bloodType == 1)
            {
                type = BloodType.B;
            }
            else
            {
                type = BloodType.AB;
            }

            Invoke(() =>
            {
                _persons.Add(new Person { Name = name, Height = height, Age = age, BloodType = type });

            });


        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DownloadButton.Enabled = true;

            if (_persons != null)
            {
                _timer.Dispose();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(sfd.FileName))
                    {
                        for (int i = 0; i < _persons.Count; i++)
                        {
                            writer.Write(_persons[i].Name);
                            writer.Write(";");
                            writer.Write(_persons[i].Age);
                            writer.Write(";");
                            writer.Write(_persons[i].Height);
                            writer.Write(";");
                            writer.Write(_persons[i].BloodType);
                            writer.WriteLine();
                        }
                    }
                }
                _persons.Clear();
            }
            else
            {
                MessageBox.Show("Таблица должна быть заполнена!");
            }

        }
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            string file;
            String str = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                string[] lines = File.ReadAllLines(file);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] personInfos = lines[i].Split(";");
                    for (int j = 0; j < 1; j++)
                    {
                        Invoke(() =>
                        {
                            _persons.Add(new Person
                            { 
                                Name = personInfos[j], 
                                Height = Convert.ToInt16(personInfos[j + 1]), 
                                Age = Convert.ToInt16(personInfos[j + 2]), 
                                BloodType = (BloodType)Enum.Parse(typeof(BloodType), (personInfos[j + 3]))
                            });
                        });
                    }
                }

            }
        }
    }
}