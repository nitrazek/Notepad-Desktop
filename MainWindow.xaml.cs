using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotepadDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            note.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla vitae nisl eget elit tincidunt convallis. Duis ultricies, velit ac commodo ullamcorper, elit odio feugiat sapien, nec varius odio tortor vel tortor. Vestibulum auctor quam sit amet ante eleifend, eu commodo nulla tristique. Vivamus congue arcu non nisi suscipit, nec tempor justo volutpat. Integer euismod magna nec eros fringilla, sed rhoncus ipsum convallis. In hac habitasse platea dictumst. Pellentesque vel ante vel nunc cursus euismod. Vivamus vel nisi in purus luctus tempus. Sed vehicula ullamcorper enim, non varius magna fermentum non. Integer id libero eu elit mollis iaculis. Curabitur venenatis nisl eget augue blandit, nec cursus leo convallis. Nunc dignissim massa nec eleifend malesuada. Sed vitae magna vel risus feugiat vestibulum. Etiam nec mi in diam rutrum elementum.\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Integer gravida commodo purus, ac lacinia velit tincidunt vel. Cras sit amet justo ac lectus efficitur consectetur. Nam a turpis sed libero consequat ultrices. Nullam gravida, justo eget volutpat cursus, purus est convallis tortor, ac vestibulum odio lectus nec felis. Fusce sodales sapien eget purus aliquam tincidunt. Proin aliquet diam sit amet libero ullamcorper, nec dictum lorem tempus. Mauris in ipsum eu lorem commodo facilisis. Sed sit amet dui eleifend, ultricies dolor ac, fermentum ligula. Integer eu ipsum lacinia, blandit arcu nec, consectetur urna. Sed vulputate massa in nisi faucibus, eu dictum orci accumsan. Sed fermentum accumsan ex, nec congue arcu vulputate at. Phasellus aliquam semper velit eget interdum.";
            List<string> notes = new List<string>();
            notes.Add("Lista zakupów");
            notes.Add("Prace domowe");
            notesList.ItemsSource = notes;
        }
    }
}