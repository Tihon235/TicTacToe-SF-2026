using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_tac_toe
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
        }

        private void pic1_Click(object sender, EventArgs e)
        {
            PictureBox clickedBox = (PictureBox)sender;
            //Проверяем наличие изобрадение в PictorBox
            if (clickedBox.Image != null) return;
            //Проверяем ход и ставим соотвествующую картинку
            if (isCrossClick == true)
            {
                clickedBox.Image = Properties.Resources.cross_red;
                clickedBox.Tag = "X";
            }
            else {
                clickedBox.Image = Properties.Resources.circle_blue;
                clickedBox.Tag = "O";
            }
            //Меняем ход
            isCrossClick = !isCrossClick;
            if (CheckWinner()) return;
        }

        //Функция проверки победы
        private bool CheckWinner() {
            //Проверка горизонтали
            if (CheckLine(pic1, pic2, pic3)) return true;
            if (CheckLine(pic4, pic5, pic6)) return true;
            if (CheckLine(pic7, pic8, pic9)) return true;
            //Проверка вертикали
            if (CheckLine(pic1, pic4, pic7)) return true;
            if (CheckLine(pic2, pic5, pic8)) return true;
            if (CheckLine(pic3, pic6, pic9)) return true;
            //Проверка диагоналей
            if (CheckLine(pic1, pic5, pic9)) return true;
            if (CheckLine(pic3, pic5, pic7)) return true;
            //Проверка на ничью
            if (pic1.Image != null && pic2.Image != null && pic3.Image != null &&
                pic4.Image != null && pic5.Image != null && pic6.Image != null &&
                pic7.Image != null && pic8.Image != null && pic9.Image != null) 
            {
                MessageBox.Show("Ничья! Очки никому не начисляются.");
                ResetGame();
                return true;
            }
            else return false;
        }
        private bool CheckLine(PictureBox pb1, PictureBox pb2, PictureBox pb3) {
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
                labelScore.Text = "Крестики: " + scoreX + " | Нолики: " + scoreO;
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
        private void ResetSession() {
            scoreX = 0;
            scoreO = 0;
            labelScore.Text = "Крестики: 0 | Нолики: 0";
            ResetGame();
        }
        //Перезапуск игры
        private void ResetGame() {
            pic1.Image = null; pic1.Tag = null;
            pic2.Image = null; pic2.Tag = null;
            pic3.Image = null; pic3.Tag = null;
            pic4.Image = null; pic4.Tag = null;
            pic5.Image = null; pic5.Tag = null;
            pic6.Image = null; pic6.Tag = null;
            pic7.Image = null; pic7.Tag = null;
            pic8.Image = null; pic8.Tag = null;
            pic9.Image = null; pic9.Tag = null;
            //Обновляем флаг
            isCrossClick = true;
        }
    }
}
