using System.Text;

namespace TicTacToeLib;

public class Field
{
    public string[] _grid = { "1", "2", "3", "4", "5", "6" , "7", "8", "9"};
    
    public Field() { }

    /// <summary>
    /// Метод позволяет совершить ход. Если выбрана уже занятая ячейка, оповещает об этом.
    /// </summary>
    /// <param name="position">желаемое поле для хода</param>
    /// <param name="symbol">символ</param>
    /// <returns>Тrue, если ход был совершен, и false в противном случае.</returns>
    public bool Move(int position, string symbol)
    {
        bool flag = true;
        if (_grid[position - 1] != "X" && _grid[position - 1] != "0")
        {
            _grid[position - 1] = symbol;
        }
        else
        {
            Console.WriteLine("This cell is already filled. Try another one");
            flag = false;
        }

        return flag;
    }

    /// <summary>
    /// Метод для совершения хода человеком.
    /// </summary>
    /// <param name="symbol">символ</param>
    /// <param name="i">значение параметра внешнего цикла</param>
    public void Game(string symbol, ref int i)
    {
        Console.WriteLine($"{symbol} turn. Please choose an available position\n");
        if (int.TryParse(Console.ReadLine(), out int x))
        {
            if (x >= 1 && x <= 9)
            {
                if (!Move(x, symbol))
                {
                    i--;
                };
            }
            else
            {
                Console.WriteLine("Incorrect position. Try again.\n");
                i--;
            }
        }
        else
        {
            Console.WriteLine("Really incorrect position. Try again.\n");
            i--;
        }
    }

    /// <summary>
    /// Метод проверяет, нет ли на поле выигрышной последовательности.
    /// </summary>
    /// <returns>Возвращает победивший символ, или пробел в случае ничьей.</returns>
    public char Check()
    {
        char victory = ' ';
        string concat = String.Join("", _grid);
        
        // Строки
        if (concat[0] == concat[1] && concat[1] == concat[2])
        {
            if (concat[0] == 'X') victory = 'X';
            else if (concat[0] == '0') victory = '0';
        }
        
        else if (concat[3] == concat[4] && concat[4] == concat[5])
        {
            if (concat[3] == 'X') victory = 'X';
            else if (concat[3] == '0') victory = '0';
        }
        
        else if (concat[6] == concat[7] && concat[7] == concat[8])
        {
            if (concat[6] == 'X') victory = 'X';
            else if (concat[6] == '0') victory = '0';
        }
        
        // Столбцы
        else if (concat[0] == concat[3] && concat[3] == concat[6])
        {
            if (concat[0] == 'X') victory = 'X';
            else if (concat[0] == '0') victory = '0';
        }
        
        else if (concat[1] == concat[4] && concat[4] == concat[7])
        {
            if (concat[1] == 'X') victory = 'X';
            else if (concat[1] == '0') victory = '0';
        }
        
        else if (concat[2] == concat[5] && concat[5] == concat[8])
        {
            if (concat[2] == 'X') victory = 'X';
            else if (concat[2] == '0') victory = '0';
        }
        
        // Диагонали
        else if (concat[0] == concat[4] && concat[4] == concat[8])
        {
            if (concat[0] == 'X') victory = 'X';
            else if (concat[0] == '0') victory = '0';
        }
        
        else if (concat[2] == concat[4] && concat[4] == concat[6])
        {
            if (concat[2] == 'X') victory = 'X';
            else if (concat[2] == '0') victory = '0';
        }

        return victory;

    }

    /// <summary>
    /// Метод для игры с компьютером. Совершает первый ход.
    /// Вынесен в отедльный метод, так как тактика первого хода отличается.
    /// Компьютер всегда стремится занять наиболее выгодную позицию в центре поля.
    /// Если она уже занята, что занимаем левый верхний угол.
    /// </summary>
    public void FirstMove(string symb)
    {
        if (_grid[4] != "X" && _grid[4] != "0")
        {
            _grid[4] = symb;
        }
        else _grid[0] = symb;
    }

    /// <summary>
    ///  Метод для игры с компьютером. Подбирает наиболее выгодный компьютеру ход.
    /// Состоит из нескольких частей, обрабатывающих разные ситуации на поле.
    /// </summary>
    public void ComputerMove(string symbol)
    {
        int start, end;
        string symb = symbol;
        string[] meow =
        {
            $"1{symb}{symb}", $"{symb}2{symb}", $"{symb}{symb}3", $"4{symb}{symb}", $"{symb}5{symb}", $"{symb}{symb}6",
            $"7{symb}{symb}", $"{symb}8{symb}", $"{symb}{symb}9", $"2{symb}{symb}", $"3{symb}{symb}", $"{symb}4{symb}",
            $"{symb}6{symb}", $"{symb}{symb}7", $"{symb}{symb}8"
        };

        Dictionary<int, int> trash = new Dictionary<int, int>
        {
            { 2, 8 },
            { 4, 8 },
            { 1, 3 },
            { 3, 1 }
        };

        // Attack. Если есть возможность победить в один ход, компьютер это сделает.
        foreach (var element in trash)
        {

            if (element.Key == 2) start = 2;
            else start = 0;

            if (element.Key == 3) end = 3;
            else end = 8;

            for (int i = start; i < end; i += element.Value)
            {
                if (meow.Contains(_grid[i] + _grid[i + element.Key] + _grid[i + 2 * element.Key]))
                {
                    _grid[i] = _grid[i + element.Key] = _grid[i + 2 * element.Key] = symb;
                    return;
                }
            }
        }

        /* Defend. Если соперника от победы отделяет один ход, компьютер сделает такой, 
        что бы противник не победил. */
        if (symbol == "0") symb = "X";
        else symb = "0";

        foreach (var element in trash)
        {

            if (element.Key == 2) 
                start = 2;
            else 
                start = 0;

            end = element.Key == 3 ? 3 : 8;

            for (int i = start; i < end; i += element.Value)
            {
                if (meow.Contains(_grid[i] + _grid[i + element.Key] + _grid[i + 2 * element.Key]))
                {
                    if (_grid[i] != symb)
                    {
                        _grid[i] = symbol;
                        return;
                    }

                    if (_grid[i + element.Key] != symb)
                    {
                        _grid[i + element.Key] = symbol;
                        return;
                    }

                    if (_grid[i + 2 * element.Key] != symb)
                    {
                        _grid[i + 2 * element.Key] = symbol;
                        return;
                    }
                }
            }
        }

        /* Freestyle - вольный ход. Если нет возможности победить за один ход, 
        компьютер пытается выстроить стратегию. */
        meow = new[]
        {
            $"12{symbol}", $"1{symbol}3", $"{symbol}23", $"45{symbol}", $"{symbol}56", $"4{symbol}6",
            $"78{symbol}", $"{symbol}89", $"7{symbol}9", $"25{symbol}", $"2{symbol}8", $"{symbol}58",
            $"3{symbol}9", $"36{symbol}", $"{symbol}69", $"14{symbol}", $"{symbol}47", $"1{symbol}7",
            $"1{symbol}9", $"{symbol}59", $"15{symbol}", $"35{symbol}", $"3{symbol}7", $"{symbol}57"
        };

        foreach (var element in trash)
        {

            if (element.Key == 2) start = 2;
            else start = 0;

            if (element.Key == 3) end = 3;
            else end = 8;

            for (int i = start; i < end; i += element.Value)
            {
                if (meow.Contains(_grid[i] + _grid[i + element.Key] + _grid[i + 2 * element.Key]))
                {
                    if (_grid[i] != symbol && _grid[i] != symb)
                    {
                        _grid[i] = symbol;
                        return;
                    }

                    if (_grid[i + element.Key] != symbol && _grid[i] != symb)
                    {
                        _grid[i + element.Key] = symbol;
                        return;
                    }

                    if (_grid[i + 2 * element.Key] != symbol && _grid[i] != symb)
                    {
                        _grid[i + 2 * element.Key] = symbol;
                        return;
                    }
                }
            }
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("\n");
        int counter = 0;
        foreach (var row in _grid)
        {
            sb.Append($"{row}     ");
            counter += 1;
            if (counter % 3 == 0 && counter != 9)
            {
                sb.Append("\n\n");
            }
        }

        return sb.ToString();
    }
}