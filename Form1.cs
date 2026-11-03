using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.Properties;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Флажок хода
        private bool isCrossClick = true;
        private int scoreX = 0;
        private int scoreO = 0;

        public Form1()
        {
            InitializeComponent();
            UpdateTurnLabel();
        }

        private void pictureBox1_Cliсk(object sender, EventArgs e)
        {
            PictureBox clickedBox = (PictureBox)sender;
            //Проверяем наличие изобрадение в PictorBox
            if (clickedBox.Image != null) return;
            //Проверяем ход и ставим соотвествующую картинку
            if (isCrossClick == true)
            {
                clickedBox.Image = Resources.cross_red;
                clickedBox.Tag = "X";

            }
            else
            {
                clickedBox.Image = Resources.circle_blue;
                clickedBox.Tag = "O";
            }
            //Меняем ход
            isCrossClick = !isCrossClick;
            UpdateTurnLabel();
            if (CheckWinner()) return;
        }

        //Функция обновления текста для хода
        private void UpdateTurnLabel()
        {
            if (isCrossClick)
            {
                label3.Text = "Ходят: Крестики";
            }
            else
            {
                label3.Text = "Ходят: Нолики";
            }
        }

        //Функция проверки победы
        private bool CheckWinner()
        {
            //Проверка горизонтали
            if (CheckLine(pictureBox1, pictureBox2, pictureBox3)) return true;
            if (CheckLine(pictureBox4, pictureBox5, pictureBox6)) return true;
            if (CheckLine(pictureBox7, pictureBox8, pictureBox9)) return true;
            //Проверка вертикали
            if (CheckLine(pictureBox1, pictureBox4, pictureBox7)) return true;
            if (CheckLine(pictureBox2, pictureBox5, pictureBox8)) return true;
            if (CheckLine(pictureBox3, pictureBox6, pictureBox9)) return true;
            //Проверка диагоналей
            if (CheckLine(pictureBox1, pictureBox5, pictureBox9)) return true;
            if (CheckLine(pictureBox3, pictureBox5, pictureBox7)) return true;
            //Проверка на ничью
            if (pictureBox1.Image != null && pictureBox2.Image != null && pictureBox3.Image != null &&
                pictureBox4.Image != null && pictureBox5.Image != null && pictureBox6.Image != null &&
                pictureBox7.Image != null && pictureBox8.Image != null && pictureBox9.Image != null)
            {
                MessageBox.Show("Ничья! Очки никому не начисляются.");
                ResetGame();
                return true;
            }
            else return false;
        }
        private bool CheckLine(PictureBox pb1, PictureBox pb2, PictureBox pb3)
        {
            //Проверка победы
            if (pb1.Tag != null && pb1.Tag == pb2.Tag && pb2.Tag == pb3.Tag)
            {
                //Определение победителя по тэгу
                string winner = (pb1.Tag.ToString() == "X") ? "крестики!" : "нолики!";
                //Работа статистики по матчу
                if (pb1.Tag.ToString() == "X")
                {
                    scoreX++;
                }
                else
                {
                    scoreO++;
                }
                label2.Text = "Крестики: " + scoreX + " | Нолики: " + scoreO;
                //Проверка победителя матча
                if (scoreX >= 3 || scoreO >= 3)
                {
                    string GlobalWinner = (scoreX >= 3) ? "Крестики" : "Нолики";
                    DialogResult globalResult = MessageBox.Show("МАТЧ ОКОНЧЕН!\nПобедили " +
                        GlobalWinner + " со счётом " + scoreX + ":" + scoreO + "!\nХотите начать новую сессию?",
                        "Глобальная победа", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (globalResult == DialogResult.Yes)
                    {
                        ResetSession();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                //Победа в раунде
                else
                {
                    DialogResult result = MessageBox.Show("Раунд выиграли " + winner +
                        "!\nСледующий раунд?", "Победа в раунде", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        ResetGame();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                return true;
            }
            return false;
        }
        //Сброс сессии
        private void ResetSession()
        {
            scoreX = 0;
            scoreO = 0;
            label2.Text = "Крестики: 0 | Нолики: 0";
            ResetGame();
        }
        //Перезапуск игры
        private void ResetGame()
        {
            pictureBox1.Image = null; pictureBox1.Tag = null;
            pictureBox2.Image = null; pictureBox2.Tag = null;
            pictureBox3.Image = null; pictureBox3.Tag = null;
            pictureBox4.Image = null; pictureBox4.Tag = null;
            pictureBox5.Image = null; pictureBox5.Tag = null;
            pictureBox6.Image = null; pictureBox6.Tag = null;
            pictureBox7.Image = null; pictureBox7.Tag = null;
            pictureBox8.Image = null; pictureBox8.Tag = null;
            pictureBox9.Image = null; pictureBox9.Tag = null;
            //Обновляем флаг
            isCrossClick = true;
            UpdateTurnLabel();
        }
    }
}
