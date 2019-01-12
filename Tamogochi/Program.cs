using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
namespace TamagochyCsharpc
{
    [DataContract]
    public struct pet
    {
        [DataMember]
        public string petim;
        [DataMember]
        public int health;
        [DataMember]
        public int hunger;
        [DataMember]
        public int spirit;
        [DataMember]
        public string date;
    }


    class Program
    {

      

        static void Main(string[] args)
        {

            TamagochyMain t = new TamagochyMain();

          StreamReader read_pets = new StreamReader("pets.txt");

            string str = "";
            int pointer = 0;
            List<string> petss = new List<string>();

            petss.Add("");

            while ( (str = read_pets.ReadLine()) != null )
            {
                if (str != "q")
                {

                    petss[pointer] += "\n" + str;
                }
                else
                {
                    petss.Add(" ");
                    pointer++;
                }
            }

         

            string chose;
            Console.WriteLine("выбрать - 1 загрузить - 2");

                chose = Console.ReadLine();


            if (chose == "1")
            {
                read_pets.Close();

                pet[] array = new pet[petss.Count];

                Random rnd = new Random();

                for (int i = 0; i < petss.Count; i++)
                {
                    array[i].health = rnd.Next(10, 40);
                    array[i].hunger = rnd.Next(10, 40);
                    array[i].spirit = rnd.Next(10, 40);
                    array[i].petim = petss[i];
                }

                Console.WriteLine(" выбирите питомца ");

                int position = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    Console.WriteLine(array[i].petim);
                }

                position = Convert.ToInt32(Console.ReadLine());
                t = new TamagochyMain(array[position]);
            }
            else if (chose == "2")
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(pet));
                pet pt1;
                using (FileStream fs = new FileStream("saves/save.json", FileMode.OpenOrCreate))
                {
                    pt1 = (pet)jsonFormatter.ReadObject(fs);
                }

                Console.WriteLine(pt1.health);
                Console.WriteLine(pt1.hunger);
                Console.WriteLine(pt1.spirit);
                Console.WriteLine(pt1.petim);
                Console.WriteLine(pt1.date);
                t = new TamagochyMain(pt1);
            }

            Console.ReadKey();
            t.start(1, 5);
        }
    }
}
