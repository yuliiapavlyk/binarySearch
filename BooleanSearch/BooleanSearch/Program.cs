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

        
        static string and = "and";
        static string not = "not";
        static string or = "or";

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
            lists.Add("yulia");
            lists.Add("maridel");
            lists.Add("gianni");
            lists.Add("gianni");
            lists.Add("Maridel");
            lists.Add("tris");
            lists.Add("maridel");
            lists.Add("oleg");
            lists.Add("Petriv oleg");

            string searchValue = "maridel or gianni and not tris ";
            Console.WriteLine(searchValue);
            string[] separator = { " " };
            string line = " ";

            var searchElements = new List<string>();

            string[] dataElements = searchValue.Split(separator, StringSplitOptions.None);

            var qElement = new List<QueryElement>();
            qElement.Add(new QueryElement((dataElements[0] == not ? dataElements[1] : dataElements[0]), dataElements[0]==not?true:false));

            for (int i = 1; i < dataElements.Length; i++)
            {
                if (dataElements[i] == and || dataElements[i] == or)
                {
                    var newElement = new QueryElement();
                    newElement.operation = dataElements[i] == and ? and : or;
                    newElement.notCondition = (dataElements[i + 1] == not) ? true : false;
                    newElement.name = newElement.notCondition == true ? dataElements[i + 2] : dataElements[i + 1];

                    qElement.Add(newElement);
                }
            }
            #region
            //display data : object and info
            foreach (var i in qElement)
            {
                Console.WriteLine("Name : " + i.name);
                Console.WriteLine("Not condition : " + i.notCondition);
                Console.WriteLine("Operation (or , and ) " + i.operation);
                Console.WriteLine("____________________________");
            }

            #endregion
            #region

            //   //split " "
            //   string[] searchList = searchValue.Split(separator, StringSplitOptions.None);


            //   for (int i = 0; i < searchList.Length; i++)
            //   {
            //       if (searchList[i] == and || searchList[i] == or || searchList[i] == not)
            //       {
            //           line += searchList[i];
            //           line += " ";
            //       }
            //       else
            //       {
            //           line += searchList[i];
            //           searchElements.Add(line);
            //           line = " ";
            //       }
            //   }
            //   string element = "";
            //   //create object fields
            ////   var qElement = new List<QueryElement>();
            //   foreach (var i in searchElements)
            //   {
            //       if (i.Contains(and) || i.Contains(or))
            //       {

            //               element = i.Substring(4);


            //           qElement.Add(new QueryElement(element, i.Contains(not) ? true : false, i.Contains(or) ? or : and));
            //       }

            //       else
            //       {
            //           qElement.Add(new QueryElement(i, i.Contains(not) ? true : false));

            //       }
            //   }

            ////display data : object and info
            //foreach (var i in qElement)
            //{
            //    Console.WriteLine("Name : " + i.name);
            //    Console.WriteLine("Not condition : " + i.notCondition);
            //    Console.WriteLine("Operation (or , and ) " + i.operation);
            //    Console.WriteLine("____________________________");
            //}

            #endregion

            IQueryable<String> queryableData = lists.AsQueryable<string>();

            ParameterExpression list = Expression.Parameter(typeof(string), "res");

            Expression predicateBody;
            Expression e1, e2;

            Expression leftFirst = Expression.Call(list, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

            Expression rightFirst = Expression.Constant(qElement[0].name.Trim());


            var firstCondition = qElement[0].notCondition
                      ? Expression.NotEqual(leftFirst, rightFirst)
                      : Expression.Equal(leftFirst, rightFirst);
            predicateBody = firstCondition;            

            var elements = qElement.OrderBy(qe => qe.operation);

            foreach (var el in elements)
            {
                Expression right = Expression.Constant(el.name);

                var newCondition = el.notCondition
                    ? Expression.NotEqual(leftFirst, right)
                    : Expression.Equal(leftFirst, right);

                if (el.operation == and)
                {
                    predicateBody = Expression.And(predicateBody, newCondition);
                }
                if (el.operation == or)
                {
                    predicateBody = Expression.Or(predicateBody, newCondition);
                }
            }
            MethodCallExpression whereCallExpression = Expression.Call(
               typeof(Queryable),
               "Where",
               new Type[] { queryableData.ElementType },
               queryableData.Expression,
            Expression.Lambda<Func<string, bool>>(predicateBody, new ParameterExpression[] { list }));
            Console.WriteLine(whereCallExpression);

            var results = queryableData.Provider.CreateQuery(whereCallExpression);           

            foreach (var el in results)
            {
                Console.WriteLine(el);
            }
            Console.ReadKey();

        }
    }
}
