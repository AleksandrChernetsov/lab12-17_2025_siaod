using System.Diagnostics;

namespace lab12_17_2025_siaod
{
    public partial class Form1 : Form
    {
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 7;      // число строк 
            dataGridView1.ColumnCount = 6;  // число столбцов

            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "Шелла";
            dataGridView1.Rows[4].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";

            dataGridView1.Rows[0].Cells[0].Value = true;
            dataGridView1.Rows[1].Cells[0].Value = true;
            dataGridView1.Rows[2].Cells[0].Value = true;
        }

        // Функция проверки упорядоченности массива по возрастанию
        private bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }
            return true;
        }

        // Сортировка прямым обменом (пузырьковая)
        private (int comparisons, int swaps, long time) BubbleSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            bool swapped;
            for (int i = 0; i < array.Length - 1; i++)
            {
                swapped = false;
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    comparisons++;
                    if (array[j] > array[j + 1])
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        swaps++;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
            stopwatch.Stop();

            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        // Сортировка прямым выбором
        private (int comparisons, int swaps, long time) SelectionSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    comparisons++;
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }
                (array[i], array[minIndex]) = (array[minIndex], array[i]);
                swaps++;
            }
            stopwatch.Stop();

            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        // Сортировка прямым включением (сортировка вставками с минимальным барьером)
        private (int comparisons, int swaps, long time) InsertionSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            int minIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                comparisons++;
                if (array[i] < array[minIndex])
                {
                    minIndex = i;
                }
            }

            (array[0], array[minIndex]) = (array[minIndex], array[0]);
            swaps++;

            for (int i = 2; i < array.Length; i++)
            {
                int barrier = array[i];
                int j = i - 1;

                while (array[j] > barrier)
                {
                    comparisons++;
                    array[j + 1] = array[j]; 
                    swaps++;
                    j--;
                }
                comparisons++;

                array[j + 1] = barrier; 
                swaps++;
            }

            stopwatch.Stop();

            return (comparisons, swaps, stopwatch.ElapsedMilliseconds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = (int)numericUpDown1.Value;
            int[] originalArray = new int[size];
            for (int i = 0; i < size; i++)
                originalArray[i] = random.Next(size);

            if ((bool)dataGridView1.Rows[0].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = BubbleSort(sortingArray);

                dataGridView1.Rows[0].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[0].Cells[3].Value = result.swaps;
                dataGridView1.Rows[0].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[0].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[1].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = SelectionSort(sortingArray);

                dataGridView1.Rows[1].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[1].Cells[3].Value = result.swaps;
                dataGridView1.Rows[1].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[1].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }

            if ((bool)dataGridView1.Rows[2].Cells[0].Value)
            {
                int[] sortingArray = (int[])originalArray.Clone();
                var result = InsertionSort(sortingArray);

                dataGridView1.Rows[2].Cells[2].Value = result.comparisons;
                dataGridView1.Rows[2].Cells[3].Value = result.swaps;
                dataGridView1.Rows[2].Cells[4].Value = result.time + " мс";
                dataGridView1.Rows[2].Cells[5].Value = IsSorted(sortingArray) ? "Да" : "Нет";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}