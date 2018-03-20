using GraphPlan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GraphPlan.Extensions
{
    public static class PlanningActionExtensions
    {
        #region serialization

        public static List<IPlanningAction<T>> Save<T>(this List<IPlanningAction<T>> actions, string Path)
        {
            using (StreamWriter sr = new StreamWriter(Path))
            {
                return actions.Save(sr.BaseStream);
            }
        }
        public static List<IPlanningAction<T>> Save<T>(this List<IPlanningAction<T>> actions, Stream writer)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(writer, actions);

            return actions;
        }

        #endregion

        public static List<IPlanningAction<T>> PrintToConsole<T>(this List<IPlanningAction<T>> actions)
        {
            int i = 0;

            Console.WriteLine($"{actions.Count()} step(s) suggested");
            foreach (var action in actions)
            {
                i++;
                Console.WriteLine($"Step {i}: {action.name}");
            }

            return actions;
        }

        
    }
}
