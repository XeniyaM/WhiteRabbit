using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Tree
    {
        public int [,] matrix;
        public int vertex_count;
        public int edge_count;
        public int start_point;
        public int end_point;
        public int buf_ways;
        public string result_string;
       
        public Tree()
        {
           buf_ways = 0;
           result_string = "";
        }
        public void Init_matrix(int Avertex_count)
        {
            matrix = new int[Avertex_count, Avertex_count];
            for (int j = 0; j < vertex_count; j++)
                for (int k = 0; k < vertex_count; k++)
                {
                    matrix[j, k] = 0;
                }
        }
        public void Ford(int AStart_point,bool decPoint, int stack_count)
        {
            
                int min_value = 0;
                int index_value = 0;
                int real_startpoint = 0;
                int real_endpoint = 0;
                stack_count++;

                if (decPoint)
                {
                    real_startpoint = AStart_point - 1;
                    real_endpoint = this.end_point - 1;
                }
                else
                {
                    real_startpoint = AStart_point;
                    real_endpoint = this.end_point - 1;
                }
                
                for (int j = 0; j < this.vertex_count; j++)
                {
                    if (matrix[real_startpoint, j] != 0 && matrix[real_startpoint, j] != min_value)
                    {
                        if (j == real_endpoint)
                        {
                            min_value = matrix[real_startpoint, j];
                            index_value = j;
                            break;
                        }

                        if (min_value == 0 || min_value > matrix[real_startpoint, j])
                        {
                            min_value = matrix[real_startpoint, j];
                            index_value = j;
                        }
                    }
                }
                this.buf_ways += min_value;
                if (stack_count > edge_count)
                {
                    result_string = "Infinity big";
                    return;
                }

                if (index_value != real_endpoint)
                {
                    Ford(index_value, false, stack_count);
                }
                else
                {
                    return;
                }
            
            
            
        }
        public bool ReadFile()
        {
            try
            {
                string path = System.IO.Path.GetFullPath(@"input.txt");
                
                string[] readText = System.IO.File.ReadAllLines(path, Encoding.Default);
                string first = readText[0];
                string last = readText[readText.Length-1];

                string[] split = last.Split(new Char[] { ' ', '\t' });
                this.start_point = int.Parse(split[0]);
                this.end_point = int.Parse(split[1]);

                string[] split_first = first.Split(new Char[] { ' ', '\t' });
                this.vertex_count = int.Parse(split_first[0]);
                this.edge_count = int.Parse(split_first[1]);

                Init_matrix(this.vertex_count);

                for (int i = 1; i <= readText.Length-2; i++)
                {
                    if (i > this.edge_count)
                        break;
                      
                    string words = readText[i];
                    string[] split_list = words.Split(new Char[] { ' ', '\t' });
                    bool item_searched = false;
                    for (int j = 0; j < vertex_count; j++)
                        for (int k = 0; k < vertex_count; k++)
                        {
                            if ((j+1) == int.Parse(split_list[0]) && (k+1) == int.Parse(split_list[1]))
                            {
                                item_searched = true;
                                if (matrix[j, k] == 0 || matrix[j, k] > int.Parse(split_list[2]))
                                {
                                    matrix[j, k] = int.Parse(split_list[2]);
                                    break;
                                }

                            }
                            if (item_searched)
                                break;   
                        }
                }
                

            }
            catch
            {
                Console.WriteLine("Error read file");
                return false;
            }
            finally
            {
                Console.WriteLine("Successful read file");
            }
            return true;
        }
        public bool WriteFile()
        {
            try
            {
                string path = System.IO.Path.GetFullPath(@"output.txt");
                string text;
                if (result_string != "")
                    text = result_string;
                else if (buf_ways < 0)
                    text = "Infinity small";
                else
                    text = buf_ways.ToString();

                System.IO.File.WriteAllText(path, text);

                Console.WriteLine("Result = " + text);
            }
            catch
            {
                Console.WriteLine("Error write file");
                return false;
            }
            finally
            {
                Console.WriteLine("Successful write file");
            }
            return true;
        }
               
    }
    



    class Program
    {
        static void Main(string[] args)
        {
            Tree result = new Tree();
            result.ReadFile();
            result.Ford(result.start_point,true,0);
            result.WriteFile();
            Console.ReadLine();  
        }
 
}
}