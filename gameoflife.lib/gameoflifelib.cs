﻿using System;
using System.Text.RegularExpressions;

namespace gameoflife.lib
{

    // Game of life library.
    // Using static since we are just providing methods for handling input and create next generation 
    public static class GOL
    {
        // outputs a object with header, boardsize and board.
        public static GolData ParseInput(string input)
        {

            var s = input.Split('\n');
            // check if we got any input
            if (s.Length <= 3) throw new IncorrectInputFormat($"Needs 3 or more lines, got: {s.Length}");
            // first line is the Generation line.
            var generation = GetCurrentGenerationNumber(s[0]);
            // second line is the board size first is height and second is width.
            var boardsize = GetCurrentBoardSize(s[1]);

            // by the rules we now that the "board" starts at line 3.

            var board = GetCurrentBoard(s[2..], boardsize[1], boardsize[0]);

            var output = new GolData { Generation = generation, Width = boardsize[1], Height = boardsize[0], Board = board };
            return output;
        }

        // Create next generation board.
        public static GolData CreateNextGeneration(GolData oldGeneration)
        {

            // parse cells for life.
            var nextBoard = NextGenBoard(oldGeneration.Board, oldGeneration.Height, oldGeneration.Width);



            var nextGeneration = new GolData
            {
                Generation = oldGeneration.Generation + 1,
                Width = oldGeneration.Width,
                Height = oldGeneration.Height,
                Board = nextBoard

            };


            return nextGeneration;
        }

        // first attempt.
        // Rules : 
        // 1.  As a dead cell I will regain life if i have exactly three neighbours = OK
        // 2. 
        private static char[][] NextGenBoard(char[][] oldboard, int height, int width)
        {
            var nextboard = new char[height][];

            for (int i = 0; i < height; i++)
            {

                if (i == 0)
                {
                    var column = new char[width];
                    for (int c = 0; c < width; c++)
                    {

                        if (c == 0)
                        {
                            int neighbours = 0;
                            if (oldboard[i][c + 1] == '*') neighbours++;
                            if (oldboard[i + 1][c] == '*') neighbours++;
                            if (oldboard[i + 1][c + 1] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';
                        }
                        else if (c != 0 && c != width - 1)
                        {
                            int neighbours = 0;
                            if (oldboard[i][c - 1] == '*') neighbours++;
                            if (oldboard[i][c + 1] == '*') neighbours++;
                            if (oldboard[i + 1][c - 1] == '*') neighbours++;
                            if (oldboard[i + 1][c] == '*') neighbours++;
                            if (oldboard[i + 1][c + 1] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';

                        }
                        else if (c == width - 1)
                        {
                            int neighbours = 0;
                            if (oldboard[i][c - 1] == '*') neighbours++;
                            if (oldboard[i + 1][c] == '*') neighbours++;
                            if (oldboard[i + 1][c - 1] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';

                        }

                    }
                    nextboard[i] = column;
                }
                else if (i != 0 && i != height - 1)
                {
                    var column = new char[width];
                    for (int c = 0; c < width; c++)
                    {
                        if (c == 0)
                        {
                            int neighbours = 0;

                            if (oldboard[i][c + 1] == '*') neighbours++;
                            if (oldboard[i - 1][c] == '*') neighbours++;
                            if (oldboard[i + 1][c] == '*') neighbours++;
                            if (oldboard[i - 1][c + 1] == '*') neighbours++;
                            if (oldboard[i + 1][c + 1] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';
                        }
                        else if (i != 0 && c != width - 1)
                        {
                            int neighbours = 0;

                            if (oldboard[i][c + 1] == '*') neighbours++; //etter
                            if (oldboard[i][c - 1] == '*') neighbours++; //foran
                            if (oldboard[i - 1][c + 1] == '*') neighbours++; //rad over, skrått frem
                            if (oldboard[i - 1][c - 1] == '*') neighbours++; // rad over, skrått bak
                            if (oldboard[i - 1][c] == '*') neighbours++; // rad over, rett over
                            if (oldboard[i + 1][c + 1] == '*') neighbours++; // rad under, skrått frem
                            if (oldboard[i + 1][c - 1] == '*') neighbours++; // rad under, skrått bak
                            if (oldboard[i + 1][c] == '*') neighbours++; // rad under, rett under.


                            column[c] = neighbours == 3 ? '*' : '.';

                        }
                        else if (c == width - 1)
                        {
                            int neighbours = 0;

                            if (oldboard[i - 1][c] == '*') neighbours++;
                            if (oldboard[i - 1][c - 1] == '*') neighbours++;
                            if (oldboard[i][c - 1] == '*') neighbours++;
                            if (oldboard[i + 1][c - 1] == '*') neighbours++;
                            if (oldboard[i + 1][c] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';

                        }

                    }
                    nextboard[i] = column;
                }
                else if (i == height - 1)
                {
                    var column = new char[width];
                    for (int c = 0; c < width; c++)
                    {
                        if (c == 0)
                        {
                            int neighbours = 0;

                            if (oldboard[i][c + 1] == '*') neighbours++;
                            if (oldboard[i - 1][c] == '*') neighbours++;
                            if (oldboard[i - 1][c + 1] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';
                        }
                        else if (c != 0 && c != width - 1)
                        {
                            int neighbours = 0;
                            if (oldboard[i][c - 1] == '*') neighbours++;
                            if (oldboard[i][c + 1] == '*') neighbours++;
                            if (oldboard[i - 1][c - 1] == '*') neighbours++;
                            if (oldboard[i - 1][c] == '*') neighbours++;
                            if (oldboard[i - 1][c + 1] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';
                        }
                        else if (c == width - 1)
                        {
                            int neighbours = 0;

                            if (oldboard[i][c - 1] == '*') neighbours++;
                            if (oldboard[i - 1][c] == '*') neighbours++;
                            if (oldboard[i - 1][c - 1] == '*') neighbours++;

                            column[c] = neighbours == 3 ? '*' : '.';
                        }
                    }
                    nextboard[i] = column;
                }

            }

            return nextboard;
        }




        // extract the generation number and return it.
        // throw an error if not correct syntax.
        //
        private static int GetCurrentGenerationNumber(string input)
        {
            var res = input.Replace("Generation ", "").Replace(":", "");
            var stoiresult = int.TryParse(res, out int currentGeneration);
            if (!stoiresult)
            {
                throw new IncorrectInputFormat("Generation line is wrong format");
            }

            return currentGeneration;
        }

        //output rows/width and height/columns
        // throws an error if not correct syntax.
        // this we know: first char in second line should be an int.
        // third char in second line should be an int
        // 
        private static int[] GetCurrentBoardSize(string input)
        {


            var h = input.IndexOf(' ');
            if (h == -1) throw new IncorrectInputFormat("Incorrect boardsize line.");
            var w = h + 1;


            var firstcharparsed = int.TryParse(input[0..h], out int height);
            if (!firstcharparsed) throw new IncorrectInputFormat("Board height not found");


            var secondcharparsed = int.TryParse(input[w..], out int width);
            if (!secondcharparsed) throw new IncorrectInputFormat("Board width not found");

            return new int[2] { height, width };
        }

        // create a jagged char array of current board.
        private static char[][] GetCurrentBoard(string[] input, int boardwidth, int boardheight)
        {
            if (input.Length != boardheight) throw new IncorrectInputFormat($"input length = {input.Length} not equal to supplied board height = {boardheight}");

            var output = new char[boardheight][];
            for (int i = 0; i < boardheight; i++)
            {

                var row = input[i].Replace("\r", "").Replace("\n", "").ToCharArray();
                if (row.Length != boardwidth) throw new IncorrectInputFormat($"board width for line:{i} is not {boardwidth}");
                foreach (var cell in row)
                {
                    if (cell == '.' || cell == '*') { }
                    else { throw new IncorrectBoardCharacters($"row {i} contains character = {cell}"); }
                }
                output[i] = row;

            }

            return output;
        }

    }
}
