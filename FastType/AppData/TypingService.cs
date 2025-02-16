using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastType.AppData
{
    public class TypingService
    {
        private string _textToType;
        private Stopwatch _timer;
        private int _correctChars;
        private int _totalChars;
        private int _currentCharIndex;

        private Grid _keyboardGrid;
        private TextBox _typingTextBox;
        private TextBlock _typingTutorTextBlock;
        private TextBlock _accuracyTextBlock;
        private TextBlock _speedTextBlock;
        private ProgressBar _progressBar;

        public double Speed { get; private set; }
        public double Accuracy { get; private set; }
        public double Progress { get; private set; }

        public TypingService(Grid keyboardGrid, TextBox typingTextBox, TextBlock typingTutorTextBlock, TextBlock accuracyTextBlock, TextBlock speedTextBlock, ProgressBar progressBar)
        {
            _timer = new Stopwatch();

            _keyboardGrid = keyboardGrid;
            _typingTextBox = typingTextBox;
            _typingTutorTextBlock = typingTutorTextBlock;
            _totalChars = _typingTutorTextBlock.Text.Length;
            _accuracyTextBlock = accuracyTextBlock;
            _speedTextBlock = speedTextBlock;
            _progressBar = progressBar;

            // Добавляем обработчики событий
            _typingTextBox.PreviewKeyDown += TypingTextBox_PreviewKeyDown;
            _typingTextBox.PreviewKeyUp += TypingTextBox_PreviewKeyUp;
            _typingTextBox.TextChanged += TypingTextBox_TextChanged;
        }

        private void TypingTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_typingTextBox.Text.Length >= 1 && !_timer.IsRunning)
            {
                _timer.Start();
            }

            if (_typingTutorTextBlock.Text == _typingTextBox.Text)
            {
                _timer.Stop();
            }

            if (_typingTextBox.Text.Length > 2)
            {
                CalculateSpeed();
                CalculateAccuracy();
            }

            UpdateProgress();
        }

        private void TypingTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var button = FindButtonByKey(e.Key);
            if (button != null)
            {
                button.BorderThickness = new Thickness(0.0);
            }
        }

        private void TypingTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var button = FindButtonByKey(e.Key);
            if (button != null)
            {
                button.BorderThickness = new Thickness(2.0, 2.0, 2.0, 4.0);
            }
        }

        private Button FindButtonByKey(Key key)
        {
            foreach (var keysPanel in _keyboardGrid.Children.OfType<StackPanel>())
            {
                foreach (var button in keysPanel.Children.OfType<Button>())
                {
                    if (button.Tag.ToString() == key.ToString())
                    {
                        return button;
                    }
                }
            }
            return null;
        }
        private void UpdateProgress()
        {
            var currentLength = _typingTextBox.Text.Length;
            Progress = Math.Floor((double)currentLength / _totalChars * 100);
            _progressBar.Value = Progress;
        }
        private void CalculateSpeed()
        {
            Speed = _typingTextBox.Text.Length / _timer.Elapsed.TotalSeconds * 60;
            _speedTextBlock.Text = $"{Speed:F0} СВМ";
        }
        private void CalculateAccuracy()
        {

        }
    }
}
