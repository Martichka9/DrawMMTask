using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMM
{
    class Program
    {
        public static void CheckIfNum(string n)
        {
            int num;
            bool check = Int32.TryParse(n, out num);
            if (check) { ValidNum(num); }
            else {
                Console.WriteLine("Input don't match format expected.");
                Console.WriteLine("Enter an odd number:");
                Program.CheckIfNum(Console.ReadLine());
            }
        }

        public static bool IsItOdd(int n)
        {
            return (n % 2 != 0) ? true : false;
        }

        public static bool IsItInRange(int n) {
	        return (n > 2 && n < 10000 ) ? true : false;
        }

        public static void ValidNum(int num) {
            if (Program.IsItOdd(num))
            {
                if (!Program.IsItInRange(num))
                {
                    Console.WriteLine("The number should be in range 3 - 9999! Try again.");
                    Console.WriteLine("Enter an odd number:");
                    Program.CheckIfNum(Console.ReadLine());
                }
                else {
                    Program.DrawMM(Program.DrawMMLoop(num));
                }
            }
            else
            {
                Console.WriteLine("This is not an odd number!");
                Console.WriteLine("Enter an odd number:");
                Program.CheckIfNum(Console.ReadLine());
            }
        
        }

        //Create array map
        public static List<List<int>> DrawMMLoop(int n)
        { 
            List<List<int>> result = new List<List<int>>();
	        int rows = n + 1;
	        int steps = 5; // rotation of '-' , '*' simbols per row
	        int stepS = n; // spaces out of M character
	        int stepS2 = n; // upper space in M character
	        int stepL = n; // line width

	        int space = 0;
	        int letter = 1;
	        int[] spacesMap = {0, 2, 4}; // '-' columns on rows with index < n/2
            int[] spacesMap2 = { 0, 2, 4, 6 }; // '-'columns on rows with index >= n/2 && index != n-1
            int[] spacesMapInner = { 2, 4 };  // '-'columns on last row, separated due to their width is growing while outer is reducing
	        int letterMapInner = 3;

	        int stepInner = -1; // lower M character inner spaces
	        int letterInner = (n*2)+1; // middle M character angle

            for (var i = 0; i < rows; i++) {
		        List<int> row = new List<int>();
		        if (i >= rows/2 && i != rows-1) { // lower part of M character
			        steps = 7;
			        stepInner += 2;
			        letterInner -=2;
			        for (var c = 0; c < steps; c++) {
				        if (Array.IndexOf(spacesMap2,c) != -1) {
					        if (Array.IndexOf(spacesMapInner,c) != -1) {
						        row.AddRange(Enumerable.Repeat(space,stepInner)); // inner white spaces which are growing 
					        } else {
                                row.AddRange(Enumerable.Repeat(space, stepS));
					        }
				        } else if (letterMapInner == c) {
					        row.AddRange(Enumerable.Repeat(letter,letterInner)); // lower center of M character
				        } else {
					        row.AddRange(Enumerable.Repeat(letter,n));
				        }
			        }
		        } else { // upper part of M character map / last row of M character map
			        steps = 5;
		            for (var c = 0; c < steps; c++) {
			            if (Array.IndexOf(spacesMap,c) != -1 && (i < rows/2)) {
				            if (c == 2) {
                                row.AddRange(Enumerable.Repeat(space, stepS2));
				            } else {
                                row.AddRange(Enumerable.Repeat(space, stepS));
				            }
			            } else if(Array.IndexOf(spacesMap,c) == -1 && (i == rows-1)){
                            row.AddRange(Enumerable.Repeat(space, n));
			            } else if(Array.IndexOf(spacesMap,c) != -1 && (i == rows-1)){
                            row.AddRange(Enumerable.Repeat(letter, n));
			            } else {
                            row.AddRange(Enumerable.Repeat(letter, stepL));
			            }
		            }
                }
                result.Add(row);
                //adjust spaces for next row depending on their places
		        if ((i + 1 <= rows / 2)) {
			        stepS = (stepS-1) > 0 ? stepS-1 : 0;
			        stepS2 = (stepS2-2) > 0 ? stepS2-2 : 0;
			        stepL += 2;
		        } else if (i == ((rows/2))){
			        stepS = (stepS-1) > 0 ? stepS-1 : 0;
			        stepS2 = (stepS2-2) > 0 ? stepS2-2 : 0;
			        stepL += 1;
		        } else {
			        stepS = (stepS-1) > 0 ? stepS-1 : 0;
			        stepS2 = (stepS2-2) > 0 ? stepS2-2 : 0;
			        stepL -= 2;;
		        }
	        }
            return result;
        }

        //Print symbols as per array map
        public static void DrawMM(List<List<int>> mm)
        {
            foreach (var item in mm)
            {
                foreach (var symbol in item)
                {
                    Console.Write(symbol == 1 ? '*' : '-');
                }
                foreach (var symbol in item)
                {
                    Console.Write(symbol == 1 ? '*' : '-');
                }
                Console.WriteLine();
            }
            Program.Main();
        }
        public static void Main()
        {
            Console.WriteLine("Enter an odd number:");
            Program.CheckIfNum(Console.ReadLine());
        }
    }
}
