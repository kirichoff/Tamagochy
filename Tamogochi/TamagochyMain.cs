using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace TamagochyCsharpc
{
    class TamagochyMain
    {

        bool alive = true;
        bool selfkill = false;

        Stat health;
        Stat hunger;
        Stat spirit;

        int range1;
        int range2;

        Random rnd;

        Thread draw_screen;
        Thread button_handler;
        Thread stat_count;



        string petim;
        public TamagochyMain() {  }

        public TamagochyMain(int hunger,int health,int spirit,string im)
        {

            rnd = new Random();
            this.health = new Stat(health);
            this.hunger = new Stat(hunger);
            this.spirit = new Stat(spirit);
            petim = im;
            draw_screen = new Thread(sceen);
            button_handler = new Thread(press);
            stat_count = new Thread(live_sycle);
            
        }
        public TamagochyMain(pet pt)
        {
            rnd = new Random();
            this.health = new Stat(pt.health);
            this.hunger = new Stat(pt.hunger);
            this.spirit = new Stat(pt.spirit);
            petim = pt.petim;
            draw_screen = new Thread(sceen);
            button_handler = new Thread(press);
            stat_count = new Thread(live_sycle);

        }

        public void start(int range1,int range2)
        {
            this.range1 = range1;
            this.range2 = range2;
           draw_screen.Start();
            button_handler.Start();
            stat_count.Start();
        }



        public void sceen()
        {
            while (alive)
            {
                Console.WriteLine(petim);
                Console.WriteLine(health.val() + " health    " + health.print());
                Console.WriteLine(hunger.val() + " hunger    " + hunger.print());
                Console.WriteLine(spirit.val() + " spirit    " + spirit.print());
                Console.WriteLine("press F to pay respect  /  S to save /  press G to fead  /  presss  H to heal ");

                Thread.Sleep(200);
                Console.Clear();
            }


            if (!alive)
            {
                if (!selfkill)
                {
                    Console.WriteLine("Game over!!");
                }
                else
                {
                    Console.WriteLine("Game over!! selfkill !!");
                }
            }

        }

        public void live_sycle()
        {
            int range;
           range = rnd.Next(range1, range2);

            while (alive)
            {
                if (hunger.val() == 0)
                {
                    health.pop(range);
                }

                hunger.pop(range);
                spirit.pop(range);


                if (spirit.val() == 0)
                {
                    selfkill = true;
                    alive = false;
                    break;
                }

                if (health.val() == 0)
                {
                    alive = false;
                    break;
                }
                Thread.Sleep(1000);
            }
        }

        public void press()
        {
            int range;

            range = rnd.Next(range1, range2);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(pet));

            string KEY;
            while (alive)
            {
                KEY = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine(KEY);
                if (KEY == "f")
                {
                    spirit.pusch(range);
                    Console.WriteLine("repsect");
                }
                if (KEY == "h")
                {
                    health.pusch(range);
                    Console.WriteLine(" heal ");
                }
                if (KEY == "g")
                {
                    hunger.pusch(range);
                    Console.WriteLine("  fead  ");
                }
                if (KEY ==  "s")
                {
                    using (FileStream fs = new FileStream("saves/save.json", FileMode.OpenOrCreate))
                    {
                        pet pt;
                        pt.health = health.val();
                        pt.hunger = hunger.val();
                        pt.spirit = spirit.val();
                        pt.petim = petim;
                        pt.date = DateTime.Now.ToLongDateString()+DateTime.Now.ToLongTimeString();
                        jsonFormatter.WriteObject(fs,pt);
                    }
                }
                Thread.Sleep(100);
            }
        }

    }
}
