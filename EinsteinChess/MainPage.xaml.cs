using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace EinsteinChess
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        //Cell => Cells obj
        private ObservableCollection<Cell> Cells { get; set; }
            = new ObservableCollection<Cell>();

        public event PropertyChangedEventHandler PropertyChanged;

        private string ChessManToVisibility(bool isChessMan) => isChessMan ? "Visible" : "Collapsed";

        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i < 25; ++i)
            {
                Cells.Add(new Cell() { FillColor = "White", ChessManNo = 0, IsChessMan = false, IsVisibility= "Collapsed" });
            }

            int[,] test = {
                {1,2,3,0,0 },
                {4,5,0,0,0 },
                {6,0,0,0,-1 },
                {0,0,0,-2,-3 },
                {0,0,-4,-5,-6 } };
            UpdateCells(test);
        }

        

        private void UpdateCells(int[,] chessBoard)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {

                    if (chessBoard[x, y] < 0)
                    {

                        Cells[(x * 5) + y].FillColor = "Red";
                        Cells[(x * 5) + y].ChessManNo = Math.Abs(chessBoard[x, y]);
                        Cells[(x * 5) + y].IsChessMan = true;
                    }
                    else if (chessBoard[x, y] > 0)
                    {
                        Cells[(x * 5) + y].FillColor = "Blue";
                        Cells[(x * 5) + y].ChessManNo = Math.Abs(chessBoard[x, y]);
                        Cells[(x * 5) + y].IsChessMan = true;
                    }
                    else
                    {
                        Cells[(x * 5) + y].FillColor = "White";
                        Cells[(x * 5) + y].ChessManNo = 0;
                        Cells[(x * 5) + y].IsChessMan = false;
                    }
                    Cells[(x * 5) + y].IsVisibility = ChessManToVisibility(Cells[(x * 5) + y].IsChessMan);
                }
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Cells)));
        }
    }
}
