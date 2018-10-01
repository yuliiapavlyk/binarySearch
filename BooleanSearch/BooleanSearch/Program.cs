using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooleanSearch
{
    class Program
    {       
                       
        static string[] operations = { "and ", "or ", "not " };
     

        static void Main(string[] args)
        {
            #region
            //Console.WriteLine("Enter query: ");
            //string searchValue = "yulia or not andriy and oleg or petro and not sofia";
            //Console.WriteLine(searchValue);
            //// searchValue = Convert.ToString(Console.ReadLine());

            //string[] separator = { " or ", " and " };

            //string[] searchList = searchValue.Split(separator, StringSplitOptions.RemoveEmptyEntries);



            //foreach (var i in searchList) 
            //{
            //    qElement.Add(new QueryElement(i, i.Contains("not ")? true:false));

            //}

            //foreach (var i in qElement)
            //{
            //    Console.WriteLine(i.name+" "+ i.type + " " + i.operation);
            //}
            #endregion
            //data for searching
            List<string> lists = new List<string>();
            lists.Add("yulia Omelchenko");
            lists.Add("Michael Petriv");
            lists.Add("Sven oleg");
            lists.Add("lalal Terry");
            lists.Add("Claire Adams");
            lists.Add("yulia Hugo");
            lists.Add("Garcia yulia");
            lists.Add("oleg Petriv");
            lists.Add("Petriv oleg");

            
           
            string searchValue = "yulia and not andriy and oleg and petro and not sofia";
            Console.WriteLine(searchValue);
            string[] separator = { " " };
            string line = " ";

            var searchElements = new List<string>();

            //split " "
            string[] searchList = searchValue.Split(separator, StringSplitOptions.None);


            for (int i = 0; i < searchList.Length; i++)
            {
                if (searchList[i]== "and" || searchList[i] == "or" || searchList[i] == "not")
                {
                    line += searchList[i];
                    line += " ";
                }
                else
                {
                    line += searchList[i];
                    searchElements.Add(line);
                    line = " ";
                }
            }

            //create object fields
            var qElement = new List<QueryElement>();
            foreach (var i in searchElements)
            {
                if (i.Contains("and ") || i.Contains("or "))
                {
                    string element= i.Substring(4);


                    qElement.Add(new QueryElement(element, i.Contains("not ") ? true : false, i.Contains("or ") ? "or" : "and"));
                }

                else
                {
                    qElement.Add(new QueryElement(i, i.Contains("not ") ? true : false));

                }
            }
                    
            //display data : object and info
            foreach (var i in qElement)
            {
                Console.WriteLine("Name : " + i.name );
                Console.WriteLine("Type(not) : " + i.type );
               Console.WriteLine("Operation (or , and ) " + i.operation);
                Console.WriteLine("____________________________");
            }


            //string variable = "Petriv";

            //string variable1 = "Petriv AND l";
            //Console.WriteLine("________________");



            //IEnumerable<string> personQuery =
            //    from person in lists
            //    where person.Contains(variable1)
            //    select person;

            //foreach (var i in personQuery)
            //{
            //    Console.WriteLine(i);
            //}\  
            var andExpression = new List<Expression>();
            IQueryable<String> queryableData = lists.AsQueryable<string>();

            ParameterExpression list = Expression.Parameter(typeof(string), "lists");

            foreach (var i in qElement)
            {
                if (i.type == true)
                {
                    Expression name = Expression.Constant(i.name);
                      Expression el1 = Expression.Not(name);
                     notExpression.Add(el1); 
                }

                if (i.operation == "not ")
                {
                    Expression name = Expression.Constant(i.name);
                    andExpression.Add(name);
                }


                Expression left = Expression.Call(list, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                Expression right = Expression.Constant(i.name);
                Expression e1 = Expression.Equal(left, right);

                Expression le = Expression.Call(list, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                Expression ri = Expression.Constant(i.name);
                Expression e2 = Expression.Equal(le, ri);

                Expression e3 = Expression.Equal(le, ri);



                Expression predicateBody = Expression.And(e1, e2);
                predicateBody = Expression.And(predicateBody, e3);
            }

                MethodCallExpression whereCallExpression = Expression.Call(
                   typeof(Queryable),
                   "Where",
                   new Type[] { queryableData.ElementType },
                   queryableData.Expression,
                   Expression.Lambda<Func<string, bool>>(predicateBody, new ParameterExpression[] { list }));
                Console.ReadKey();
            
        }
    }
}
