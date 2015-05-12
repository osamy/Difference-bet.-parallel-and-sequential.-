using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
namespace pararllel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> list = new List<int>();
        List<int> list1 = new List<int>();
        int[] j = new int[2024];
        int[] y=new int[2024];
        public MainWindow()
        {
            InitializeComponent();
            seq.IsEnabled = false;
            par.IsEnabled = false;
        }

   public    static int[] oddEvenSort2( int []x) {

       int temp;
  var sorted = false;
  while(!sorted)
  {
    sorted = true;
   
      for(var i = 1; i < x.Length-1; i += 2)
    {
      if(x[i] > x[i+1])
      {
          temp = x[i];
          x[i] = x[i + 1];
          x[i + 1] = temp;
       
        sorted = false;
      }
    }
 
    for(var i = 0; i < x.Length-1; i += 2)
    {
      if(x[i] > x[i+1])
      {
          temp = x[i];
          x[i] = x[i + 1];
          x[i + 1] = temp;
        sorted = false;
      }
    }
  }
 
  return x;
}
   public static int[] p_oddEvenSort2(int[] x)
   {

       int temp;
       var sorted = false;
       while (!sorted)
       {
           sorted = true;
         
               Parallel.For (1,  x.Length - 1 ,i => 
               {
                   if (x[i] > x[i + 1])
                   {
                       temp = x[i];
                       x[i] = x[i + 1];
                       x[i + 1] = temp;

                       sorted = false;
                   }
               });
         
          
                  Parallel.For (0,  x.Length - 1 ,i => 
               {
                   if (x[i] > x[i + 1])
                   {
                       temp = x[i];
                       x[i] = x[i + 1];
                       x[i + 1] = temp;

                       sorted = false;
                   }
               });
         
          
       }
      
     
       return x;
   }



   public static void Swap<T>(IList<T> arr, int i, int j)
   {
       var tmp = arr[i];
       arr[i] = arr[j];
       arr[j] = tmp;
   }
   /// <summary>
   ///     Partitions an IList by defining a pivot and then comparing the other members to this pivot.
   /// </summary>
   /// <param name="arr">The IList to partition</param>
   /// <param name="low">The lowest index of the partition</param>
   /// <param name="high">The highest index of the partition</param>
   /// <returns>Returns the index of the chosen pivot</returns>
   private static int Partition(int[]  arr, int low, int high)
       
   {
       /*
           * Defining the pivot position, here the middle element is used but the choice of a pivot
           * is a rather complicated issue. Choosing the left element brings us to a worst-case performance,
           * which is quite a common case, that's why it's not used here.
           */
       var pivotPos = (high + low) / 2;
       var pivot = arr[pivotPos];
       // Putting the pivot on the left of the partition (lowest index) to simplify the loop
       Swap(arr, low, pivotPos);

       /** The pivot remains on the lowest index until the end of the loop
           * The left variable is here to keep track of the number of values
           * that were compared as "less than" the pivot.
           */
       var left = low;
       for (var i = low + 1; i <= high; i++)
       {
           // If the value is greater than the pivot value we continue to the next index.
           if (arr[i].CompareTo(pivot) >= 0) continue;

           // If the value is less than the pivot, we increment our left counter (one more element below the pivot)
           left++;
           // and finally we swap our element on our left index.
           Swap(arr, i, left);
       }

       // The pivot is still on the lowest index, we need to put it back after all values found to be "less than" the pivot.
       Swap(arr, low, left);

       // We return the new index of our pivot
       return left;
   }
   // <summary>
   ///     Realizes a Quicksort on an IList of IComparable items in a sequential way.
   /// </summary>
   /// <param name="arr">The IList of IComaparable to Quicksort</param>
   /// <param name="left">The minimum index of the IList to Quicksort</param>
   /// <param name="right">The maximum index of the IList to Quicksort</param>
   private static int [] Quicksort(int[] arr, int left, int right) 
   {


       // ...


       // If the list contains one or less element: no need to sort!
       if (right <= left) return arr;

       // Partitioning our list
       var pivot = Partition(arr, left, right);

       // Sorting the left of the pivot
       Quicksort(arr, left, pivot - 1);
       // Sorting the right of the pivot
       Quicksort(arr, pivot + 1, right);
      
       return arr;

   }
   /// <summary>
   ///     Realizes a Quicksort on an IList of IComparable items.
   ///     Left and right side of the pivot are processed in parallel.
   /// </summary>
   /// <param name="arr">The IList of IComaparable to Quicksort</param>
   /// <param name="left">The minimum index of the IList to Quicksort</param>
   /// <param name="right">The maximum index of the IList to Quicksort</param>
   private static int [] QuicksortParallel(int[] arr, int left, int right)
      
   {
       // Defining a minimum length to use parallelism, over which using parallelism
       // got better performance than the sequential version.
       const int threshold = 2048;

       // If the list to sort contains one or less element, the list is already sorted.
       if (right <= left) return arr;

       // If the size of the list is under the threshold, sequential version is used.
       if (right - left < threshold)
        return   Quicksort(arr, left, right);

       else
       {
           // Partitioning our list and getting a pivot.
           var pivot = Partition(arr, left, right);

           // Sorting the left and right of the pivot in parallel
           Parallel.Invoke(
               () => QuicksortParallel(arr, left, pivot - 1),
               () => QuicksortParallel(arr, pivot + 1, right));
       }
       return arr;
   }
   
private void Button_Click_1(object sender, RoutedEventArgs e)
{
    s_olist.Items.Clear();
    s_qlist.Items.Clear();
    Stopwatch sw = new Stopwatch();
    sw.Start();
   int[] result= oddEvenSort2(j);
    sw.Stop();
    label1.Content = sw.Elapsed;
    for (int i = 0; i < result.Length; i++) {
        s_olist.Items.Add(result[i]);
    }

    sw.Reset();

    sw.Start();
   int [] result1= Quicksort(j, 0, j.Length - 1);
    sw.Stop();
    s_qlabel.Content = sw.Elapsed;
    for (int i = 0; i < result.Length; i++)
    {
        s_qlist.Items.Add(result[i]);
    }

}

private void Button_Click_2(object sender, RoutedEventArgs e)
{
    p_olist.Items.Clear();
    p_qlist.Items.Clear();
    Stopwatch sw = new Stopwatch();
    sw.Start();
   int[] result=  p_oddEvenSort2(y);
    sw.Stop();
    label2.Content = sw.Elapsed;
    for (int i = 0; i < result.Length; i++)
    {
        p_olist.Items.Add(result[i]);
    }

    sw.Reset();
    sw.Start();
    int[] result1 = QuicksortParallel(y, 0, y.Length - 1);
    sw.Stop();
    p_qlabel.Content = sw.Elapsed;
    for (int i = 0; i < result.Length; i++)
    {
        p_qlist.Items.Add(result[i]);
    }
}

private void Button_Click_3(object sender, RoutedEventArgs e)
{
    seq.IsEnabled = true;
    par.IsEnabled = true;
    Random r = new Random();
     j = new int[int.Parse(num.Text)];
     y = new int[int.Parse(num.Text)];
   
    array.Items.Clear();
    for (int i = 0; i < j.Length; i++)
    {
        int x = r.Next(0, 5000);
        list1.Add(x);
        list.Add(x);
        j[i] = x;
        y[i] = x;
        array.Items.Add(x);
    }
}

    }
}
