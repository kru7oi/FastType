using FastType.AppData;
using System.Windows.Controls;

namespace FastType.View.Pages
{
    /// <summary>
    /// Interaction logic for TypingTutorPage.xaml
    /// </summary>
    public partial class TypingTutorPage : Page
    {
        private TypingService _typingService;

        public TypingTutorPage()
        {
            InitializeComponent();

            _typingService = new TypingService(KeyboardGrid, TypingTutorTb, TypingTutorTbl, AccuracyTbl, SpeedTbl, TypingProgressPb);
        }
    }
}
