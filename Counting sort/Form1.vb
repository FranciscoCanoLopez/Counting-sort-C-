Imports System.Diagnostics

Namespace Counting_sort
    Public Partial Class Form1
        Inherits System.Windows.Forms.Form
        Private inputArray As Integer()
        Private countArray As Integer()
        Private outputArray As Integer()
        Private maxIndex As Integer
        Private currentIndex As Integer
        Private stopwatch As Stopwatch

        Public Sub New()
            Me.InitializeComponent()
            stopwatch = New Stopwatch()

            ' Configurar ComboBox con opciones de orden
            Me.cBOrden.Items.Add("Ascendente")
            Me.cBOrden.Items.Add("Descendente")
            Me.cBOrden.SelectedIndex = 0 ' Seleccionar "Ascendente" por defecto
        End Sub

        Private Sub btnSort_Click(sender As Object, e As EventArgs)
            ' Leer y procesar los números ingresados en el TextBox
            inputArray = Me.txtInput.Text.Split(","c).[Select](Function(n) Integer.Parse(n.Trim())).ToArray()

            ' Inicializar los arreglos y variables para el proceso de Counting Sort
            outputArray = New Integer(inputArray.Length - 1) {}
            Dim maxValue As Integer = inputArray.Max()
            countArray = New Integer(maxValue + 1 - 1) {}
            maxIndex = If(Equals(Me.cBOrden.SelectedItem.ToString(), "Ascendente"), 0, countArray.Length - 1)
            currentIndex = 0

            ' Crear el arreglo de conteo
            For Each num In inputArray
                countArray(num) += 1
            Next

            ' Limpiar el DataGridView y mostrar el arreglo desordenado inicialmente
            Me.dgvOutput.Rows.Clear()
            Me.dgvOutput.Rows.Add("Desordenado: " & String.Join(", ", inputArray))

            ' Iniciar el temporizador para mostrar el ordenamiento paso a paso
            stopwatch.Restart() ' Iniciar el cronómetro
            Me.timer.Start()
        End Sub

        Private Sub timer_Tick(sender As Object, e As EventArgs)
            Dim ascending As Boolean = Equals(Me.cBOrden.SelectedItem.ToString(), "Ascendente")

            ' Condición para detener el temporizador según el orden elegido
            If ascending AndAlso maxIndex >= countArray.Length OrElse Not ascending AndAlso maxIndex < 0 Then
                Me.timer.Stop()
                stopwatch.Stop() ' Detener el cronómetro

                ' Mostrar el tiempo total de ordenamiento en el label con formato hh:mm:ss:fff
                Dim elapsed = stopwatch.Elapsed
                Me.lblTimeElapsed.Text = $"Tiempo de ordenamiento: {elapsed:hh\:mm\:ss\:fff}"
                System.Windows.Forms.MessageBox.Show($"El ordenamiento ha finalizado.
Tiempo total: {elapsed:hh\:mm\:ss\:fff}", "Ordenamiento Completado", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information)
                Return
            End If

            ' Si el valor en countArray[maxIndex] es mayor que cero, agregar al outputArray
            If countArray(maxIndex) > 0 Then
                outputArray(Math.Min(Threading.Interlocked.Increment(currentIndex), currentIndex - 1)) = maxIndex
                countArray(maxIndex) -= 1

                ' Mostrar el estado actual del arreglo ordenado en una fila del DataGridView
                Dim currentState As String = String.Join(", ", outputArray.[Select](Function(n) If(n = 0, "0", n.ToString())))
                Me.dgvOutput.Rows.Add("Ordenando: " & currentState)
            Else
                ' Avanzar al siguiente índice en el arreglo de conteo según el orden elegido
                maxIndex += If(ascending, 1, -1)
            End If
        End Sub
    End Class
End Namespace
