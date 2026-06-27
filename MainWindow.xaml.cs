using MySql.Data.MySqlClient;
using part.Database;
using part.Models;
using System;
using System.Collections.Generic;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace part
{
    public partial class MainWindow : Window
    {
        Random random = new Random();

        int quizScore = 0;
        int currentQuestion = 0;
        int taskCount = 0;
        int completed = 0;
        int securityScore = 100;

        string userName = "";
        bool waitingForName = true;

        List<CyberTask> tasks = new List<CyberTask>();
        List<string> activityLog = new List<string>();

        string[] questions =
        {
            "Which password is strongest?",
            "What should you do if you receive a suspicious email?",
            "What does HTTPS indicate?",
            "Should you share your OTP with anyone?",
            "Which is an example of phishing?",
            "Why should you update software?",
            "What is Multi-Factor Authentication (MFA)?",
            "Which password is weakest?",
            "What should you do before clicking a link?",
            "Why should public Wi-Fi be used carefully?"
        };

        string[,] options =
        {
            { "password123", "P@ssw0rd!2024", "qwerty", "123456" },
            { "Click the link", "Report and delete", "Reply", "Forward it" },
            { "HTTP with extra security", "A browser", "A scam", "Wi-Fi" },
            { "No, never", "Only friends", "Yes", "Sometimes" },
            { "Official email", "School email", "Urgent email from unknown sender", "Newsletter" },
            { "To slow your PC", "To fix bugs and security issues", "To waste data", "No reason" },
            { "Two passwords", "Using two methods to verify identity", "Two computers", "Two accounts" },
            { "P@ssw0rd2024", "Summer@2026", "Football#88", "password" },
            { "Click immediately", "Hover and inspect the link", "Ignore forever", "Forward it" },
            { "Hackers can intercept data", "It is always safe", "It is encrypted forever", "Nothing happens" }
        };

        int[] answers = { 1, 1, 0, 0, 2, 1, 1, 3, 1, 0 };

        List<string> passwordTips = new List<string>()
        {
            "Use strong passwords with symbols and numbers.",
            "Never reuse the same password everywhere.",
            "Use a password manager whenever possible."
        };

        List<string> phishingTips = new List<string>()
        {
            "Never click suspicious email links.",
            "Always verify the sender before responding.",
            "Phishing emails often create urgency."
        };

        public MainWindow()
        {
            InitializeComponent();

            TestDatabaseConnection();

            PlayGreeting();

            btnSend.Click += BtnSend_Click;
            btnPassword.Click += BtnPassword_Click;
            btnPhishing.Click += BtnPhishing_Click;
            btnBrowsing.Click += BtnBrowsing_Click;
            btnHelp.Click += BtnHelp_Click;
            btnClear.Click += BtnClear_Click;

            btnAddTask.Click += BtnAddTask_Click;
            btnCompleteTask.Click += BtnCompleteTask_Click;
            btnDeleteTask.Click += BtnDeleteTask_Click;

            btnOption1.Click += QuizAnswer_Click;
            btnOption2.Click += QuizAnswer_Click;
            btnOption3.Click += QuizAnswer_Click;
            btnOption4.Click += QuizAnswer_Click;

            LoadQuestion();
        }

        private void TestDatabaseConnection()
        {  
            try
            {
                MessageBox.Show(part.Database.Database.ConnectionString);
                 
                using (MySqlConnection con =
                    new MySqlConnection(part.Database.Database.ConnectionString))
                {
                    con.Open();
                    MessageBox.Show("Connected successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());  

            }
        }

        private void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("audio.wav");
                player.Play();
            }
            catch
            {
            }
        }

        private void LogActivity(string activity)
        {
            string entry =
                DateTime.Now.ToShortTimeString()
                + " - "
                + activity;

            activityLog.Add(entry);

            lstActivity.Items.Add(entry);
        }

        private void AddMessage(string sender, string message, Brush colour, bool isUser)
        {
            Border bubble = new Border();

            bubble.Background = colour;
            bubble.CornerRadius = new CornerRadius(12);
            bubble.Padding = new Thickness(12);
            bubble.Margin = new Thickness(5);
            bubble.MaxWidth = 420;
            bubble.HorizontalAlignment =
                isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left;

            TextBlock txt = new TextBlock();

            txt.Text = sender + ": " + message;
            txt.Foreground = Brushes.White;
            txt.FontSize = 15;
            txt.TextWrapping = TextWrapping.Wrap;

            bubble.Child = txt;

            ChatPanel.Children.Add(bubble);

            ChatScrollViewer.ScrollToEnd();
        }

        private string GetBotResponse(string input)
        {
            input = input.ToLower();

            if (waitingForName)
            {
                userName = input;

                lblUser.Text = "👤 Welcome, " + userName;

                waitingForName = false;

                return "Nice to meet you, " + userName +
                       "! I'm ready to help you stay safe online.";
            }

            if (input.Contains("password"))
                return passwordTips[random.Next(passwordTips.Count)];

            if (input.Contains("phishing"))
                return phishingTips[random.Next(phishingTips.Count)];

            if (input.Contains("browse"))
                return "Always check for HTTPS before entering personal information.";

            if (input.Contains("wifi"))
                return "Avoid logging into banking or important accounts on public Wi-Fi.";

            if (input.Contains("hello") || input.Contains("hi"))
                return "Hello " + userName + "! 😊";

            if (input.Contains("thank"))
                return "You're welcome! Stay cyber safe!";

            if (input.Contains("sad") || input.Contains("stress"))
                return "Take a break, breathe, and remember cybersecurity is learned one step at a time.";

            return "I can help with passwords, phishing, malware, safe browsing and cybersecurity awareness.";
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserInput.Text))
                return;

            string input = txtUserInput.Text;

            AddMessage("You", input, Brushes.DeepSkyBlue, true);

            AddMessage("LockWise AI", "Typing...", Brushes.Gray, false);

            await Task.Delay(1000);

            ChatPanel.Children.RemoveAt(ChatPanel.Children.Count - 1);

            AddMessage("LockWise AI",
                GetBotResponse(input),
                Brushes.MediumPurple,
                false);

            txtUserInput.Clear();
        }

        private async void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSend_Click(sender, e);
            }
        }

        private void BtnPassword_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                passwordTips[random.Next(passwordTips.Count)],
                Brushes.MediumPurple,
                false);
        }

        private void BtnPhishing_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                phishingTips[random.Next(phishingTips.Count)],
                Brushes.MediumPurple,
                false);
        }

        private void BtnBrowsing_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                "Safe browsing means checking links, avoiding suspicious websites and using HTTPS.",
                Brushes.MediumPurple,
                false);
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            AddMessage("LockWise AI",
                "Ask me about passwords, phishing, malware, safe browsing or take the cybersecurity quiz.",
                Brushes.MediumPurple,
                false);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ChatPanel.Children.Clear();

            AddMessage("LockWise AI",
                "Chat cleared. How can I help you?",
                Brushes.MediumPurple,
                false);
        }

        // ===========================
        // TASK MANAGEMENT
        // ===========================

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTask.Text))
            {
                MessageBox.Show("Please enter a task.");
                return;
            }

            CyberTask task = new CyberTask()
            {
                Title = txtTask.Text.Trim(),
                Description = "Cybersecurity Task",
                ReminderDate = null,
                Completed = false
            };

            try
            {
                TaskRepository repo = new TaskRepository();
                repo.AddTask(task);
            }
            catch
            {
                // App still works even if database isn't available.
            }

            tasks.Add(task);

            lstTasks.Items.Add(task.Title);

            taskCount++;

            lblTasks.Text = "📋 Tasks: " + taskCount;

            LogActivity("Added task: " + task.Title);

            txtTask.Clear();

            MessageBox.Show("Task added successfully!");
        }

        private void BtnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task.");
                return;
            }

            CyberTask task = tasks[lstTasks.SelectedIndex];

            if (task.Completed)
            {
                MessageBox.Show("Task already completed.");
                return;
            }

            task.Completed = true;

            lstTasks.Items[lstTasks.SelectedIndex] =
                "✔ " + task.Title;

            completed++;

            securityScore += 10;

            lblCompleted.Text =
                "✅ Completed: " + completed;

            lblScore.Text =
                "🛡 Score: " + securityScore;

            LogActivity("Completed task: " + task.Title);
        }

        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task.");
                return;
            }

            int index = lstTasks.SelectedIndex;

            LogActivity("Deleted task: " + tasks[index].Title);

            tasks.RemoveAt(index);

            lstTasks.Items.RemoveAt(index);

            taskCount--;

            lblTasks.Text =
                "📋 Tasks: " + taskCount;

            MessageBox.Show("Task deleted.");
        }

        // ===========================
        // QUIZ
        // ===========================

        private void LoadQuestion()
        {
            if (currentQuestion >= questions.Length)
            {
                txtQuestion.Text =
                    "🎉 Quiz Completed!\n\nFinal Score: "
                    + quizScore +
                    " / "
                    + questions.Length;

                btnOption1.IsEnabled = false;
                btnOption2.IsEnabled = false;
                btnOption3.IsEnabled = false;
                btnOption4.IsEnabled = false;

                return;
            }

            txtQuestion.Text = questions[currentQuestion];

            btnOption1.Content = options[currentQuestion, 0];
            btnOption2.Content = options[currentQuestion, 1];
            btnOption3.Content = options[currentQuestion, 2];
            btnOption4.Content = options[currentQuestion, 3];
        }

        private void QuizAnswer_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;

            int chosen = 0;

            if (clicked == btnOption1)
                chosen = 0;
            else if (clicked == btnOption2)
                chosen = 1;
            else if (clicked == btnOption3)
                chosen = 2;
            else
                chosen = 3;

            if (chosen == answers[currentQuestion])
            {
                quizScore++;

                MessageBox.Show("✅ Correct!");

                securityScore += 5;

                lblScore.Text =
                    "🛡 Score: " + securityScore;
            }
            else
            {
                MessageBox.Show("❌ Incorrect!");
            }

            lblQuiz.Text =
                "🎯 Quiz: "
                + quizScore
                + "/"
                + questions.Length;

            lblQuizScore.Text =
                "Score : "
                + quizScore;

            currentQuestion++;

            LoadQuestion();
        }

    }
}