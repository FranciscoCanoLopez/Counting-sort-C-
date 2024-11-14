using System.Diagnostics;

namespace Counting_sort
{
    public partial class Form1 : Form
    {
        int[] inputArray;
        int[] countArray;
        int[] outputArray;
        int maxIndex;
        int currentIndex;
        Stopwatch stopwatch;

        public Form1()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            // Configurar ComboBox con opciones de orden
            cBOrden.Items.Add("Ascendente");
            cBOrden.Items.Add("Descendente");
            cBOrden.SelectedIndex = 0; // Seleccionar "Ascendente" por defecto
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            // Leer y procesar los n�meros ingresados en el TextBox
            inputArray = txtInput.Text.Split(',')
                                   .Select(n => int.Parse(n.Trim()))
                                   .ToArray();

            // Inicializar los arreglos y variables para el proceso de Counting Sort
            outputArray = new int[inputArray.Length];
            int maxValue = inputArray.Max();
            countArray = new int[maxValue + 1];
            maxIndex = cBOrden.SelectedItem.ToString() == "Ascendente" ? 0 : countArray.Length - 1;
            currentIndex = 0;

            // Crear el arreglo de conteo
            foreach (int num in inputArray)
            {
                countArray[num]++;
            }

            // Limpiar el DataGridView y mostrar el arreglo desordenado inicialmente
            dgvOutput.Rows.Clear();
            dgvOutput.Rows.Add("Desordenado: " + string.Join(", ", inputArray));

            // Iniciar el temporizador para mostrar el ordenamiento paso a paso
            stopwatch.Restart(); // Iniciar el cron�metro
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            bool ascending = cBOrden.SelectedItem.ToString() == "Ascendente";

            // Condici�n para detener el temporizador seg�n el orden elegido
            if ((ascending && maxIndex >= countArray.Length) || (!ascending && maxIndex < 0))
            {
                timer.Stop();
                stopwatch.Stop(); // Detener el cron�metro

                // Mostrar el tiempo total de ordenamiento en el label con formato hh:mm:ss:fff
                TimeSpan elapsed = stopwatch.Elapsed;
                lblTimeElapsed.Text = $"Tiempo de ordenamiento: {elapsed:hh\\:mm\\:ss\\:fff}";
                MessageBox.Show($"El ordenamiento ha finalizado.\nTiempo total: {elapsed:hh\\:mm\\:ss\\:fff}", "Ordenamiento Completado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Si el valor en countArray[maxIndex] es mayor que cero, agregar al outputArray
            if (countArray[maxIndex] > 0)
            {
                outputArray[currentIndex++] = maxIndex;
                countArray[maxIndex]--;

                // Mostrar el estado actual del arreglo ordenado en una fila del DataGridView
                string currentState = string.Join(", ",
                    outputArray.Select(n => n == 0 ? "0" : n.ToString()));
                dgvOutput.Rows.Add("Ordenando: " + currentState);
            }
            else
            {
                // Avanzar al siguiente �ndice en el arreglo de conteo seg�n el orden elegido
                maxIndex += ascending ? 1 : -1;
            }
        }
    }
}
